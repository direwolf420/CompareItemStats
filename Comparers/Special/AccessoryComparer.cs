using Terraria;

namespace CompareItemStats.Comparers.Special
{
	public abstract class AccessoryComparer : SpecialComparer
	{
		public AccessoryComparer(string internalName) : base(internalName) { }

		public override bool CanShow(Item item, Item compareItem)
		{
			return item.accessory && compareItem.accessory;
		}
	}
}
