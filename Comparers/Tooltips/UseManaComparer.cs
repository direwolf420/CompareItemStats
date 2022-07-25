namespace CompareItemStats.Comparers.Tooltips
{
	/// <summary>
	/// Less is better
	/// </summary>
	public class UseManaComparer : StatComparer
	{
		public UseManaComparer() : base("UseMana") { }

		public override float GetAbsDiff(float num, float compNum, out bool positive)
		{
			//Swap the numbers since less is better
			return base.GetAbsDiff(compNum, num, out positive);
		}

		public override string GetSignString(bool positive)
		{
			//Invert positive
			return base.GetSignString(!positive);
		}
	}
}
