using System.Windows.Forms;
using System.Drawing;
using UserData.UserForms.Elements;
using System.Data;
using System;
using UserData.MyElements;
using System.Drawing.Printing;

namespace UserData.UserForms
{
    public class UserLogInForm : Form
    {
        private UserData UserData { get; set; }

        private Panel panel;

        private Label loginLabel,
            errorLabel;
        private NickNameGroupBox nickNameGroupBox;
        private PasswordGroupBox passwordGroupBox;
        private Button button;

        public bool Authorized { get; private set; } = false;

        public UserLogInForm(UserData userData)
        {
            UserData = userData;

            Icon = userData.formIcon;
            StartPosition = FormStartPosition.CenterParent;
            AutoSize = false;
            MinimizeBox = false;
            MaximizeBox = false;

            loginLabel = new Label()
            {
                TextAlign = ContentAlignment.BottomCenter,
                Font = new Font("Calibri", 20, FontStyle.Bold),
                AutoSize = false,
                Size = new Size(300, 30),
                Location = new Point(42, 5),
                Text = Vars.Authorization
            };

            nickNameGroupBox = new NickNameGroupBox(41,45);

            passwordGroupBox = new PasswordGroupBox(41, 105);
            passwordGroupBox.PasswordChar = '*';

            errorLabel = new Label()
            {
                AutoSize = false,
                Name = "errorLabel",
                Size = new Size(303, 12),
                Font = new Font("Calibri", 8, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Red,
                Location = new Point(41, 155)
            };

            button = new Button()
            {
                Font = new Font("Calibri", 15, FontStyle.Bold),
                Size = new Size(200, 40),
                Text = Vars.Enter,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(92, 175)
            };
            button.Click += Button_Click;

            panel = new Panel();
            panel.Size = new Size(384, 241);

            panel.Controls.Add(loginLabel);
            panel.Controls.Add(nickNameGroupBox);
            panel.Controls.Add(passwordGroupBox);
            panel.Controls.Add(errorLabel);
            panel.Controls.Add(button);

            Controls.Add(panel);

            ClientSize = new Size(panel.Width, panel.Height);

            nickNameGroupBox.SelectionStart = 0;

            ShowDialog();
        }
        internal void Button_Click(object sender, EventArgs e)
        {
            if (!nickNameGroupBox.TextBoxComplete) { errorLabel.Text = $"{Vars.TextBoxEmpty} {Vars.Name}!"; return; }
            if (!passwordGroupBox.TextBoxComplete) { errorLabel.Text = $"{Vars.TextBoxEmpty} {Vars.Password}!"; return; }
            
            DataRow userDataRow = UserData.DB.GetRow($"SELECT * FROM {UserData.tableName} " +
                $"WHERE {Vars.UserNickName} = N'{nickNameGroupBox.TextBoxText}' " +
                $"AND {Vars.UserPassword} = N'{passwordGroupBox.TextBoxText}'");

            if (userDataRow == null)
            {
                MessageBox.Show(Vars.UserAbsent);
                return;
            }

            UserData.User.UserId = Convert.ToInt32(userDataRow["Id"]);
            UserData.User.UserPassword = userDataRow[Vars.UserPassword].ToString();
            UserData.User.UserName = userDataRow[Vars.UserName].ToString();
            UserData.User.UserMiddleName = userDataRow[Vars.UserMiddleName].ToString();
            UserData.User.UserLastName = userDataRow[Vars.UserLastName].ToString();
            UserData.User.UserEmail = userDataRow[Vars.UserEMail].ToString();
            UserData.User.UserPhoneNumber = userDataRow[Vars.UserPhone].ToString();
            UserData.User.UserAddress = userDataRow[Vars.UserAddress].ToString();
            UserData.User.UserGroup = userDataRow[Vars.UserGroup].ToString();

            Authorized = true;

            Close();
        }
    }
}
