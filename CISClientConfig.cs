using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace CompareItemStats
{
	public class CISClientConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		public static CISClientConfig Instance => ModContent.GetInstance< CISClientConfig>();
		
		[DefaultValue(false)]
		public bool AlwaysShowComparison;

		[DefaultValue(false)]
		public bool DontShowHintTooltip;
	}
}
