using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace WorkWithUser.UserForms
{
    internal class UsersList : DataGridView
    {
        User user;
        DataGridViewTextBoxColumn id_column, name_column,
            lastname_column, email_column, phone_column, group_column;
        StripMenu stripMenu;
        int rowId;
        public UsersList(User user)
        {
            this.user = user;

            //Dock = DockStyle.Fill;
            Name = "dataGridView";
            DataSource = ReadValidUsers();
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ReadOnly = true;
            RowHeadersVisible = false;
            Size = new Size(800, 201);
            MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width - 200, Screen.PrimaryScreen.WorkingArea.Height - 100);
            
            ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Bold);

            user.userListForm.ClientSize = new Size(Width, Height + 40);

            id_column = new DataGridViewTextBoxColumn()
            {
                Name = "Id",
                HeaderText = "Id",
                MinimumWidth = 10,
                Width = 10
            };
            name_column = new DataGridViewTextBoxColumn()
            {
                Name = "name_column",
                HeaderText = Vars.Name,
                MinimumWidth = 10,
                Width = 10
            };
            lastname_column = new DataGridViewTextBoxColumn()
            {
                Name = "lastname_column",
                HeaderText = Vars.LastName,
                MinimumWidth = 10,
                Width = 10
            };
            email_column = new DataGridViewTextBoxColumn()
            {
                Name = "email_column",
                HeaderText = Vars.EMail,
                MinimumWidth = 10,
                Width = 10
            };
            phone_column = new DataGridViewTextBoxColumn()
            {
                Name = "phone_column",
                HeaderText = Vars.Phone,
                MinimumWidth = 10,
                Width = 160
            };
            group_column = new DataGridViewTextBoxColumn()
            {
                Name = "group_column",
                HeaderText = Vars.Group,
                MinimumWidth = 10,
                Width = 10
            };

            DataGridViewTextBoxColumnAdjast(id_column, "Id", 1);
            DataGridViewTextBoxColumnAdjast(name_column, Vars.UserName, 2);
            DataGridViewTextBoxColumnAdjast(lastname_column, Vars.UserLastName, 3);
            DataGridViewTextBoxColumnAdjast(email_column, Vars.UserEMail, 4);
            DataGridViewTextBoxColumnAdjast(phone_column, Vars.UserPhone, 5);
            DataGridViewTextBoxColumnAdjast(group_column, Vars.UserGroup, 6);

            // Добавляем колонки в DataGridView
            Columns.AddRange(new DataGridViewColumn[] {
                id_column, name_column, lastname_column,
                email_column, phone_column, group_column });

            // Настраиваем колонки
            AutoGenerateColumns = false;
            AllowUserToAddRows = false;
            Columns["Id"].Visible = false;
            Columns["name_column"].Visible = true;
            Columns["lastname_column"].Visible = true;
            Columns["email_column"].Visible = true;
            Columns["phone_column"].Visible = true;
            Columns["group_column"].Visible = true;

            // Добавляем всплывающее меню к таблице
            stripMenu = new StripMenu(Vars.Add, Vars.Change, Vars.Delete);
            ContextMenuStrip = stripMenu;

            MouseDown += DataGridView_MouseDown;
            CellMouseDown += DataGridView_CellMouseDown;
            CellDoubleClick += DataGridView_CellDoubleClick;

            stripMenu.Items[Vars.Add].Click += Add_Click;
            stripMenu.Items[Vars.Change].Click += Change_Click;
            stripMenu.Items[Vars.Delete].Click += Delete_Click;
        }


        private void Add_Click(object sender, EventArgs e)
        {
            user.AddNewUser();
            DataSource = null;
            DataSource = ReadValidUsers();
        }
        private void Change_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void Delete_Click(object sender, EventArgs e)
        {
            UsersTableActions.RemoveTableRow(user.sqlConnection, rowId);
            DataSource = null;
            DataSource = ReadValidUsers();
        }


        private void DataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) return;
            stripMenu.Items[Vars.Delete].Visible = false;
        }
        private void DataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { return; }
            stripMenu.Items[Vars.Delete].Visible = true;
            rowId = DataGridViewActions.DataTableRowId(this, e.RowIndex);
        }
        private void DataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Узнаем номер строки в таблице
            rowId = DataGridViewActions.DataTableRowId(this, e.RowIndex);

            //пишем Form для добавления и изменения данных об авто
            // Создаем объект окна для добавления новой части в ВОМ
            // и передаем в качестве аргумента новую строку для заполнения



            // Обновляем DataSource
            DataSource = null;
            DataSource = RequestToSQL.ExecuteReaderToDataTable(user.sqlConnection,
                    $"select * from {Vars.TableName}");
        }

        private DataTable TableForDataGridView()
        {
            ///написать это
            return new DataTable();
        }
        private void DataGridViewTextBoxColumnAdjast(DataGridViewTextBoxColumn columnName, string PropertyName, int DispIndex)
        {
            columnName.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            columnName.DataPropertyName = PropertyName;
            columnName.DisplayIndex = DispIndex;
        }
        internal DataTable ReadValidUsers()
        {
            user.UsersData = RequestToSQL.ExecuteReaderToDataTable(user.sqlConnection,
                    $"select * from {Vars.TableName}");
            DataTable table = user.UsersData.Clone();
            foreach (DataRow dataRow in user.UsersData.Rows)
            {
                if (dataRow[Vars.UserName].ToString() != null && dataRow[Vars.UserName].ToString() != "")
                {
                    DataRow row = table.NewRow();
                    row["Id"] = dataRow["Id"];
                    row[Vars.UserName] = dataRow[Vars.UserName];
                    row[Vars.UserLastName] = dataRow[Vars.UserLastName];
                    row[Vars.UserEMail] = dataRow[Vars.UserEMail];
                    row[Vars.UserPhone] = dataRow[Vars.UserPhone];
                    row[Vars.UserGroup] = dataRow[Vars.UserGroup];
                    table.Rows.Add(row);
                }
            }
            return table;
        }
    }
}