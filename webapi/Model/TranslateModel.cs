namespace chatdict.webapi.Model;

public record TranslateModel
{
    public TranslateModel(string originContent, string originLanguage, string targetLanguage, string translatedContent)
    {
        OriginContent = originContent;
        OriginLanguage = originLanguage;
        TargetLanguage = targetLanguage;
        TranslatedContent = translatedContent;
    }

    public string OriginContent { get; set; }
    
    public string OriginLanguage { get; set; }
    
    public string TargetLanguage { get; set; }
    
    public String TranslatedContent { get; set; }
    
}