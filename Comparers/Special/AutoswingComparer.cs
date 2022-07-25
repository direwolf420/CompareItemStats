using Terraria;
using Terraria.ID;

namespace CompareItemStats.Comparers.Special
{
	public class AutoswingComparer : SpecialComparer
	{
		public AutoswingComparer() : base("Autoswing") { }

		public override bool CanShow(Item item, Item compareItem)
		{
			//Autoswing really only relevant on weapons. There are things like placeables but does that matter?
			//Exclude coins as they count as ammo for coin gun == ranged damage
			return item.damage > 0 && compareItem.damage > 0 &&
				!ItemID.Sets.CommonCoin[item.type] && !ItemID.Sets.CommonCoin[compareItem.type];
		}

		public override float GetValueFromString(string text, Item item)
		{
			return (!item.autoReuse).ToDirectionInt();
		}

		public override float GetAbsDiff(float num, float compNum, out bool positive)
		{
			float result = base.GetAbsDiff(num, compNum, out positive);
			positive = !positive;
			return result;
		}

		public override string GetSignString(bool positive)
		{
			return positive ? LangHelper.GetTextFromMod("Common.Yes") : LangHelper.GetTextFromMod("Common.No");
		}

		public override string DiffToString(float diff)
		{
			return "";
		}
	}
}
