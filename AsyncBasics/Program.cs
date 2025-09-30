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

        public static async Task Main()
        {
            TryUpdateDataAsync(100);
        }
    }
}
