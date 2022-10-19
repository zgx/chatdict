using Telegram.Bot;

namespace chatdict.webapi.Service;

public class TelegramBotService
{
    public TelegramBotService(string token)
    {
        Client = new TelegramBotClient(token);
    }

    public const string TelegramBotTokenEnvName = "TELEGRAM_BOT_TOKEN";
    
    public TelegramBotClient Client { get; }

    private static TelegramBotService? _instance = null;

    public static TelegramBotService Default
    {
        get
        {
            if (_instance == null)
            {
                string? token = Environment.GetEnvironmentVariable(TelegramBotTokenEnvName);
                if (token == null)
                {
                    throw new Exception("Telegram bot token is not set");
                }

                _instance = new TelegramBotService(token);
            }
            return _instance;
        }
    }


}