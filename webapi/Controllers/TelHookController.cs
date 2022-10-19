
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types;


namespace chatdict.webapi.Controllers;

[Route("tel-hook")]
public class TelHookController: ControllerBase
{

    [HttpPost]
    public async Task<string> OnTelUpdate([FromBody]Update? update)
    {
        Console.WriteLine($"on tel webhook. {Request.ContentType}");

        if (update == null)
        {
            Console.WriteLine($"update is null. content type {Request.ContentType}");
            Request.Body.Seek(0, SeekOrigin.Begin);
            var readResult = await Request.BodyReader.ReadAsync();
            var body = Encoding.UTF8.GetString(readResult.Buffer);
            Console.WriteLine($"request body: {body}");
            return "empty update";
        }
        
        var msg = update.Message;
        if (msg == null)
        {
            Console.WriteLine("empty message");
            return "empty msg";
        }
        
        Console.WriteLine($"update id: {update.Id}.  chat type: {msg.Chat.Type}, chat id: {msg.Chat.Id} {msg.Chat.Username}, text: {msg.Text}");
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