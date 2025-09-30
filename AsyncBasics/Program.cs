using System.Security.AccessControl;

namespace AsyncBasics
{
    class Program
    {
        class BackgroundTask
        {
            public static async Task UpdateDataAsync(int id)
            {
                await Task.Delay(5000);
            }
        }

        static async Task TryUpdateDataAsync(int id)
        {
            try
            {
                await BackgroundTask.UpdateDataAsync(id);
            }
            catch (Exception ex) 
            { 
                if (ex is AggregateException taskEx)
                {
                    foreach (var innerEx in taskEx.InnerExceptions)
                    {
                        Console.WriteLine(innerEx.Message);
                    }
                }
                else
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static async Task<bool> AsyncMethodSyncCall(int id)
        {
            var result = Task.Run(() =>
            {
                if (id < 0)
                {
                    throw new InvalidOperationException("Exception thrown from Task.Run()");
                }
                return false;
            }).Result;
            // If GetAwaiter().GetResult() is called instead of Result, the exception won't be wrapped in a AggregateException exception.
            
            if (id == 0)
            {
                result = false;
                throw new InvalidOperationException("Exception thrown from AsyncMethodSyncCall()");
            }
            return result;
        }

        public static async Task Main()
        {
            Task<bool> myTask;
            try
            {
                myTask = AsyncMethodSyncCall(-100);
                Console.WriteLine("The task returned" + await myTask);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"The exception of type {ex.GetType()} was catched!" + 
                    "\nThe message from the exception:\n" + ex.Message);
            }

            //TryUpdateDataAsync(100);
        }
    }
}
