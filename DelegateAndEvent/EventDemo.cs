using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateAndEvent
{
    public class EventDemo
    {
        public delegate int[] SortMethod(int[] array);

        public event SortMethod Sorthandler;
        public void Sort(ref int[] array)
        {
            if (Sorthandler != null)
            {
                array = Sorthandler(array);
            }
        }
      

    }
}
