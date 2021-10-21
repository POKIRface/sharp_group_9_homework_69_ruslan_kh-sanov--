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
                switch(message.Text)
                {
                    case"/start":
                        await _bot.SendTextMessageAsync(message.Chat.Id, "Победитель определяется по следующим правилам:Бумага побеждает камень(«бумага обёртывает камень»).Камень побеждает ножницы(«камень затупляет или ломает ножницы»).Ножницы побеждают бумагу(«ножницы разрезают бумагу»).\n/help-Правила игры \n/game-начало игры");
                        break ;
                    case "/help":
                        await _bot.SendTextMessageAsync(message.Chat.Id, "Победитель определяется по следующим правилам:Бумага побеждает камень(«бумага обёртывает камень»).Камень побеждает ножницы(«камень затупляет или ломает ножницы»).Ножницы побеждают бумагу(«ножницы разрезают бумагу»).");
                        break;
                    case "/game":
                        await _bot.SendTextMessageAsync(message.Chat.Id, "Победитель определяется по следующим правилам:Бумага побеждает камень(«бумага обёртывает камень»).Камень побеждает ножницы(«камень затупляет или ломает ножницы»).Ножницы побеждают бумагу(«ножницы разрезают бумагу»).\n/help-Правила игры \n/game-начало игры");
                        break;
                }
                await _bot.SendTextMessageAsync(message.Chat.Id, "Не ну ты выдал");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }



    }
}
