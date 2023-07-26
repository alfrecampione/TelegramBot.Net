using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable
namespace DLL;

public class Bot
{
    private readonly TelegramBotClient? _botClient;
    private readonly IBehavior _behavior;
    public Dictionary<long, List<Message>> Messages => _messages;
    private Dictionary<long, List<Message>> _messages = new();

    public Bot(string token, IBehavior behavior)
    {
        _botClient = new TelegramBotClient(token);
        _behavior = behavior;
        this._behavior.StartReceiver(this._botClient);
        this._behavior.OnMessageReceived += MessageReceived;
    }

    private void MessageReceived(Message message)
    {
        if (!_messages.ContainsKey(message.Chat.Id))
            _messages.Add(message.Chat.Id, new List<Message>());
        _messages[message.Chat.Id].Add(message);
        Console.WriteLine($"Message received from {message.Chat.Id}");
    }
}