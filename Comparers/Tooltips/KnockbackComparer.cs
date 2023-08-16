using Terraria;

namespace CompareItemStats.Comparers.Tooltips
{
	/// <summary>
	/// Has no value representation in the tooltip
	/// </summary>
	public class KnockbackComparer : StatComparer
	{
		public KnockbackComparer() : base("Knockback") { }

		public override bool CanShow(Item item, Item compareItem)
		{
			//Exclude accessories with assigned knockback (eoc shield)
			return !item.accessory && !compareItem.accessory;
		}

		public override float GetValueFromString(string text, Item item)
		{
			float kb = Main.LocalPlayer.GetWeaponKnockback(item, item.knockBack);
			return kb;
		}
	}
}
