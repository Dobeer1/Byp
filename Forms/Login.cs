using System;
using System.Windows.Forms;
using KeyAuth; // Make sure you have the KeyAuth library referenced
using never.Forms;

namespace never
{
    public partial class Login : Form
    {


        public static api KeyAuthApp = new api(
            name: "Bypass", // App name
            ownerid: "aMqDtDj9MK", // Account ID
            version: "1.0" // Application version. Used for automatic downloads see video here https://www.youtube.com/watch?v=kW195PLCBKs
            //path: @"Your_Path_Here" // (OPTIONAL) see tutorial here https://www.youtube.com/watch?v=I9rxt821gMk&t=1s
);


        private void buttonanimated1_Click(object sender, EventArgs e)
        {

            Console.WriteLine(" Connecting...");
            KeyAuthApp.init();

            string key = usernameTextBox.Text; // Assuming you have a TextBox named licenseTextBox

            // Call the KeyAuth API to validate the license
            KeyAuthApp.license(key);

            if (KeyAuthApp.response.success)
            {
                // License is valid, open the main form
                OpenMainForm();
            }
            else
            {
                // License is invalid, show the error message
                MessageBox.Show("Status: " + KeyAuthApp.response.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit(); // Close the application
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