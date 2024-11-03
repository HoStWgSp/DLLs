using System.Windows.Forms;
using System.Drawing;
using UserData.UserForms.Elements;
using System.Data;
using System;
using UserData.MyElements;

namespace UserData.UserForms
{
    public class UserLogInForm : Form
    {
        private UserData UD { get; set; }

        private Panel panel;

        private Label loginLabel,
            errorLabel;
        private GroupBoxTextBox nameGroupBox,
            passwordGroupBox;
        private Button button;

        internal bool Authorized { get; private set; } = false;

        public UserLogInForm(UserData userData)
        {
            UD = userData;

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

            nameGroupBox = new GroupBoxTextBox(Vars.User + ":", Properties.Resources.User, 41,45,
                Vars.NameAndLastName, Vars.UserTextL);

            passwordGroupBox = new GroupBoxTextBox(Vars.Password + ":", Properties.Resources.Password,
                41, 105, Vars.Password, Vars.UserTextL);
            passwordGroupBox.textBox.PasswordChar = '*';

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
            panel.Controls.Add(nameGroupBox);
            panel.Controls.Add(passwordGroupBox);
            panel.Controls.Add(errorLabel);
            panel.Controls.Add(button);

            Controls.Add(panel);

            ClientSize = new Size(panel.Width, panel.Height);

            nameGroupBox.textBox.SelectionStart = 0;

            ShowDialog();
        }
        internal void Button_Click(object sender, EventArgs e)
        {
            if (!Checks.TextBoxNotEmptyOrWaterMarked(nameGroupBox.textBox, errorLabel, Vars.NameAndLastName, Vars.TextBoxEmpty + " " + Vars.Name + "!")) return;
            if (!Checks.TextBoxNotEmptyOrWaterMarked(passwordGroupBox.textBox, errorLabel, Vars.Password, Vars.TextBoxEmpty + " " + Vars.Password + "!")) return;

            DataRow userDataRow = Aauthorization();

            if (userDataRow == null) return;

            UD.User.Id = Convert.ToInt32(userDataRow[nameof(UD.User.Id)]);
            UD.User.UserPassword = userDataRow[nameof(UD.User.UserPassword)].ToString();
            UD.User.UserAdmin = userDataRow[nameof(UD.User.UserAdmin)].ToString();
            UD.User.UserName = userDataRow[nameof(UD.User.UserName)].ToString();
            UD.User.UserMiddleName = userDataRow[nameof(UD.User.UserMiddleName)].ToString();
            UD.User.UserLastName = userDataRow[nameof(UD.User.UserLastName)].ToString();
            UD.User.UserEmail = userDataRow[nameof(UD.User.UserEmail)].ToString();
            UD.User.UserPhoneNumber = userDataRow[nameof(UD.User.UserPhoneNumber)].ToString();
            UD.User.UserAddress = userDataRow[nameof(UD.User.UserAddress)].ToString();
            UD.User.UserGroup = userDataRow[nameof(UD.User.UserGroup)].ToString();

            Authorized = true;
            Close();
        }

        /// <summary>
        /// Проверяет существование пользователя в DataTable. И возвращает строку с данными найденного пользователя.
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public DataRow Aauthorization()
        {
            // Проверка существования пользователя            
            foreach (DataRow row in UD.UsersData.Rows)
            {
                if (row[Vars.UserName].ToString() == nameGroupBox.textBox.Text ||
                    (row[Vars.UserName].ToString() + " " + row[Vars.UserLastName].ToString()) == nameGroupBox.textBox.Text ||
                    row[Vars.UserLastName].ToString() == nameGroupBox.textBox.Text)
                {
                    if (row[Vars.UserPassword].ToString() == passwordGroupBox.textBox.Text)
                        return row;
                    else
                    {
                        passwordGroupBox.textBox.ForeColor = Color.Red;
                        errorLabel.Text = Vars.PasswordError;
                        return null;
                    }
                }
            }
            nameGroupBox.textBox.ForeColor = Color.Red;
            errorLabel.Text = Vars.LoginNameError;
            return null;
        }
    }
}
