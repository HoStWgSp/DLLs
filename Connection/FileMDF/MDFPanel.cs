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
        private DB DataBase;
        public MDFPanel(DB dataBase)
        {
            DataBase = dataBase;
            button = new Button();
            button.Location = new Point(10, 10);
            button.Text = Vars.FileChoise;
            Controls.Add(button);

            button.Click += Button_Click;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Files(*.mdf)|*.mdf|All files(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) return;
            string connectionString = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={openFileDialog.FileName};Integrated Security=True";


            DataBase.DataProvider = new SQLOpenConnection(connectionString);

            if (!DataBase.DataProvider.DataBaseConnection)  { MessageBox.Show(Vars.MDFConnectionFailed); return; }

            DataBase.Close();
        }
    }
}
