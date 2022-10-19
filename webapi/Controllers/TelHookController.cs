using System.Text.Json.Serialization;
using chatdict.webapi.Service;
using Telegram.Bot;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Microsoft.Extensions.Logging;

namespace chatdict.webapi.Controllers;

[Route("tel-hook")]
public class TelHookController: ControllerBase
{

    [HttpPost]
    public async Task<string> OnTelUpdate([FromBody]Update? update)
    {
        if (update == null)
        {
            return "update is null";
        }
        var msg = update.Message;
        if (msg == null)
        {
            return "empty msg";
        }

        if (string.IsNullOrEmpty(msg.Text))
        {
            return "empty text";
        }

        if (msg.Text.StartsWith("/"))
        {
            Console.WriteLine($"received tel command {msg.Text}");
            return "command";
        }

        Console.WriteLine($"Update Hook {update.Id} {msg.Type} {msg.Text}");

        var translateResult = await TranslateService.Default.Translate(text: msg.Text, "zh-CN");

        var botClient = TelegramBotService.Default.Client;
        var sentMsg = await botClient.SendTextMessageAsync(msg.Chat.Id, translateResult.TranslatedText);

        return "ok";
    }
}