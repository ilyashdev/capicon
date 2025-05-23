using Telegram.Bot;

namespace capicon.Services;

public class TelegramService
{
    private readonly IConfiguration _configuration;
    private readonly TelegramBotClient _botClient;
    public TelegramService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    
}