using Terraria;

namespace CompareItemStats.Comparers.Special
{
	public class WingFlyTimeComparer : WingComparer
	{
		public WingFlyTimeComparer() : base("WingFlyTime") { }

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
			return ((int)(diff * 100) / 100f) + LangHelper.GetTextFromMod("Common.Seconds"); //Round last two digits
		}
	}
}
