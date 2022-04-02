using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace CompareItemStats.Comparers
{
	public class StatComparer
	{
		/// <summary>
		/// Name of the stat shown in the tooltip
		/// </summary>
		public string DisplayName { get; init; }

		public StatComparer(string displayName)
		{
			DisplayName = displayName;
		}

		/// <summary>
		/// Return false to prevent the stat from showing in the tooltip
		/// </summary>
		/// <param name="item">Item that is being mouseovered</param>
		/// <param name="compareItem">Item that is being compared against</param>
		/// <returns></returns>
		public virtual bool CanShow(Item item, Item compareItem)
		{
			return true;
		}

		/// <summary>
		/// Extracts the raw value from the tooltip text. Alternatively, can use item.
		/// </summary>
		/// <param name="text">Tooltip text</param>
		/// <param name="item">Item</param>
		/// <returns></returns>
		public virtual float GetValueFromString(string text, Item item)
		{
			string[] splitText = text.Split(' ');

			foreach (var subText in splitText)
			{
				//if any part of the tooltip parses as a number, use it
				if (float.TryParse(subText, out float result))
				{
					return result;
				}
			}

			return 0f;
		}

		/// <summary>
		/// Allows you to customize the difference that is being generated, substraction by default.
		/// Should return the absolute value. If the absolute value is 0, it will not be displayed.
		/// </summary>
		/// <param name="num">Stat of the mouseovered item</param>
		/// <param name="compNum">Stat of the compared item</param>
		/// <param name="positive">If the diff is positive or negative</param>
		/// <returns></returns>
		public virtual float GetAbsDiff(float num, float compNum, out bool positive)
		{
			float diff = num - compNum;
			positive = diff > 0;
			return Math.Abs(diff);
		}

		/// <summary>
		/// Returns the sign (prefix) for the stat representation. By default + for positive, - for negative
		/// </summary>
		/// <param name="positive">If the diff is positive or negative</param>
		/// <returns></returns>
		public virtual string GetSignString(bool positive)
		{
			return positive ? "+" : "-";
		}

		/// <summary>
		/// Returns the string representation of the diff.
		/// </summary>
		/// <param name="diff">The diff</param>
		/// <returns></returns>
		public virtual string DiffToString(float diff)
		{
			return diff.ToString();
		}

		/// <summary>
		/// Adds the stat to the tooltip at the end.
		/// </summary>
		/// <param name="mod">Mod this belongs to</param>
		/// <param name="name">Name of the comparer for the unique tooltip</param>
		/// <param name="newLines">The list of lines to append the tooltip to</param>
		/// <param name="positive">If the diff is positive or negative</param>
		/// <param name="diffText">String representation of the diff</param>
		public virtual void AddTooltip(Mod mod, string name, List<TooltipLine> newLines, bool positive, string diffText)
		{
			Color mouseColor = Main.MouseTextColorReal;
			newLines.Add(new TooltipLine(mod, $"Diff_{name}", $"{DisplayName}: {diffText}")
			{
				OverrideColor = Color.Lerp(mouseColor, positive ? Color.Green : Color.Red, 0.4f)
			});
		}
	}
}
