using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Security.Cryptography.X509Certificates;

namespace CSharpBasics
{
    class Program
    {
        public class A
        {
            public string Concat<T>(T item1, T item2)
            {
                string concat = item1.ToString() + item2.ToString();
                return concat;
            }
        }

        public struct Employee
        {
            public Employee() { }
            public Employee(string name, int id) => (Name, Id) = (name, id);
            public string? Name { get; set; }
            public int Id { get; set; }
            
        }
        // Driver Code
        static void Main(string[] args)
        {
            #region Dictionary
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


            Console.WriteLine("Hello nice people!");
            
            
            ////////////////////////////////////////////////////////////
            // write a console app
            // input numbers in dynamic list
            // input the necessary number to find
            // count steps for finding the specific number
            ////////////////////////////////////////////////////////////

        }
    }
}