namespace DishCraft.Domain.Utility;

public static class SlugGenerator
{
    public static string Slugify(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        return value
            .Trim()
            .ToLowerInvariant()
            .Replace(" ", "-")
            .Replace("å", "a")
            .Replace("ø", "o")
            .Replace("æ", "ae");
    }
}