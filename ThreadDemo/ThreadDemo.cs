using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
namespace ThreadsDemo
{
    public class ThreadDemo
    {
        public static void ShowTest()
        {
            SimpleThreadDemo simpleThreadDemo = new SimpleThreadDemo();
            Stopwatch stopWatch = new Stopwatch();
            int start = 100;
            int end = 100000;

            stopWatch.Start();
            long temp = simpleThreadDemo.Sum(start, end);
            stopWatch.Stop();
            Console.WriteLine("单线程求和：{0}  用时：{1}", temp, stopWatch.ElapsedMilliseconds);

            stopWatch.Restart();
            temp = simpleThreadDemo.SumThread(start, end);
            stopWatch.Stop();
            Console.WriteLine("多线程求和：{0}  用时：{1}", temp, stopWatch.ElapsedMilliseconds);
        }
    }

    public class SimpleThreadDemo
    {
        /// <summary>
        /// 单线程求和
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public long Sum(int start, int end)
        {
            long sum = 0;
            for (long i = start; i < end + 1; i++)
            {
                sum += i;
            }
            return sum;

        }
        /// <summary>
        /// 多线程求和
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public long SumThread(long start, long end)
        {
            long temp = (end - start + 1) / 2;
            List<ManualResetEvent> manualEventList = new List<ManualResetEvent>();
            Thread[] threads = new Thread[2];
            threads[0] = new Thread(new ParameterizedThreadStart(Run));
            ManualResetEvent manualResetEvent1 = new ManualResetEvent(false);
            Param para1 = new Param() { start = start, end = temp, manualResetEvent = manualResetEvent1 };
            manualEventList.Add(manualResetEvent1);
            threads[0].Start(para1);

            threads[1] = new Thread(new ParameterizedThreadStart(Run));
            ManualResetEvent manualResetEvent2 = new ManualResetEvent(false);
            Param para2 = new Param() { start = temp+1, end = end, manualResetEvent = manualResetEvent2 };
            manualEventList.Add(manualResetEvent2);
            threads[1].Start(para2);

            WaitHandle.WaitAll(manualEventList.ToArray());
            return sums;

        }
        object obj = new object();
        long sums = 0;
        public void Run(object obj)
        {
            Param param = (Param)obj;
            long sum = 0;
            Console.WriteLine("线程：{0} start:{1} end:{2}", Thread.CurrentThread.ManagedThreadId, param.start, param.end);
            for (long i = param.start; i < param.end + 1; i++)
            {
                sum += i;

            }
            lock (obj)
            {
                sums += sum;
            }
            param.manualResetEvent.Set();
        }

    }
    public class Param
    {
        public long start;
        public long end;
        public ManualResetEvent manualResetEvent;
    }
}
