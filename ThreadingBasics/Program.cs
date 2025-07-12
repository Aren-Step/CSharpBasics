using System.Threading;
namespace CSharpBasics;

class ThreadingBasics
{
    static void Work(object? name)
    {
        try 
        {
            Console.WriteLine($"Work from a thread {name}");

            throw new Exception("An error occurred in the thread");

        }
        catch (Exception ex) {
            Console.WriteLine($"Exception in thread {name}: {ex.Message}");
        }
        finally 
        {
            Console.WriteLine($"Thread {name} is finishing up.");
        }
    }
    
    static void Main()
    {
        var t1 = new Thread(Work);
        t1.Start("Thread 1");
        Thread.Sleep(1000); // Wait for threads to finish
        var t2 = new Thread(Work);
        t2.Start("Thread 2");


    }
}