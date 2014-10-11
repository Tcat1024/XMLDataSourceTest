using DuHisPic;
using Expression;
using iTextSharp.text;
using iTextSharp.text.pdf;
using log4net;
using QtDataTrace.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Text;

namespace QtDataTrace.Access
{
    public class SingleQtTable
    {
        public System.Data.DataTable ReadDatatable_OraDB(string strSQL)
        {
            System.Data.DataTable dt = new DataTable();
            OleDbConnection Conn = new OleDbConnection();
            System.Data.OleDb.OleDbDataAdapter Da = new OleDbDataAdapter();

            Conn = new OleDbConnection(ConnectionString.LYQ_DB);
            Conn.Open();
            Da = new OleDbDataAdapter(strSQL, Conn);
            Da.Fill(dt);

            Da.Dispose();
            Conn.Close();
            Conn.Dispose();

            return dt;
        }
    }
}