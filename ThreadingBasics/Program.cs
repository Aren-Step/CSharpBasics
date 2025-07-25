using System.Threading;
namespace CSharpBasics;

class ThreadingBasics
{
    public class ThreadSafe
    {
        static readonly object _locker = new object();
        static bool lockTaken = false;
        static int _val1 = 1, _val2 = 1;

        public static void Go()
        {
            Monitor.Enter(_locker, ref lockTaken);
            try
            {
                if (_val2 != 0) Console.WriteLine(_val1 / _val2);
                _val2 = 0;
                Console.WriteLine($"val1 = {_val1}, val2 = {_val2}");
            }
            finally {  if (lockTaken) Monitor.Exit(_locker); }
        }
    }
    static void Main()
    {
        Thread t1 = new(ThreadSafe.Go), t2 = new(ThreadSafe.Go);
        t1.Start();
        t2.Start();
    }
}