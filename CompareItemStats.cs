using CompareItemStats.Comparers;
using CompareItemStats.Comparers.Special;
using CompareItemStats.Comparers.Tooltips;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace CompareItemStats
{
	public class CompareItemStats : Mod
	{
		/// <summary>
		/// Comparers indexed by tooltip line name
		/// </summary>
		public static Dictionary<string, StatComparer> TooltipComparers { get; private set; }

		/// <summary>
		/// Comparers not tied to tooltip lines
		/// </summary>
		public static List<SpecialComparer> SpecialComparers { get; private set; }

		//Not actually used anywhere
		public static readonly string[] StatTooltips = new string[]
		{
			"Damage", //
			"CritChance", //
			"Speed", //Effective speed which includes animation, time, and delay
			"Knockback", //
			"FishingPower", //
			"BaitPower", //
			"Defense", //
			"PickPower", //
			"AxePower", //
			"HammerPower", //
			//"TileBoost", //Separate as it only shows as a tooltip if != 0
			"HealLife", //
			"HealMana", //
			"UseMana", //
			//Price diff is useless without real value being visible
		};

		public override void Load()
		{
			TooltipComparers = new Dictionary<string, StatComparer>
			{
				{"Damage", new StatComparer("Damage") },
				{"CritChance", new PercentageComparer("CritChance") },
				{"Speed", new SpeedComparer() },
				{"Knockback", new KnockbackComparer() },
				{"FishingPower", new PercentageComparer("FishingPower") },
				{"BaitPower", new PercentageComparer("BaitPower") },
				{"Defense", new StatComparer("Defense") },
				{"PickPower", new PercentageComparer("PickaxePower") },
				{"AxePower", new PercentageComparer("AxePower") },
				{"HammerPower", new PercentageComparer("HammerPower") },
				{"HealLife", new StatComparer("HealLife") },
				{"HealMana", new StatComparer("HealMana") },
				{"UseMana", new UseManaComparer() },
			};

			//TODO comparers for Tooltip# to get info about equipables

			SpecialComparers = new List<SpecialComparer>()
			{
				new AutoswingComparer(),
				new CanCatchLavaCrittersComparer(),
				new CanFishInLavaComparer(),
				new TileBoostComparer(),
				new WingFlyTimeComparer(),
				new WingHorizontalComparer(),
				new WingVerticalComparer(),
				new ClassComparer(),
				new RegenerationComparer(),
			};
		}

		public override void Unload()
		{
			TooltipComparers = null;
			SpecialComparers = null;
		}
	}
}
