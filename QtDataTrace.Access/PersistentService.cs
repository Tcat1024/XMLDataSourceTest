using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Reflection;
using QtDataTrace.Interfaces;

namespace QtDataTrace.Access
{
    public class PersistentService<T> where T : new()
    {
        class ColumnInfo
        {
            private bool field;
            private Type dataType;

            public Type DataType
            {
                get { return dataType; }
                set { dataType = value; }
            }
            public bool Field
            {
                get { return field; }
                set { field = value; }
            }
            private string name;

            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            private string mapTo;

            public string MapTo
            {
                get { return mapTo; }
                set { mapTo = value; }
            }

            public ColumnInfo(bool field, string name, Type dataType)
            {
                this.field = field;
                this.name = name;
                this.mapTo = name;
                this.dataType = dataType;
            }

            public ColumnInfo(bool field, string name, string mapTo, Type dataType)
            {
                this.field = field;
                this.name = name;
                this.mapTo = mapTo;
                this.dataType = dataType;
            }
        }

        static List<ColumnInfo> columns = new List<ColumnInfo>();

        static PersistentService()
        {
            GetMemberInfo();
        }

        public List<T> Load(string sql, OleDbConnection connection)
        {
            List<T> result = new List<T>();

            OleDbCommand command = new OleDbCommand(sql, connection);

            using (OleDbDataReader reader = command.ExecuteReader())
            {
                Type type = typeof(T);

                while (reader.Read())
                {
                    T obj = new T();
                    result.Add(obj);

                    foreach (ColumnInfo column in columns)
                    {
                        Object value = System.Convert.ChangeType(reader[column.MapTo], column.DataType);

                        if (column.Field)
                        {
                            type.InvokeMember(column.Name,
                                BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetField,
                                null,
                                obj,
                                new object[] { value });
                        }
                        else
                        {
                            type.InvokeMember(column.Name,
                                    BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                                    null,
                                    obj,
                                    new object[] { value });
                        }
                    }
                }
            }

            return result;
        }

        public static void GetMemberInfo()
        {
            Type type = typeof(T);
            PersistentAttribute persistAttr;

            //Querying Class-Property (only public)Attributes  
            foreach (PropertyInfo prop in type.GetProperties())
            {
                bool found = false;

                foreach (Attribute attr in prop.GetCustomAttributes(true))
                {
                    persistAttr = attr as PersistentAttribute;
                    if (null != persistAttr)
                    {
                        found = true;
                        columns.Add(new ColumnInfo(false, prop.Name, persistAttr.MapTo, prop.PropertyType));
                    }
                }

                if (found == false)
                {
                    columns.Add(new ColumnInfo(false, prop.Name, prop.PropertyType));
                }
            }

            //Querying Class-Field (only public) Attributes
            foreach (FieldInfo field in type.GetFields())
            {
                bool found = false;

                foreach (Attribute attr in field.GetCustomAttributes(true))
                {
                    persistAttr = attr as PersistentAttribute;
                    if (null != persistAttr)
                    {
                        found = true;
                        columns.Add(new ColumnInfo(true, field.Name, persistAttr.MapTo, field.FieldType));
                    }
                }

                if (found == false)
                {
                    columns.Add(new ColumnInfo(true, field.Name, field.FieldType));
                }
            }
        }
    }
}
