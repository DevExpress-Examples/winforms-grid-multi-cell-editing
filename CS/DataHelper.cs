using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace WindowsApplication1
{
    public class DataHelper
    {
        public static DataTable CreateTable(int RowCount)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("ID", typeof(string));
            tbl.Columns.Add("Number", typeof(string));
            tbl.Columns.Add("Test", typeof(string));
            for (int i = 0; i < RowCount; i++)
                tbl.Rows.Add(new object[] { String.Format("Name{0}", i), i, 3 - i, "Test" });
            return tbl;
        }
    }
}
