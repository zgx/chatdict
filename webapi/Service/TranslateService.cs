using System.Diagnostics;
using Google.Cloud.Translation.V2;

namespace chatdict.webapi.Service;

public class TranslateService
{
    public TranslateService()
    {
        _client = new TranslationClientBuilder().Build();
    }

    public static TranslateService Default
    {
        get
        {
            if (_instance == null)
            {
                _instance = new TranslateService();
            }
            return _instance;
        }
    }

    private static TranslateService? _instance;
    private TranslationClient _client;

    public async Task<TranslationResult> Translate(string text, string targetLanguage, string? sourceLanguage = null)
    {
        Debug.Assert(_client != null);
        return await _client.TranslateTextAsync(text, targetLanguage, sourceLanguage);
    }
}