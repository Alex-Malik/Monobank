using System;
using System.Linq;
using System.Threading.Tasks;

namespace Monobank.Client.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var monobank = new Monobank(args[0]);
            var user = await monobank.GetUserInfo();
            var statements = await Task.WhenAll(user.Accounts.Select(account =>
                monobank.GetStatementAsync(account.Id, DateTime.Now.Date, DateTime.Now)));
            
            
        }
    }
}