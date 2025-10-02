using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices.Marshalling;

namespace CSharpBasics
{
    internal enum Colors
    {
        red = 1000,
        green,
        blue,
        yellow,
    }
    class Program
    {
        public abstract class A
        {
            private enum Cars
            {
                Bmw,
                Mercedes,
                Audi,
                Tesla,
                Toyota,
            }
            public string Concat<T>(T item1, T item2)
            {
                string concat = item1?.ToString() + item2?.ToString() ?? "nothing";
                return concat;
            }
        }
        public class B : A
        {
            B() 
            {
                Console.WriteLine("Constructor called");
            }
            public string Concat<T>(T item1, T item2, T item3)
            {
                return item1?.ToString() + item2?.ToString() + item3?.ToString();
            }
        }

        public struct Employee
        {
            public Employee() { }
            public Employee(string name, int id) => (Name, Id) = (name, id);
            public string? Name { get; set; }
            public int Id { get; set; }
            public static int SomeMethod() { return 0; }
            public readonly int GetId()
            {
                return Id;
            }
        }

        
        #region Events
        public delegate void EventHandlerDelegate();

        public class Publisher
        {
            public event EventHandlerDelegate EventMember;

            public void EventRaiser()
            {
                Console.WriteLine("Raising an event...");
                EventMember?.Invoke();
            }
        }

        public class Subscriber
        {
            public Subscriber(Publisher pb)
            {
                Console.WriteLine("Subscribing all the methods to the event...");
                pb.EventMember += EventHandlerMethod1;
                pb.EventMember += EventHandlerMethod2;
            }

            public static void EventHandlerMethod1()
            {
                Console.WriteLine("Event Handler 1 called!");
            }
            public static void EventHandlerMethod2()
            {
                Console.WriteLine("Event Handler 1 called!");
            }
            #endregion


        public static IEnumerable GetDigits()
        {
            int digit = 0;
            while (digit < 10) 
            {
                yield return digit++;
            }
        }
        public delegate IEnumerable GetDigitsDelegate();
        // Driver Code

        static void Main(string[] args)
        {

            #region Dictionary, Stack and Queue
            //Dictionary<int, string> mydic = new Dictionary<int, string>()
            //{
            //    {1, "Davit Stepanyan" },
            //    {2, "Oksana Kutikova" }
            //};
            //mydic.Add(123, "Aren Stepanyan");
            //mydic.Add(124, "Arman Stepanyan");


            //foreach (KeyValuePair<int, string> element1 in mydic)
            //{
            //    Console.WriteLine($"Person with ID {element1.Key} is {element1.Value}");
            //}

            #region finding key/value
            //int i = Convert.ToInt32(Console.ReadLine());
            //string n = Console.ReadLine();
            //if (mydic.ContainsKey(i) == true)
            //{
            //    Console.WriteLine($"the ID {i} corresponds to the name {mydic.ElementAt(123)}");
            //}
            //else
            //{
            //    Console.WriteLine("there is no person with that ID");
            //}
            //if (mydic.ContainsValue(n) == true)
            //{
            //    Console.WriteLine($"there is a person named {n} that has an ID");
            //}
            //else
            //{
            //    Console.WriteLine("there is no person with that name");
            //}
            #endregion

            //Stack<Employee> stack = new Stack<Employee>();
            //stack.Push(new Employee { Id = 1 });
            //stack.Push(new Employee { Id = 2 });
            //stack.Push(new Employee { Id = 3 });
            //stack.Push(new Employee { Id = 4 });
            //foreach (var element in stack)
            //{
            //    Console.WriteLine($"Stack Employee with ID {element.Id}");
            //}

            //Queue<Employee> queue = new Queue<Employee>();
            //queue.Enqueue(new Employee { Id = 1 });
            //queue.Enqueue(new Employee { Id = 2 });
            //foreach (var element in queue)
            //{
            //    Console.WriteLine($"Queue Employee with ID {element.Id}");
            //}

            //LinkedList<Employee> list = new LinkedList<Employee>();
            #endregion

            //A obj = new A();
            //obj.Concat<int>();

            //bool in24format = false;
            //string clock = DateTime.Now.ToString(in24format ? "HH:mm:ss" : "hh:mm:ss tt");
            //Console.WriteLine(clock);

            //GetDigitsDelegate getDigitsDel = GetDigits;

            //foreach (var digit in getDigitsDel())
            //{
            //    Console.WriteLine(digit);
            //}

            string str1 = "Hello", str2 = String.Concat(string.Empty + str1);
            Console.WriteLine(Object.ReferenceEquals(str1, str2));

            Console.WriteLine(Colors.red);

            var input = "100";
            if (int.TryParse(input, out int result))
                Console.WriteLine(result);
        }
    }
}
