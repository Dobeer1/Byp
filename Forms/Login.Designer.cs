using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace never
{
    partial class Login : Form
    {
        private System.ComponentModel.IContainer components = null;

        public Login()
        {
            InitializeComponent();
            SetRoundedCorners(30);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void SetRoundedCorners(int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(this.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(this.Width - radius, this.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, this.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            this.Region = new Region(path);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.siticoneDragControl1 = new Siticone.Desktop.UI.WinForms.SiticoneDragControl(this.components);
            this.siticonePanel1 = new Siticone.Desktop.UI.WinForms.SiticonePanel();
            this.label2 = new System.Windows.Forms.Label();
            this.usernameTextBox = new Siticone.Desktop.UI.WinForms.SiticoneTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonanimated1 = new Siticone.Desktop.UI.WinForms.SiticoneButton();
            this.siticonePanel1.SuspendLayout();
            this.SuspendLayout();

            // Apply a deep black background
            this.BackColor = Color.FromArgb(10, 10, 10); // Deep black background

            // siticoneDragControl1
            this.siticoneDragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.siticoneDragControl1.UseTransparentDrag = true;
            this.siticoneDragControl1.TargetControl = this;

            // siticonePanel1 (Header Bar)
            this.siticonePanel1.BackColor = Color.FromArgb(50, 0, 0); // Dark red-black
            this.siticonePanel1.Controls.Add(this.label2);
            this.siticonePanel1.Location = new System.Drawing.Point(0, 0);
            this.siticonePanel1.Name = "siticonePanel1";
            this.siticonePanel1.Size = new System.Drawing.Size(490, 50);
            this.siticonePanel1.TabIndex = 0;
            this.siticonePanel1.BorderRadius = 20;

            // label2 (Title)
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = Color.White;
            this.label2.Location = new System.Drawing.Point(200, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 30);
            this.label2.TabIndex = 0;
            this.label2.Text = "BYPASS";

            // usernameTextBox (Username Field)
            this.usernameTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.usernameTextBox.DefaultText = "";
            this.usernameTextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.usernameTextBox.Location = new System.Drawing.Point(120, 120);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.PasswordChar = '\0';
            this.usernameTextBox.PlaceholderText = "Enter Username";
            this.usernameTextBox.SelectedText = "";
            this.usernameTextBox.Size = new System.Drawing.Size(250, 39);
            this.usernameTextBox.TabIndex = 1;
            this.usernameTextBox.FillColor = Color.FromArgb(30, 0, 0);  // Dark red
            this.usernameTextBox.ForeColor = Color.White;
            this.usernameTextBox.FocusedState.FillColor = Color.FromArgb(60, 10, 10); // Brighter red focus
            this.usernameTextBox.BorderRadius = 10;
            this.usernameTextBox.BorderColor = Color.Red;

            // label1 (Username Label)
            this.label1.Location = new System.Drawing.Point(117, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username:";
            this.label1.ForeColor = Color.White;

            // buttonanimated1 (Login Button)
            this.buttonanimated1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonanimated1.ForeColor = Color.White;
            this.buttonanimated1.Location = new System.Drawing.Point(120, 180);
            this.buttonanimated1.Name = "buttonanimated1";
            this.buttonanimated1.Size = new System.Drawing.Size(250, 45);
            this.buttonanimated1.TabIndex = 3;
            this.buttonanimated1.Text = "Login";
            this.buttonanimated1.FillColor = Color.FromArgb(0, 0, 0); // Deep Red
            this.buttonanimated1.HoverState.FillColor = Color.FromArgb(200, 0, 0); // Brighter red hover
            this.buttonanimated1.Click += new System.EventHandler(this.buttonanimated1_Click);
            this.buttonanimated1.BorderRadius = 10;
            this.buttonanimated1.BorderColor = Color.Red;
            this.buttonanimated1.BorderThickness = 1;

            // Login Form Settings
            this.ClientSize = new System.Drawing.Size(490, 270);
            this.Controls.Add(this.siticonePanel1);
            this.Controls.Add(this.usernameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonanimated1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Login_MouseDown);

            this.siticonePanel1.ResumeLayout(false);
            this.siticonePanel1.PerformLayout();
            this.ResumeLayout(false);
        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            if (!usernameTextBox.Bounds.Contains(e.Location))
            {
                this.ActiveControl = null;
            }
        }

        private Siticone.Desktop.UI.WinForms.SiticoneDragControl siticoneDragControl1;
        private Siticone.Desktop.UI.WinForms.SiticonePanel siticonePanel1;
        private System.Windows.Forms.Label label2;
        private Siticone.Desktop.UI.WinForms.SiticoneTextBox usernameTextBox;
        private System.Windows.Forms.Label label1;
        private Siticone.Desktop.UI.WinForms.SiticoneButton buttonanimated1;
    }
}
