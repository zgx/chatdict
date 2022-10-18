using System.Collections;
using chatdict.webapi.Model;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Mvc;
using Google.Cloud.Translation.V2;

namespace chatdict.webapi.Controllers;

[Route("[controller]")]
public class TranslateController: ControllerBase
{
    [HttpGet]
    public TranslateModel? Get(string content)
    {
        var clientBuilder = new TranslationClientBuilder();
        var client = clientBuilder.Build();
        var result = client.TranslateText(content, "zn-CN");
        string resultText = result.TranslatedText;
        return new TranslateModel(originContent: content, originLanguage: "en", targetLanguage: "cn",
            translatedContent: resultText);
    }
}