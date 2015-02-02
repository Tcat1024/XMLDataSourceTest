using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace QtDataTrace.AnalyzeService
{
    public class ConnectionString
    {
        public static string LYQ_DB = "Provider=MSDAORA.1;Password=lyq;User ID=lyq;Data Source=lyq;Persist Security Info=True";
        //public static string strOraConn_210 = "Provider=MSDAORA;  user id=LYQ210; password=lyq210; data source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.9.20.10)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=LYQ210))); Persist Security Info=False;"; 
        public static string LYQ_OLEDB = "Provider=OraOLEDB.Oracle.1;Password=lyq;Persist Security Info=True;User ID=lyq;Data Source=lyq";
        public static string LYQ_HISTORAIN = "Password=Lysteel2013;Persist Security Info=True;User ID=sa;Data Source=10.8.6.21";
    }
}
