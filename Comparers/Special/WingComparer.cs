using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CompareItemStats.Comparers.Special
{
	public abstract class WingComparer : SpecialComparer
	{
		/*
		 *  public int FlyTime;
			public float AccRunSpeedOverride;
			public float AccRunAccelerationMult;
			public bool HasDownHoverStats;
			public float DownHoverSpeedOverride;
			public float DownHoverAccelerationMult;

		 public WingStats(int flyTime = 100, float flySpeedOverride = -1f, float accelerationMultiplier = 1f, bool hasHoldDownHoverFeatures = false, float hoverFlySpeedOverride = -1f, float hoverAccelerationMultiplier = 1f) {
			FlyTime = flyTime;
			AccRunSpeedOverride = flySpeedOverride;
			AccRunAccelerationMult = accelerationMultiplier;
			HasDownHoverStats = hasHoldDownHoverFeatures;
			DownHoverSpeedOverride = hoverFlySpeedOverride;
			DownHoverAccelerationMult = hoverAccelerationMultiplier;
		}
		 */

		public WingComparer(string displayName) : base(displayName) { }

		public override bool CanShow(Item item, Item compareItem)
		{
			return item.wingSlot > 0 && compareItem.wingSlot > 0;
		}
	}
}
