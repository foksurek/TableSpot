using System.Text.RegularExpressions;

namespace TableSpot.Utils;

public static partial class Regexes
{
    [GeneratedRegex(@"^\d+$")]
    public static partial Regex CheckIfNumber();
}