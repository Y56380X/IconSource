namespace IconSource;

internal static class IconSource
{
	public static IDictionary<string, string> GenerateCodePointMapping(string[]codePointLines)
	{
		var codePointMap = codePointLines.ToDictionary(
			l => CodepointNameToEnumName(l.Split(' ')[0]),
			l => l.Split(' ')[1]);
		return codePointMap;
		
		string Capitalize(string s)
		{
			var cs = s.ToCharArray();
			cs[0] = char.ToUpper(cs[0]);
			return new string(cs);
		}

		string ResSnC(string snCs) =>
			snCs.IndexOf('_') is var index and > -1
				? Capitalize($"{snCs.Substring(0, index)}{ResSnC(snCs.Substring(index + 1))}")
				: Capitalize(snCs);

		string CodepointNameToEnumName(string s) =>
			s.FirstOrDefault() is var c && !char.IsLetter(c)
				? $"_{ResSnC(s)}"
				: ResSnC(s);
	}
}