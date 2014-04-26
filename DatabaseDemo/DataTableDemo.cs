using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DatabaseDemo
{
    public class DataTableDemo
    {
        /// <summary>
        /// 测试入口
        /// </summary>
        public void ShowTest() {
            DataTable dataTable =InititalDataRows();
            ShowDataTable(dataTable);
        }
        public DataTable GetDataTable()
        {
            DataTable datatable = new DataTable("Student");
            datatable.Columns.Add(new DataColumn("Id", typeof(int)));
            datatable.Columns.Add(new DataColumn("Name", typeof(string)));
            datatable.Columns.Add(new DataColumn("Class", typeof(string)));

            return datatable;
        }
        public DataTable InititalDataRows()
        {
            DataTable datatable = GetDataTable();
            DataRow datarow = datatable.NewRow();
            datarow["Id"] = 1;
            datarow["Name"] = "陈福佑";
            datarow["Class"] = "三年1班";
            datatable.Rows.Add(datarow.ItemArray);

            datarow["Id"] = 2;
            datarow["Name"] = "丁洁兰";
            datarow["Class"] = "三年1班";
            datatable.Rows.Add(datarow.ItemArray);

            return datatable;
        }

        public void ShowDataTable(DataTable table)
        {
            Console.WriteLine("TableName:{0}", table.TableName);
            Console.WriteLine("============================");
            string columns = string.Empty;
            for (int i = 0; i < table.Columns.Count; i++)
            {
                columns += table.Columns[i].ColumnName + "||";
            }
            Console.WriteLine(columns.Trim(new char[] { '|' }));
            Console.WriteLine("============================");
            foreach (DataRow item in table.Rows)
            {
                columns = string.Empty;
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    columns += item[i].ToString() + "||";
                }
                Console.WriteLine(columns.Trim(new char[] { '|' }));

            }

        }
    }
}
