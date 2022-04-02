using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CompareItemStats.Comparers.Special
{
	public class ClassComparer : SpecialComparer
	{
		public ClassComparer() : base("Same Class") { }

		public override bool CanShow(Item item, Item compareItem)
		{
			//Exclude coins as they count as ammo for coin gun == ranged damage
			return item.damage > 0 && compareItem.damage > 0 &&
				!ItemID.Sets.CommonCoin[item.type] && !ItemID.Sets.CommonCoin[compareItem.type]/* &&
				item.DamageType != DamageClass.Generic && item.DamageType != DamageClass.NoScaling &&
				compareItem.DamageType != DamageClass.Generic && compareItem.DamageType != DamageClass.NoScaling*/;
			//There might be weapons that use those classes, but it covers most things. Otherwise every item would have this tag
		}

		public override float GetValueFromString(string text, Item item)
		{
			//TODO maybe figure something out with benefitsCache?
			return item.DamageType.Type; //Same class with return 0, but this is good because we only want this to show negative response
		}

		public override float GetAbsDiff(float num, float compNum, out bool positive)
		{
			float result = base.GetAbsDiff(num, compNum, out _);
			positive = false;
			return result;
		}

		public override string GetSignString(bool positive)
		{
			return positive ? "Yes" : "No"; //It will only show when negative
		}

		public override string DiffToString(float diff)
		{
			return "";
		}
	}
}
