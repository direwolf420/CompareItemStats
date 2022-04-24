using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace CompareItemStats
{
	[Label("Client Config")]
	public class CISClientConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		public static CISClientConfig Instance => ModContent.GetInstance< CISClientConfig>();
		
		[Label("Always Show Comparison")]
		[Tooltip("If on, always displays comparison if it exists. Use the 'Auto Select' key to toggle it otherwise (usually <LeftShift>)")]
		[DefaultValue(false)]
		public bool AlwaysShowComparison;
	}
}
