using System;
using System.Windows.Forms;

namespace client
{
    public partial class ReturnBookWindow : Form
    {
        private LibraryClientController controller;

        public ReturnBookWindow(LibraryClientController controller)
        {
            InitializeComponent();
            this.controller = controller;
        }

        private void returnButton_Click(object sender, EventArgs e)
        {
            controller.returnBook(Int32.Parse(userIdTextBox.Text), Int32.Parse(bookIdTextBox.Text));
        }

        private void ReturnBookWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("ReturnBookwindow closing " + e.CloseReason);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                controller.logout();
                Application.Exit();
            }
        }
    }
}
