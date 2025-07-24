using System.Threading;
namespace CSharpBasics;

class ThreadingBasics
{
    static void PrintText()
    {
        Thread.Sleep(3000);
        Console.WriteLine("Hello from thread " + Thread.CurrentThread.ManagedThreadId);
    }
    static void CheckThreadState(Thread thread)
    {
        Console.WriteLine("The thread " + thread.Name + " is " + thread.ThreadState switch
        {
            ThreadState.WaitSleepJoin => "Blocked",
            _ => thread.ThreadState
        });
    }
    static void Main()
    {
        Thread t1 = new Thread(PrintText);
        t1.Name = "UltraThread69";
        CheckThreadState(t1);
        t1.Start();
        CheckThreadState(t1);
        Thread.Sleep(500);
        CheckThreadState(t1);
        t1.Join();
        CheckThreadState(t1);
    }
}