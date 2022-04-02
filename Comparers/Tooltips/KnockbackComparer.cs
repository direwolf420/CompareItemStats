using Terraria;

namespace CompareItemStats.Comparers.Tooltips
{
	/// <summary>
	/// Has no value representation in the tooltip
	/// </summary>
	public class KnockbackComparer : StatComparer
	{
		public KnockbackComparer() : base("Knockback") { }

		public override float GetValueFromString(string text, Item item)
		{
			float kb = Main.LocalPlayer.GetWeaponKnockback(item, item.knockBack);
			return kb;
		}
	}
}
