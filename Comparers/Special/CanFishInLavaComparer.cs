using Terraria;
using Terraria.ID;

namespace CompareItemStats.Comparers.Special
{
	public class CanFishInLavaComparer : SpecialComparer
	{
		public CanFishInLavaComparer() : base("CanFishInLava") { }

		public override bool CanShow(Item item, Item compareItem)
		{
			return item.fishingPole > 0 && compareItem.fishingPole > 0;
		}

		public override float GetValueFromString(string text, Item item)
		{
			return ItemID.Sets.CanFishInLava[item.type].ToDirectionInt();
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
