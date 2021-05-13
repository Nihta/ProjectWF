using System;
using System.Data;

namespace ProjectWF
{
    class DataTableHelpers
    {
        /// <summary>
        /// Thêm cột vào dataTable
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="colName"></param>
        /// <param name="type">
        /// System.String hoặc System.Int32
        /// </param>
        /// <param name="isAutoInc"></param>
        /// <param name="isReadOnly"></param>
        /// <param name="isUnique"></param>
        public static void AddCol(DataTable dataTable, string colName, string type = "System.String", bool isAutoInc = false, bool isReadOnly = false, bool isUnique = false)
        {
            DataColumn column = new DataColumn();

            column.DataType = Type.GetType(type);
            column.ColumnName = colName;
            column.AutoIncrement = isAutoInc;
            column.ReadOnly = isReadOnly;
            column.Unique = isUnique;

            dataTable.Columns.Add(column);
        }

        public static void RemoveRow(DataTable dataTable, string rowIDName, int rowIDValue)
        {
            for (int i = dataTable.Rows.Count - 1; i >= 0; i--)
            {
                DataRow row = dataTable.Rows[i];
                if (Convert.ToInt32(row[rowIDName].ToString()) == rowIDValue)
                {
                    row.Delete();
                    Console.WriteLine($"Remove item have id: {rowIDValue}");
                    return;
                }
            }
        }
    }
}
