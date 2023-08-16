using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CompareItemStats.Comparers.Special
{
	//TODO melee/meleenospeed and variations of this pattern will show as different classes here
	public class ClassComparer : SpecialComparer
	{
		public ClassComparer() : base("Class") { }

		public override bool CanShow(Item item, Item compareItem)
		{
			//Exclude coins as they count as ammo for coin gun == ranged damage
			//Exclude accessories with assigned damage (eoc shield)
			return !item.accessory && !compareItem.accessory && item.damage > 0 && compareItem.damage > 0 &&
				!ItemID.Sets.CommonCoin[item.type] && !ItemID.Sets.CommonCoin[compareItem.type]/* &&
				item.DamageType != DamageClass.Generic && item.DamageType != DamageClass.NoScaling &&
				compareItem.DamageType != DamageClass.Generic && compareItem.DamageType != DamageClass.NoScaling*/;
			//There might be weapons that use those classes, but it covers most things. Otherwise every item would have this tag
		}

		public override float GetValueFromString(string text, Item item)
		{
			//Pass class type as "value", convert to class later
			return item.DamageType.Type; //Same class with return 0, but this is good because we only want this to show negative response
		}

		public override float GetAbsDiff(float num, float compNum, out bool positive)
		{
			var firstClass = DamageClassLoader.GetDamageClass((int)num);
			var secondClass = DamageClassLoader.GetDamageClass((int)compNum);

			positive = false;

			if (firstClass.CountsAsClass(secondClass) || secondClass.CountsAsClass(firstClass))
			{
				//This is for things like spears vs swords or whips vs summons
				positive = true;
				return 0f;
			}

			return base.GetAbsDiff(num, compNum, out _);
		}

		public override string GetSignString(bool positive)
		{
			return positive ? LangHelper.GetTextFromMod("Common.Yes") : LangHelper.GetTextFromMod("Common.No"); //It will only show when negative
		}

		public override string DiffToString(float diff)
		{
			return "";
		}
	}
}
