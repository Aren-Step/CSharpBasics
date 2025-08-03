// using System.Threading;
namespace CSharpBasics;

class ThreadingBasics
{
    public class ThreadSafe
    {
        static readonly object _locker = new object();
        bool lockTaken = false;
        static int _val1 = 1, _val2 = 1;

        public static void Go()
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

        public static void WaitForThread(Thread thread)
        {
            thread.Start();
            thread.Join();
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        }
    }
    static void Main()
    {
        /*Thread t1 = new(ThreadSafe.Go), t2 = new(ThreadSafe.Go);
        t1.Start();
        t2.Start();*/

        // 1) Using AutoResetEvent
        AutoResetEvent are1 = new AutoResetEvent(false);
        AutoResetEvent are2 = new AutoResetEvent(false);
        var thread1 = new Thread(() =>
        {
            Thread.Sleep(1000);
            are1.WaitOne();
            are2.Set();
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        });
        Thread.Sleep(1000);
        are2.WaitOne();
        are1.Set();
        Console.WriteLine(Thread.CurrentThread.ManagedThreadId);

        // 2) Waiting for all threads by calling Join() from each of them
        Thread t1 = null, t2 = null;
        t1 = new Thread(() =>
        {
            t2.Join();
            Console.WriteLine("thread 1 id: " + Thread.CurrentThread.ManagedThreadId);
        });
        t2 = new Thread(() =>
        {
            t1.Join();
            Console.WriteLine("thread 2 id: " + Thread.CurrentThread.ManagedThreadId);
        });
        t1.Start();
        t2.Start();

        // 3) Using locks
        object locker1 = new object();
        object locker2 = new object();
        object locker3 = new object();

        new Thread(() => {
            lock (locker1)
            {
                Thread.Sleep(1000);
                lock (locker2);      // Deadlock
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            }
        }).Start();
        new Thread(() => {
            lock (locker2)
            {
                Thread.Sleep(1000);
                lock (locker3);      // Deadlock
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            }
        }).Start();
        lock (locker3)
        {
            Thread.Sleep(1000);
            lock (locker1);         // Deadlock
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        }

        Console.ReadLine();
    }
}
