using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace ExceptionBasics
{
    public class NegativeArgumentException : ArithmeticException
    {
        public NegativeArgumentException(string? message) : base(message)
        {

        }
    }
    class ExceptionBasics
    {
        //static void swap<T>(ref T a, ref T b)
        //{
        //    T temp = a;
        //    a = b;
        //    b = temp;
        //}
        static int factorial(int n)
        {
            if (n < 0)
                throw new NegativeArgumentException("Please Input an unsigned integer.");
            if (n == 1 || n == 0)
                return 1;
            return n * factorial(n - 1);
        }
        //static int RemoveElement(int[] nums, int val)
        //{
        //    int k = nums.Length;
        //    for (int i = 0; i < k; i++)
        //    {
        //        if (nums[i] == val)
        //        {
        //            swap(ref nums[i], ref nums[k - 1]);
        //            k--;
        //        }
        //    }
        //    return k;
        //}
        static void Main(string[] args)
        {
            {
                //int[] nums = new int[7];
                //for (int i = 0; i < nums.Length; i++)
                //{
                //    Console.Write($"Input number {i}: ");
                //    nums[i] = Convert.ToInt32(Console.ReadLine());
                //}
                //Console.Write("Input number that needs to be removed: ");
                //int val = Convert.ToInt32(Console.ReadLine());
                //int k = RemoveElement(nums, val);
                //Console.WriteLine("Array after removing " + val + ": ");
                //for (int i = 0; i < nums.Length; i++)
                //    Console.Write(nums[i] + (i == k - 1 ? "\n" : " "));
                //Console.WriteLine("Number of elements after removing {0}: {1}", val, k);
            }

            int num1 = 6, num2 = 10;
            try
            {
                Console.WriteLine($"{num1}! = {factorial(num1)}");
                Console.WriteLine(num1 / num2);
                throw new Exception("Exception from try block is thrown!");
            }
            catch (NegativeArgumentException)
            {
                Console.WriteLine("Negative argument!");
            }
            catch (Exception)
            {
                Console.WriteLine("Divided by zero!");
            }
            //finally
            //{
            //   //throw new Exception("Exception from finally block is thrown!");
            //}
        }
    }
}