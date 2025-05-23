using Telegram.Bot;

namespace capicon.Services;

public class TelegramService
{
    private readonly IConfiguration _configuration;
    private readonly TelegramBotClient _botClient;
    private readonly string? _chatId;
    public TelegramService(IConfiguration configuration)
    {
        _configuration = configuration;
        var token = _configuration
            .GetSection("TelegramBot")
            .GetValue<string>("ApiKey");
        _botClient = new TelegramBotClient(token);
        _chatId = _configuration
            .GetSection("TelegramBot")
            .GetValue<string>("ChatId");
    }

    public void Send(string text)
    {
        _botClient.SendMessage(_chatId, text);
    }
    
}