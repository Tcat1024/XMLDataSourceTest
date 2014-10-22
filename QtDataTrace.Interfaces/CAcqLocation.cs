using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace QtDataTrace.Interfaces
{
    [System.SerializableAttribute]
    public class CAcqLocation
    {
        private string location;
        private string comment;
        private string expression;
        private uint resolution;

        private IList<CPoint> points = new BindingList<CPoint>();

        [DisplayName("数据采集位置")]
        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        [DisplayName("描述")]
        public string Comment
        {
            get { return comment; }
            set { comment = value; }
        }

        [DisplayName("表达式")]
        public string Expression
        {
            get { return expression; }
            set { expression = value; }
        }

        [DisplayName("分辨率")]
        public uint Resolution
        {
            get { return resolution; }
            set { resolution = value; }
        }

        public IList<CPoint> Points
        {
            get { return points; }
            set { points = value; }
        }
    }
}
