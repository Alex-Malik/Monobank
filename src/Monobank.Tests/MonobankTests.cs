using System;
using System.Threading.Tasks;
using Monobank.Exceptions;
using Xunit;

namespace Monobank.Tests
{
    public class MonobankTests
    {
        private const string Token = "";
    
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
        public async Task GetUserInfoAsync_WrongToken_ReturnsUserInfo()
        {
            var monobank = new Monobank("123");
            Assert.ThrowsAsync<InvalidTokenException>(() => monobank.GetUserInfoAsync());
        }
    }
}