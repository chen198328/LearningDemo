using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresandAlgorithms
{
    public class InsertSorter : ISort
    {
        #region ISort 成员

        int[] ISort.Sort(int[] array)
        {
            int index = 0;
            int temp = 0;
            for (int i = 1; i < array.Length; i++)
            {
                temp = array[i];
                index = i;
                while (index > 0 && array[index - 1] >= temp)
                {
                    array[index] = array[index - 1];
                    index -= 1;
                }
                array[index] = temp;
            }
            return array;
        }

        string ISort.GetType()
        {
            return "插入排序算法";
        }

        #endregion
    }
}
