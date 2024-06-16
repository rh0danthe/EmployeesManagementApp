using System.Text.RegularExpressions;

namespace Application.Utils;

public static class StringCleaner
{
    public static string CleanInput(string input)
    {
        if (input is null) return null;
        if (string.IsNullOrWhiteSpace(input))
            return input;

        input = input.Trim();

        input = Regex.Replace(input, @"\s+", " ");
        
        return input;
    }
}