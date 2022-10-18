namespace chatdict.webapi.Model;

public record Translation(string OriginContent, string OriginLanguage, string TargetLanguage, string TranslatedContent)
{
    public string OriginContent { get; set; } = OriginContent;

    public string OriginLanguage { get; set; } = OriginLanguage;

    public string TargetLanguage { get; set; } = TargetLanguage;

    public String TranslatedContent { get; set; } = TranslatedContent;
}