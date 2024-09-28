using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkWithDB.ConnectionString
{
    internal class MDFPanel : MainPanel
    {
        ConStrCreate conStrCreate;
        public MDFPanel(ConStrCreate conStrCreate)
        {
            this.conStrCreate = conStrCreate;
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
            string constr = $"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={openFileDialog.FileName};Integrated Security=True";

            SqlConnection sqlConnection = new SqlConnection(constr);
            try { sqlConnection.Open(); conStrCreate.ConStr = constr; }
            catch { MessageBox.Show("Убедитесь, что выбран правильный файл с базой данных."); return; }
            conStrCreate.Close();
        }
    }
}
