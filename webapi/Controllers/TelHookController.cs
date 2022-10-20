
using System.Text;
using chatdict.webapi.Service;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
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
            Console.WriteLine("empty text");
            return "empty text";
        }
        
        var botClient = TelegramBotService.Default.Client;

        if (msg.Text.StartsWith("/"))
        {
            Console.WriteLine($"received tel command {msg.Text}");
            if (msg.Text.StartsWith("/start"))
            {
                string replyText = ReplyOfStartCommand();
                await botClient.SendTextMessageAsync(msg.Chat.Id, replyText);
                return "start";
            }
            return "command";
        }
        
        Console.WriteLine($"Update Hook {update.Id} {msg.Type} {msg.Text}");
        
        var translateResult = await TranslateService.Default.Translate(text: msg.Text, "zh-CN");
        

        var sentMsg = await botClient.SendTextMessageAsync(msg.Chat.Id, translateResult.TranslatedText);
        
        return "ok";
    }

    private string ReplyOfStartCommand()
    {
        string content = @"欢迎使用KiKi翻译机器人
用法很简单
看不懂的发给我，转发也行
我回你中文
";
        return content;

    }
}