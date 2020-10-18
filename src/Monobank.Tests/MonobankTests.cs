using System;
using System.Threading.Tasks;
using Monobank.Exceptions;
using Xunit;

namespace Monobank.Tests
{
    public class MonobankTests
    {
        [Fact]
        public void Ctor_TokenIsValid_InitializesNewInstance()
        {
            var monobank = new Monobank("TODO Get token from settings");
        }

        [Fact]
        public void Ctor_TokenIsNull_ThrowsInvalidTokenException()
        {
            Assert.Throws<InvalidTokenException>(() => new Monobank(null));
        }

        [Fact]
        public void Ctor_TokenIsEmptyString_ThrowsInvalidTokenException()
        {
            Assert.Throws<InvalidTokenException>(() => new Monobank(string.Empty));
        }

        [Fact]
        public async Task GetCurrencyRatesAsync_Called_ReturnsCollectionOfCurrencyRates()
        {
            var monobank = new Monobank("token");
            var result = await monobank.GetCurrencyRatesAsync();
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetCurrencyRatesAsync_WrongToken_ReturnsCollectionOfCurrencyRates()
        {
            // Currency Rates is a public API and doesn't require authentication.
            var monobank = new Monobank("token");
            var result = await monobank.GetCurrencyRatesAsync();
            Assert.NotEmpty(result);
        }
    }
}