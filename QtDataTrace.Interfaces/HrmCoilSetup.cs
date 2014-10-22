using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace QtDataTrace.Interfaces
{
    [Serializable]
    public class HrmCoilSetup
    {
        private string stand;

        [Persistent("STD"), DisplayName("机架")]
        public string Stand
        {
            get { return stand; }
            set { stand = value; }
        }
        private int course;

        [DisplayName("设定时序")]
        public int Course
        {
            get { return course; }
            set { course = value; }
        }
        private double thk;

        [DisplayName("厚度")]
        public double Thk
        {
            get { return thk; }
            set { thk = value; }
        }
        private double width;

        [DisplayName("宽度")]
        public double Width
        {
            get { return width; }
            set { width = value; }
        }
        private double length;

        [DisplayName("长度")]
        public double Length
        {
            get { return length; }
            set { length = value; }
        }
        private double reduction;

        [Persistent("RED"), DisplayName("压下量")]
        public double Reduction
        {
            get { return reduction; }
            set { reduction = value; }
        }
        private double temperature;

        [Persistent("temp"), DisplayName("温度")]
        public double Temperature
        {
            get { return temperature; }
            set { temperature = value; }
        }
        private double threadSpeed;

        [Persistent("thread_speed"), DisplayName("穿带速度")]
        public double ThreadSpeed
        {
            get { return threadSpeed; }
            set { threadSpeed = value; }
        }
        private double runSpeed;

        [Persistent("run_speed"), DisplayName("运行速度")]
        public double RunSpeed
        {
            get { return runSpeed; }
            set { runSpeed = value; }
        }
        private double gap;

        [Persistent("GAP"), DisplayName("辊缝")]
        public double Gap
        {
            get { return gap; }
            set { gap = value; }
        }
        private double fs;

        [Persistent("fs"), DisplayName("前滑")]
        public double ForwardSlip
        {
            get { return fs; }
            set { fs = value; }
        }

        private double force;

        [DisplayName("轧制力")]
        public double Force
        {
            get { return force; }
            set { force = value; }
        }
        private double power;

        [DisplayName("功率")]
        public double Power
        {
            get { return power; }
            set { power = value; }
        }
        private double torque;

        [DisplayName("转矩")]
        public double Torque
        {
            get { return torque; }
            set { torque = value; }
        }
    }
}
