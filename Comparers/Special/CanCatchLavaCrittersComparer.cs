using Terraria;
using Terraria.ID;

namespace CompareItemStats.Comparers.Special
{
	public class CanCatchLavaCrittersComparer : SpecialComparer
	{
		public CanCatchLavaCrittersComparer() : base("CanCatchLavaCritters") { }

		public override bool CanShow(Item item, Item compareItem)
		{
			return ItemID.Sets.CatchingTool[item.type] && ItemID.Sets.CatchingTool[compareItem.type];
		}

		public override float GetValueFromString(string text, Item item)
		{
			return ItemID.Sets.LavaproofCatchingTool[item.type].ToDirectionInt();
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
