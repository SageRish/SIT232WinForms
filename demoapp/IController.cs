namespace demoapp
{
    public interface IController
    {
        private static string _currency;
        private static double _amount;
        private static double _conversionRate;

        public string Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }

        public double Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        public double ConversionRate
        {
            get { return _conversionRate; }
            set { _conversionRate = value; }
        }

        //Connect controller to gui
        //(This method will be called before any other methods)
        void Connect(IGui gui);

        //Called to initialise the controller
        void Init();

        //Called whenever amount is inserted into the machine
        void AmountInserted();

        //Called whenever the go/stop button is pressed
        void ConvertPressed();

        //Called when currency is inserted
        void CurrencyInserted();
    }
}
