namespace IconSource;

internal static class IconSource
{
	public static IDictionary<string, string> GenerateCodePointMapping(IEnumerable<string> codePointLines)
	{
		var codePointMap = codePointLines
			.Select(l =>
			{
				var split = l.Split([' '], StringSplitOptions.RemoveEmptyEntries);
				return split.Length == 2 ? new { Name = split[0], CodePoint = split[1] } : null;
			})
			.Where(s => s is not null)
			.ToDictionary(
			l => CodepointNameToEnumName(l.Name),
			l => l.CodePoint);
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