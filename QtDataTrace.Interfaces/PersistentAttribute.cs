using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace QtDataTrace.Interfaces
{
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class, Inherited = true)]
    public class PersistentAttribute : System.Attribute
    {
        public static readonly Type AttributeType;
        private string mapTo;

        static PersistentAttribute()
        {
            AttributeType = typeof(PersistentAttribute);
        }

        PersistentAttribute()
        {
        }

        public PersistentAttribute(string mapTo)
        {
            this.mapTo = mapTo;
        }

        public string MapTo
        {
            get { return mapTo; }
            set { mapTo = value; }
        }
    }

    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class, Inherited = true)]
    public class DataSizeAttribute : System.Attribute
    {
        public static readonly Type AttributeType;

        static DataSizeAttribute()
        {
            AttributeType = typeof(PersistentAttribute);
        }

        DataSizeAttribute()
        {
        }

        public DataSizeAttribute(int size, int s)
        {
        }
    }
}
