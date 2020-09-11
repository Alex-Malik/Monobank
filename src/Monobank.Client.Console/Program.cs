using System;
using System.Linq;
using System.Threading.Tasks;

namespace Monobank.Client.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var monobank = new Monobank(args[0]);

            monobank.GetCurrencyRatesAsync().GetAwaiter().GetResult();
            monobank.GetUserInfo().GetAwaiter().GetResult();
            monobank.GetStatementAsync("0", DateTime.UtcNow.Date, DateTime.UtcNow).GetAwaiter().GetResult();
            monobank.SetWebhookAsync("https://63891b3d4d0a.ngrok.io").GetAwaiter().GetResult();
        }
    }
}