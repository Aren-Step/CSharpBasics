// using System.Threading;
namespace CSharpBasics;

class ThreadingBasics
{
    public class ThreadSafe
    {
        static readonly object _locker = new object();
        bool lockTaken = false;
        static int _val1 = 1, _val2 = 1;

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
    }
    static void Main()
    {
        Thread t1 = new(ThreadSafe.MutexSyncMethod), t2 = new(ThreadSafe.MutexSyncMethod);
        t1.Start();
        t2.Start();
    }
}
