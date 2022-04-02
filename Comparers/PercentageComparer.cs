using Terraria;

namespace CompareItemStats.Comparers
{
	/// <summary>
	/// Used to extract information from the tooltip when the number is coupled with a % at the beginning
	/// </summary>
	public class PercentageComparer : StatComparer
	{
		private static readonly char percent = '%';

		public PercentageComparer(string displayName) : base(displayName) { }

		public override float GetValueFromString(string text, Item item)
		{
			string[] splitText = text.Split(' ');

			foreach (var subText in splitText)
			{
				int index = subText.IndexOf(percent);
				int lastCharIndex = subText.Length - 1;
				if (index == lastCharIndex)
				{
					string numberText = subText.Substring(0, lastCharIndex);

					if (float.TryParse(numberText, out float result))
					{
						return result;
					}
				}
			}
			return 0;
		}

		public override string DiffToString(float diff)
		{
			return base.DiffToString(diff) + percent;
		}
	}
}
