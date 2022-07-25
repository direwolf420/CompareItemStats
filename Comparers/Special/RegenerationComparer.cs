using Terraria;

namespace CompareItemStats.Comparers.Special
{
	//Only 2 vanilla items use this (band of regen, charm of myths)
	public class RegenerationComparer : AccessoryComparer
	{
		public RegenerationComparer() : base("Regeneration") { }

		public override float GetValueFromString(string text, Item item)
		{
			return item.lifeRegen / 2f;
		}
	}
}
