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
            // Открываем диалоговое окно выбора расположения файла
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // Фильтр для отображения файлов на выбор
            openFileDialog.Filter = "Files(*.mdf)|*.mdf|All files(*.*)|*.*";
            // Если закрываем окно принудительно, то выходим и возвращаемся к окну выбора БД
            if (openFileDialog.ShowDialog() == DialogResult.Cancel) return;

            // Подключаемся к БД в файле
            DataBase.DataProvider = new SQLOpenConnection(
                $"Data Source=(LocalDB)\\MSSQLLocalDB;" +
                $"AttachDbFilename={openFileDialog.FileName};" +
                $"Integrated Security=True"
                );

            // Если подключение не состоялос, выводим сообщение
            if (!DataBase.DataProvider.DataBaseConnection)  { MessageBox.Show(Vars.MDFConnectionFailed); return; }
            // Закрываем окно выбора файла с БД
            DataBase.Close();
        }
    }
}
