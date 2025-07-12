using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static SerializationBasics.Run;

namespace SerializationBasics
{
    internal class Run
    {
        public class Person
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public List<Person> Friends { get; set; }
        }

        public static void Main()
        {
            var json = @"{""Name"":""John"", 
                    ""Age"":17, 
                    ""Friends"": 
                        [{""Name"":""Anna"",""Age"":16,""Friends"":null}, 
                         {""Name"":""Harry"",""Age"":20, ""Friends"":null}]}";

            var person = JsonSerializer.Deserialize<Person>(json);
            var result = $"{person.Name}:{person.Age} ";
            var friends = person.Friends.ToArray();
            foreach (var friend in friends)
            {
                result += $"{friend.Name}:{friend.Age} ";
            }
            //result = ? 
            Console.WriteLine(result);
        }
    }
}
