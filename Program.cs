using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;

namespace Program
{
    class Program
    {
        public static void Main()
        {
            var summary = BenchmarkRunner.Run<Benchmark>();
        }
    }

    public class Benchmark
    {
        [Params(10, 1000, 100000)]
        public int arrSize;

        // INSERTION

        [Benchmark]
        public void InsertionSort_Random() { SortingAlgorithms.InsertionSort(ArrayGenerator.GetRandomArray(arrSize, 0, arrSize)); }
        [Benchmark]
        public void InsertionSort_Sorted() { SortingAlgorithms.InsertionSort(ArrayGenerator.GetSortedArray(arrSize, 0, arrSize)); }
        [Benchmark]
        public void InsertionSort_Reversed() { SortingAlgorithms.InsertionSort(ArrayGenerator.GetReversedArray(arrSize, 0, arrSize)); }
        [Benchmark]
        public void InsertionSort_Almost() { SortingAlgorithms.InsertionSort(ArrayGenerator.GetPartiallySortedArray(arrSize, 0, arrSize, 0.1f)); }
        [Benchmark]
        public void InsertionSort_FewUnique() { SortingAlgorithms.InsertionSort(ArrayGenerator.GetRandomArray(arrSize, 0, 5)); }

        // MERGE

        [Benchmark]
        public void MergeSort_Random() { SortingAlgorithms.MergeSort(ArrayGenerator.GetRandomArray(arrSize, 0, arrSize)); }
        [Benchmark]
        public void MergeSort_Sorted() { SortingAlgorithms.MergeSort(ArrayGenerator.GetSortedArray(arrSize, 0, arrSize)); }
        [Benchmark]
        public void MergeSort_Reversed() { SortingAlgorithms.MergeSort(ArrayGenerator.GetReversedArray(arrSize, 0, arrSize)); }
        [Benchmark]
        public void MergeSort_Almost() { SortingAlgorithms.MergeSort(ArrayGenerator.GetPartiallySortedArray(arrSize, 0, arrSize, 0.1f)); }
        [Benchmark]
        public void MergeSort_FewUnique() { SortingAlgorithms.MergeSort(ArrayGenerator.GetRandomArray(arrSize, 0, 5)); }

        // QUICK CLASSICAL

        [Benchmark]
        public void QuickClassicalSort_Random() { SortingAlgorithms.QuickSortClassical(ArrayGenerator.GetRandomArray(arrSize, 0, arrSize)); }
        [Benchmark]
        public void QuickClassicalSort_Sorted() { SortingAlgorithms.QuickSortClassical(ArrayGenerator.GetSortedArray(arrSize, 0, arrSize)); }
        [Benchmark]
        public void QuickClassicalSort_Reversed() { SortingAlgorithms.QuickSortClassical(ArrayGenerator.GetReversedArray(arrSize, 0, arrSize)); }
        [Benchmark]
        public void QuickClassicalSort_Almost() { SortingAlgorithms.QuickSortClassical(ArrayGenerator.GetPartiallySortedArray(arrSize, 0, arrSize, 0.1f)); }
        [Benchmark]
        public void QuickClassicalSort_FewUnique() { SortingAlgorithms.QuickSortClassical(ArrayGenerator.GetRandomArray(arrSize, 0, 5)); }

        // QUICK

        [Benchmark]
        public void QuickSort_Random() { SortingAlgorithms.QuickSort(ArrayGenerator.GetRandomArray(arrSize, 0, arrSize)); }
        [Benchmark]
        public void QuickSort_Sorted() { SortingAlgorithms.QuickSort(ArrayGenerator.GetSortedArray(arrSize, 0, arrSize)); }
        [Benchmark]
        public void QuickSort_Reversed() { SortingAlgorithms.QuickSort(ArrayGenerator.GetReversedArray(arrSize, 0, arrSize)); }
        [Benchmark]
        public void QuickSort_Almost() { SortingAlgorithms.QuickSort(ArrayGenerator.GetPartiallySortedArray(arrSize, 0, arrSize, 0.1f)); }
        [Benchmark]
        public void QuickSort_FewUnique() { SortingAlgorithms.QuickSort(ArrayGenerator.GetRandomArray(arrSize, 0, 5)); }
    }

    public static class SortingAlgorithms
    {
        public static void InsertionSort(int[] arr)
        {
            int n = arr.Length;
            for (int i = 1; i < n; ++i)
            {
                int key = arr[i];
                int j = i - 1;

                // Move elements of arr[0..i-1],
                // that are greater than key,
                // to one position ahead of
                // their current position
                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j = j - 1;
                }
                arr[j + 1] = key;
            }
        }

        public static void MergeSort(int[] arr) { MergeSort(arr, 0, arr.Length - 1); }
        static void MergeSort(int[] arr, int l, int r)
        {
            if (l < r)
            {

                // Find the middle point
                int m = l + (r - l) / 2;

                // Sort first and second halves
                MergeSort(arr, l, m);
                MergeSort(arr, m + 1, r);

                // Merge the sorted halves
                // Find sizes of two
                // subarrays to be merged
                int n1 = m - l + 1;
                int n2 = r - m;

                // Create temp arrays
                int[] L = new int[n1];
                int[] R = new int[n2];
                int i, j;

                // Copy data to temp arrays
                for (i = 0; i < n1; ++i)
                    L[i] = arr[l + i];
                for (j = 0; j < n2; ++j)
                    R[j] = arr[m + 1 + j];

                // Merge the temp arrays

                // Initial indexes of first
                // and second subarrays
                i = 0;
                j = 0;

                // Initial index of merged
                // subarray array
                int k = l;
                while (i < n1 && j < n2)
                {
                    if (L[i] <= R[j])
                    {
                        arr[k] = L[i];
                        i++;
                    }
                    else
                    {
                        arr[k] = R[j];
                        j++;
                    }
                    k++;
                }

                // Copy remaining elements
                // of L[] if any
                while (i < n1)
                {
                    arr[k] = L[i];
                    i++;
                    k++;
                }

                // Copy remaining elements
                // of R[] if any
                while (j < n2)
                {
                    arr[k] = R[j];
                    j++;
                    k++;
                }
            }
        }

        public static void QuickSortClassical(int[] arr) { QuickSortClassical(arr, 0, arr.Length - 1); }
        static void QuickSortClassical(int[] arr, int low, int high)
        {
            if (low < high)
            {

                // pi is partitioning index, arr[p]
                // is now at right place
                int pi = QuickSortClassical_Partition(arr, low, high);

                // Separately sort elements before
                // and after partition index
                QuickSortClassical(arr, low, pi - 1);
                QuickSortClassical(arr, pi + 1, high);
            }
        }
        static int QuickSortClassical_Partition(int[] arr, int low, int high)
        {
            // Choosing the pivot
            int pivot = arr[high];

            // Index of smaller element and indicates
            // the right position of pivot found so far
            int i = (low - 1);

            for (int j = low; j <= high - 1; j++)
            {

                // If current element is smaller than the pivot
                if (arr[j] < pivot)
                {

                    // Increment index of smaller element
                    i++;
                    QuickSortClassical_Swap(arr, i, j);
                }
            }
            QuickSortClassical_Swap(arr, i + 1, high);
            return (i + 1);
        }
        static void QuickSortClassical_Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        public static void QuickSort(int[] arr)
        {
            Array.Sort(arr);
        }
    }

    public static class ArrayGenerator
    {
        static Random RNG = new();

        public static int[] GetRandomArray(int size, int minValue, int maxValue)
        {
            int[] generatedArray = new int[size];
            for (int i = 0; i < generatedArray.Length; i++) generatedArray[i] = RNG.Next(minValue, maxValue);
            return generatedArray;
        }

        public static int[] GetSortedArray(int size, int minValue, int maxValue)
        {
            int[] generatedArray = GetRandomArray(size, minValue, maxValue);
            Array.Sort(generatedArray);
            return generatedArray;
        }

        public static int[] GetReversedArray(int size, int minValue, int maxValue)
        {
            int[] generatedArray = GetRandomArray(size, minValue, maxValue);
            Array.Reverse(generatedArray);
            return generatedArray;
        }

        public static int[] GetPartiallySortedArray(int size, int minValue, int maxValue, float imprecisionPercent)
        {
            int[] generatedArray = GetSortedArray(size, minValue, maxValue);
            int stepSize = size / (int)Math.Ceiling(size * imprecisionPercent);
            for (int i = 0; i < generatedArray.Length; i += stepSize) generatedArray[i] = RNG.Next(minValue, maxValue);
            return generatedArray;
        }
    }

    public static class Tools
    {
        public static void DisplayArray(int[] arr)
        {
            StringBuilder strBuilder = new("[ ");
            for (int i = 0; i < arr.Length - 2; i++) strBuilder.Append($"{arr[i]}, ");
            strBuilder.Append($"{arr[arr.Length - 1]} ]");
            Console.WriteLine(strBuilder.ToString());
        }
    }
}