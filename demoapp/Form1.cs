namespace demoapp
{
    public partial class Form1 : Form, IGui
    {
        private IController Controller;
        delegate void DisplayFunction(string str);
        public Form1()
        {
            InitializeComponent();
        }

        public void Connect(IController controller)
        {
            Controller = controller;
        }

        public void Init()
        {
            SetDisplay("Waiting...");
        }

        public void SetDisplay(string s)
        {
            if (this.InvokeRequired)
            {
                DisplayFunction Displaylabel1Text = delegate (string str) { label1.Text = str; };
                BeginInvoke(Displaylabel1Text, s);
            }
            else
            {
                label1.Text = s;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Controller.ConvertPressed();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s = textBox1.Text;
            Controller.Amount = Convert.ToDouble(s);
            Controller.AmountInserted();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Controller.Currency = textBox2.Text;
            Controller.CurrencyInserted();
        }
    }
}