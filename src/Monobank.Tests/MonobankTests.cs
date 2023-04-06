using System;
using System.Linq;
using System.Threading.Tasks;
using Monobank.Exceptions;
using Xunit;

namespace Monobank.Tests
{
    public class MonobankTests
    {
        private const string Token = "";
        private const string WebhookUrl = "";
        private const string RedirectUrl = "";
    
        [Fact]
        public void Ctor_TokenIsValid_InitializesNewInstance()
        {
            var monobank = new Monobank(Token);
        }

        [Fact]
        public void Ctor_TokenIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Monobank(null));
        }

        [Fact]
        public void Ctor_TokenIsEmptyString_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Monobank(string.Empty));
        }

        [Fact]
        public async Task GetCurrencyRatesAsync_Called_ReturnsCollectionOfCurrencyRates()
        {
            var monobank = new Monobank(Token);
            var result = await monobank.GetCurrencyRatesAsync();
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetCurrencyRatesAsync_WrongToken_ReturnsCollectionOfCurrencyRates()
        {
            // Currency Rates is a public API and doesn't require authentication.
            var monobank = new Monobank("123");
            var result = await monobank.GetCurrencyRatesAsync();
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetUserInfoAsync_Called_ReturnsUserInfo()
        {
            var monobank = new Monobank(Token);
            var result = await monobank.GetUserInfoAsync();
            Assert.NotNull(result);
            Assert.NotEmpty(result.Name);
            Assert.NotEmpty(result.Accounts);
        }
        
        [Fact]
        public async Task GetUserInfoAsync_WrongToken_ThrowsInvalidTokenException()
        {
            await Assert.ThrowsAsync<InvalidTokenException>(() => new Monobank("123").GetUserInfoAsync());
        }

        [Fact]
        public async Task GetStatementAsync_Called_ReturnsStatementItems()
        {
            var monobank = new Monobank(Token);
            var user = await monobank.GetUserInfoAsync();
            var accountId = user.Accounts.First().Id;
            var from = DateTime.Parse("2022-01-01 00:00:00");
            var to = DateTime.Parse("2022-01-31 23:59:59");
            var result = await monobank.GetStatementAsync(accountId, from, to);
            Assert.NotEmpty(result);
        }
        
        [Fact]
        public async Task GetStatementAsync_WrongToken_ThrowsInvalidTokenException()
        {
            await Assert.ThrowsAsync<InvalidTokenException>(async () 
                => await new Monobank("123").GetStatementAsync("123", DateTime.Now, DateTime.Now));
        }

        #region Acquiring

        [Fact]
        public async Task CreateInvoice_Called_ReturnsInvoiceInfo()
        {
            var monobank = new Monobank(Token);
            var result = await monobank.CreateInvoiceAsync(new Invoice
            {
                Amount = 100,
                RedirectUrl = RedirectUrl
            });
            Assert.NotNull(result);
        }

        #endregion
    }
}