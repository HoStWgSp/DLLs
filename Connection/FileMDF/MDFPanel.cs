using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBase.Connection.FileMDF
{
    internal class MDFPanel : MainPanel
    {
        DataBase dataBase;
        public MDFPanel(DataBase dataBase)
        {
            this.dataBase = dataBase;
            button = new Button();
            button.Location = new Point(10, 10);
            button.Text = "Выберите файл";
            Controls.Add(button);

            button.Click += Button_Click;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Files(*.mdf)|*.mdf|All files(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) return;
            dataBase.ConStr = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={openFileDialog.FileName};Integrated Security=True";


            OpenConnection openConnection = new OpenConnection(dataBase);

            if (dataBase.SqlConnection == null)
            {
                MessageBox.Show("Невозможно подключиться к базе данных. Проверьте правильно ли вы выбрали файл.");
                return;
            }

            dataBase.Close();
        }
    }
}
