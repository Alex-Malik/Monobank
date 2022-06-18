# Monobank
Friendly C# library which makes it easy for you to interect with [Monobank API](https://api.monobank.ua/docs/) 🐈‍
## Installation
```
nuget install Monobank
```
## Usage
Please reference to the Monobank [official documentation](https://api.monobank.ua/docs/) 🐈‍⬛ to find answers to most common questions about how the Monobank API itself works.
To see the usage examples for this library please read the sections below ⬇️
### Personal API
To instantiate a client just create an instance of `Monobank` class with a personal token (generated by [scanning Monobank QR code](https://api.monobank.ua)).
Like this:
```
var mono = new Monobank("u3AulkpZFI1lIuGsik6vuPsVWqN7GoWs6o_MO2sdf301");
```
The personal token is required almost for all operations you may want to do so, please don't forget to generate it!
Actually it's not required only for currency rates 🙄, think about it.
#### Get User Info
```
var myMono = await mono.GetUserInfoAsync();
```
#### Get Statement
```
var accountId = "kKGVoZuHWzqVoZuH";
var from = DateTime.Parse("2022-06-01 00:00:00");
var to = DateTime.Parse("2022-06-31 23:59:59");
var juneStatement = await mono.GetStatementAsync(accountId, from, to);
```
#### Set a Webhook
```
mono.SetWebhookAsync("https://example.com/some_random_data_for_security_reasons"); 
```
### Service Provider API
Sorry... This one is in development at this very moment. But it will be done soon! I promise. 