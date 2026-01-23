using Event_Monitor.Data;

namespace Event_Monitor
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

            using (var context = new EventMonitorDbContext())
            {
                context.Database.EnsureCreated();
            }

            // Run the async initialization synchronously to maintain STA context
            AppContext.LoadFromDatabaseAsync().GetAwaiter().GetResult();

            Application.Run(new Form1());
        }
    }
}