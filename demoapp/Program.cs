namespace demoapp
{
    internal static class Program
    {
        static private IController Controller;
        static private IGui Gui;
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            Form1 form = new Form1();
            Controller = new Conversion();
            Gui = form;
            Gui.Connect(Controller);
            Controller.Connect(Gui);
            Application.Run(form);
        }
    }
}