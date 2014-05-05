using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateAndEvent
{
    public class Program
    {

        static void Main(string[] args)
        {
            #region DelegateDemo
            DelegateDemo demo = new DelegateDemo();
            int[] array = new int[] { 3, 2, 3, 5, 6, 8, 9 };

            demo.Sort(ref array, SortMethod);
            Ouput(array);
            #endregion
            EventDemo eventDemo = new EventDemo();
            array = new int[] { 3, 4, 3, 6, 9, 10 };
            eventDemo.Sorthandler += eventDemo_Sorthandler;
            eventDemo.Sort(ref array);
            Ouput(array);
            #region

            #endregion
            Console.ReadKey();
        }
        static void Ouput(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }
            Console.WriteLine();
        }
        static int[] eventDemo_Sorthandler(int[] array)
        {
            return SortMethod(array);
        }
        public static int[] SortMethod(int[] array)
        {
            List<int> list = new List<int>(array.Length);
            for (int i = 0; i < array.Length; i++)
            {
                list.Add(array[i]);
            }
            list.Sort();
            for (int i = 0; i < list.Count; i++)
            {
                array[i] = list[i];
            }
            return array;
        }
    }
}
