using DLL;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Test;

class Program
{
    private class MyBehavior : IBehavior
    {
        public event Action<Message>? OnMessageReceived;

        public async Task StartReceiver(ITelegramBotClient? botClient)
        {
            var cancellationTokenSource = new CancellationTokenSource().Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };
            if (botClient != null)
                await botClient.ReceiveAsync(OnMessage, ErrorMessage, receiverOptions, cancellationTokenSource);
        }

        public async Task OnMessage(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is Message message)
            {
                OnMessageReceived?.Invoke(message);
                await bot.SendTextMessageAsync(message.Chat.Id, "Message received",
                    cancellationToken: cancellationToken);
            }
        }

        public async Task ErrorMessage(ITelegramBotClient bot, Exception e, CancellationToken cancellationToken)
        {
            if (e is ApiRequestException requestException)
                await bot.SendTextMessageAsync(0, $"Error: {requestException.ErrorCode} - {requestException.Message}",
                    cancellationToken: cancellationToken);
        }
    }

    public static void Main()
    {
        IBehavior behavior = new MyBehavior();
        var bot = new Bot("5827307309:AAGDgNR7tzjbP42YEB6n1_e26fSPY0tglS4", behavior);
        Console.ReadLine();
    }
}