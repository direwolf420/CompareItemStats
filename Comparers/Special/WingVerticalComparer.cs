using Terraria;

namespace CompareItemStats.Comparers.Special
{
	public class WingVerticalComparer : WingComparer
	{
		/// <summary>
		/// %
		/// </summary>
		private static readonly char percent = '%';

		public WingVerticalComparer() : base("Vertical Acceleration") { }

		public override float GetValueFromString(string text, Item item)
		{
			//Lowest vanilla value: 150% most wings, Code: 1f
			//Highest vanilla value: 46 (solar/stardust) Code: 9f
			float speed = Main.LocalPlayer.GetWingStats(item.wingSlot).AccRunAccelerationMult;
			if (speed == 1f)
			{
				speed = 1.5f; //Wiki lists them all as 150%
			}
			return ToHuman(speed);
		}

		public static float ToHuman(float speed)
		{
			return (int)(speed * 100);
		}

		public override string DiffToString(float diff)
		{
			return base.DiffToString(diff) + percent;
		}
	}
}
