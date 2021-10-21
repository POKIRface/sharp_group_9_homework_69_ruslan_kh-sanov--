using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace task1
{
    public class Bot
    {
        private readonly TelegramBotClient _bot;
        public Bot(string token)
        {
            _bot = new TelegramBotClient(token);
        }

        public void StartBot()
        {
            _bot.OnMessage += OnMessageReceived;
            _bot.StartReceiving();
            while (true)
            {
                Console.WriteLine("Bot is worked all right");
                Thread.Sleep(int.MaxValue);
            }
        }

        private async void OnMessageReceived(object sender, MessageEventArgs messageEventArgs)

        {
            try
            {
                Message message = messageEventArgs.Message;
                Console.WriteLine(message.Text);
                await _bot.SendTextMessageAsync(message.Chat.Id, message.Text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }



    }
}
