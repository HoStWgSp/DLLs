using DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBase.Connection.MySQL
{
    internal class MySQLPanel : MainPanel
    {
        DataBase dataBase;

        Label serverLabel,
            dataBaseLabel,
            uidLabel,
            passworfLabel;

        int labelWidth = 110;
        int labelHeingt = 19;

        TextBox serverTextBox,
            dataBaseTextBox,
            uidTextBox,
            passwordTextBox;
        int textBoxWidth = 160;

        public MySQLPanel(DataBase dataBase)
        {
            this.dataBase = dataBase;
            //BackColor = Color.Green;

            Size = new Size(dataBase.ClientSize.Width, dataBase.ClientSize.Height - 30);

            serverLabel = new Label()
            {
                Location = new Point(3, 3),
                Text = Vars.Server + ":",
                //BackColor = Color.Red,
                Font = Fonts.SearchLabelFont(),
                Size = new Size(labelWidth, labelHeingt),
                TextAlign = ContentAlignment.MiddleRight
            };
            Controls.Add(serverLabel);

            dataBaseLabel = new Label()
            {
                Location = new Point(serverLabel.Location.X, serverLabel.Height + serverLabel.Location.Y),
                Text = Vars.DataBaseName + ":",
                //BackColor = Color.Blue,
                Font = Fonts.SearchLabelFont(),
                Size = serverLabel.Size,
                TextAlign = ContentAlignment.MiddleRight
            };
            Controls.Add(dataBaseLabel);

            uidLabel = new Label()
            {
                Location = new Point(dataBaseLabel.Location.X, dataBaseLabel.Height + dataBaseLabel.Location.Y),
                Text = Vars.User + ":",
                //BackColor = Color.Red,
                Font = Fonts.SearchLabelFont(),
                Size = dataBaseLabel.Size,
                TextAlign = ContentAlignment.MiddleRight
            };
            Controls.Add(uidLabel);

            passworfLabel = new Label()
            {
                Location = new Point(uidLabel.Location.X, uidLabel.Height + uidLabel.Location.Y),
                Text = Vars.Password + ":",
                //BackColor = Color.Blue,
                Font = Fonts.SearchLabelFont(),
                Size = uidLabel.Size,
                TextAlign = ContentAlignment.MiddleRight
            };
            Controls.Add(passworfLabel);

            serverTextBox = new TextBox()
            {
                Location = new Point(serverLabel.Location.X + labelWidth, serverLabel.Location.Y - 1),
                Width = textBoxWidth
            };
            Controls.Add(serverTextBox);

            dataBaseTextBox = new TextBox()
            {
                Location = new Point(dataBaseLabel.Location.X + labelWidth, dataBaseLabel.Location.Y - 1),
                Width = textBoxWidth
            };
            Controls.Add(dataBaseTextBox);

            uidTextBox = new TextBox()
            {
                Location = new Point(uidLabel.Location.X + labelWidth, uidLabel.Location.Y - 1),
                Width = textBoxWidth
            };
            Controls.Add(uidTextBox);

            passwordTextBox = new TextBox()
            {
                Location = new Point(passworfLabel.Location.X + labelWidth, passworfLabel.Location.Y - 1),
                Width = textBoxWidth
            };
            Controls.Add(passwordTextBox);


            button=new Button()
            {
                Size = new Size(100, 30),
                Text = Vars.GetConnect
            };
            button.Location = new Point((Width - button.Width)/2, passworfLabel.Location.Y + labelHeingt + 10);
            Controls.Add(button);
            button.Click += Button_Click;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            dataBase.ConStr = $"Server={serverTextBox.Text}; Database={dataBaseTextBox.Text}; Uid={uidTextBox.Text}; " +
                $"Pwd={passwordTextBox.Text}";

            dataBase.dataProvider = new MySQLOpenConnection(dataBase);

            if (dataBase.MySqlConnection == null)
            {
                MessageBox.Show("Невозможно подключиться к базе данных. Проверьте введеные данные.");
                return;
            }
            dataBase.Close();
        }
    }
}
