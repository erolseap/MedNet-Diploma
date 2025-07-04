namespace MedNet.Application.Helpers;

using System.Text.Json;

public static class StringExtensions
{
    public static string ToSnakeCaseLower(this string input)
    {
        return JsonNamingPolicy.SnakeCaseLower.ConvertName(input);
    }
    
    public static string ToSnakeCaseUpper(this string input)
    {
        return JsonNamingPolicy.SnakeCaseUpper.ConvertName(input);
    }
    
    public static string ToCamelCase(this string input)
    {
        return JsonNamingPolicy.CamelCase.ConvertName(input);
    }
}

