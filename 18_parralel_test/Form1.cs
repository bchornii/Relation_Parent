using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Concurrent;

namespace _18_parralel_test
{
    public partial class Form1 : Form
    {
        SemaphoreSlim mutext = new SemaphoreSlim(1);
        int value = 4;
        CancellationTokenSource cts = null;

        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string filePath = @"D:\test_file.txt";
            string[] files = new string[]
            {
                @"D:\test_file.txt",
                @"D:\test_file.txt",
                @"D:\test_file.txt"
            };

            if (File.Exists(filePath) == false)
            {
                Debug.WriteLine("file not found: " + filePath);
            }
            else
            {
                // try
                {
                    //string text = await ReadTextAsync(filePath);

                    //StringBuilder sb = new StringBuilder();
                    //foreach (var file in files)
                    //{
                    //    string text = await ReadTextAsync(file);
                    //    sb.Append(text);
                    //}
                    //Debug.WriteLine(sb.ToString());

                    //using (HttpClient client = new HttpClient())
                    //{
                    //    string s = await client.GetStringAsync("http://csharpindepth.com");
                    //    Debug.WriteLine(s);
                    //}

                    //await Task.Delay(1000);

                    //List<Task<int>> list = new List<Task<int>>();
                    //list.Add(BookCar());
                    //list.Add(BookHotel());
                    //list.Add(BookPlane());

                    //var res = await Task.WhenAll(list.ToArray());
                    //foreach (var item in res)
                    //{
                    //    MessageBox.Show(item.ToString());
                    //}

                    Task<int> t = LenOfHello();
                    MessageBox.Show("From event handler.");
                    int len = await t;
                    MessageBox.Show(len.ToString());

                    // провірка викидування ексепшина з метода - якщо він викинений в методі async void - він прокидується 
                    // в контекст; якщо в методі async Task - залишається в обєкті Task
                    //try
                    //{
                    //    MethodWithException();
                    //}
                    //catch(Exception ex) { MessageBox.Show(ex.Message); }

                    // Перевірка метода з CancellationToken
                    // Якщо було викинено ексепшин в асинхронному методі через завершення метода через CancellationToken
                    // Task завершується в статусі Cancelled. При всіх інших ексепшинах таск завершиться в статусі Faulted.
                    //cts = new CancellationTokenSource();
                    //Task<int> t = null;
                    //try
                    //{
                    //    t = CalculateAsync(1000000000, cts.Token);
                    //    await t;
                    //    MessageBox.Show(t.Status.ToString());
                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show(t.Exception.Message);
                    //    MessageBox.Show(ex.Message + " : " + ex.GetType().ToString());
                    //    MessageBox.Show(t.Status.ToString());
                    //}

                    // I/O bound task based on TaskCompletionSource<bool>
                    //Task<bool> t = DelayedActivityAsync(3000);
                    //await t;
                    //MessageBox.Show(t.Status.ToString()); 

                    // Progress
                    //Progress<int> progress = new Progress<int>(val => progressBar1.Value = val);                    
                    //await Poll(1000, progress);

                    // Check for errors when using configureAwait = false with GUI
                    //await UpdateGUI(40);

                    // Async cache test
                    //await AsyncCacheCheck();
                }
                //catch (Exception ex)
                {
                    //  Debug.WriteLine(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }

        private async Task<int> BookCar()
        {
            await Task.Delay(1000);
            return 1;
        }

        private async Task<int> BookPlane()
        {
            await Task.Delay(2000);
            return 2;
        }

        private async Task<int> BookHotel()
        {
            await Task.Delay(3000);
            return 3;
        }

        public async Task MultipleAwait()
        {
            await Task.Delay(1000);
            await Task.Delay(1000);
            await new HttpClient().GetStringAsync("http://google.com");
        }

        public async void MethodWithException()
        {
            throw new InvalidOperationException();
        }

        public async Task MethodWithLock()
        {
            await mutext.WaitAsync().ConfigureAwait(continueOnCapturedContext: false);
            try
            {
                await SomeDelay();
            }
            finally
            {
                mutext.Release();
            }
        }

        public async Task SomeDelay()
        {
            MessageBox.Show(Thread.CurrentThread.ManagedThreadId.ToString());
            await Task.Delay(1000)
                      .ConfigureAwait(continueOnCapturedContext: false);
            MessageBox.Show(Thread.CurrentThread.ManagedThreadId.ToString());
        }

        public void SomeLambda(IEnumerable<string> source)
        {
            var res = source.Select(async url =>
            {
                using (HttpClient client = new HttpClient())
                {
                    await client.GetStringAsync(url);
                }
            }).ToList();
            //await Task.WhenAll(res);
        }

        private async Task<int> LenOfHello()
        {
            Stopwatch w = Stopwatch.StartNew();
            Task<string> t = HelloWord();
            MessageBox.Show("During delay await we do some stuff");
            Thread.Sleep(5000);
            w.Stop();
            MessageBox.Show("Elapsed time : " + (w.ElapsedMilliseconds / 1000.0).ToString());
            MessageBox.Show("Some work is doing...");
            var hello = await t;
            return hello.Length;
        }

        private async Task<string> HelloWord()
        {
            MessageBox.Show("Hello from hello word!");
            await Task.Delay(1000).ConfigureAwait(continueOnCapturedContext: false);
            MessageBox.Show("Hello from hello word : end!");
            return "hello";
        }

        /// <summary>
        /// Method for slow calculations based on Task.Run
        /// </summary>
        /// <param name="max_val"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<int> CalculateAsync(int max_val, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                int i;
                for (i = 0; i < max_val; i++)
                {
                    throw new ArgumentNullException();
                    cancellationToken.ThrowIfCancellationRequested();
                }
                return i;
            }, cancellationToken);
        }

        /// <summary>
        /// timeout - provides delay in milliseconds
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public Task<bool> DelayedActivityAsync(int timeout)
        {
            TaskCompletionSource<bool> tcs = null;
            System.Threading.Timer timer = null;

            timer = new System.Threading.Timer(state =>
            {
                timer.Dispose();
                tcs.SetResult(true);
            }, null, Timeout.Infinite, Timeout.Infinite);
            tcs = new TaskCompletionSource<bool>(timer);
            timer.Change(timeout, Timeout.Infinite);
            return tcs.Task;
        }

        /// <summary>
        /// Method with progress (IProgress<int>)
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public async Task Poll(int timeout ,IProgress<int> progress)
        {
            int val = 0;
            while (true)
            {
                await Task.Delay(timeout);                          
                val++;
                progress.Report(val);
            }
        }

        public async Task UpdateGUI(int val)
        {
            await Task.Delay(1000)
                      .ConfigureAwait(continueOnCapturedContext: true); // при false - вилітає ексепшин про те що оновлення з двох потоків
            progressBar1.Value = val;            
        }

        /// <summary>
        /// Wait only one of task subset and returns its ret value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methods"></param>
        /// <returns></returns>
        private async Task<T> NeedOnlyOne<T>(params Func<CancellationToken,Task<T>>[] methods)
        {
            var cts = new CancellationTokenSource();
            var tasks = methods.Select(m => m(cts.Token)).ToArray();
            var completed = await Task.WhenAny(tasks);
            cts.Cancel();
            return await completed;
        }      
        
        /// <summary>
        /// Retry on fault 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="function"></param>
        /// <param name="maxTries"></param>
        /// <returns></returns>
        public async Task<T> RetryOnFault<T>(Func<Task<T>> function, int maxTries)
        {
            for (int i = 0; i < maxTries; i++)
            {
                try { return await function().ConfigureAwait(continueOnCapturedContext: false); }
                catch { if (i == maxTries - 1) throw; }
            }
            return default(T);
        }
        
        /// <summary>
        /// Wait all and return result for all or set exception
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tasks"></param>
        /// <returns></returns>        
        public Task<T[]> WhenAllOrFirstException<T>(IEnumerable<Task<T>> source)
        {
            var task_list = source.ToList();
            var ce = new CountdownEvent(task_list.Count);
            var tsc = new TaskCompletionSource<T[]>();
            Action<Task> onCompleted = t =>
            {
                if (t.IsFaulted)
                {
                    tsc.TrySetException(t.Exception.InnerException);
                }
                if(ce.Signal() && !tsc.Task.IsCompleted)
                {
                    tsc.TrySetResult(task_list.Select(task => task.Result).ToArray());
                }
            };
            task_list.ForEach(t => t.ContinueWith(onCompleted));
            return tsc.Task;
        }

        /// <summary>
        /// Test async cache type
        /// </summary>
        /// <returns></returns>
        public async Task AsyncCacheCheck()
        {
            AsyncCache<string, string> _cache = new AsyncCache<string, string>(IntegerDelayAsync);

            MessageBox.Show("Press ok to start...1");
            var t = _cache["hello"];
            MessageBox.Show(await t);

            MessageBox.Show("Press ok to start...2");
            t = _cache["hello"];
            MessageBox.Show(await t);

            MessageBox.Show("Press ok to start...3");
            t = _cache["hello"];
            MessageBox.Show(await t);


            MessageBox.Show("Press ok to start...4");
            t = _cache["world"];
            MessageBox.Show(await t);

            MessageBox.Show("Press ok to start...5");
            t = _cache["world"];
            MessageBox.Show(await t);

            MessageBox.Show("Press ok to start...6");
            t = _cache["world"];
            MessageBox.Show(await t);

        }

        public async Task<string> IntegerDelayAsync(string s)
        {                        
            await Task.Delay(2000);
            return s;
        }

        private async Task<string> ReadTextAsync(string filePath)
        {
            using (FileStream sourceStream = new FileStream(filePath,
                FileMode.Open, FileAccess.Read, FileShare.Read,
                bufferSize: 4096, useAsync: true))
            {
                StringBuilder sb = new StringBuilder();

                byte[] buffer = new byte[0x1000];
                int numRead;
                while ((numRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string text = Encoding.Unicode.GetString(buffer, 0, numRead);
                    sb.Append(text);
                }

                return sb.ToString();
            }
        }
    }

    class AsyncCache<TKey,TValue>
    {
        private readonly Func<TKey, Task<TValue>> _valueFactory;
        private readonly ConcurrentDictionary<TKey, Lazy<Task<TValue>>> _cache;

        public AsyncCache(Func<TKey,Task<TValue>> _func)
        {
            _valueFactory = _func;
            _cache = new ConcurrentDictionary<TKey, Lazy<Task<TValue>>>();
        }

        public Task<TValue> this[TKey key]
        {
            get
            {
                return _cache.GetOrAdd(key, tkey => new Lazy<Task<TValue>>(() => _valueFactory(tkey))).Value;
            }
        }

        public bool ClearCache(TKey key)
        {
            Lazy<Task<TValue>> _out_val = null;
            return _cache.TryRemove(key, out _out_val);
        }   
    }
}
