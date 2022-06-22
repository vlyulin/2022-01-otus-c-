
using hw07_inprocess_communication;

namespace inprocess_communication
{
    class Programm
    {
        enum sizes : int
        {
<<<<<<< HEAD
            ONE_HUNDRED = 100000,
            ONE_MILLION = 1000000,
            TEN_MILLIONS = 10000000
=======
            OneHundred = 100000,
            OneMillion = 1000000,
            TenMillions = 10000000
>>>>>>> main
        };
        public static void Main(string[] args)
        {

            Calculator calc = new Calculator();
            Console.WriteLine("Обычное вычисление суммы элементов массива через Loop:");
            foreach (int arraySize in Enum.GetValues(typeof(sizes)))
            {
                int[] intArray = getIntArray(arraySize);
                calc.TimeViaLoop(intArray);
            }

            Console.WriteLine("Параллельное вычисление суммы элементов массива через Thread:");
            foreach (int arraySize in Enum.GetValues(typeof(sizes)))
            {
                int[] intArray = getIntArray(arraySize);
                calc.TimeViaThreads(intArray);
            }

            Console.WriteLine("Параллельное вычисление суммы элементов массива через PLinq:");
            foreach (int arraySize in Enum.GetValues(typeof(sizes)))
            {
                int[] intArray = getIntArray(arraySize);
                calc.TimeViaLinq(intArray);
            }

            Console.WriteLine("Параллельное вычисление суммы элементов массива через Partitioning:");
            foreach (int arraySize in Enum.GetValues(typeof(sizes)))
            {
                int[] intArray = getIntArray(arraySize);
                calc.TimeViaPartitioning(intArray);
            }
        }

        private static int[] getIntArray(int size)
        {
            int[] arr = new int[size];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = 1;
            }
            return arr;
        }
    }
}