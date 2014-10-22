using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace QtDataTrace.Interfaces
{
    [Serializable]
    public class MaterialInfo
    {
        string inId;
        string id;
        DateTime time;
        string process;
        string steelGrade;
        string thickness;
        string width;
        string batch;
        string orderId;
        IList<EquipmentInfo> devices;

        public MaterialInfo()
        {
        }       
        private bool _choose = true;
        public bool choose
        {
            get
            {
                return _choose;
            }
            set
            {
                _choose = value;
            }
        }
        [DisplayName("生产时间")]
        public DateTime Time
        {
            get { return time; }
            set { time = value; }
        }

        [DisplayName("入口物料号")]
        public string InId
        {
            get { return inId; }
            set { inId = value; }
        }

        [DisplayName("出口物料号")]
        public string OutId
        {
            get { return id; }
            set { id = value; }
        }

        [DisplayName("生产工序")]
        public string Process
        {
            get { return process; }
            set { process = value; }
        }

        [DisplayName("钢种")]
        public string SteelGrade
        {
            get { return steelGrade; }
            set { steelGrade = value; }
        }

        [DisplayName("厚度")]
        public string Thickness
        {
            get { return thickness; }
            set { thickness = value; }
        }

        [DisplayName("宽度")]
        public string Width
        {
            get { return width; }
            set { width = value; }
        }

        [DisplayName("批号")]
        public string Batch
        {
            get { return batch; }
            set { batch = value; }
        }

        [DisplayName("订单号")]
        public string OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        public IList<EquipmentInfo> Equipments
        {
            get { return devices; }
            set { devices = value; }
        }
    }
}
