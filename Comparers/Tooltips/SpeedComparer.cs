using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CompareItemStats.Comparers.Tooltips
{
	public class SpeedComparer : StatComparer
	{
		//uT = useTime
		//uA = useAnimation
		//b = bursts
		//rD = reuseDelay
		//eS = effective speed

		//eS calc: uT x b + rD
		//non-melee: 10 uT, 2 b (== ceil(uA/uT)), 5 rD == 15 eS
		//melee + !noMelee: 12 uA == 12 eS
		//melee + noMelee: 12 uT = 12 eS

		//Melee: useTime does not matter at all, it is just used for projectile spawns

		//useAnimation:useTime
		//12:3 => 4 = 4 uses
		//12:4 => 3 = 3 uses
		//12:5 => 2.4 = 3 uses
		//12:6 => 2 = 2 uses
		//12:11 => 1.09 = 2 uses
		//12:12 => 1 = 1 use
		//12:30 => 0.4 = 1 use, but repeat rate based on useTime
		//=> (int)Math.Ceiling((float)item.useAnimation / item.useTime); == number of uses per swing

		//Functionality:
		//GetValueFromString returns the "effective speed" which does uT x b + rD
		//GetAbsDiff gets multiplier
		//DiffToString returns it 

		public SpeedComparer() : base("Speed") { }

		private static void GetItemSpeedStats(Item item, out int bursts, out float rawSpeed, out float totalSpeed)
		{
			bursts = 1;
			if (item.CountsAsClass(DamageClass.Melee)) //TODO maybe check useStyle == ItemUseStyleID.Swing? but then things like arkhalis/terragrim get botched stats
			{
				if (item.noMelee)
				{
					rawSpeed = item.useTime;
					//This does not reflect very well in DPS with things like terragrim which have uT 15, but the projectile that spawns has immune time of 5
				}
				else
				{
					rawSpeed = item.useAnimation;
				}
			}
			else
			{
				bursts = (int)Math.Ceiling((float)item.useAnimation / item.useTime);
				rawSpeed = item.useTime * bursts;
			}

			totalSpeed = rawSpeed + item.reuseDelay; //Higher = slower
		}

		public override float GetValueFromString(string text, Item item)
		{
			GetItemSpeedStats(item, out _, out _, out float totalSpeed);
			return totalSpeed;

			//return item.useAnimation; //Higher = slower
		}

		public override float GetAbsDiff(float num, float compNum, out bool positive)
		{
			//Get the higher ratio
			//If compNum is the higher value, this means positive
			positive = num <= compNum;
			float low = !positive ? compNum : num;
			float high = positive ? compNum : num;

			float diff = high / low;
			if (diff == 1)
			{
				//Equal speeds, should be 0
				return 0;
			}

			return diff; //Represent as percentage
		}

		public override string GetSignString(bool positive)
		{
			return "x";
		}

		public override string DiffToString(float diff)
		{
			string effectiveSpeed = ((int)(diff * 100) / 100f).ToString();
			return effectiveSpeed;
		}
	}
}
