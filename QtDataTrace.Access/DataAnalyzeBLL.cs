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
        Thread mainThread;
        int progress;
        IDataTable<DataRow> data;
        int pp;
        string target;
        string[] f;
        public double[] Result;
        public CCTAnalyzeFactory(IDataTable<DataRow> data,string target,string[] f):base()
        {
            this.data = data;
            this.target = target;
            this.f = f;
            this.pp = data.RowCount * f.Length;
        }
        public override bool Start()
        {
            try
            {
                this.Working = true;
                Stop();
                progress = 0;
                mainThread = new Thread(() => { Result = SPC.Algorithm.Relations.GetCCTs(data, target, f, progress); this.Working = false; });
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
            if (mainThread != null && mainThread.ThreadState == System.Threading.ThreadState.Running)
            {
                mainThread.Abort();
                this.Working = false;
                return true;
            }
            return false;
        }

        public override int GetProgress()
        {
            return progress/pp;
        }
    }
}
