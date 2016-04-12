using System;
using System.Windows.Forms;
using networking;
using services;


namespace client
{
    static class StartLibraryClient
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           
            ILibraryServer server = new LibraryServerObjectProxy("127.0.0.1", 55555);
            LibraryClientController controller=new LibraryClientController(server);
            LoginWindow win=new LoginWindow(controller);
            Application.Run(win);
        }
    }
}
