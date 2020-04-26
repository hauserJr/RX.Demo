using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RX.Demo
{
    public class CustomObserver
    {
        private static object _objectLock = new object();
        /// <summary>
        /// 上班看股票的同事
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class Workmate_WatchStock<T> : IObserver<T> where T : BossAlert
        {
            public void OnCompleted()
            {
                Console.WriteLine($"=====OnCompleted Thread：{Thread.CurrentThread.ManagedThreadId}=====");
            }

            public void OnError(Exception error)
            {
                Console.WriteLine($"=====OnError Thread：{Thread.CurrentThread.ManagedThreadId}=====");
            }

            public void OnNext(T value)
            {
                var bossAlert = (BossAlert)value;
                var result = !bossAlert.IsComing ? "老闆還沒來繼續看Stock" : "老闆來了快關掉Stock,假裝工作中 ...";
                Word.Display(Thread.CurrentThread.ManagedThreadId, result, bossAlert.IsLock);
            }
        }

        /// <summary>
        /// 上班看NBA的同事
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class Workmate_WatchNBA<T> : IObserver<T> where T : BossAlert
        {
            public void OnCompleted()
            {
                Console.WriteLine($"=====OnCompleted Thread：{Thread.CurrentThread.ManagedThreadId}=====");
            }

            public void OnError(Exception error)
            {
                Console.WriteLine($"=====OnError Thread：{Thread.CurrentThread.ManagedThreadId}=====");
            }

            public void OnNext(T value)
            {
                var bossAlert = (BossAlert)value;
                var result = !bossAlert.IsComing ? "老闆還沒來繼續看NBA" : "老闆來了快關掉NBA,假裝工作中 ...";
                Word.Display(Thread.CurrentThread.ManagedThreadId, result, bossAlert.IsLock);
            }
        }

        /// <summary>
        /// 上班看MLB的同事
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class Workmate_WatchMLB<T> : IObserver<T> where T : BossAlert
        {
            public void OnCompleted()
            {
                Console.WriteLine($"=====OnCompleted Thread：{Thread.CurrentThread.ManagedThreadId}=====");
            }

            public void OnError(Exception error)
            {
                Console.WriteLine($"=====OnError Thread：{Thread.CurrentThread.ManagedThreadId}=====");
            }

            public void OnNext(T value)
            {
                var bossAlert = (BossAlert)value;
                var result = !bossAlert.IsComing ? "老闆還沒來繼續看MLB" : "老闆來了快關掉MLB,假裝工作中 ...";
                Word.Display(Thread.CurrentThread.ManagedThreadId, result, bossAlert.IsLock);
            }
        }

        public static class Word
        {
            public static void Display(int threadId, string value, bool isLock)
            {
                if (isLock)
                {
                    lock (_objectLock)
                    {// 避免多個執行緒同時執行lock內的程式, 一次只允許一個執行緒, 其他需要使用的則需列隊等待封鎖
                        // 模擬很多事情要做,所以延遲3秒
                        Thread.Sleep(3000);
                        Console.WriteLine($"\r\nOnNext Thread：{threadId} \r\n{value} \r\nLock：{isLock}");
                    }
                }
                else
                {
                    Console.WriteLine($"\r\nOnNext Thread：{threadId} \r\n{value} \r\nLock：{isLock}");
                    Console.WriteLine($"--------");
                }
                
            }
        }
    }
}
