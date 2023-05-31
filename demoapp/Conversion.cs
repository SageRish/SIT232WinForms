using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.ComponentModel;

namespace demoapp
{
    enum Currency
    {
        AUD,
        CAD,
        CHF,
        CNY,
        INR,
    }

    public class Conversion : IController
    {
        private static Currency stringtoEnum(string s)
        {
            switch(s)
            {
                case "AUD":
                    return Currency.AUD;
                case "CAD":
                    return Currency.CAD;
                case "CHF":
                    return Currency.CHF;
                case "CNY":
                    return Currency.CNY;
                case "INR":
                    return Currency.INR;
                default:
                    return Currency.AUD;
            }
        }
        private static async Task DoConvert(string currency)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://openexchangerates.org/api/latest.json?app_id=13804999fb6e4d53b4007f57067a90a9"),
                Headers =
                {
                    { "accept", "application/json" },
                },
                        };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                JsonDocument doc = JsonDocument.Parse(body);
                JsonElement root = doc.RootElement;
                double curRate = root.GetProperty("rates").GetProperty(currency).GetDouble();

                _controller.ConversionRate = curRate;
            }
        }

        static async Task<double> Convert(double amount, Currency currency)
        {
            double convertedAmount = 0;
            switch (currency)
            {
                case Currency.AUD:
                    await DoConvert("AUD");
                    convertedAmount = amount * _controller.ConversionRate;
                    break;
                case Currency.CAD:
                    await DoConvert("CAD");
                    convertedAmount = amount * _controller.ConversionRate;
                    break;
                case Currency.CHF:
                    await DoConvert("CHF");
                    convertedAmount = amount * _controller.ConversionRate;
                    break;
                case Currency.CNY:
                    await DoConvert("CNY");
                    convertedAmount = amount * _controller.ConversionRate;
                    break;
                case Currency.INR:
                    await DoConvert("INR");
                    convertedAmount = amount * _controller.ConversionRate;
                    break;
            }
            return convertedAmount;
        }

        private static IController _controller = new Start();
        private static IGui _gui;

        public void Connect(IGui gui)
        {
            _controller.Connect(gui);
        }

        public void Init()
        {
            _controller.Init();
        }

        public void AmountInserted()
        {
            _controller.AmountInserted();
        }

        public void ConvertPressed()
        {
            _controller.ConvertPressed();
        }

        public void CurrencyInserted()
        {
            _controller.CurrencyInserted();
        }

        public class Start : IController
        {
            public void Connect(IGui gui)
            {
                _gui = gui;
                _controller.Init();
            }

            public void Init()
            {
                _gui.SetDisplay("0");
            }

            public void AmountInserted()
            {
                _gui.SetDisplay(_controller.Amount.ToString());
            }

            public void CurrencyInserted()
            {
                _gui.SetDisplay(_controller.Currency);
            }

            //public async 
            public async void ConvertPressed()
            {
                _gui.SetDisplay("Converting...");
                _controller.Amount =  await Convert(_controller.Amount, stringtoEnum(_controller.Currency));
                _gui.SetDisplay(_controller.Amount.ToString());
            }
        }
    }
}
