using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using static RX.Demo.CustomObserver;

namespace RX.Demo
{
    public class Program
    {
        static void Main(string[] args)
        {
            // 主執行
            Console.WriteLine($"\r\n\r\nStart On Thread：{ Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"====================================================");

            // 資料源
            var data = new List<BossAlert>();

            /*
             * RX 作法
             */

            // 將資料源宣告為資料提供者
            IObservable<BossAlert> observableOfInts = data.ToObservable();
            
            // 宣告觀察者
            IObserver<BossAlert> workMate_1 = new Workmate_WatchStock<BossAlert>();
            IObserver<BossAlert> workMate_2 = new Workmate_WatchNBA<BossAlert>();
            IObserver<BossAlert> workMate_3 = new Workmate_WatchMLB<BossAlert>();

            /*
             * 一個提供者可以有多個訂閱
             * 但一個執行緒只能有一個訂閱
             */
            IDisposable subScription_1 = observableOfInts
                // SubscribeOn 多執行緒
                .SubscribeOn(NewThreadScheduler.Default)
                .Subscribe(workMate_1);

            IDisposable subScription_2 = observableOfInts
                // SubscribeOn 多執行緒
                .SubscribeOn(NewThreadScheduler.Default)
                .Subscribe(workMate_2);

            IDisposable subScription_3 = observableOfInts
                // SubscribeOn 多執行緒
                .SubscribeOn(NewThreadScheduler.Default)
                .Subscribe(workMate_3);

            /* 
             * 嘗試變更資料
             * 觸發OnNext()
             * 多執行緒沒有Lock
             */
            data.Add(new BossAlert()
            {
                isComing = false,
                isLock = false
            });

            // 多執行緒有Lock
            data.Add(new BossAlert()
            {

                isComing = true,
                isLock = true
            });
            Console.ReadLine();


            // 關閉
            subScription_1.Dispose();
            subScription_2.Dispose();
            subScription_3.Dispose();

            // 按下任意鍵離開
            Console.ReadKey();
        }
    }
}
