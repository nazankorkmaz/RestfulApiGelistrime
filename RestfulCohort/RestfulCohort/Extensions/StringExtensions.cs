using System.Globalization;

public static class StringExtensions
{
    public static string ToTitleCase(this string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
    }
}
