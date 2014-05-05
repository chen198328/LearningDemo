using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateAndEvent
{
    public class DelegateDemo
    {
        public delegate int[] SortMethod(int[] array);
        public delegate void Log();
        public void Sort(ref int[] array, SortMethod method)
        {
            Log log = new Log(TimeLog);
            log += ContentLog;
            if (method != null)
            {
                array = method(array);
                log();
            }

        }
        public void TimeLog()
        {

            Console.WriteLine(DateTime.Now);
        }
        public void ContentLog()
        {
            Console.WriteLine("排序算法");
        }

    }
}
