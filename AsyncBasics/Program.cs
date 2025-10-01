using System.Security.AccessControl;

namespace AsyncBasics
{
    class Program
    {
        public static async Task IterateAsync()
        {
            for (int i = 1; i <= 50; i++)
            {
                Console.WriteLine($"{i} \t Async method called \t" + 
                    $"Executing threadID: {Thread.CurrentThread.ManagedThreadId}, Is it ThreadPoolThread? {Thread.CurrentThread.IsThreadPoolThread}");
                if (i == 10) 
                    await Task.Delay(1).ConfigureAwait(true);  // ConfigureAwait(true) does not change the await statement's default behavior.
                                                               // it's called to clearly show that the execution continues on the captured context.

                /*
                 * When the async method completes its awaited task, the control from the calling method (in this case - Main()) returns to itself
                 * and the async method grabs a thread from the ThreadPool, but the calling method continues executing, creating parallelism.
                 * The end result in this case is UNDETERMINISTIC.
                 * NOTE: Whether the captured context after the await statement's completion changes or not, the executing thread MAY CHANGE.
                 */
            }
        }

        public static async Task Main()
        {
            Task iterateTask = IterateAsync();

            for (int i = 1; i <= 50; i++)
            {
                Console.WriteLine($"{i} \t Calling method called \t" + 
                    $"Executing threadID: {Thread.CurrentThread.ManagedThreadId}, Is it ThreadPoolThread? {Thread.CurrentThread.IsThreadPoolThread}");
                if (i == 10)
                    await Task.Delay(1);   // Parallelism is caused after the completion of the IterateAsync()'s await statement.
                                           // It's enough to await once to create concurrency.
            }
        }
    }
}
