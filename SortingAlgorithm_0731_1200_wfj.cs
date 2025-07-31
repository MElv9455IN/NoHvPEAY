// 代码生成时间: 2025-07-31 12:00:50
// SortingAlgorithm.cs
using System;
using System.Linq;

/// <summary>
/// A class for demonstrating basic sorting algorithms.
/// </summary>
public class SortingAlgorithm
{
    /// <summary>
    /// Sorts an array of integers using the Bubble Sort algorithm.
    /// </summary>
    /// <param name="array">The array to be sorted.</param>
    /// <returns>The sorted array.</returns>
    public int[] BubbleSort(int[] array)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array), "The array to sort cannot be null.");
        }

        for (int i = 0; i < array.Length - 1; i++)
        {
            for (int j = 0; j < array.Length - i - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    int temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }

        return array;
    }

    /// <summary>
    /// Sorts an array of integers using the Quick Sort algorithm.
    /// </summary>
    /// <param name="array">The array to be sorted.</param>
    /// <param name="low">The starting index of the subarray.</param>
    /// <param name="high">The ending index of the subarray.</param>
    /// <returns>The sorted array.</returns>
    private int[] QuickSort(int[] array, int low, int high)
    {
        if (array == null)
        {
            throw new ArgumentNullException(nameof(array), "The array to sort cannot be null.");
        }

        if (low < high)
        {
            int pi = Partition(array, low, high);

            QuickSort(array, low, pi - 1);
            QuickSort(array, pi + 1, high);
        }

        return array;
    }

    /// <summary>
    /// The partition logic used by the Quick Sort algorithm.
    /// </summary>
    /// <param name="array">The array to partition.</param>
    /// <param name="low">The starting index of the subarray.</param>
    /// <param name="high">The ending index of the subarray.</param>
    /// <returns>The pivot index.</returns>
    private int Partition(int[] array, int low, int high)
    {
        int pivot = array[high];
        int i = (low - 1);
        for (int j = low; j < high; j++)
        {
            if (array[j] < pivot)
            {
                i++;
                int temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
        }
        int temp = array[i + 1];
        array[i + 1] = array[high];
        array[high] = temp;
        return i + 1;
    }

    /// <summary>
    /// Public method to sort an array using Quick Sort.
    /// </summary>
    /// <param name="array">The array to be sorted.</param>
    /// <returns>The sorted array.</returns>
    public int[] QuickSortPublic(int[] array)
    {
        return QuickSort(array, 0, array.Length - 1);
    }

    /// <summary>
    /// Main method for demonstration purposes.
    /// </summary>
    /// <param name="args">Command line arguments.</param>
    public static void Main(string[] args)
    {
        SortingAlgorithm sorter = new SortingAlgorithm();
        int[] unsortedArray = { 5, 1, 4, 2, 8 };
        int[] sortedArray;

        try
        {
            sortedArray = sorter.BubbleSort(unsortedArray);
            Console.WriteLine("Bubble Sort: " + string.Join(", ", sortedArray));

            sortedArray = sorter.QuickSortPublic(unsortedArray);
            Console.WriteLine("Quick Sort: " + string.Join(", ", sortedArray));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}