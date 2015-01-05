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
                        return Guid.Empty;
                }
                id = Guid.NewGuid();
                factories.Add(id, factory);
            }
            factory.Start();
            return id;
        }
        public static bool Stop(string username, Guid id)
        {
            Dictionary<Guid, DataAnalyzeFactory> factories;
            if (DataAnalyzeFactoryContainer.TryGetValue(username, out factories))
            {
                DataAnalyzeFactory factory;
                if (factories.TryGetValue(id, out factory))
                {
                    if (factory.Stop())
                    {
                        factories.Remove(id);
                        return true;
                    }
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
        protected DataAnalyzeFactory()
        {
            Working = false;
        }
        public string Name { get; set; }
        public bool Working { get; protected set; }
        public abstract bool Start();
        public abstract bool Stop();
        public abstract int GetProgress();
    }
    public class CCTAnalyzeFactory: DataAnalyzeFactory
    {
        Task mainThread;
        CancellationTokenSource cancelToken;
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
        }
        public override bool Start()
        {
            try
            {
                this.Working = true;
                Stop();
                mainThread = new Task(() => { Result = SPC.Algorithm.Relations.GetCCTs(data, target, f, waitobj); this.Working = false; }, (cancelToken = new CancellationTokenSource()).Token);
                mainThread.Start();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override bool Stop()
        {
            if (mainThread != null &&mainThread.Status== TaskStatus.Running)
            {
                cancelToken.Cancel();
                this.Working = false;
                return true;
            }
            return false;
        }

        public override int GetProgress()
        {
            return waitobj.GetProgress();
        }
    }
    public class KMeansAnalyzeFactory : DataAnalyzeFactory
    {
        Task mainThread;
        CancellationTokenSource cancelToken;
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
        }
        public override bool Start()
        {
            try
            {
                this.Working = true;
                Stop();
                cancelToken = new CancellationTokenSource();
                mainThread = new Task(() => { Result = SPC.Algorithm.KMeans.StartCluster(cancelToken, data, Properties, maxCount, minClusterCount, maxClusterCount, m, s, waitobj, initialMode, 0, maxThread); this.Working = false; }, cancelToken.Token);
                mainThread.Start();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override bool Stop()
        {
            if (mainThread != null && mainThread.Status == TaskStatus.Running)
            {
                cancelToken.Cancel();
                this.Working = false;
                return true;
            }
            return false;
        }

        public override int GetProgress()
        {
            return waitobj.GetProgress();
        }
    }
}
