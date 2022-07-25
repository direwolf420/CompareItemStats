using System.ComponentModel;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace CompareItemStats
{
	[Label("$Mods.CompareItemStats.CISClientConfig.Label")]
	public class CISClientConfig : ModConfig
	{
		public override ConfigScope Mode => ConfigScope.ClientSide;

		public static CISClientConfig Instance => ModContent.GetInstance< CISClientConfig>();
		
		[Label("$Mods.CompareItemStats.CISClientConfig.AlwaysShowComparison.Label")]
		[Tooltip("$Mods.CompareItemStats.CISClientConfig.AlwaysShowComparison.Tooltip")]
		[DefaultValue(false)]
		public bool AlwaysShowComparison;
	}
}
