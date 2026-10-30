using DataBase.Elements;
using DataBase.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DataBase.Connection.MySQL
{
    internal class MySQLPanel : MainPanel
    {
        DBConnection dataBase;
        DBStartForm dBStartForm;

        Label serverLabel,
            dataBaseLabel,
            uidLabel,
            passworfLabel;

        int labelWidth = 110,
            labelHeingt = 19,
            textBoxWidth = 160;

        TextBox serverTextBox,
            dataBaseTextBox,
            uidTextBox,
            passwordTextBox;

        public MySQLPanel(DBConnection dataBase, DBStartForm dBStartForm)
        {
            this.dataBase = dataBase;
            this.dBStartForm = dBStartForm;
            //BackColor = Color.Green;

            Size = new Size(dBStartForm.ClientSize.Width, dBStartForm.ClientSize.Height - 30);

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

        void Button_Click(object sender, EventArgs e)
        {
            string connectionString = $"Server={serverTextBox.Text}; Database={dataBaseTextBox.Text}; Uid={uidTextBox.Text}; " +
                $"Pwd={passwordTextBox.Text}";

            dataBase.DataProvider = new MySQLOpenConnection(connectionString);

            if (!dataBase.DataProvider.DataBaseConnection)
            {
                MessageBox.Show(Vars.MySQLOpenConnectionFailed);
                return;
            }
            dBStartForm.Close();
        }
    }
}
