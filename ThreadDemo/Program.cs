using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace ThreadDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //SomethingAboutThreadPool();

            //MethodofQueueUserWorkItem();

            MethodofRegisterWaitForSingleObject();
            Console.ReadKey();
        }
        public static void SomethingAboutThreadPool()
        {
            int workerThreads;
            int completionPortsThreads;

            ThreadPool.GetMaxThreads(out workerThreads, out completionPortsThreads);
            Console.WriteLine("线程池中最大的线程数:{0},线程池中异步IO线程的最大数目{1}", workerThreads, completionPortsThreads);

            ThreadPool.SetMaxThreads(100, 100);
            ThreadPool.GetMaxThreads(out workerThreads, out completionPortsThreads);
            Console.WriteLine("修改 线程池中最大的线程数:{0},线程池中异步IO线程的最大数目{1}", workerThreads, completionPortsThreads);

            ThreadPool.GetMinThreads(out workerThreads, out completionPortsThreads);
            Console.WriteLine("线程池中最小的线程数：{0}，线程池中异步IO线程的最小数目{1}", workerThreads, completionPortsThreads);

            ThreadPool.SetMinThreads(1, 1);
            ThreadPool.GetMinThreads(out workerThreads, out completionPortsThreads);
            Console.WriteLine("修改 线程池中最小的线程数：{0}，线程池中异步IO线程的最小数目{1}", workerThreads, completionPortsThreads);


        }

        public static void MethodofQueueUserWorkItem() {
            Fish fish1 = new Fish();
            Fish fish2 = new Fish();
            ThreadPool.QueueUserWorkItem(fish1.Swing, "fish1");
            ThreadPool.QueueUserWorkItem(fish2.Swing, "fish2");
            ThreadPool.QueueUserWorkItem(fish2.Swing, "fish3");
            ThreadPool.QueueUserWorkItem(fish2.Swing, "fish4");
            ThreadPool.QueueUserWorkItem(fish2.Swing, "fish5");
            ThreadPool.QueueUserWorkItem(fish2.Swing, "fish6");
        }
        public static void MethodofRegisterWaitForSingleObject() {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            //  ThreadPool.RegisterWaitForSingleObject(autoResetEvent, Run1, null, Timeout.Infinite, false);无限长时间等待
            ThreadPool.RegisterWaitForSingleObject(autoResetEvent, Run1, null, 500, false);
            Console.WriteLine("时间:{0} 工作线程请注意，您需要等待5s才能执行", DateTime.Now);
            Thread.Sleep(5000);
            autoResetEvent.Set();
            Console.WriteLine("时间:{0}  工作线程已执行", DateTime.Now);
        }

        static void Run(object obj) {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("线程{0}, 输出{1},属于线程池：{2}", Thread.CurrentThread.ManagedThreadId, i,Thread.CurrentThread.IsThreadPoolThread);
            }
        }
        static void Run1(object obj, bool sign) {
            Console.WriteLine("时间:{0}  我是线程{1}", DateTime.Now, Thread.CurrentThread.ManagedThreadId);
        }
    }
    public class Fish{
        public void Swing(object obj){
            while (true) {
                Console.WriteLine("{0}  游泳",obj);
                Thread.Sleep(500);
            }
        }
    }
}
