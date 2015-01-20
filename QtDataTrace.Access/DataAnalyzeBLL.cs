using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using EAS.Services;
using QtDataTrace.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using SPC.Base.Interface;

namespace QtDataTrace.Access
{
    internal class DataAnalyzeBLL
    {
        private static Dictionary<string, Dictionary<Guid, DataAnalyzeFactory>> DataAnalyzeFactoryContainer = new Dictionary<string, Dictionary<Guid, DataAnalyzeFactory>>();
        public static Guid Add(string username, DataAnalyzeFactory factory)
        {
            Guid id;
            Dictionary<Guid, DataAnalyzeFactory> factories;
            lock (DataAnalyzeFactoryContainer)
            {
                if (!DataAnalyzeFactoryContainer.TryGetValue(username, out factories))
                {
                    factories = new Dictionary<Guid, DataAnalyzeFactory>();
                    DataAnalyzeFactoryContainer.Add(username, factories);
                }
            }
            lock (factories)
            {
                if (factories.Count > 3)
                {
                    bool temp = false;
                    foreach (var k in factories.Keys)
                    {
                        var f = factories[k];
                        if (!f.Working)
                        {
                            factories.Remove(k);
                            temp = true;
                            break;
                        }
                    }
                    if (!temp)
                        throw new Exception("该用户建立运算过多，请等待其他运算完成");
                }
                id = Guid.NewGuid();
                factories.Add(id, factory);
            }
            factory.Start();
            return id;
        }
        public static bool Remove(string username, Guid id)
        {
            Dictionary<Guid, DataAnalyzeFactory> factories;
            if (DataAnalyzeFactoryContainer.TryGetValue(username, out factories))
            {
                DataAnalyzeFactory factory;
                if (factories.TryGetValue(id, out factory))
                {
                    if (factory.Working)
                        factory.Stop();
                    factories.Remove(id);
                    return true;
                }
            }
            return false;
        }
        public static bool Stop(string username, Guid id)
        {
            Dictionary<Guid, DataAnalyzeFactory> factories;
            if (DataAnalyzeFactoryContainer.TryGetValue(username, out factories))
            {
                DataAnalyzeFactory factory;
                if (factories.TryGetValue(id, out factory))
                {
                    factory.Stop();
                    return true;
                }
            }
            return false;
        }
        public static DataAnalyzeFactory GetFactory(string username, Guid id)
        {
            Dictionary<Guid, DataAnalyzeFactory> factories;
            if (DataAnalyzeFactoryContainer.TryGetValue(username, out factories))
            {
                DataAnalyzeFactory factory;
                if (factories.TryGetValue(id, out factory))
                {
                    return factory;
                }
            }
            return null;
        }
    }
    public abstract class DataAnalyzeFactory
    {
        protected Task mainThread;
        protected CancellationTokenSource cancelToken;
        protected Action doMethod;
        public string Name { get; set; }
        private string _Error = "";
        public string Error 
        { 
            get
            {
                return this._Error;
            }
            private set
            {
                this._Error = value;
            }
        }
        public event EventHandler StopedWorking;
        private bool _Working = false;
        public bool Working 
        { 
            get
            {
                return _Working;
            }
            protected set
            {
                _Working = value;
                if (!value && StopedWorking != null)
                    StopedWorking(this, new EventArgs());
            }
        }
        public virtual bool Start()
        {
            try
            {
                this.Working = true;
                Stop();
                cancelToken = new CancellationTokenSource();
                mainThread = new Task(() => 
                {
                    try
                    {
                        doMethod();
                        this.Error = "";
                    }
                    catch (Exception ex)
                    {
                        this.Error = ex.Message;
                    }
                    finally
                    {
                        this.Working = false;
                    }
                }, (cancelToken = new CancellationTokenSource()).Token);
                mainThread.Start();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public virtual bool Stop()
        {
            if (mainThread != null && mainThread.Status == TaskStatus.Running)
            {
                cancelToken.Cancel();
                this.Working = false;
                return true;
            }
            return false;
        }
        public abstract int GetProgress();
    }
    public class CCTAnalyzeFactory: DataAnalyzeFactory
    {
        IDataTable<DataRow> data;
        string target;
        string[] f;
        public double[] Result;
        WaitObject waitobj = new WaitObject();
        public CCTAnalyzeFactory(IDataTable<DataRow> data,string target,string[] f):base()
        {
            this.data = data;
            this.target = target;
            this.f = f;
            this.doMethod = () => { this.Result = SPC.Algorithm.Relations.GetCCTs(this.data, this.target, this.f, this.waitobj); };
        }
    
        public override int GetProgress()
        {
            return waitobj.GetProgress();
        }
    }
    public class KMeansAnalyzeFactory : DataAnalyzeFactory
    {
        IDataTable<DataRow> data;
        int maxCount;
        int minClusterCount;
        int maxClusterCount;
        double m;
        double s;
        int initialMode;
        int maxThread;
        string[] Properties;
        public DataSet Result;
        WaitObject waitobj = new WaitObject();
        public KMeansAnalyzeFactory(IDataTable<DataRow> data,string[] properties,int maxcount,int minclustercount,int maxclustercount,double m,double s,int initialmode,int maxthread)
            : base()
        {
            this.data = data;
            this.Properties = properties;
            this.maxCount = maxcount;
            this.minClusterCount = minclustercount;
            this.maxClusterCount = maxclustercount;
            this.m = m;
            this.s = s;
            this.initialMode = initialmode;
            this.maxThread = maxthread;
            this.doMethod = () => { Result = SPC.Algorithm.KMeans.StartCluster(this.cancelToken, this.data, this.Properties, this.maxCount, this.minClusterCount, this.maxClusterCount, this.m, this.s, this.waitobj, this.initialMode, 0, this.maxThread); };
        }
     
        public override int GetProgress()
        {
            return waitobj.GetProgress();
        }
    }
    public class ContourPlotFactory : DataAnalyzeFactory
    {
        IDataTable<DataRow> data;
        string X;
        string Y;
        string Z;
        int Width;
        int Height;
        public System.Drawing.Image Result;
        int process = 0;
        double[] Levels;
        bool Drawline;
        public ContourPlotFactory(IDataTable<DataRow> data, string x, string y, string z, int Width,int Height,double[] levels,bool drawline)
            : base()
        {
            this.data = data;
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.Width = Width;
            this.Height = Height;
            this.Levels = levels;
            this.Drawline = drawline;
            this.doMethod = () => { Result = SPC.Rnet.Methods.DrawContourPlot(this.data, this.X, this.Y, this.Z, this.Width, this.Height, this.Levels, this.Drawline); };
        }
        public override int GetProgress()
        {
            return process = (process+10)%100;
        }
    }
}
