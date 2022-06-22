using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw07_inprocess_communication
{
    internal class Calculator
    {
        public void TimeViaLoop(int[] numbers)
        {
            var sw = Stopwatch.StartNew();
            long total = CalculateByLoop(numbers);
            sw.Stop();
            Console.WriteLine("Total is: " + total + ". Elapsed time for calculation of " + numbers.Length +
                                  " numbers is :" + sw.Elapsed.TotalMilliseconds + " ms.");
        }

        public long CalculateByLoop(int[] numbers)
        {
            long total = 0;
            for (int i = 0; i < numbers.Length; ++i)
            {
                total += numbers[i];
            }
            return total;
        }

        public void TimeViaThreads(int[] numbers)
        {
            var sw = Stopwatch.StartNew();
            long total = CalculateByThreads(numbers);
            sw.Stop();
            Console.WriteLine("Total is: " + total + ". Elapsed time for calculation of " + numbers.Length +
                                  " numbers is :" + sw.Elapsed + " ms.");
        }
        public long CalculateByThreads(int[] numbers)
        {
            int threads = Environment.ProcessorCount;
            ThreadPool.SetMaxThreads(threads, threads);


            int recordNumber = numbers.Length;
            // Вычисление количества записей, которые будут загружены одним потоком
            int piece = recordNumber / threads;

            Thread[] threadsarray = new Thread[threads];
            long[] sum = new long[threads];

            for (int i = 0; i < threads; i++)
            {
                int from = i * piece;
                int to = i * piece + piece;
                to = Math.Min(to, recordNumber);

                // Console.WriteLine("Thread number: " + i + ". Range: " + from + " to " + to);
                int idx = i;
                threadsarray[i] = new Thread(() => calculator(from, to, numbers, sum, idx));
                threadsarray[i].Start();
            }

            for (int i = 0; i < threads; i++)
            {
                threadsarray[i].Join();
            }

            long total = 0;
            for (int i = 0; i < threads; i++)
            {
                total += sum[i];
            }

            return total;
        }

        private static void calculator(int from, int to, int[] intArray, long[] sum, int idx)
        {
            long total = 0;
            for (int i = from; i < to; i++)
            {
                total += intArray[i];
            }
            sum[idx] = total;
        }

        public void TimeViaLinq(int[] numbers)
        {
            var sw = Stopwatch.StartNew();
            long total = CalculateByLinq(numbers);
            sw.Stop();
            Console.WriteLine("Total is: " + total + ". Elapsed time for calculation of " + numbers.Length +
                                  " numbers is :" + sw.Elapsed + " ms.");
        }

        public long CalculateByLinq(int[] numbers)
        {
            return numbers.AsParallel().Sum();
        }

        public void TimeViaPartitioning(int[] numbers)
        {
            var sw = Stopwatch.StartNew();
            long total = CalculateByPartitioning(numbers);
            sw.Stop();
            Console.WriteLine("Total is: " + total + ". Elapsed time for calculation of " + numbers.Length +
                                  " numbers is :" + sw.Elapsed + " ms.");
        }

        public long CalculateByPartitioning(int[] numbers)
        {
            long total = 0;
            object _locker = new object();

            var rangePartitioner = Partitioner.Create(0, numbers.Length);
            Parallel.ForEach(rangePartitioner, (range, loopState) =>
            {
                long subtotal = 0;
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    subtotal += numbers[i];
                }

                lock(_locker)
                {
                    total += subtotal;
                }
            });
            return total;
        }
    }

}
