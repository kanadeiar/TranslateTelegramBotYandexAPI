using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace TranslateTelegramBotYandexAPI
{
    // https://yandex.ru/dev/
    // https://core.telegram.org/api
    // https://yandex.ru/dev/translate/
    class Program
    {
        static string apiKey = " ";

        private static string GetEngText(string text)
        {
            string url = $"https://translate.yandex.net/api/v1.5/tr.json/translate?key={apiKey}&text={text}&lang=ru-en&format=plain";
            var json = new WebClient() { Encoding = Encoding.UTF8 }.DownloadString(url);
            dynamic jObject = JObject.Parse(json);
            return jObject.text[0];
        }

        static void Main(string[] args)
        {
            TelegramBotClient bot = new TelegramBotClient(File.ReadAllText(@"D:\Work\token.txt"));

            bot.OnMessage += (s, arg) =>
            {
                Console.WriteLine($"{arg.Message.Chat.FirstName}: {arg.Message.Text}");
                bot.SendTextMessageAsync(arg.Message.Chat.Id, $"Вы прислали: {GetEngText(arg.Message.Text)}");
            };

            bot.StartReceiving();

            Console.ReadKey();
        }

      
    }
}
