using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using CompareItemStats.Comparers;
using Terraria.GameInput;

namespace CompareItemStats
{
	public class CISGlobalItem : GlobalItem
	{
		/// <summary>
		/// Set to false when fetching tooltips from a non-mouseovered item. Checked to prevent recursion
		/// </summary>
		private static bool firstCall = true;

		public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
		{
			if (Main.gameMenu)
			{
				//Prevent jank with mods (TRaI) and the bottom left main menu icons
				return;
			}

			Player player = Main.LocalPlayer;
			Item compItem = player.HeldItem;
			
			//If set to true, will show that it is being compared against the equipped item
			//If set to false, this comparison is made against the held item (only set to false if setting true is technically possible)
			//Null will not display anything
			bool? equipItemOverride = null;

			if (firstCall && IsArmor(item, out Item armor) &&
				(EmptyOrSame(item, player.HeldItem) || !IsArmor(player.HeldItem, out _)))
			{
				//If mouseovered armor item and selected item is a compatible armor: Compare to selected item //SKIP

				//If mouseovered equipped armor item and selected item is a compatible armor: Compare to selected item //SKIP

				//If mouseovered armor item and selected item is that item: Compare to equipped armor item //SWITCH

				//If mouseovered armor item and selected item is not a compatible armor: Compare to equipped armor item //SWITCH
				if (!EmptyOrSame(item, armor))
				{
					compItem = armor;
					equipItemOverride = true;
				}
			}
			else if (firstCall && item.wingSlot > -1 && (EmptyOrSame(item, player.HeldItem) || player.HeldItem.wingSlot == -1) && GetPlayerWings(out Item wing))
			{
				//If mouseovered wing item and selected item is a compatible wing: Compare to selected item //SKIP

				//If mouseovered equipped wing item and selected item is a compatible wing: Compare to selected item //SKIP

				//If mouseovered wing item and selected item is that item: Compare to equipped wing item //SWITCH

				//If mouseovered wing item and selected item is not a compatible wing: Compare to equipped wing item //SWITCH
				if (!EmptyOrSame(item, wing))
				{
					compItem = wing;
					equipItemOverride = true;
				}
			}

			bool compareIsArmor = IsArmor(compItem, out _);
			bool compareIsWing = compItem.wingSlot > -1;
			bool compareIsEquip = compareIsArmor || compareIsWing;

			if (equipItemOverride == null && compareIsEquip)
			{
				equipItemOverride = false;
			}

			//Armors should only be compared if they represent the same equip type
			//Only compare if both are heads, body, or leg
			if (compareIsArmor)
			{
				bool bothHead = item.headSlot > -1 && compItem.headSlot > -1;
				bool bothBody = item.bodySlot > -1 && compItem.bodySlot > -1;
				bool bothLeg = item.legSlot > -1 && compItem.legSlot > -1;
				if (!bothHead && !bothBody && !bothLeg)
				{
					return;
				}
			}

			if (EmptyOrSame(item, compItem))
			{
				return;
			}

			var newLines = new List<TooltipLine>();

			firstCall = false;
			var compLines = GetTooltipLines(compItem); //This can cause recursion if conditions above are not handled properly
			firstCall = true;

			foreach (var line in tooltips)
			{
				/*if (line.mod != "Terraria")
				{
					//No mod support
					continue;
				}*/

				foreach (var compLine in compLines)
				{
					string name = compLine.Name;
					if (/*compLine.mod != "Terraria" ||*/name != line.Name)
					{
						//TODO mod support?
						//Do not compare different lines
						continue;
					}

					//Tooltip comparers match tooltip by tooltip
					if (CompareItemStats.TooltipComparers.TryGetValue(name, out var comparer))
					{
#if TML_2022_03
						HandleComparer(comparer, name, newLines, item, compItem, line.text, compLine.text);
#else
						HandleComparer(comparer, name, newLines, item, compItem, line.Text, compLine.Text);
#endif
					}
				}
			}

			//Special comparers do not need the tooltip
			foreach (var comparer in CompareItemStats.SpecialComparers)
			{
				HandleComparer(comparer, comparer.GetType().Name, newLines, item, compItem);
			}

			if (newLines.Count > 0)
			{
				var keys = PlayerInput.CurrentProfile.InputModes[InputMode.Keyboard].KeyStatus[TriggerNames.SmartSelect];
				string key = keys.Count == 0 ? null : keys[0];

				//If has a key, but not pressing it, show the ForMoreInfo text
				//Otherwise, list all comparisons

				//No tml hooks between controlTorch getting set, and then reset again in SmartSelectLookup, so we have to use the raw data from PlayerInput
				bool comparisonKeyPressed = key == null || PlayerInput.Triggers.Current.SmartSelect;
				bool showComparison = comparisonKeyPressed || CISClientConfig.Instance.AlwaysShowComparison;

				Color mouseColor = Main.MouseTextColorReal;

				string equipItem = "";
				if (equipItemOverride.HasValue)
				{
					equipItem = equipItemOverride.Value ? " (Equipped Item)" : " (Selected Item)";
				}

				string statComparisonHeaderText = "=Stat Comparison";
				Color color = Color.Gold;
				if (!showComparison)
				{
					color = Color.Gray;
					statComparisonHeaderText += $" Available{equipItem}";
					statComparisonHeaderText += $": (Hold 'Auto Select' key ({key}) to show comparison)";
				}
				else
				{
					statComparisonHeaderText += equipItem;
				}
				statComparisonHeaderText += "=";

				tooltips.Add(new TooltipLine(Mod, "Diff", statComparisonHeaderText)
				{
#if TML_2022_03
					overrideColor = Color.Lerp(mouseColor, color, 0.8f)
#else
					OverrideColor = Color.Lerp(mouseColor, color, 0.8f)
#endif
				});

				if (showComparison)
				{
					tooltips.AddRange(newLines);
				}
			}
		}

		public static bool EmptyOrSame(Item item, Item compItem)
		{
			if (compItem.IsAir)
			{
				return true;
			}

			if (compItem.type == item.type)
			{
				return true;
			}

			return false;
		}

		public static bool IsArmor(Item item, out Item armor)
		{
			bool isArmor = false;
			armor = null;
			Player player = Main.LocalPlayer;
			if (item.headSlot > -1)
			{
				isArmor = true;
				armor = player.armor[0];
			}
			else if (item.bodySlot > -1)
			{
				isArmor = true;
				armor = player.armor[1];
			}
			else if (item.legSlot > -1)
			{
				isArmor = true;
				armor = player.armor[2];
			}

			return isArmor;
		}

		public static bool GetPlayerWings(out Item wing)
		{
			wing = Main.LocalPlayer.equippedWings;
			return wing != null;
		}

		private void HandleComparer(StatComparer comparer, string name, List<TooltipLine> newLines, Item item, Item compItem, string text = null, string compText = null)
		{
			if (!comparer.CanShow(item, compItem))
			{
				return;
			}

			text ??= "";
			compText ??= "";
			float num = comparer.GetValueFromString(text, item);
			float compNum = comparer.GetValueFromString(compText, compItem);
			float diff = comparer.GetAbsDiff(num, compNum, out bool positive);

			if (Math.Abs(diff) < 0.0001f)
			{
				return;
			}

			string diffText = comparer.DiffToString(diff);
			string sign = comparer.GetSignString(positive);
			diffText = sign + diffText;

			comparer.AddTooltip(Mod, name, newLines, positive, diffText);
		}

		//Copied from vanilla
		public static List<TooltipLine> GetTooltipLines(Item item)
		{
			Item hoverItem = item.Clone();
			Player player = Main.LocalPlayer;
			int yoyoLogo = -1;
			int researchLine = -1;
			float knockBack = hoverItem.knockBack;
			float num = 1f;
			if (hoverItem.CountsAsClass<MeleeDamageClass>() && player.kbGlove)
			{
				num += 1f;
			}

			if (player.kbBuff)
			{
				num += 0.5f;
			}

			if (num != 1f)
			{
				hoverItem.knockBack *= num;
			}

			if (hoverItem.CountsAsClass<RangedDamageClass>() && player.shroomiteStealth)
			{
				hoverItem.knockBack *= 1f + (1f - player.stealth) * 0.5f;
			}

			int num2 = 30;
			int numLines = 1;
			string[] array = new string[num2];
			bool[] array2 = new bool[num2];
			bool[] array3 = new bool[num2];
			for (int i = 0; i < num2; i++)
			{
				array2[i] = false;
				array3[i] = false;
			}
			string[] tooltipNames = new string[num2];

			Main.MouseText_DrawItemTooltip_GetLinesInfo(hoverItem, ref yoyoLogo, ref researchLine, knockBack, ref numLines, array, array2, array3, tooltipNames);
			if (Main.npcShop > 0 && hoverItem.value >= 0 && (hoverItem.type < ItemID.CopperCoin || hoverItem.type > ItemID.PlatinumCoin))
			{
				Main.LocalPlayer.GetItemExpectedPrice(hoverItem, out int calcForSelling, out int calcForBuying);
				int num5 = (hoverItem.isAShopItem || hoverItem.buyOnce) ? calcForBuying : calcForSelling;
				if (hoverItem.shopSpecialCurrency != -1)
				{
					tooltipNames[numLines] = "SpecialPrice";
					CustomCurrencyManager.GetPriceText(hoverItem.shopSpecialCurrency, array, ref numLines, num5);
				}
				else if (num5 > 0)
				{
					string text = "";
					int num6 = 0;
					int num7 = 0;
					int num8 = 0;
					int num9 = 0;
					int num10 = num5 * hoverItem.stack;
					if (!hoverItem.buy)
					{
						num10 = num5 / 5;
						if (num10 < 1)
						{
							num10 = 1;
						}

						int num11 = num10;
						num10 *= hoverItem.stack;
						int amount = Main.shopSellbackHelper.GetAmount(hoverItem);
						if (amount > 0)
						{
							num10 += (-num11 + calcForBuying) * Math.Min(amount, hoverItem.stack);
						}
					}

					if (num10 < 1)
					{
						num10 = 1;
					}

					if (num10 >= 1000000)
					{
						num6 = num10 / 1000000;
						num10 -= num6 * 1000000;
					}

					if (num10 >= 10000)
					{
						num7 = num10 / 10000;
						num10 -= num7 * 10000;
					}

					if (num10 >= 100)
					{
						num8 = num10 / 100;
						num10 -= num8 * 100;
					}

					if (num10 >= 1)
					{
						num9 = num10;
					}

					if (num6 > 0)
					{
						text = text + num6 + " " + Lang.inter[15].Value + " ";
					}

					if (num7 > 0)
					{
						text = text + num7 + " " + Lang.inter[16].Value + " ";
					}

					if (num8 > 0)
					{
						text = text + num8 + " " + Lang.inter[17].Value + " ";
					}

					if (num9 > 0)
					{
						text = text + num9 + " " + Lang.inter[18].Value + " ";
					}

					if (!hoverItem.buy)
					{
						array[numLines] = Lang.tip[49].Value + " " + text;
					}
					else
					{
						array[numLines] = Lang.tip[50].Value + " " + text;
					}

					tooltipNames[numLines] = "Price";
					numLines++;
				}
				else if (hoverItem.type != ItemID.DefenderMedal)
				{
					array[numLines] = Lang.tip[51].Value;
					tooltipNames[numLines] = "Price";
					numLines++;
				}
			}

			List<TooltipLine> lines = ItemLoader.ModifyTooltips(hoverItem, ref numLines, tooltipNames, ref array, ref array2, ref array3, ref yoyoLogo, out _);
			return lines;
		}
	}
}
