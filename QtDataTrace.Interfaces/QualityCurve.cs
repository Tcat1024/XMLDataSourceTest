using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Expression;

namespace QtDataTrace.Interfaces
{
    [System.SerializableAttribute]
    public class CurveValue
    {
        public double x;
        public double y;

        public CurveValue()
        {
        }

        public CurveValue(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }

    [System.SerializableAttribute]
    public class QualityCurve
    {
        public string Name;
        public int ActLength;
        public CurveValue[] curve;

        public QualityCurve()
        {
            ActLength = 0;
        }

        public QualityCurve(string name)
        {
            this.Name = name;
            ActLength = 0;
        }

        public void Initialize(Range range, double _xBase)
        {
            int num = (int)(range.Length / _xBase);
            if (!(Math.IEEERemainder(range.Length, _xBase) < 0.0000001))
            {
                num++;
            }

            if (num > ActLength || num < ActLength / 2)
            {
                curve = new CurveValue[num];
            }
            ActLength = num;
        }
    }

    public class QualityCurveInsert
    {
        int currentIndex;
        QualityCurve data;

        public QualityCurveInsert(QualityCurve data)
        {
            this.data = data;
            this.currentIndex = 0;
        }

        public void Add(double location, double value)
        {
            data.curve[currentIndex] = new CurveValue(location, value);
            currentIndex++;
        }
    }
}
