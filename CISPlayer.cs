using Terraria.GameInput;
using Terraria.ModLoader;

namespace CompareItemStats
{
	public class CISPlayer : ModPlayer
	{
		public bool HotkeyHeld { get; private set; }

		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			HotkeyHeld = CompareItemStats.ComparisonBind.Current;
		}
	}
}
