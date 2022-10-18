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
    public async Task<Translation?> Get(string content)
    {
        var clientBuilder = new TranslationClientBuilder();
        var client = await clientBuilder.BuildAsync();
        var result = await client.TranslateTextAsync(content, "zh-CN");
        return new Translation(OriginContent: content, OriginLanguage: result.DetectedSourceLanguage, TargetLanguage: result.TargetLanguage,
            TranslatedContent: result.TranslatedText);
    }
}