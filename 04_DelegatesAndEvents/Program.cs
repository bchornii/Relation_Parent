using System;
using System.Runtime.Remoting.Messaging;
using System.Threading;

// 1.delegate has two property : method and target(object which represents instance of class where method
//   is located); target is NULL for static method passed to delegate instance
// 2.If a delegate type is declared to return a value (i.e. it's not declared with a void return type) 
//   and a combined delegate instance is called, the value returned from that call is the one returned 
//   by the last simple delegate in the list.
// 3.Events are pairs of methods, appropriately decorated in IL to tie them together and let languages 
//   know that the methods represent events
// 4.Now we know what they are, what's the point of having both delegates and events? The answer is encapsulation
//   4.1. You can't call event from calling method
//   4.2. You can't use '=' operation for event
//   4.3. You can use 'add'/'remove' properties for some logic (like thread-safe)
//   4.4. You can't use delegate in interface
//   4.5. You can use add/remove methods when deal with explicitly implementing of interface (when you
//        have two interfaces with the same events)

namespace _04_DelegatesAndEvents
{
    public delegate string FirstDelegate(int x);

    public delegate int SampleDelegate(string data);

    class ClassWithEvents
    {
        private readonly object someLockObject = new object();

        private FirstDelegate firstEvent;
        public event FirstDelegate FirstEvent
        {
            add
            {
                lock (this)
                {
                    firstEvent += value;
                }
            }
            remove
            {
                lock (this)
                {
                    firstEvent -= value;
                }
            }
        }
        public void CallEvent(int x)
        {
            if (firstEvent != null)
            {
                Console.WriteLine(firstEvent(x));
            }
        }

        // -- thread safe event
        FirstDelegate someThreadSafeEvent;
        public event FirstDelegate SomeThreadSafeEvent
        {
            add
            {
                lock (someLockObject)
                {
                    someThreadSafeEvent += value;
                }
            }
            remove
            {
                lock (someLockObject)
                {
                    someThreadSafeEvent -= value;
                }
            }
        }
        protected virtual void CallThreadSafeEvent(int x)
        {
            FirstDelegate handler;
            lock (someLockObject)
            {
                handler = someThreadSafeEvent;
            }
            if (handler != null)
            {
                Console.WriteLine(handler(x));
            }
        }
        public void CallEvent_TS(int x)
        {
            CallThreadSafeEvent(x);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // class instance
            Program p = new Program();

            // -- delegates             
            //FirstDelegate del = new FirstDelegate(p.Hello);
            //FirstDelegate del2 = new FirstDelegate(p.Hello);
            //FirstDelegate del3 = new FirstDelegate(p.Hello);

            //FirstDelegate del_comb = del + del2 + del3;
            //Console.WriteLine(del_comb(23));

            //Console.WriteLine(del(10));
            //Console.WriteLine(del(12));
            //Console.WriteLine(del(23));
            //Console.WriteLine(del.Method.Name);
            //Console.WriteLine(del.Target.ToString());

            // -- events
            //ClassWithEvents cwe = new ClassWithEvents();
            //cwe.FirstEvent += firstEventHandler;
            //cwe.CallEvent(34);

            //cwe.SomeThreadSafeEvent += SomeThreadSafeEventHandler;
            //cwe.CallEvent_TS(989);  

            //Console.WriteLine(new string('-', 80));
            //cwe.SomeThreadSafeEvent -= SomeThreadSafeEventHandler;
            //cwe.CallEvent_TS(989);

            // -- async delegates   
            SampleDelegate counter = new SampleDelegate(CountCharacters);
            SampleDelegate parser = new SampleDelegate(Parse);
            // EndInvoke blocks main thread and wait for result            
            //IAsyncResult counterResult = counter.BeginInvoke("hello", null, null);
            //IAsyncResult parserResult = parser.BeginInvoke("20", null, null);
            //Console.WriteLine("Main thread continued.");

            //Console.WriteLine("Counter returned : {0}",counter.EndInvoke(counterResult));
            //Console.WriteLine("Parser returned : {0}",parser.EndInvoke(parserResult));

            // EndInvoke not blocks main thread
            AsyncCallback callBack = new AsyncCallback((result) =>
            {
                string format = result.AsyncState as string;
                AsyncResult delefateResult = result as AsyncResult;
                SampleDelegate delegateInstance = delefateResult.AsyncDelegate as SampleDelegate;

                if(delegateInstance != null)
                {
                    Console.WriteLine(format,delegateInstance.EndInvoke(result));
                }
            });

            counter.BeginInvoke("hello", callBack, "Counter returned {0}");
            parser.BeginInvoke("10", callBack, "Parser returned {0}");

            Console.WriteLine("Main thread continued.");

            Console.WriteLine("Done.");

            Console.ReadLine();
        }

        // -- methods for events examples
        private static string SomeThreadSafeEventHandler(int x)
        {
            return "Hello from first event (thread safe) : " + x.ToString();
        }

        private static string firstEventHandler(int x)
        {
            return "Hello from first event : " + x.ToString();
        }

        // -- methods for delegate examples
        string Hello(int x)
        {
            return "Hello : " + x.ToString();
        }

        static string Hello1(int x)
        {
            return "Hello1 : " + x.ToString();
        }

        // -- methods for async delegates
        static int CountCharacters(string text)
        {
            Thread.Sleep(5000);
            Console.WriteLine("Counting characters in {0}",text);
            return text.Length;
        }

        static int Parse(string text)
        {
            Thread.Sleep(3000);
            Console.WriteLine("Parsing text {0}", text);
            return int.Parse(text);
        }

    }
}
