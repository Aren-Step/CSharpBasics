using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpBasics
{
    public enum Colors
    {
        None = 0,
        Red = 1,
        Green = 2,
        Blue = 3
    }

    public class MyClass<T>
    {
        public int length { get; set; } = 7;
        public T Data { get; set; }
        public MyClass() { }

        public MyClass(T t) { Data = t; }

        public string GetDataTypeName()
        {
            return Data.GetType().Name;
        }
    }

    class Program
    {
        static async Task DoSomeWork()
        {
            await Task.Delay(500);
            Console.WriteLine("Thread of DoSomeWork id: " + Thread.CurrentThread.ManagedThreadId + ", IsThreadPoolThread: " + Thread.CurrentThread.IsThreadPoolThread);
            Console.WriteLine("DoSomeWork task is compleated!!!");
        }

        static void Main(string[] args)
        {
            //var obj1 = new MyClass<String>("ABC");
            //var obj2 = new MyClass<DateTime>(DateTime.Now);
            //var obj3 = new MyClass<Enum>(Colors.Red);
            //Console.WriteLine(obj1.GetDataTypeName());
            //Console.WriteLine(obj2.GetDataTypeName());
            //Console.WriteLine(obj3.GetDataTypeName());

            //PropertyInfo[] props = obj3.GetType().GetProperties();
            //foreach (var p in props) 
            //    Console.WriteLine($"Property name: {p.Name}, value: {p.GetValue(obj3)}");

            ////Console.WriteLine(string.Join(',', obj3.GetType().GetFields().Select(x => x.GetValue(obj3))));

            //Console.WriteLine(obj1.GetType().GetMethod("GetDataTypeName").Invoke(obj1, null));

            //var obj = Activator.CreateInstance(typeof(MyClass<Enum>));

            //Console.WriteLine(obj.ToString());
            ///*
            // {"Data": "ABC"}

            // <stringObj>
            //    <lenght>1</lenght>
            //    <Data>"ABC"</Data>
            //</stringObj>

            //*/


            //Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(obj1));

            //DoSomeWork();

            Console.WriteLine("CurrentThread id: " + Thread.CurrentThread.ManagedThreadId + ", IsThreadPoolThread: " + Thread.CurrentThread.IsThreadPoolThread);
            Task.Run(async () =>
            {
                Console.WriteLine("Thread of Run id (before): " + Thread.CurrentThread.ManagedThreadId + ", IsThreadPoolThread: " + Thread.CurrentThread.IsThreadPoolThread);
                var newObj = System.Text.Json.JsonSerializer.Deserialize<MyClass<Colors>>("{\"Data\": 2, \"length\":5}");
                await DoSomeWork().ConfigureAwait(false);
                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(newObj));
                Console.WriteLine("Thread of Run id (after): " + Thread.CurrentThread.ManagedThreadId + ", IsThreadPoolThread: " + Thread.CurrentThread.IsThreadPoolThread);
            });

            Console.WriteLine("Main thread is compleated!!!");

            Console.Read();
        }
    }
}
