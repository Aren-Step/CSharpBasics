using System.Threading;
using System.Collections.Concurrent;
using System.Diagnostics.Metrics;

namespace CSharpBasics;

class ThreadingBasics
{
    public class ThreadSafe
    {
        static readonly object _locker = new object();
        bool lockTaken = false;
        static int _val1 = 1, _val2 = 1;
        static Semaphore smph = new(1, 3);

        public static void MonitorSyncMethod()
        {
            ThreadSafe obj = new ThreadSafe();
            Monitor.Enter(_locker, ref obj.lockTaken);
            try
            {
                if (_val2 != 0) Console.WriteLine(_val1 / _val2);
                _val2 = 0;
                Console.WriteLine($"val1 = {_val1}, val2 = {_val2}");
            }
            finally { if (obj.lockTaken) Monitor.Exit(_locker); }
        }
        public static void MutexSyncMethod()
        {
            var mutex = new Mutex();
            try
            {
                mutex.WaitOne(1000);
                if (_val2 != 0) Console.WriteLine(_val1 / _val2);
                _val2 = 0;
                Console.WriteLine($"val1 = {_val1}, val2 = {_val2}");
            }
            finally { mutex.ReleaseMutex(); }
        }
        public static void SemaphoreSyncMethod()
        {
            int id = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine(id + " wants to enter");
            smph.WaitOne(1000);
            Console.WriteLine(id + " is in!");           // Only three threads
            Thread.Sleep(1000 * (int)id);               // can be here at
            Console.WriteLine(id + " is leaving");       // a time.
            smph.Release();
        }
    }
    static void Main()
    {
        //for (int i = 1; i <= 5; i++) new Thread(ThreadSafe.SemaphoreSyncMethod).Start();
        /*Thread t1 = new(ThreadSafe.SemaphoreSyncMethod), t2 = new(ThreadSafe.SemaphoreSyncMethod);
        t1.Start();
        t2.Start();*/

        Console.WriteLine("enter value:");
        int n = Convert.ToInt32(Console.ReadLine());
        ConcurrentDictionary<int, bool> range = new(Enumerable.Range(2, n - 1).ToDictionary(k => k, v => true));
        object _locker = new();
        for (int i = 2; i * i <= n; i++)
        {
            Thread t1 = new Thread(() =>
            {
                for (int j = i * i; j <= n; j += i)
                {
                    range[j] = false;
                }
            });
            t1.Start();
            t1.Join();
        }

        Console.WriteLine("the primes are:\n" + string.Join(", ", range.Where(k => k.Value).Select(p => p.Key)));
    }
}
