namespace OO_C_Sharp_WinFormsApp
{

    internal static class Program
    {

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            /*
             * システム起動時のフォーム
             */
            Application.Run(new Library(1, "バーチャル図書館"));

        }

    }

}