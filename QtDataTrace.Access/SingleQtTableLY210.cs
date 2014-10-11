using QtDataTrace.Interfaces;
using System; 
using System.Collections.Generic; 
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq; 
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using DuHisPic;

namespace QtDataTrace.Access
{
    public class SingleQtTableLY210
    {
        SingleQtTable sqt = new SingleQtTable();
        public List<String> GetSteelGradeList()
        {//从bof_heat中获取钢种列表信息
            List<String> list = new List<string>();

            string strSQL = "SELECT DISTINCT steel_grade FROM SM_bof_heat WHERE steel_grade IS NOT NULL ORDER BY steel_grade";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);
            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                string str = dt.Rows[RowIndex][0].ToString().Trim();
                if (str.Length > 0) list.Add(str);
            }
            return list;
        }
    }
}