using Terraria;

namespace CompareItemStats.Comparers.Special
{
	public class WingFlyTimeComparer : WingComparer
	{
		public WingFlyTimeComparer() : base("Fly Time") { }

		public override float GetValueFromString(string text, Item item)
		{
			return ToSeconds(Main.LocalPlayer.GetWingStats(item.wingSlot).FlyTime);
		}

		public static float ToSeconds(int time)
		{
			return time / 60f;
		}

		public override string DiffToString(float diff)
		{
			return ((int)(diff * 100) / 100f) + "s"; //Round last two digits
		}
	}
}
