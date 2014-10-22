using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    [System.SerializableAttribute]
    public class CPoint
    {
        private string device;
        private string name;
        private string comment;
        private string expression;
        private string feature;
        private string location;
        private bool   digital;
        private double minScale;
        private double maxScale;

        public string Id
        {
            get { return device + "." + location + "." + name; }
        }

        [DisplayName("设备")]
        public string Device
        {
            get { return device; }
            set { device = value; }
        }

        [DisplayName("名称")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DisplayName("注释")]
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        [DisplayName("长度变换表达式")]
        public string Expression
        {
            get { return expression; }
            set { expression = value; }
        }

        [DisplayName("特性")]
        public string Feature
        {
            get { return feature; }
            set { feature = value; }
        }

        [DisplayName("数据采集位置")]
        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        public bool Digital
        {
            get { return digital; }
            set { digital = value; }
        }

        public double MinScale
        {
            get { return minScale; }
            set { minScale = value; }
        }

        public double MaxScale
        {
            get { return maxScale; }
            set { maxScale = value; }
        }
    }
}
