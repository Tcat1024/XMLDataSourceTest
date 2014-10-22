using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    [System.SerializableAttribute]
    public class QueryArgs
    {
        public string ProcessCode;
        public DateTime StartTime;
        public DateTime StopTime;
        public string SteelGrade;
        public string MatId;
        public double MinThick;
        public double MaxThick;
        public double MinWidth;
        public double MaxWidth;

        public bool TimeFlag;
        public bool SteelGradeFlag;        
        public bool ThickFlag;
        public bool WidthFlag;
        public bool MatIdFlag;
        
        public bool Sort;
    }

    public class SelectBuilder
    {
        public QueryArgs cond;

        public string Build(string tableName)
        {
            string sql = "";

            return sql;
        }

        public string BuildWhere(string tableName)
        {
            string whereCluse = "";

            return whereCluse;
        }
    }
}
