using System.Security.AccessControl;

namespace AsyncBasics
{
    class Program
    {
        internal class HashBrown { }
        internal class Coffee { }
        internal class Egg { }
        internal class Juice { }
        internal class Toast { }

        public static async Task Main()
        {
            Coffee cup = PourCoffee();
            Console.WriteLine("coffee is ready");

            var eggsTask = FryEggsAsync(2);
            var hashBrownTask = FryHashBrownsAsync(3);
            var toastTask = MakeToastWithButterAndJamAsync(2);

            var eggs = await eggsTask;
            Console.WriteLine("eggs are ready");

            var hashBrown = await hashBrownTask;
            Console.WriteLine("hash browns are ready");

            var toast = await toastTask;
            Console.WriteLine("toast is ready");

            Juice oj = PourOJ();
            Console.WriteLine("oj is ready");
            Console.WriteLine("Breakfast is ready!");
        }

        private static Coffee PourCoffee()
        {
            Console.WriteLine("Pouring coffee");
            return new Coffee();
        }
        private static Task<Egg> FryEggsAsync(int howMany)
        {
            Console.WriteLine("Warming the egg pan...");
            Task.Delay(3000).Wait();
            Console.WriteLine($"cracking {howMany} eggs");
            Console.WriteLine("cooking the eggs ...");
            Task.Delay(3000).Wait();
            Console.WriteLine("Put eggs on plate");

            return new Task<Egg>(() => new Egg());
        }
        private static Task<HashBrown> FryHashBrownsAsync(int patties)
        {
            Console.WriteLine($"putting {patties} hash brown patties in the pan");
            Console.WriteLine("cooking first side of hash browns...");
            Task.Delay(3000).Wait();
            for (int patty = 0; patty < patties; patty++)
            {
                Console.WriteLine("flipping a hash brown patty");
            }
            Console.WriteLine("cooking the second side of hash browns...");
            Task.Delay(3000).Wait();
            Console.WriteLine("Put hash browns on plate");

            return new Task<HashBrown>(() => new HashBrown());
        }
        static async Task<Toast> MakeToastWithButterAndJamAsync(int number)
        {
            var toast = await ToastBreadAsync(number);
            ApplyButter(toast);
            ApplyJam(toast);

            return toast;
        }
        private static Task<Toast> ToastBreadAsync(int slices)
        {
            for (int slice = 0; slice < slices; slice++)
            {
                Console.WriteLine("Putting a slice of bread in the toaster");
            }
            Console.WriteLine("Start toasting...");
            Task.Delay(3000).Wait();
            Console.WriteLine("Remove toast from toaster");

            return new Task<Toast>(() => new Toast());
        }
        private static void ApplyButter(Toast toast) =>
            Console.WriteLine("Putting butter on the toast");

        private static void ApplyJam(Toast toast) =>
            Console.WriteLine("Putting jam on the toast");

        private static Juice PourOJ()
        {
            Console.WriteLine("Pouring orange juice");
            return new Juice();
        }
    }
}