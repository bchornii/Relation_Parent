using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _20_state_machine
{
    class Program
    {
        static void Main(string[] args)
        {
            new StateMachine().MoveNext();

            Thread.Sleep(10000);
        }

        static void ReadLineAndThenDo(StateMachine machine)
        {
            ThreadPool.QueueUserWorkItem(state =>
            {
                Console.WriteLine(new string('-', 80));
                Console.WriteLine("Thread id = {0}",Thread.CurrentThread.ManagedThreadId.ToString());
                string line = Console.ReadLine();
                machine.m_input = line;
                machine.MoveNext();
            });
        }

        class StateMachine
        {
            int state = 0;
            private int totCharacters = 0;
            public string m_input;

            public void MoveNext()
            {
                switch (state)
                {
                    case 0:
                        state = 1;
                        ReadLineAndThenDo(this);
                        return;
                    case 1:
                        if (!string.IsNullOrEmpty(m_input))
                        {
                            Console.WriteLine("First string = " + m_input);
                            totCharacters += m_input.Length;
                            state = 2;
                            ReadLineAndThenDo(this);
                            return;
                        }
                        goto case 3;
                    case 2:
                        if (!string.IsNullOrEmpty(m_input))
                        {
                            Console.WriteLine("Second string = " + m_input);
                            totCharacters += m_input.Length;
                        }
                        goto case 3;
                    case 3:
                        Console.WriteLine(totCharacters);
                        return;
                }
            }
        }
    }
}
