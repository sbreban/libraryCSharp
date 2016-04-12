using System;
using System.Windows.Forms;
using services;

namespace client
{
    public partial class LoginWindow : Form
    {
        private LibraryClientController controller;
        public LoginWindow(LibraryClientController controller)
        {
            InitializeComponent();
            this.controller = controller;
        }

        private void clearBut_Click(object sender, EventArgs e)
        {
            userText.Clear();
            passText.Clear();
        }

        private void loginBut_Click(object sender, EventArgs e)
        {
            String user = userText.Text;
            String pass = passText.Text;
            try
            {
                controller.login(user, pass);
                if (user.Equals("admin"))
                {
                    ReturnBookWindow returnBookWindow = new ReturnBookWindow(controller);
                    returnBookWindow.Show();
                }
                else
                {
                    LibraryWindow libraryWin = new LibraryWindow(controller);
                    libraryWin.Text = "Library window for " + user;
                    libraryWin.Show();
                }
                this.Hide();
            }
            catch (LibraryException exception)
            {
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
