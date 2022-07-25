using Terraria.Localization;

namespace CompareItemStats
{
	public static class LangHelper
	{
		/// <summary>
		/// short for Language.GetTextValue to avoid importing Terraria.Localization
		/// </summary>
		/// <returns>Text associated with this key</returns>
		internal static string GetText(string key, params object[] args)
		{
			return Language.GetTextValue(key, args);
		}

		/// <summary>
		/// Defaults to Mods.CompareItemStats. as the prefix for the key
		/// </summary>
		/// <returns>Text associated with this key</returns>
		internal static string GetTextFromMod(string partialKey, params object[] args)
		{
			return Language.GetTextValue(BuildKey(partialKey), args);
		}

		/// <summary>
		/// Creates a key suitable for lookup for the CompareItemStats mod ("Mods.CompareItemStats." as prefix)
		/// </summary>
		internal static string BuildKey(string key)
		{
			return $"Mods.CompareItemStats.{key}";
		}
	}
}
