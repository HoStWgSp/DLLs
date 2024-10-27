using System;
using System.Windows.Forms;

namespace UserData
{
    public class DataGridViewActions
    {
        /// <summary>
        /// Определяет порядковый номер строки в DataTable из DataGridView
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public static int DataTableRowId(DataGridView dataGridView, int rowIndex)
        {
            DataGridViewRow dataGridViewRow;
            if (rowIndex >= 0)
            {
                dataGridViewRow = dataGridView.Rows[rowIndex];
                if (dataGridViewRow.Cells[dataGridView.Columns[0].Name].Value != null)
                    return Convert.ToInt32(dataGridViewRow.Cells[dataGridView.Columns[0].Name].Value);
                else return 0;
            }
            else return 0;
        }

        /// <summary>
        /// Возвращает номер строки DataGridView
        /// </summary>
        /// <param name="dataGridView"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public static int DataGridViewIdDetection(DataGridView dataGridView, int rowIndex)
        {
            DataGridViewRow dataGridViewRow;
            if (rowIndex >= 0)
            {
                dataGridViewRow = dataGridView.Rows[rowIndex];
                if (dataGridViewRow.Cells[dataGridView.Columns[0].Name].Value != null)
                {
                    return Convert.ToInt32(dataGridViewRow.Cells[dataGridView.Columns[0].Name].Value);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
    }
}
