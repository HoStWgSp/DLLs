using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserData.MyElements;

namespace UserData.UserForms.Elements
{
    internal class UserDataFormTemplate : Form
    {
        string[] groupsList {  get; set; }
        //internal bool Admin {  get; private set; }

        internal Panel panel;

        internal Label loginLabel,
            errorLabel;
        internal NameMiddleNameTextBox nameGroupBox;
        internal LastNameGroupBox lastNameGroupBox;
        internal PasswordGroupBox passwordGroupBox;
        internal EMailGroupBox eMailGroupBox;
        internal AddressGroupBox addressGroupBox;
        internal PhoneNumberGroupBox phoneNumberGroupBox;
        internal GroupBoxComboBox groupsGroupBox;
        internal Button button;

        public UserDataFormTemplate (Icon formIcon, string[] groups, string labelText, string buttonText)
        {
            // Иконка формы
            Icon = formIcon;
            // Создаем массив строк из словаря
            groupsList = groups;
            //Admin = admin;
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
                Text = labelText
            };

            nameGroupBox = new NameMiddleNameTextBox(41, 45);

            lastNameGroupBox = new LastNameGroupBox(41, 105);

            passwordGroupBox = new PasswordGroupBox(41, 165);

            phoneNumberGroupBox = new PhoneNumberGroupBox(41, 225);

            eMailGroupBox = new EMailGroupBox(41, 285);

            addressGroupBox = new AddressGroupBox(41, 345);

            groupsGroupBox = new GroupBoxComboBox(41, 405, groupsList);

            errorLabel = new Label()
            {
                AutoSize = false,
                Name = "errorLabel",
                Size = new Size(303, 12),
                Font = new Font("Calibri", 8, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Red,
                Location = new Point(41, 455)
            };

            button = new Button()
            {
                Font = new Font("Calibri", 15, FontStyle.Bold),
                Size = new Size(200, 40),
                Text = buttonText,
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(92, 479)
            };
            button.Click += Button_Click;

            panel = new Panel();
            panel.Size = new Size(384, 545);

            if (groups.Length == 1)//admin)
            {
                groupsGroupBox.ComboBoxText = groupsList[0].ToString();
                groupsGroupBox.ComboBoxEnable = false;
            }

            panel.Controls.Add(loginLabel);
            panel.Controls.Add(nameGroupBox);
            panel.Controls.Add(lastNameGroupBox);
            panel.Controls.Add(passwordGroupBox);
            panel.Controls.Add(phoneNumberGroupBox);
            panel.Controls.Add(eMailGroupBox);
            panel.Controls.Add(addressGroupBox);
            panel.Controls.Add(groupsGroupBox);
            panel.Controls.Add(errorLabel);
            panel.Controls.Add(button);

            Controls.Add(panel);

            ClientSize = new Size(panel.Width, panel.Height);

            ActiveControl = nameGroupBox.TextBox1ActiveControl;
            nameGroupBox.TextBox1SelectionStart = 0;
        }

        /// <summary>
        /// Проверяет все ли поля заполнены корректно
        /// </summary>
        /// <returns></returns>
        internal bool TextBoxesFillCorrect()
        {
            if (!nameGroupBox.TextBox1Comlete) { errorLabel.Text = $"{Vars.TextBoxEmpty} {Vars.Name}!"; return false; }

            if (!lastNameGroupBox.TextBoxComplete) { errorLabel.Text = $"{Vars.TextBoxEmpty} {Vars.LastName}!"; return false; }

            if (!passwordGroupBox.TextBoxComplete) { errorLabel.Text = $"{Vars.TextBoxEmpty} {Vars.Password}!"; return false; }

            if (!phoneNumberGroupBox.PhoneMaskCompleted) { errorLabel.Text = $"{Vars.TextBoxEmpty} {Vars.Phone}!"; return false; }

            if (!eMailGroupBox.EMailCorrect) { errorLabel.Text = Vars.EmailIncorrect + "!"; return false; }
            
            if (!addressGroupBox.TextBoxComplete) { errorLabel.Text = $"{Vars.TextBoxEmpty} {Vars.Address}!"; return false; }

            if (!groupsGroupBox.GroupComboBoxSelected) { errorLabel.Text = Vars.GroupNotChoosen; return false; }

            return true;
        }
        internal virtual void Button_Click(object sender, EventArgs e) { }
    }
}
