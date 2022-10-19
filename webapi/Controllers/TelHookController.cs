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
    public async Task<string> OnTelUpdate([FromBody]Update update)
    {
        Console.WriteLine($"on tel webhook. {Request.ContentType} update id: {update.Id}");
        var msg = update.Message;
        if (msg == null)
        {
            Console.WriteLine("empty message");
            return "empty msg";
        }
        
        Console.WriteLine($"message. type: {msg.Chat.Type}, chat id: {msg.Chat.Id} {msg.Chat.Username}, text: {msg.Text}");
        if (string.IsNullOrEmpty(msg.Text))
        {
            Console.WriteLine("empty test");
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