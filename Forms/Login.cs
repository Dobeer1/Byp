using System;
using System.Windows.Forms;
using never.Forms;

namespace never
{
    public partial class Login : Form
    {


        private void buttonanimated1_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;

            // Perform basic username check (you can modify this logic)
            if (username == "admin") // Example username check
            {
                OpenMainForm();
            }
            else
            {
                Application.Exit();
            }
        }

        private void OpenMainForm()
        {
            Main form = new Main();
            form.Show();
            this.Hide();
        }
    }
}
