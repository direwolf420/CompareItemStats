using Terraria;

namespace CompareItemStats.Comparers.Special
{
	public class WingHorizontalComparer : WingComparer
	{
		public WingHorizontalComparer() : base("WingHorizontal") { }

		public override float GetValueFromString(string text, Item item)
		{
			//Lowest vanilla value: 15 (fledge). Code: 3f
			//Highest vanilla value: 46 (solar/stardust) Code: 9f
			float speed = Main.LocalPlayer.GetWingStats(item.wingSlot).AccRunSpeedOverride;
			if (speed == -1f)
			{
				speed = 6f; //Default for accRunSpeed, which this gets assigned to
			}
			return ToHuman(speed);
		}

		public static float ToHuman(float speed)
		{
			return (int)(speed * 5f) + 1;
		}

		public override string DiffToString(float diff)
		{
			return base.DiffToString(diff) + $" {LangHelper.GetTextFromMod("Common.MilesPerHour")}";
		}
	}
}
