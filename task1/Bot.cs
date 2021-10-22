using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

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
            _bot.OnCallbackQuery += HandleCallbackQuery;
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
                Random rand = new Random();
                int r = rand.Next(1, 3);
                string choose = string.Empty;
                switch (r.ToString())
                {
                    case "1":
                        choose = "rock";
                        break;
                    case "2":
                        choose = "paper";
                        break;
                    case "3":
                        choose = "scissors";
                        break;
                }
                var markup = new InlineKeyboardMarkup(new[]
                         {
                     new InlineKeyboardButton(){Text = "rock", CallbackData = "rock"},
                     new InlineKeyboardButton(){Text = "paper", CallbackData = "paper"},
                     new InlineKeyboardButton(){Text = "scissors", CallbackData= "scissors"}
                        });
                var markupend = new InlineKeyboardMarkup(new[]
                         {
                     new InlineKeyboardButton(){Text = "продолжить", CallbackData = "continue"},
                     new InlineKeyboardButton(){Text = "закончить", CallbackData = "stop"},
                        });
                switch (message.Text)
                {
                    case "/start":
                        await _bot.SendTextMessageAsync(message.Chat.Id, "Победитель определяется по следующим правилам:Бумага побеждает камень(«бумага обёртывает камень»).Камень побеждает ножницы(«камень затупляет или ломает ножницы»).Ножницы побеждают бумагу(«ножницы разрезают бумагу»).\n/help-Правила игры \n/game-начало игры");
                        break;
                    case "/help":
                        await _bot.SendTextMessageAsync(message.Chat.Id, "Победитель определяется по следующим правилам:Бумага побеждает камень(«бумага обёртывает камень»).Камень побеждает ножницы(«камень затупляет или ломает ножницы»).Ножницы побеждают бумагу(«ножницы разрезают бумагу»).");
                        break;
                    case "/game":
                        await _bot.SendTextMessageAsync(message.Chat.Id, "Начали", replyMarkup: markup);
                        _bot.OnCallbackQuery += async (object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev) =>
                        {
                            var message = ev.CallbackQuery.Message;
                            if (ev.CallbackQuery.Data == "rock")
                            {
                                await _bot.SendTextMessageAsync(message.Chat.Id, RockPaperScissors(choose, "rock"), replyToMessageId: message.MessageId);
                                await _bot.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                                await _bot.SendTextMessageAsync(message.Chat.Id, "Я загадал: " + choose);
                                await _bot.SendTextMessageAsync(message.Chat.Id, "Вы загадали: rock");
                                await _bot.SendTextMessageAsync(message.Chat.Id, "Что дальше?", replyMarkup: markupend);
                                _bot.OnCallbackQuery += async (object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev) =>
                                {
                                    var message = ev.CallbackQuery.Message;
                                    if (ev.CallbackQuery.Data == "continue")
                                    {
                                        message.Text = "/game";
                                    }
                                    else
                                    if (ev.CallbackQuery.Data == "stop")
                                    {
                                        await _bot.SendTextMessageAsync(message.Chat.Id, "Спасибо за игру");
                                    }
                                };
                            }
                            if (ev.CallbackQuery.Data == "scissors")
                            {
                                await _bot.SendTextMessageAsync(message.Chat.Id, RockPaperScissors(choose, "scissors"), replyToMessageId: message.MessageId);
                                await _bot.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                                await _bot.SendTextMessageAsync(message.Chat.Id, "Я загадал: " + choose);
                                await _bot.SendTextMessageAsync(message.Chat.Id, "Вы загадали: scissors");
                                await _bot.SendTextMessageAsync(message.Chat.Id, "Что дальше?", replyMarkup: markupend);
                                _bot.OnCallbackQuery += async (object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev) =>
                                {
                                    var message = ev.CallbackQuery.Message;
                                    if (ev.CallbackQuery.Data == "continue")
                                    {
                                        message.Text = "/game";
                                    }
                                    else
                                    if (ev.CallbackQuery.Data == "stop")
                                    {
                                        await _bot.SendTextMessageAsync(message.Chat.Id, "Спасибо за игру");
                                    }
                                };
                            }
                            else
                            if (ev.CallbackQuery.Data == "paper")
                            {
                                await _bot.SendTextMessageAsync(message.Chat.Id, RockPaperScissors(choose, "paper"), replyToMessageId: message.MessageId);
                                await _bot.AnswerCallbackQueryAsync(ev.CallbackQuery.Id);
                                await _bot.SendTextMessageAsync(message.Chat.Id, "Я загадал: " + choose);
                                await _bot.SendTextMessageAsync(message.Chat.Id, "Вы загадали: paper");
                                await _bot.SendTextMessageAsync(message.Chat.Id, "Что дальше?", replyMarkup: markupend);
                                _bot.OnCallbackQuery += async (object sc, Telegram.Bot.Args.CallbackQueryEventArgs ev) =>
                                {
                                    var message = ev.CallbackQuery.Message;
                                    if (ev.CallbackQuery.Data == "continue")
                                    {
                                        message.Text = "/game";
                                    }
                                    else
                                    if (ev.CallbackQuery.Data == "stop")
                                    {
                                        await _bot.SendTextMessageAsync(message.Chat.Id, "Спасибо за игру");
                                    }
                                };
                            }
                        };
                        break;
                        
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async void HandleCallbackQuery(object sender, CallbackQueryEventArgs callbackQueryEventArgs)

        {
            await _bot.AnswerCallbackQueryAsync(callbackQueryEventArgs.CallbackQuery.Id,
                callbackQueryEventArgs.CallbackQuery.Data);
            await _bot.EditMessageReplyMarkupAsync(callbackQueryEventArgs.CallbackQuery.Message.Chat.Id,
                callbackQueryEventArgs.CallbackQuery.Message.MessageId, null);
        }

        public static string RockPaperScissors(string first, string second)
    => (first, second) switch
    {
        ("rock", "paper") => "rock is covered by paper. Paper wins.",
        ("rock", "scissors") => "rock breaks scissors. Rock wins.",
        ("paper", "rock") => "paper covers rock. Paper wins.",
        ("paper", "scissors") => "paper is cut by scissors. Scissors wins.",
        ("scissors", "rock") => "scissors is broken by rock. Rock wins.",
        ("scissors", "paper") => "scissors cuts paper. Scissors wins.",
        (_, _) => "tie"
    };



    }
}
