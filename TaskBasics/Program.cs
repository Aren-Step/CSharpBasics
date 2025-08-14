using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace TaskBasics;

class Program
{
    class CustomData
    {
        public long CreationTime;
        public int Name;
        public int ThreadNum;
    }

    public static void Main()
    {
        Parallel.Invoke(
            () => Console.WriteLine("First Sentence"),
            () => Console.WriteLine("Second Sentence"),
            () => Console.WriteLine("Third Sentence"),
            () => Console.WriteLine("Forth Sentence")
        );

        var displayData = Task.Factory.StartNew(() => {
            Random rnd = new Random();
            int[] values = new int[100];
            for (int ctr = 0; ctr <= values.GetUpperBound(0); ctr++)
                values[ctr] = rnd.Next();

            return values;
        })
        .ContinueWith((x) =>
        {
            int n = x.Result.Length;
            long sum = 0;
            double mean;

            for (int ctr = 0; ctr <= x.Result.GetUpperBound(0); ctr++)
                sum += x.Result[ctr];

            mean = sum / (double)n;
            return Tuple.Create(n, sum, mean);
        })
        .ContinueWith((x) => {
        return String.Format("N={0:N0}, Total = {1:N0}, Mean = {2:N2}",
                                x.Result.Item1, x.Result.Item2,
                                x.Result.Item3);
        });
        Console.WriteLine(displayData.Result);

        Task[] taskArray = new Task[10];
        for (int i = 0; i < taskArray.Length; i++)
        {
            taskArray[i] = Task.Factory.StartNew((object? obj) => {
                CustomData data = obj as CustomData;
                if (data == null)
                    return;
                data.ThreadNum = Thread.CurrentThread.ManagedThreadId;
            }, new CustomData() { Name = i, CreationTime = DateTime.Now.Ticks});
        }
        Task.WaitAll(taskArray);
        foreach (var task in taskArray)
        {
            var data = task.AsyncState as CustomData;
            if (data != null)
                Console.WriteLine($"Task #{data.Name} created at {data.CreationTime} on thread #{data.ThreadNum}.");
        }

        var custom_task = new Task(() => { Console.WriteLine($"A task with id {Task.CurrentId} and custom options has been executed."); },  
            TaskCreationOptions.LongRunning | TaskCreationOptions.PreferFairness);
        custom_task.Start();
        Task.WaitAll(custom_task);

        // detached task
        var outer = Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Outer task beginning.");

            var child = Task.Factory.StartNew(() =>
            {
                Thread.SpinWait(5000000);
                Console.WriteLine("Detached task completed.");
            });
        });
        outer.Wait();
        Console.WriteLine("Outer task completed.");
        Task.WaitAll();

        // attached task
        var parent = Task.Factory.StartNew(() =>
        {
            Console.WriteLine("Parent task begins.");
            for (int i = 0; i < 10; i++)
            {
                int taskNumber = i;
                Task.Factory.StartNew((x) =>
                {
                    Thread.SpinWait(5000000);
                    Console.WriteLine($"Attached child {x} completed.");
                }, taskNumber, TaskCreationOptions.AttachedToParent);
            }
        });
        parent.Wait();
        Console.WriteLine("Parent task completed.");
        Task.WaitAll();

        ChangeCulture();
    }

    public static void ChangeCulture()
    {
        decimal[] values = { 163025412.32m, 18905365.59m };
        string formatString = "C2";
        Func<String> formatDelegate = () => {
            string output = String.Format("Formatting using the {0} culture on thread {1}.\n",
                                                                            CultureInfo.CurrentCulture.Name,
                                                                            Thread.CurrentThread.ManagedThreadId);
            foreach (var value in values)
                output += String.Format("{0}   ", value.ToString(formatString));

            output += Environment.NewLine;
            return output;
        };

        Console.WriteLine($"The example is running on thread {Thread.CurrentThread.ManagedThreadId}");
        // Make the current culture different from the system culture.
        Console.WriteLine($"The current culture is {CultureInfo.CurrentCulture.Name}");
        if (CultureInfo.CurrentCulture.Name == "fr-FR")
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        else
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");

        Console.WriteLine($"Changed the current culture to {CultureInfo.CurrentCulture.Name}.\n");

        // Execute the delegate synchronously.
        Console.WriteLine("Executing the delegate synchronously:");
        Console.WriteLine(formatDelegate());

        // Call an async delegate to format the values using one format string.
        Console.WriteLine("Executing a task asynchronously:");
        var t1 = Task.Run(formatDelegate);
        Console.WriteLine(t1.Result);

        Console.WriteLine("Executing a task synchronously:");
        var t2 = new Task<String>(formatDelegate);
        t2.RunSynchronously();
        Console.WriteLine(t2.Result);
    }
}