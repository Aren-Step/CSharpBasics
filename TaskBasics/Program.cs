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

        var getData = Task.Factory.StartNew(() => {
            Random rnd = new Random();
            int[] values = new int[100];
            for (int ctr = 0; ctr <= values.GetUpperBound(0); ctr++)
                values[ctr] = rnd.Next();

            return values;
        });
        var processData = getData.ContinueWith((x) =>
        {
            int n = x.Result.Length;
            long sum = 0;
            double mean;

            for (int ctr = 0; ctr <= x.Result.GetUpperBound(0); ctr++)
                sum += x.Result[ctr];

            mean = sum / (double)n;
            return Tuple.Create(n, sum, mean);
        });
        var displayData = processData.ContinueWith((x) => {
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
    }
}