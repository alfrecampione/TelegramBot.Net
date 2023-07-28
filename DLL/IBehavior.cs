using Telegram.Bot;
using Telegram.Bot.Types;

namespace DLL;

public interface IBehavior
{
    public Task StartReceiver(ITelegramBotClient botClient);
    public Task OnMessage(ITelegramBotClient bot, Update update, CancellationToken cancellationToken);
    public Task ErrorMessage(ITelegramBotClient bot, Exception e, CancellationToken cancellationToken);
    public event Action<Message>? OnMessageReceived; 
}