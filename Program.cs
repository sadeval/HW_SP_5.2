using System;
using System.Linq;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        int[] numbers = { 5, 3, 9, 1, 3, 7, 5, 9, 2, 8 };

        Console.WriteLine("Изначальный массив: " + string.Join(", ", numbers));

        Task<int[]> removeDuplicatesTask = Task.Run(() =>
        {
            Console.WriteLine("Удаление дубликатов...");
            return numbers.Distinct().ToArray();
        });

        Task<int[]> sortTask = removeDuplicatesTask.ContinueWith(task =>
        {
            Console.WriteLine("Сортировка массива...");
            int[] uniqueNumbers = task.Result;
            Array.Sort(uniqueNumbers);
            return uniqueNumbers;
        });

        Task searchTask = sortTask.ContinueWith(task =>
        {
            int[] sortedNumbers = task.Result;
            Console.WriteLine("Отсортированный массив: " + string.Join(", ", sortedNumbers));

            Console.Write("Введите значение для бинарного поиска: ");
            int searchValue = int.Parse(Console.ReadLine());

            int resultIndex = Array.BinarySearch(sortedNumbers, searchValue);

            if (resultIndex >= 0)
                Console.WriteLine($"Значение {searchValue} найдено под индексом {resultIndex}.");
            else
                Console.WriteLine($"Значение {searchValue} не найдено.");
        });

        searchTask.Wait();
    }
}
