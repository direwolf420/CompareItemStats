using Terraria;

namespace CompareItemStats.Comparers.Special
{
	public class TileBoostComparer : SpecialComparer
	{
		//Needs special because tooltip only appears if tileBoost != 0, but it is relevant for those items too
		public TileBoostComparer() : base("TileBoost") { }

		public override bool CanShow(Item item, Item compareItem)
		{
			return TileBoostRelevant(item) && TileBoostRelevant(compareItem);
		}

		public override float GetValueFromString(string text, Item item)
		{
			return item.tileBoost;
		}

		//Only show if for both items:
		//one of the items is != 0
		//and if one of the items is == 0, show if it is a tool
		private static bool TileBoostRelevant(Item item)
		{
			return item.tileBoost != 0 || item.hammer > 0 || item.axe > 0 || item.pick > 0;
		}
	}
}
