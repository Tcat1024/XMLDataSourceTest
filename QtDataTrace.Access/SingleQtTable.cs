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
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        //����ѡ����¯��
        private static string SelectedHeatID="";
 
        //����Pdf�ļ�ʱҪ�õ���������
        public iTextSharp.text.Font FontHei;
        public iTextSharp.text.Font FontSong;
        public iTextSharp.text.Font FontKai;
        //Ĭ�ϵı�����ɫ
        public iTextSharp.text.Color color_Name = new iTextSharp.text.Color(System.Drawing.Color.LightGray);
        public iTextSharp.text.Color color_Value = new iTextSharp.text.Color(System.Drawing.Color.White);
        
        public DataSet GetWorkshopInfo()
        {
            DataSet data = new DataSet();

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                string sql;
                OleDbDataAdapter adapter;

                sql = "SELECT * FROM WORKSHOP_CODE";
                adapter = new OleDbDataAdapter(sql, connection);
                adapter.Fill(data, "WORKSHOP");
            }
            catch (Exception ex)
            {
                throw ex;                
            }
            finally
            {
                connection.Close();
            }

            return data;
        }

        public void SaveWorkshopInfo(DataSet data)
        {
            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                string sql = "SELECT * FROM WORKSHOP_CODE WHERE 1 = 0";
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection);
                OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
                adapter.AcceptChangesDuringFill = false; // Important
                adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                adapter.Update(data.Tables["WORKSHOP"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        public DataSet GetEquipmentInfo()
        {
            DataSet data = new DataSet();

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                string sql;
                OleDbDataAdapter adapter;

                sql = "SELECT * FROM WORKSHOP_CODE";
                adapter = new OleDbDataAdapter(sql, connection);
                adapter.Fill(data, "WORKSHOP");

                sql = "select * from process_code";
                adapter = new OleDbDataAdapter(sql, connection);
                adapter.Fill(data, "PROCESS");

                sql = "select * from equipment_code";
                adapter = new OleDbDataAdapter(sql, connection);
                adapter.Fill(data, "EQUIPMENT");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return data;
        }

        public DataSet GetProcessTimeGetFunction()
        {
            DataSet data = new DataSet();

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            try
            {
                connection.Open();

                string sql = "SELECT * FROM PROCESS_TIMEGET_FUNCTION";

                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection);
                adapter.Fill(data, "PROCESS_TIMEGET_FUNCTION");
            }
            catch (OleDbException ex)
            {
                Logger.Error(ex.Message);
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return data;
        }
        
        public List<HotCoilInfo> GetHotCoilList(string where)
       {
            List<HotCoilInfo> coils = new List<HotCoilInfo>();

            string sql = "SELECT TRIM(COIL_ID), TRIM(SLAB_ID), ROLLED_TIME, STEELGRADE, THK_MEAN, WID_MEAN, COIL_LEN, THK_PCNT_ON, WID_PCNT_ON,FDT_PCNT_ON, CT_PCNT_ON, PROF_PCNT_ON, WDG_PCNT_ON FROM HRM_L2_COILREPORTS " + where;

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            connection.Open();

            OleDbCommand command = new OleDbCommand(sql, connection);

            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                HotCoilInfo info = new HotCoilInfo();
                info.CoilId = reader.IsDBNull(0) ? "" : reader.GetString(0);
                info.SlabId = reader.GetString(1);
                info.RolledTime = reader.GetDateTime(2);
                info.SteelGrade = reader.IsDBNull(3) ? "" : reader.GetString(3);
                info.Thickness = System.Convert.ToDouble(reader.GetDecimal(4));
                info.Width = System.Convert.ToDouble(reader.GetDecimal(5));
                info.Length = System.Convert.ToDouble(reader.GetDecimal(6));
                info.ThkPcntOn = System.Convert.ToDouble(reader.GetDecimal(7));
                info.WidPcntOn = System.Convert.ToDouble(reader.GetDecimal(8));
                info.FDTPcntOn = System.Convert.ToDouble(reader.GetDecimal(9));
                info.CTPcntOn = System.Convert.ToDouble(reader.GetDecimal(10));
                info.ProfPcntOn = System.Convert.ToDouble(reader.GetDecimal(11));
                info.WdgPcntOn = System.Convert.ToDouble(reader.GetDecimal(12));               
                coils.Add(info);
            }

            reader.Close();

            connection.Close();

            return coils;
        }

        public List<HotCoilInfoVama> GetHotCoilListVama(string where)
        {
            List<HotCoilInfoVama> coils = new List<HotCoilInfoVama>();

            string sql = "SELECT TRIM(COIL_ID),SLAB_ID,ROLLEDTIME,TRIM(STEELGRADE),THK,WIDTH,LEN,THKH,THKB,THKT,WIDH,WIDB,WIDT,THKOVER,DOT,FDTH,FDTB,FDTT,CTH,CTB,CTT,CONV,WDG,FLAT,RTIME FROM COIL_JUDGE_VAMA " + where;

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            connection.Open();

            OleDbCommand command = new OleDbCommand(sql, connection);

            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                HotCoilInfoVama info = new HotCoilInfoVama();
                info.CoilId = reader.IsDBNull(0) ? "" : reader.GetString(0);
                info.SlabId = reader.IsDBNull(1) ? "" : reader.GetString(1);
                info.RolledTime = reader.GetDateTime(2);
                info.SteelGrade = reader.IsDBNull(3) ? "" : reader.GetString(3);
                info.Thk = System.Convert.ToDouble(reader.GetDecimal(4));
                info.Width = System.Convert.ToDouble(reader.GetDecimal(5));
                info.Len = System.Convert.ToDouble(reader.GetDecimal(6));
                info.ThkH = reader.IsDBNull(7) ? "" : reader.GetString(7);
                info.ThkB = reader.IsDBNull(8) ? "" : reader.GetString(8);
                info.ThkT = reader.IsDBNull(9) ? "" : reader.GetString(9);
                info.WidH = reader.IsDBNull(10) ? "" : reader.GetString(10);
                info.WidB = reader.IsDBNull(11) ? "" : reader.GetString(11);
                info.WidT = reader.IsDBNull(12) ? "" : reader.GetString(12);
                info.ThkOver = reader.IsDBNull(13) ? "" : reader.GetString(13);
                info.Dot = reader.IsDBNull(14) ? "" : reader.GetString(14);
                info.FdtH = reader.IsDBNull(15) ? "" : reader.GetString(15);
                info.FdtB = reader.IsDBNull(16) ? "" : reader.GetString(16);
                info.FdtT = reader.IsDBNull(17) ? "" : reader.GetString(17);
                info.CtH = reader.IsDBNull(18) ? "" : reader.GetString(18);
                info.CtB = reader.IsDBNull(19) ? "" : reader.GetString(19);
                info.CtT = reader.IsDBNull(20) ? "" : reader.GetString(20);
                info.Conv = reader.IsDBNull(21) ? "" : reader.GetString(21);
                info.Wdg = reader.IsDBNull(22) ? "" : reader.GetString(22);
                info.Flat = reader.IsDBNull(23) ? "" : reader.GetString(23);
                info.RTime = reader.GetDateTime(24);

                coils.Add(info);
            }

            reader.Close();

            connection.Close();

            return coils;
        }

        public List<HotCoilInfoVamaCal> GetHotCoilListVamaCal(string where)
        {
            List<HotCoilInfoVamaCal> coils = new List<HotCoilInfoVamaCal>();

            //string sql = "SELECT GRADE,TOTAL_SUM,FAILED_SUM,FAILED_PCNT FROM COIL_JUDGE_VAMA_CAL " + where;
            string sql = "select b.grade, count(*) total_sum, " +
            "count(case " +
               "when b.thk_head = 'n' or b.thk_midd = 'n' or b.thk_tail = 'n' or " +
                    "b.wid_head = 'n' or b.wid_midd = 'n' or b.wid_tail = 'n' or " +
                    "b.fdt_head = 'n' or b.fdt_midd = 'n' or b.fdt_tail = 'n' or " +
                    "b.ct_head = 'n' or b.ct_midd = 'n' or b.ct_tail = 'n' or " +
                    "b.tudu = 'n' or b.xiexing = 'n' or b.flat = 'n' then 1 end) failed_sum, " +
            "trunc(count(case " +
                     "when b.thk_head = 'n' or b.thk_midd = 'n' or b.thk_tail = 'n' or " +
                          "b.wid_head = 'n' or b.wid_midd = 'n' or b.wid_tail = 'n' or " +
                          "b.fdt_head = 'n' or b.fdt_midd = 'n' or b.fdt_tail = 'n' or " +
                          "b.ct_head = 'n' or b.ct_midd = 'n' or b.ct_tail = 'n' or " +
                          "b.tudu = 'n' or b.xiexing = 'n' or b.flat = 'n' then 1 end) / count(*) * 100,2) || '%' failed_pcnt" +
           " from vama_result b " +
           where +
          " GROUP BY rollup(b.grade) ";

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            connection.Open();

            OleDbCommand command = new OleDbCommand(sql, connection);

            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                HotCoilInfoVamaCal info = new HotCoilInfoVamaCal();
                info.sGrade = reader.IsDBNull(0) ? "" : reader.GetString(0);                
                info.Total_sum = reader.IsDBNull(1) ? "" : Convert.ToString(reader["TOTAL_SUM"]);
                info.Failed_sum = reader.IsDBNull(2) ? "" : Convert.ToString(reader["FAILED_SUM"]);
                info.Failed_pcnt = reader.IsDBNull(3) ? "" : Convert.ToString(reader["FAILED_PCNT"]);                

                coils.Add(info);
            }

            reader.Close();

            connection.Close();

            return coils;
        }
        
        
        public void SetSelectedHeatID(string strSelectedHeatID) 
        {
            SelectedHeatID = strSelectedHeatID;
        }
        public string GetSelectedHeatID()
        {
            return SelectedHeatID;
        }

        public HotCoilInfo GetHotCoilInfo(string matId)
        {
            string sql = string.Format("SELECT TRIM(COIL_ID), TRIM(SLAB_ID), ROLLED_TIME, STEELGRADE, THK_MEAN , THK_MAX, THK_MIN, THK_PCNT_ON, THK_PCNT_OVER, THK_PCNT_UNDER, THK_STD, WID_MEAN, WID_MAX, WID_MIN, WID_PCNT_ON, WID_PCNT_OVER, WID_PCNT_UNDER,FDT_MEAN, FDT_MAX, FDT_MIN, FDT_PCNT_ON, FDT_PCNT_OVER, FDT_PCNT_UNDER,CT_MEAN, CT_MAX, CT_MIN, CT_PCNT_ON, CT_PCNT_OVER, CT_PCNT_UNDER,PROF_MEAN, PROF_MAX, PROF_MIN, PROF_PCNT_ON, PROF_PCNT_OVER, PROF_PCNT_UNDER,WDG_MEAN, WDG_MAX, WDG_MIN, WDG_PCNT_ON, WDG_PCNT_OVER, WDG_PCNT_UNDER, COIL_LEN, FLAT_MEAN, FLAT_MAX, FLAT_MIN, FLAT_PCT_ON, FLAT_PCT_OVER, FLAT_PCT_UNDER FROM HRM_L2_COILREPORTS where slab_id = '{0}' or coil_id = '{0}'", matId);

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            connection.Open();

            OleDbCommand command = new OleDbCommand(sql, connection);

            OleDbDataReader reader = command.ExecuteReader();

            HotCoilInfo info = null;

            if (reader.Read())
            {
                info = new HotCoilInfo();

                info.CoilId = reader.IsDBNull(0) ? "" : reader.GetString(0);
                info.SlabId = reader.GetString(1);
                info.RolledTime = reader.GetDateTime(2);
                info.SteelGrade = reader.IsDBNull(3) ? "" : reader.GetString(3);               
                info.ThkMean = System.Convert.ToDouble(reader.GetDecimal(4));
                info.ThkMax = System.Convert.ToDouble(reader.GetDecimal(5));
                info.ThkMin = System.Convert.ToDouble(reader.GetDecimal(6));
                info.ThkPcntOn = System.Convert.ToDouble(reader.GetDecimal(7));
                info.ThkPcntOver = System.Convert.ToDouble(reader.GetDecimal(8));
                info.ThkPcntUnder = System.Convert.ToDouble(reader.GetDecimal(9));
                info.ThkStd = System.Convert.ToDouble(reader.GetDecimal(10));
                info.WidMean = System.Convert.ToDouble(reader.GetDecimal(11));
                info.WidMax = System.Convert.ToDouble(reader.GetDecimal(12));
                info.WidMin = System.Convert.ToDouble(reader.GetDecimal(13));
                info.WidPcntOn = System.Convert.ToDouble(reader.GetDecimal(14));
                info.WidPcntOver = System.Convert.ToDouble(reader.GetDecimal(15));
                info.WidPcntUnder = System.Convert.ToDouble(reader.GetDecimal(16));
                info.FDTMean = System.Convert.ToDouble(reader.GetDecimal(17));
                info.FDTMax = System.Convert.ToDouble(reader.GetDecimal(18));
                info.FDTMin = System.Convert.ToDouble(reader.GetDecimal(19));
                info.FDTPcntOn = System.Convert.ToDouble(reader.GetDecimal(20));
                info.FDTPcntOver = System.Convert.ToDouble(reader.GetDecimal(21));
                info.FDTPcntUnder = System.Convert.ToDouble(reader.GetDecimal(22));
                info.CTMean = System.Convert.ToDouble(reader.GetDecimal(23));
                info.CTMax = System.Convert.ToDouble(reader.GetDecimal(24));
                info.CTMin = System.Convert.ToDouble(reader.GetDecimal(25));
                info.CTPcntOn = System.Convert.ToDouble(reader.GetDecimal(26));
                info.CTPcntOver = System.Convert.ToDouble(reader.GetDecimal(27));
                info.CTPcntUnder = System.Convert.ToDouble(reader.GetDecimal(28));
                info.ProfMean = System.Convert.ToDouble(reader.GetDecimal(29));
                info.ProfMax = System.Convert.ToDouble(reader.GetDecimal(30));
                info.ProfMin = System.Convert.ToDouble(reader.GetDecimal(31));
                info.ProfPcntOn = System.Convert.ToDouble(reader.GetDecimal(32));
                info.ProfPcntOver = System.Convert.ToDouble(reader.GetDecimal(33));
                info.ProfPcntUnder = System.Convert.ToDouble(reader.GetDecimal(34));
                info.WdgMean = System.Convert.ToDouble(reader.GetDecimal(35));
                info.WdgMax = System.Convert.ToDouble(reader.GetDecimal(36));
                info.WdgMin = System.Convert.ToDouble(reader.GetDecimal(37));
                info.WdgPcntOn = System.Convert.ToDouble(reader.GetDecimal(38));
                info.WdgPcntOver = System.Convert.ToDouble(reader.GetDecimal(39));
                info.WdgPcntUnder = System.Convert.ToDouble(reader.GetDecimal(40));
                info.Length = System.Convert.ToDouble(reader.GetDecimal(41));
                info.FlatMean = System.Convert.ToDouble(reader.GetDecimal(42));
                info.FlatMax = System.Convert.ToDouble(reader.GetDecimal(43));
                info.FlatMin = System.Convert.ToDouble(reader.GetDecimal(44));
                info.FlatPcntOn = System.Convert.ToDouble(reader.GetDecimal(45));
                info.FlatPcntOver = System.Convert.ToDouble(reader.GetDecimal(46));
                info.FlatPcntUnder = System.Convert.ToDouble(reader.GetDecimal(47));


            }

            reader.Close();
            connection.Close();

            if (info == null)
                throw new Exception(string.Format("����û���֣� MatId={0}", matId));

            return info;
        }

        public HotCoilInfo GetHotCoilPdi(string matId)
        {
            string sql = string.Format("SELECT TRIM(COIL_ID), TRIM(SLAB_ID),F_FMDELTEMPTARG,F_TEMPTOLPOS,F_TEMPTOLNEG,F_COILTEMPTARG,F_CTTOLPOS,F_CTTOLNEG FROM HRM_L2_SETVALUE where slab_id = '{0}' or coil_id = '{0}'", matId);

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            connection.Open();

            OleDbCommand command = new OleDbCommand(sql, connection);

            OleDbDataReader reader = command.ExecuteReader();

            HotCoilInfo info = null;

            if (reader.Read())
            {
                info = new HotCoilInfo();

                info.CoilId = reader.IsDBNull(0) ? "" : reader.GetString(0);
                info.SlabId = reader.GetString(1);
                
                info.FdtTrg = System.Convert.ToDouble(reader.GetDecimal(2));
                info.FdtPos = System.Convert.ToDouble(reader.GetDecimal(3));
                info.FdtNeg = System.Convert.ToDouble(reader.GetDecimal(4));
               
                info.CtTrg = System.Convert.ToDouble(reader.GetDecimal(5));
                info.CtPos = System.Convert.ToDouble(reader.GetDecimal(6));
                info.CtNeg = System.Convert.ToDouble(reader.GetDecimal(7));
                        

               

            }

            reader.Close();
            connection.Close();

            if (info == null)
                throw new Exception(string.Format("����û���֣� MatId={0}", matId));

            return info;
        }

        public DataSet GetBofHeatList(QueryArgs cond)
        {
            string whereClause = "";

            if (cond.TimeFlag)
            {
                if (whereClause == "")
                    whereClause = "WHERE ";
                else
                    whereClause += " AND ";

                whereClause = string.Format("where start_time = to_date({0}, 'yyyymm') and stop_time = to_date({1}, 'yyyymm')", cond.StartTime, cond.StopTime);
            }

            string sql = "SELECT * FROM BOF_L2_REPORTS " + whereClause;

            OleDbConnection connection = new OleDbConnection(ConnectionString.LYQ_DB);

            OleDbDataAdapter adapt = new OleDbDataAdapter(sql, connection);

            DataSet data = new DataSet();

            adapt.Fill(data);

            connection.Close();

            return data;
        }

      

        public System.Data.DataTable ReadDatatable_OraDB(string  strSQL)
        { 
            System.Data.DataTable dt=new DataTable();
            OleDbConnection Conn= new OleDbConnection();
            System.Data.OleDb.OleDbDataAdapter Da=new OleDbDataAdapter();
            
            Conn = new OleDbConnection(ConnectionString.LYQ_DB);
            Conn.Open();
            Da = new OleDbDataAdapter(strSQL, Conn);
            Da.Fill(dt);
            
            Da.Dispose();
            Conn.Close();
            Conn.Dispose();

            return dt;
        }

        /// <summary>
        /// ��������趨������֮��ļ���Ƿ����ָ�������������������������
        /// </summary>
        /// <param name="StartDate"></param>
        /// <param name="EndString"></param>
        /// <param name="Days"></param>
        public void CheckDateRange(string StartDate,ref string EndDate, double Days)
        {
            DateTime dateStartDate = Convert.ToDateTime(StartDate);
            DateTime dateEndDate = Convert.ToDateTime(EndDate);

            TimeSpan ts = dateEndDate - dateStartDate;
            if (ts.Days > Days)
            {
                dateEndDate = dateStartDate.AddDays(Days);
                EndDate = dateEndDate.Year.ToString() + "-"
                        + dateEndDate.Month.ToString() + "-" 
                        + dateEndDate.Day.ToString();
            }

        }

        //ʹ�����ݿ�ķ������в�ѯ
        public System.Data.DataTable ReadDatatable_HisDB(string[] tags, DateTime StartDateTime, DateTime EndDateTime, int wwCycleCount=1000)
        {

            //History���ݿ��������ȡ����            
            System.Data.SqlClient.SqlConnection SqlClientConn = new System.Data.SqlClient.SqlConnection();

            string strConn = "Integrated Security=False;"
                  + "Persist Security Info=False;"
                  + "User ID=sa;"
                  + "PassWord=Lysteel2013;"
                  + "Initial Catalog='';"
                  + "Data Source=10.8.6.21;"
                  + "Initial File Name='';";

            //���������ַ���
            SqlClientConn.ConnectionString = strConn;

            //��
            SqlClientConn.Open();

            string strTags = "";
            for (int I = 0; I <= tags.GetUpperBound(0); I++)
            {
                if ((null != tags[I]) && (tags[I].Length > 0))
                {
                    strTags = strTags + "[" + tags[I] + "]=[" + tags[I] + "],";
                }
            }
            //ȥ�����һ�� ","
            strTags = strTags.Substring(0, strTags.Length - 1);

            string strSQL = "SET QUOTED_IDENTIFIER OFF"
                         + " SELECT * FROM OPENQUERY(INSQL, " + (char)34
                         + "SELECT DateTime = convert(nvarchar, DateTime, 21),"
                         + strTags
                         + " FROM WideHistory"
                         + " WHERE wwRetrievalMode = 'Cyclic'"
                         + " AND wwCycleCount = " + wwCycleCount.ToString()
                         + " AND wwVersion = 'Latest'"
                         + " AND DateTime >= '" + StartDateTime + "'"
                         + " AND DateTime <= '" + EndDateTime + "'" + (char)34 + ")"
                         + " ORDER BY DateTime ASC";

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
            System.Data.DataTable dt = new System.Data.DataTable();

            try
            {
                da.SelectCommand = new System.Data.SqlClient.SqlCommand(strSQL, SqlClientConn);
                da.Fill(dt);

                da.Dispose();
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }

            SqlClientConn.Dispose();
             
            return dt;
        }


         //ʹ�����ݿ�ķ������в�ѯ
        public System.Data.DataTable ReadDatatable_HisDB( string strSQL)
        {

            //History���ݿ��������ȡ����            
            System.Data.SqlClient.SqlConnection SqlClientConn = new System.Data.SqlClient.SqlConnection();

            string strConn = "Integrated Security=False;"
                  + "Persist Security Info=False;"
                  + "User ID=sa;"
                  + "PassWord=Lysteel2013;"
                  + "Initial Catalog='';"
                  + "Data Source=10.8.6.21;"
                  + "Initial File Name='';";

            //���������ַ���
            SqlClientConn.ConnectionString = strConn;

            //��
            SqlClientConn.Open();

            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
            System.Data.DataTable dt = new System.Data.DataTable();

            try
            {
                da.SelectCommand = new System.Data.SqlClient.SqlCommand(strSQL, SqlClientConn);
                da.Fill(dt);

                da.Dispose();
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }

            da.Dispose();
            SqlClientConn.Dispose();
             
            return dt;
        }

        //ʹ�����ݿ�ķ������в�ѯ
        public System.Data.DataTable SearchTimeFromCastingLength_HisDB(string CCM,string Strand, double Castingength,string StartTime,string EndTime)
        {

            //History���ݿ��������ȡ����            
            System.Data.SqlClient.SqlConnection SqlClientConn = new System.Data.SqlClient.SqlConnection();

            string strConn = "Integrated Security=False;"
                  + "Persist Security Info=False;"
                  + "User ID=sa;"
                  + "PassWord=Lysteel2013;"
                  + "Initial Catalog='';"
                  + "Data Source=10.8.6.21;"
                  + "Initial File Name='';";

            //���������ַ���
            SqlClientConn.ConnectionString = strConn;

            //��
            SqlClientConn.Open();

            string strSQL = " SELECT DateTime, Value"
                          + " FROM History"
                           + " WHERE History.TagName IN ('LYQ210.CCM" + CCM + "S" + Strand + ".CastingLength')"
                           + " AND wwRetrievalMode = 'Cyclic'"
                           + " AND wwCycleCount = 1000"
                           + " AND wwVersion = 'Latest'"
                           + " AND Value>" + Castingength.ToString()
                           + " AND Value<" +( Castingength+100).ToString ()
                           + " AND DateTime >= '"+ StartTime +"'"
                           + " AND DateTime <= '" + EndTime + "'";


            System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
            System.Data.DataTable dt = new System.Data.DataTable();

            try
            {
                da.SelectCommand = new System.Data.SqlClient.SqlCommand(strSQL, SqlClientConn);
                da.Fill(dt);

                da.Dispose();
            }
            catch (Exception ex)
            {
                string str = ex.ToString();
            }

            SqlClientConn.Dispose();

            return dt;
        }

        //����Pdf�ļ����Լ�Ҫ������html�ļ�
        public string ShowPDFReport(string HeatID, string ProcessList)
        {
            //��⴫��������Ϣ��
            string[] PL = ProcessList.Split(new char[]{' '});

            //��ȡ�ͻ��˵�IP��ַ
            string ClientIP = PL[0];
            
            //����pdf�ļ���
            string pdfFileName = "Report_" + ClientIP + "_" + HeatID + ".pdf";
            string strHtmlFileName_Local = "C:\\inetpub\\wwwroot\\LYQ\\PdfReport\\" + pdfFileName;
            string strHtmlFileName_WEB = "http://10.8.6.28/LYQ/PdfReport/" + pdfFileName;

            //������ļ��Ѿ����ڣ�����Ҫ��ɾ��
            if (System.IO.File.Exists(strHtmlFileName_Local)) System.IO.File.Delete(strHtmlFileName_Local);
 
            //��һ��������һ�� iTextSharp.text.Document�����ʵ����
            iTextSharp.text.Document pdfDocument = new iTextSharp.text.Document(PageSize.A4);

            pdfDocument.addTitle("����׷���ļ�");
            pdfDocument.addSubject("�����Ƽ���ѧ");
            pdfDocument.addKeywords("����׷��");
            pdfDocument.addAuthor("��Դ������˾");
            pdfDocument.addCreator("��Ϣ�Զ�������");
            pdfDocument.addProducer();
            pdfDocument.addCreationDate();
            pdfDocument.addHeader("�����Ƽ���ѧ����о�Ժ���޹�˾", "2014");

            //��������
            iTextSharp.text.pdf.BaseFont bfHei = iTextSharp.text.pdf.BaseFont.createFont(@"C:\Windows\Fonts\SIMHEI.TTF", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
            FontHei = new iTextSharp.text.Font(bfHei, 32,1);
            iTextSharp.text.pdf.BaseFont bfKai = iTextSharp.text.pdf.BaseFont.createFont(@"C:\Windows\Fonts\simkai.ttf", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
            FontKai = new iTextSharp.text.Font(bfKai, 32, 1);
            iTextSharp.text.pdf.BaseFont bfSun = iTextSharp.text.pdf.BaseFont.createFont(@"C:\Windows\Fonts\SIMSUN.TTC,1", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
            FontSong = new iTextSharp.text.Font(bfSun, 32,1);

            //�ڶ�����Ϊ��Document����һ��Writerʵ����
            iTextSharp.text.pdf.PdfWriter.getInstance(pdfDocument, new System.IO.FileStream(strHtmlFileName_Local, System.IO.FileMode.Create));

            //ҳü��ҳ��
            GetPdfReport_PageHeaderFooter(ref pdfDocument);
            
            //���������򿪵�ǰDocument
            pdfDocument.Open();

            //���Ĳ���д����Pdfҳ���ĵ� 
            SingleQtTableLY210 LY210 = new SingleQtTableLY210();
            SingleQtTableLY2250 sqtLY2250 = new SingleQtTableLY2250();
            SingleQtTableLYCRM sqtLYCRM = new SingleQtTableLYCRM();

            for (int intPL = 1; intPL <= PL.GetUpperBound(0); intPL++)
            {
                //����
                if ("FACE" == PL[intPL])   LY210.GetPdfReport_Face(HeatID, ref pdfDocument);                 

                //ұ���Ĵ�ӡ��Ϣ
                if ("MI" == PL[intPL].ToUpper())  LY210.GetReportPdf_MI(HeatID, ref pdfDocument);
                if ("KR" == PL[intPL].ToUpper())  LY210.GetReportPdf_KR(HeatID, ref pdfDocument);
                if ("BOF" == PL[intPL].ToUpper()) LY210.GetReportPdf_BOF(HeatID, ref pdfDocument); 
                if ("LF" == PL[intPL].ToUpper())  LY210.GetReportPdf_LF(HeatID, ref pdfDocument);
                if ("RH" == PL[intPL].ToUpper())  LY210.GetReportPdf_RH(HeatID, ref pdfDocument);
                if ("CC" == PL[intPL].ToUpper())  LY210.GetReportPdf_CC(HeatID, ref pdfDocument);

                //����
                if ("HF" == PL[intPL].ToUpper()) sqtLY2250.GetReportPdf_HF(HeatID, ref pdfDocument);
                if ("RM" == PL[intPL].ToUpper()) sqtLY2250.GetReportPdf_RM(HeatID, ref pdfDocument);
                if ("FR" == PL[intPL].ToUpper()) sqtLY2250.GetReportPdf_FR(HeatID, ref pdfDocument);                
                if ("CTC" == PL[intPL].ToUpper()) sqtLY2250.GetReportPdf_CTC(HeatID, ref pdfDocument);

                //����
                if ("AF" == PL[intPL].ToUpper()) sqtLYCRM.GetReportPdf_AF(HeatID, ref pdfDocument);
                if ("PL" == PL[intPL].ToUpper()) sqtLYCRM.GetReportPdf_PL(HeatID, ref pdfDocument);
                if ("CGL" == PL[intPL].ToUpper()) sqtLYCRM.GetReportPdf_CGL(HeatID, ref pdfDocument);    
                
            }
           
            //���岽���ر�Document
            pdfDocument.Close();


            //// ****** ����HTML�ļ� ****** //            
            //string[] strHtmlLine = new string[7];
            ////���� �� \" ��ʾֱ�Ӷ�˫����"������
            //strHtmlLine[0] = "<object classid=\"clsid:CA8A9780-280D-11CF-A24D-444553540000\" width=\"100%\" height=\"100%\" border=\"0\"> ";
            //strHtmlLine[1] = "<param name=\"_Version\" value=\"65539\"> ";
            //strHtmlLine[2] = "<param name=\"_ExtentX\" value=\"20108\"> ";
            //strHtmlLine[3] = "<param name=\"_ExtentY\" value=\"10866\"> ";
            //strHtmlLine[4] = "<param name=\"_StockProps\" value=\"0\"> ";
            //strHtmlLine[5] = "<param name=\"SRC\" value=\"Report_" + ClientIP + "_" + HeatID +".pdf\"> ";
            //strHtmlLine[6] = "</object> ";
            

            ////��10.6.8.28�ϵ�C:\inetpub\wwwroot\LYQ\bin�е���ЩDll��̬���ӿ�ֻ�Ǳ����ã�
            ////ʵ�ʵ�ִ��·��ΪC:\Windows\SysWOW64\inetsrv\�����·�����û�Ȩ�������ƣ����ܱ�ʹ��
            ////��ˣ�ʹ�þ���·��
            //string strHtmlFileName_Local = "C:\\inetpub\\wwwroot\\LYQ\\PdfReport\\Report_" + ClientIP + "_" + HeatID + ".html";

            ////web�û����ʵ�·��
            //string strHtmlFileName_WEB = "http://10.8.6.28/LYQ/PdfReport/Report_" + ClientIP + "_"+ HeatID+".html";
            ////������ļ��Ѿ����ڣ�����Ҫ��ɾ��
            //if (System.IO.File.Exists(strHtmlFileName_Local)) System.IO.File.Delete(strHtmlFileName_Local);
            //System.IO.File.WriteAllText(strHtmlFileName_Local, "");
            //System.IO.File.AppendAllLines(strHtmlFileName_Local, strHtmlLine);
            

            return strHtmlFileName_WEB;
          
        }


        //����Txt�ļ�
        public string ShowTXTReport(string ParaA, string txtFileName)
        {
                        
            //����txt�ļ���������ļ��Ѿ����ڣ�����Ҫ��ɾ��            
            string txtFilePathName = "C:\\inetpub\\wwwroot\\LYQ\\PdfReport\\" + txtFileName;
            string strHtmlFilePathName_WEB = "http://10.8.6.28/LYQ/PdfReport/" + txtFileName; 
           
            //���У���ɾ��
            if (System.IO.File.Exists(txtFilePathName)) System.IO.File.Delete(txtFilePathName);
            
            //���ɿյ�
            System.IO.File.WriteAllText(txtFilePathName, "");

            string[] ss = ParaA.Split(new char []{'*'});
            if (ss[0] == "MatPedigree")
            {                
                //����
                string[] strTitle= new string[1];
                strTitle[0] =  "�����1";
                strTitle[0] += "\t�����2";
                strTitle[0] += "\t������"  ;
                strTitle[0] += "\t�豸"  ;
                strTitle[0] += "\t����"  ;
                strTitle[0] += "\t��ʼ" ;
                strTitle[0] += "\t����";
                System.IO.File.AppendAllLines(  txtFilePathName, strTitle,Encoding.Default );

                
                //���������
                List<Mat_Pedigree> LST = GetMatPedigree(ss[1], ss[2]);
                string[] strTxtLine = new string[LST.Count];
                for (int I = 0; I < LST.Count; I++)
                {
                    strTxtLine[I] = LST[I].IN_MAT_ID1;
                    strTxtLine[I] += "\t" + LST[I].IN_MAT_ID2;
                    strTxtLine[I] += "\t" + LST[I].OUT_MAT_ID;
                    strTxtLine[I] += "\t" + LST[I].DEVICE_NO;
                    strTxtLine[I] += "\t" + LST[I].STEEL_GRADE;
                    strTxtLine[I] += "\t" + LST[I].START_TIME;
                    strTxtLine[I] += "\t" + LST[I].STOP_TIME;
                }
                System.IO.File.AppendAllLines(txtFilePathName, strTxtLine, Encoding.Default);
            }


            return strHtmlFilePathName_WEB;
           
        }

        public void GetPdfReport_PageHeaderFooter(ref iTextSharp.text.Document pdfDocument)
        {
            // ���ҳü           
            iTextSharp.text.Image jpeg = iTextSharp.text.Image.getInstance("C:\\inetpub\\wwwroot\\LYQ\\HLLG.jpg");
            jpeg.scaleAbsolute(44, 12);
            iTextSharp.text.Chunk chunk = new iTextSharp.text.Chunk(jpeg, 0, 0, false);
            Phrase para = new Phrase();
            para.Add(chunk);
            FontKai.Size = 10;
            para.Add(new Phrase(" ȫ���̹�����������׷��", FontKai));
            iTextSharp.text.HeaderFooter header = new iTextSharp.text.HeaderFooter(para, false);
            header.Border = iTextSharp.text.Rectangle.BOTTOM;
            header.BorderColor = new iTextSharp.text.Color(0, 0, 0);
            header.Alignment=  Element.ALIGN_LEFT;
            pdfDocument.Header = header;

            // ���ҳ��
            FontSong.Size = 10.5F;
            iTextSharp.text.HeaderFooter footer = new iTextSharp.text.HeaderFooter(new Phrase("ҳ��: ", FontSong), true);
            footer.Border = iTextSharp.text.Rectangle.TOP;
            footer.Alignment = Element.ALIGN_CENTER | Element.ALIGN_MIDDLE;
            pdfDocument.Footer = footer;
        }

        public void DrawstuDrawPicInfo2pdfDocument(string strTitle,ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo, ref iTextSharp.text.Document pdfDocument)
        {
            //��ͼ
            DuHisPic.ClsDuHisPic clsDuHisPic = new DuHisPic.ClsDuHisPic();
            clsDuHisPic.DrawImage(ref stuDrawPicInfo);

            //���Ƶ�pdf��
            iTextSharp.text.Image Img = iTextSharp.text.Image.getInstance(stuDrawPicInfo.BMP, System.Drawing.Imaging.ImageFormat.Jpeg);
            //����ͼƬ�Ĵ�С
            Img.Alignment = iTextSharp.text.Image.MIDDLE;
            float ImgWidth = pdfDocument.PageSize.Width - pdfDocument.LeftMargin - pdfDocument.RightMargin;
            Img.scaleAbsolute(ImgWidth, 150);
            pdfDocument.Add(Img);

            //ͼ����
            iTextSharp.text.pdf.BaseFont bfKai = iTextSharp.text.pdf.BaseFont.createFont(@"C:\Windows\Fonts\simkai.ttf", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font FontKai = new iTextSharp.text.Font( bfKai, 12, 1);            
            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph(strTitle, FontKai);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);
        }

        /// <summary>
        /// ��ȡĳ������ �� ���� �б�
        /// </summary>
        public List<string> GetSteelGrade(string WorkProcess,string StartTime,string StopTime)
        {
            List<string> LST = new List<string>();

            //�����ĸ�ʽΪ"210ת¯��-ת¯(LY210_BOF)"
            //���ΪBOF
            WorkProcess = WorkProcess.Substring(0, WorkProcess.Length - 1);
            string[] WP = WorkProcess.Split(new char[] { '(' });
            WorkProcess = WP[1];

            string strSQL= " SELECT DISTINCT steel_grade FROM MAT_PEDIGREE "
                         + " WHERE DEVICE_no LIKE '" + WorkProcess + "%' "
                         + " AND start_time>=to_date('" + StartTime + "','yyyy-mm-dd')"
                         + " AND start_time<=to_date('" + StopTime + "','yyyy-mm-dd')"
                         + " ORDER BY steel_grade";            

            //��������
            DataTable dt = ReadDatatable_OraDB(strSQL);

            //����д���б�
            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                LST.Add(dt.Rows[RowIndex]["steel_grade"].ToString());
            }

            return LST;
        }

        
        //��ȡ�����б�
        public List<Mat_Pedigree> GetMatList(string WorkProcess, string StartTime, string StopTime,string SteelGrade)
        {

            //���ʱ������÷�Χ�������Ϊ15�졣
            //ʱ�䷶Χ����̫����������̫��Ӱ��ϵͳ�ٶ�
            CheckDateRange(StartTime,ref StopTime, 15.0);
            
            List<Mat_Pedigree> LST = new List<Mat_Pedigree>();
            Mat_Pedigree lst;
            string str;


            //�����ĸ�ʽΪ"210ת¯��-ת¯(LY210_BOF)"
            //���ΪBOF
            WorkProcess = WorkProcess.Substring(0, WorkProcess.Length - 1);
            string[] WP = WorkProcess.Split(new char[] { '(' });
            WorkProcess = WP[1];

            string strSQL = " SELECT * FROM MAT_PEDIGREE "
                         + " WHERE DEVICE_no LIKE '" + WorkProcess + "%' "
                         + " AND start_time>=to_date('" + StartTime + "','yyyy-mm-dd')"
                         + " AND start_time<=to_date('" + StopTime + "','yyyy-mm-dd')";
            if (SteelGrade.Length > 0)
                strSQL = strSQL + " AND Steel_Grade='" + SteelGrade + "'";

            strSQL = strSQL + " ORDER BY start_time DESC";

            //��������
            DataTable dt = ReadDatatable_OraDB(strSQL);

            //����д���б�
            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                lst = new Mat_Pedigree();

                str = dt.Rows[RowIndex]["OUT_MAT_ID"].ToString(); if (str.Trim().Length > 0) lst.OUT_MAT_ID = str;
                str = dt.Rows[RowIndex]["IN_MAT_ID1"].ToString(); if (str.Trim().Length > 0) lst.IN_MAT_ID1 = str;
                str = dt.Rows[RowIndex]["IN_MAT_ID2"].ToString(); if (str.Trim().Length > 0) lst.IN_MAT_ID2 = str;
                str = dt.Rows[RowIndex]["DEVICE_NO"].ToString(); if (str.Trim().Length > 0) lst.DEVICE_NO = str;
                str = dt.Rows[RowIndex]["STEEL_GRADE"].ToString(); if (str.Trim().Length > 0) lst.STEEL_GRADE = str;
                str = dt.Rows[RowIndex]["ORDER_ID"].ToString(); if (str.Trim().Length > 0) lst.ORDER_ID = str;               
                str = dt.Rows[RowIndex]["START_TIME"].ToString(); if (str.Trim().Length > 0) lst.START_TIME =Convert.ToDateTime( str);
                str = dt.Rows[RowIndex]["STOP_TIME"].ToString(); if (str.Trim().Length > 0) lst.STOP_TIME = Convert.ToDateTime(str);
                                 

                LST.Add(lst);
            }


            return LST;
        }

        /// <summary>
        ///��ȡ������ϵ
        /// </summary
        public List<Mat_Pedigree> GetMatPedigree(string MatID,string CurrProcess)
        {
            List<Mat_Pedigree> LST = new List<Mat_Pedigree>();
            Mat_Pedigree lst = new Mat_Pedigree();
            string strSQL;
            DataTable dt;
 
            //�����ĸ�ʽΪ"210ת¯��-ת¯(LY210_BOF)"
            //���ΪBOF
            CurrProcess = CurrProcess.Substring(0, CurrProcess.Length - 1);
            string[] WP = CurrProcess.Split(new char[] { '(' });
            CurrProcess = WP[1];

            
            //�ڲ�ͬ�ĳ����������ϴ��ű仯���ɲ�ͬҪ�ֱ�Դ�            
            if (CurrProcess.StartsWith("LY210"))
            {
                GetMatPedigree_LY210(MatID, CurrProcess, ref LST);
                               

                //���������ɿ�������ÿ������������Ҫ������
                for (int I = 0; I < LST.Count; I++)
                {
                    //����ÿ������������Ҫ������
                    if (LST[I].DEVICE_NO.StartsWith("LY210_CC"))
                    {
                        //���������в��ң��Ӽ���¯��ʼ����ȡȫ��������������Ϣ
                        GetMatPedigree_LYHRM(LST[I].OUT_MAT_ID,"LYHRM_FCE", ref LST);

                        //����ÿ���������ɵĸ־���Ҫ������                        
                        for (int J = 0; J < LST.Count; J++)
                        {
                            //ʹ�ô������ɵľ��������׷��������Ų���
                            if (LST[J].DEVICE_NO.StartsWith("LY210_RM"))
                            {   
                                //�������е����ݣ���������ʼ����
                                GetMatPedigree_LYHRM(LST[J].OUT_MAT_ID, "LYHRM_TCM", ref LST);
                            }                            
                        }
                    }
                }
            }
            
            if (CurrProcess.StartsWith("LYHRM"))
            {
                //ע�⣺���ϵ�����LY210��
                string Mat_CoilID = MatID, MATID_SlabID = "", MATID_HeatID = "", MATID_IronID = "";

                if (CurrProcess.StartsWith("LYHRM_FM") || CurrProcess.StartsWith("LYHRM_CTC") || CurrProcess.StartsWith("LYHRM_DC"))
                {//����� �������� CTC��ȡ  DC��ȡ�� �������Ǹ־�ţ���Ҫ�Ӵ����л�ȡ ������
                    strSQL = " SELECT * FROM MAT_PEDIGREE WHERE out_mat_id='" + Mat_CoilID + "' AND device_no like 'LYHRM_RM%'";
                    dt = ReadDatatable_OraDB(strSQL);
                    if (dt.Rows.Count > 0) MATID_SlabID = dt.Rows[0]["in_mat_id1"].ToString();//������                    
                }
                else MATID_SlabID = MatID;      
                //FCE����¯ ������ RM�������������λ���ţ���ֱ�Ӳ� CC���� ���������Ϊ ¯�κ�
                strSQL = " SELECT * FROM MAT_PEDIGREE WHERE out_mat_id='" + MATID_SlabID + "' AND device_no like 'LY210_CC%'";                    
                dt = ReadDatatable_OraDB(strSQL);
                if (dt.Rows.Count > 0) 
                {
                    MATID_HeatID= dt.Rows[0]["in_mat_id1"].ToString();//¯�κ�

                    strSQL = " SELECT * FROM MAT_PEDIGREE WHERE out_mat_id='" + MATID_HeatID + "' AND device_no like 'LY210_BOF%'"; 
                    dt = ReadDatatable_OraDB(strSQL);
                    if (dt.Rows.Count > 0) MATID_IronID = dt.Rows[0]["in_mat_id1"].ToString();//���κ�
                }
               
                //ע��CCֻ��ʾһ�����ţ���˲�����ȫ�Ժ����ˣ�ֻ��һ��һ������ʾ
                GetMatPedigree_LY210_MI_KR_BOF(MATID_IronID, "", ref LST);
                strSQL = " SELECT * FROM MAT_PEDIGREE WHERE in_mat_id1='" + MATID_HeatID + "' AND device_no like 'LY210_LF%'";
                GetMatPedigreeByStrSQL(strSQL, ref LST);
                strSQL = " SELECT * FROM MAT_PEDIGREE WHERE in_mat_id1='" + MATID_HeatID + "' AND device_no like 'LY210_RH%'";
                GetMatPedigreeByStrSQL(strSQL, ref LST);
                strSQL = " SELECT * FROM MAT_PEDIGREE WHERE out_mat_id='" + MATID_SlabID + "' AND device_no like 'LY210_CC%'";
                GetMatPedigreeByStrSQL(strSQL, ref LST);

                //**�ڶ���**����ʾ������������
                GetMatPedigree_LYHRM(MatID, CurrProcess, ref LST);


                //** ������ **����Ҫ�����������е����ݣ���������ʼ����
                string MatID_Coil = MatID;
                if (CurrProcess.StartsWith("LYHRM_FCE") || CurrProcess.StartsWith("LYHRM_RM"))
                {//�����   FCE ���� RM������Ҫ֪�� �־�� ��������һ������ȥ
                    strSQL = " SELECT * FROM MAT_PEDIGREE WHERE in_mat_id1='" + MatID + "' AND device_no like 'LY210_RM%'";
                    dt = ReadDatatable_OraDB(strSQL);
                    if (dt.Rows.Count > 0) MatID_Coil = dt.Rows[0]["out_mat_id"].ToString();//������
                }                
                GetMatPedigree_LYHRM(MatID_Coil, "LYHRM_TCM", ref LST);
            }



            //********** ������׷�ݵ���ϵ��  *************************************
            if (CurrProcess.StartsWith("LYCRM"))
            {  
                GetMatPedigree_LYCRM(MatID, CurrProcess, ref LST);

                string Mat_CoilID = "", MATID_SlabID = "", MATID_HeatID = "", MATID_IronID = "";
                //TCM
                for (int I = 0; I < LST.Count; I++)
                {
                    if (LST[I].DEVICE_NO.StartsWith("LYCRM_TCM"))
                    {
                        //�ҵ�����������ڸ־��
                        Mat_CoilID = LST[I].IN_MAT_ID1;
                        I = LST.Count+10;
                    }
                }
                if (Mat_CoilID!="")
                {
                    strSQL = " SELECT * FROM MAT_PEDIGREE WHERE out_mat_id='" + Mat_CoilID + "' AND device_no like 'LYHRM_RM%'";
                    dt = ReadDatatable_OraDB(strSQL);
                    if (dt.Rows.Count > 0)
                    {
                        //������
                        MATID_SlabID = dt.Rows[0]["in_mat_id1"].ToString();

                        strSQL = " SELECT * FROM MAT_PEDIGREE WHERE out_mat_id='" + MATID_SlabID + "' AND device_no like 'LY210_CC%'";
                        dt = ReadDatatable_OraDB(strSQL);
                        if (dt.Rows.Count > 0)
                        {
                            //¯�κ�
                            MATID_HeatID = dt.Rows[0]["in_mat_id1"].ToString();

                            strSQL = " SELECT * FROM MAT_PEDIGREE WHERE out_mat_id='" + MATID_HeatID + "' AND device_no like 'LY210_BOF%'";
                            dt = ReadDatatable_OraDB(strSQL);

                            //���κ�
                            if (dt.Rows.Count > 0) 
                                MATID_IronID = dt.Rows[0]["in_mat_id1"].ToString();
                        }
                    }
                }

                //֪��Ϊɶ����
                LST.Clear();

                //ע��CCֻ��ʾһ�����ţ���˲�����ȫ�Ժ����ˣ�ֻ��һ��һ������ʾ
                GetMatPedigree_LY210_MI_KR_BOF(MATID_IronID, "", ref LST);
                strSQL = " SELECT * FROM MAT_PEDIGREE WHERE in_mat_id1='" + MATID_HeatID + "' AND device_no like 'LY210_LF%'";
                GetMatPedigreeByStrSQL(strSQL, ref LST);
                strSQL = " SELECT * FROM MAT_PEDIGREE WHERE in_mat_id1='" + MATID_HeatID + "' AND device_no like 'LY210_RH%'";
                GetMatPedigreeByStrSQL(strSQL, ref LST);
                strSQL = " SELECT * FROM MAT_PEDIGREE WHERE out_mat_id='" + MATID_SlabID + "' AND device_no like 'LY210_CC%'";
                GetMatPedigreeByStrSQL(strSQL, ref LST);

                //��������
                GetMatPedigree_LYHRM(MATID_SlabID,"LYHRM_RM", ref LST);

                //��������
                GetMatPedigree_LYCRM(MatID, CurrProcess, ref LST);
            }
            
            //���ս�����������
            //LST.Sort(delegate(Mat_Pedigree a, Mat_Pedigree b) { return a.START_TIME.CompareTo(b.START_TIME); });

            return LST;
        }

        ///��ȡ������ϵ,210ת¯��        
        public void GetMatPedigree_LY210(string MatID, string Process, ref List<Mat_Pedigree> LST)
        {
            string strSQL;             
            if (Process.StartsWith("LY210_MI") || Process.StartsWith("LY210_KR") || Process.StartsWith("LY210_BOF"))
            {
                GetMatPedigree_LY210_MI_KR_BOF(MatID, Process, ref  LST);
                
                //Ȼ��ͨ��BOF����������ϣ�����LF_RH_CCF����
                strSQL = " SELECT * FROM MAT_PEDIGREE WHERE in_mat_id1='" + MatID + "' AND device_no like 'LY210_BOF%'";
                DataTable dt = ReadDatatable_OraDB(strSQL);
                if (dt.Rows.Count > 0) 
                {
                    MatID = dt.Rows[0]["out_mat_id"].ToString();
                    GetMatPedigree_LY210_LF_RH_CC(MatID, "LY210_CC", ref  LST);
                }
            }

            if (Process.StartsWith("LY210_LF") || Process.StartsWith("LY210_RH") || Process.StartsWith("LY210_CC"))
            {
                //��ͨ��BOF������������ϣ�����LF_RH_CCF����
                strSQL = " SELECT * FROM MAT_PEDIGREE WHERE out_mat_id='" + MatID + "' AND device_no like 'LY210_BOF%'";
                DataTable dt = ReadDatatable_OraDB(strSQL);
                if (dt.Rows.Count > 0)
                {
                    string MatID_IronID = dt.Rows[0]["in_mat_id1"].ToString();
                    GetMatPedigree_LY210_MI_KR_BOF(MatID_IronID, "LY210_BOF", ref  LST);
                }

                //Ȼ����LF_RH_CC����
                GetMatPedigree_LY210_LF_RH_CC(MatID, "LY210_CC", ref  LST);
            }
        }
     
        //��ȡ������ϵ,210ת¯���� 
        //��������¯�κ�
        public void GetMatPedigree_LY210_LF_RH_CC(string MatID, string Process, ref List<Mat_Pedigree> LST)
        {       
             string  strSQL;
             strSQL= " SELECT * FROM MAT_PEDIGREE WHERE in_mat_id1='" + MatID + "'";
             GetMatPedigreeByStrSQL(strSQL, ref LST);                           
        }

        //��ȡ������ϵ,210ת¯���� MI_KR_BOF
        //�����������κ�
        public void GetMatPedigree_LY210_MI_KR_BOF(string MatID, string Process, ref List<Mat_Pedigree> LST)
        {
            string strSQL;

            strSQL = " SELECT * FROM MAT_PEDIGREE WHERE out_mat_id='" + MatID + "' AND device_no like 'LY210_MI%'";
            GetMatPedigreeByStrSQL(strSQL, ref LST);

            strSQL = " SELECT * FROM MAT_PEDIGREE WHERE out_mat_id='" + MatID + "' AND device_no like 'LY210_KR%'";
            GetMatPedigreeByStrSQL(strSQL, ref LST); 
                
            strSQL = " SELECT * FROM MAT_PEDIGREE WHERE in_mat_id1='" + MatID + "' AND device_no like 'LY210_BOF%'";
            GetMatPedigreeByStrSQL(strSQL, ref LST);
             
        }


        ///��ȡ������ϵ,������        
        public void GetMatPedigree_LYHRM(string MatID, string Process,ref List<Mat_Pedigree> LST)
        {
            string strSQL;

            //����¯,�������Ų���
            //����������BOF�������Ϻŷ����仯
            if (Process.StartsWith("LYHRM_FCE") ||Process.StartsWith("LYHRM_RM"))
            {
                //                
                GetMatPedigree_LYHRM_FCE_RM(MatID,ref LST);

                //���Ҵ�����������
                strSQL = " SELECT * FROM MAT_PEDIGREE"
                         + " WHERE in_mat_id1='" + MatID + "'"
                         + " AND device_no LIKE 'LYHRM_RM%'";                

                //��ȡ���������ĸ־��
                DataTable dt = ReadDatatable_OraDB(strSQL);
                if (dt.Rows.Count > 0)
                {
                   string  MatID_CoilID = dt.Rows[0]["out_mat_id"].ToString();                
                   //Ȼ����FM_CRC_DC,��������У����������ϺŲ���
                   GetMatPedigree_LYHRM_FM_CRC_DC(MatID_CoilID, ref  LST);
                }
            }

 

            //���������䡢��ȡ�����ϴ��Ų������仯
            if (Process.StartsWith("LYHRM_FM") || Process.StartsWith("LYHRM_CTC") || Process.StartsWith("LYHRM_DC"))
            {
                //�������Ǹ־�ţ�Ҫ���ϵ������ң�ͨ��RM�����ȡ�����ĺ�              
                strSQL = " SELECT * FROM MAT_PEDIGREE"
                         + " WHERE out_mat_id='" + MatID + "'"
                         + " AND device_no LIKE 'LYHRM_RM%'";                
                DataTable dt = ReadDatatable_OraDB(strSQL);
                if (dt.Rows.Count > 0)
                {
                   string  MatID_SlabID = dt.Rows[0]["in_mat_id1"].ToString();
                    //�ҵ��˶�Ӧ�������ţ� ������ϵ
                   GetMatPedigree_LYHRM_FCE_RM(MatID_SlabID, ref  LST);
                }

                //Ȼ��Ų���Ӧ�õ�
                GetMatPedigree_LYHRM_FM_CRC_DC(MatID,  ref  LST);
            }
        }

        private void GetMatPedigree_LYHRM_FCE_RM(string MatID,  ref List<Mat_Pedigree> LST)
        {
            string strSQL;
            strSQL = " SELECT * FROM MAT_PEDIGREE WHERE in_mat_id1='" + MatID + "' AND  device_no LIKE 'LYHRM_FCE%'";
            GetMatPedigreeByStrSQL(strSQL, ref LST);

            strSQL = " SELECT * FROM MAT_PEDIGREE WHERE in_mat_id1='" + MatID + "' AND  device_no LIKE 'LYHRM_RM%'";
            GetMatPedigreeByStrSQL(strSQL, ref LST);
        }

        private void GetMatPedigree_LYHRM_FM_CRC_DC(string MatID, ref List<Mat_Pedigree> LST)
        {
           string  strSQL;
           strSQL= " SELECT * FROM MAT_PEDIGREE WHERE in_mat_id1='" + MatID + "' AND  device_no LIKE 'LYHRM_FM%'";
           GetMatPedigreeByStrSQL(strSQL, ref LST);

           strSQL = " SELECT * FROM MAT_PEDIGREE WHERE in_mat_id1='" + MatID + "' AND  device_no LIKE 'LYHRM_CTC%'";
           GetMatPedigreeByStrSQL(strSQL, ref LST);

           strSQL = " SELECT * FROM MAT_PEDIGREE WHERE in_mat_id1='" + MatID + "' AND  device_no LIKE 'LYHRM_DC%'";
           GetMatPedigreeByStrSQL(strSQL, ref LST);

        }

        //��ȡ������ϵ,������        
        //�������ص㣺ÿ��һ�����������ϱ��һ���ᷢ���仯
        public void GetMatPedigree_LYCRM(string MatID, string Process, ref List<Mat_Pedigree> LST)
        {
            string strSQL;
            string str;

            //��ǰ�ң���Ϊ��������ĳ�����
            do
            {
                strSQL = " SELECT * FROM MAT_PEDIGREE"
                            + " WHERE out_mat_id='" + MatID + "'"
                            + " AND  device_no LIKE 'LYCRM%'";
                GetMatPedigreeByStrSQL(strSQL, ref LST);
            
                DataTable dt = ReadDatatable_OraDB(strSQL);

                if (dt.Rows.Count<= 0)
                    break;
                else
                {
                    //Ȼ��ѳ�����Ϊ���ϣ��ٴβ���
                    str = dt.Rows[0]["IN_MAT_ID1"].ToString();
                    if (str.Trim().Length > 0) MatID = str; else break;                    
                }
            } while (true);

            //�����ң���Ϊ������
            string CoilID_Next = MatID;
            do
            {
                //��Ϊ��������������
                strSQL = " SELECT * FROM MAT_PEDIGREE"
                            + " WHERE in_mat_id1='" + CoilID_Next + "'"
                            + " OR in_mat_id2='" + CoilID_Next + "'"
                            + " AND  device_no LIKE 'LYCRM%'";
                GetMatPedigreeByStrSQL(strSQL, ref LST);

                DataTable dt = ReadDatatable_OraDB(strSQL);
                if (dt.Rows.Count <= 0)
                    break;
                else
                {
                    //Ȼ��ѳ�����Ϊ���ϣ��ٴβ���
                    str = dt.Rows[0]["OUT_MAT_ID"].ToString();
                    if (str.Trim().Length > 0) CoilID_Next = str; else break;                
                }
            } while (true);
        }

        private void GetMatPedigreeByStrSQL(string strSQL, ref List<Mat_Pedigree> LST)
        {
            Mat_Pedigree lst ; 
            DataTable dt = ReadDatatable_OraDB(strSQL);
            string str;
            for (int RowIndex=0; RowIndex<dt.Rows.Count;RowIndex++) 
            {
                lst = new Mat_Pedigree();

                str = dt.Rows[RowIndex]["OUT_MAT_ID"].ToString(); if (str.Trim().Length > 0) lst.OUT_MAT_ID = str;
                str = dt.Rows[RowIndex]["IN_MAT_ID1"].ToString(); if (str.Trim().Length > 0) lst.IN_MAT_ID1 = str;
                str = dt.Rows[RowIndex]["IN_MAT_ID2"].ToString(); if (str.Trim().Length > 0) lst.IN_MAT_ID2 = str;
                str = dt.Rows[RowIndex]["DEVICE_NO"].ToString(); if (str.Trim().Length > 0) lst.DEVICE_NO = str;
                str = dt.Rows[RowIndex]["STEEL_GRADE"].ToString(); if (str.Trim().Length > 0) lst.STEEL_GRADE = str; 
                str = dt.Rows[RowIndex]["START_TIME"].ToString(); if (str.Trim().Length > 0) lst.START_TIME = Convert.ToDateTime(str);
                str = dt.Rows[RowIndex]["STOP_TIME"].ToString(); if (str.Trim().Length > 0) lst.STOP_TIME = Convert.ToDateTime(str);               

                LST.Add(lst);
            }
        }
    }
}
