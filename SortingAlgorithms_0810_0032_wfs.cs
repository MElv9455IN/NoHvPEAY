// 代码生成时间: 2025-08-10 00:32:04
using System;
using System.Collections.Generic;
using System.Linq;

namespace SortingAlgorithms
{
    // 该类提供了几种基本的排序算法实现
    public static class SortingAlgorithms
    {
        // 冒泡排序算法
        public static List<int> BubbleSort(List<int> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            bool swapped;
            do
            {
                swapped = false;
                for (int i = 0; i < list.Count - 1; i++)
                {
                    if (list[i] > list[i + 1])
                    {
                        // 交换元素
                        var temp = list[i];
                        list[i] = list[i + 1];
                        list[i + 1] = temp;
                        swapped = true;
                    }
                }
            } while (swapped);
            return list;
        }

        // 快速排序算法
        public static List<int> QuickSort(List<int> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            return QuickSortHelper(list, 0, list.Count - 1);
        }

        private static List<int> QuickSortHelper(List<int> list, int left, int right)
        {
            while (left < right)
            {
                int pivotIndex = Partition(list, left, right);
                if (pivotIndex - left < 2)
                {
                    if (left < pivotIndex - 1)
                        QuickSortHelper(list, left, pivotIndex - 1);
                }
                else
                    QuickSortHelper(list, left, pivotIndex - 1);

                left = pivotIndex + 1;
                if (right - pivotIndex < 2)
                {
                    if (pivotIndex + 1 < right)
                        QuickSortHelper(list, pivotIndex + 1, right);
                }
                else
                    QuickSortHelper(list, pivotIndex + 1, right);
            }

            return list;
        }

        private static int Partition(List<int> list, int left, int right)
        {
            int pivot = list[right];
            int i = left;
            for (int j = left; j < right; j++)
            {
                if (list[j] <= pivot)
                {
                    Swap(list, i, j);
                    i++;
                }
            }
            Swap(list, i, right);
            return i;
        }

        private static void Swap(List<int> list, int i, int j)
        {
            int temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        // 主函数，用于测试排序算法
        public static void Main(string[] args)
        {
            List<int> numbers = new List<int> { 5, 3, 8, 4, 2, 7, 1, 6 };

            Console.WriteLine("Original list: " + string.Join(", ", numbers));

            try
            {
                Console.WriteLine("Sorted list (Bubble Sort): " + string.Join(", ", BubbleSort(new List<int>(numbers))));
                Console.WriteLine("Sorted list (Quick Sort): " + string.Join(", ", QuickSort(new List<int>(numbers))));
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}