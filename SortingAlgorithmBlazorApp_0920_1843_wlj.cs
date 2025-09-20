// 代码生成时间: 2025-09-20 18:43:44
 * error handling, and commenting for maintainability and extensibility.
 */

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SortingAlgorithmBlazorApp
{
    public partial class SortingAlgorithmComponent
    {
        [Parameter]
        public List<int> Numbers { get; set; } = new List<int>();

        private List<int> sortedList;
        private List<int> originalList;
        private bool isSorted;

        // Method to sort the list using a simple Bubble Sort algorithm
        private void BubbleSort()
        {
            try
            {
                if (Numbers == null || Numbers.Count == 0)
                {
                    throw new InvalidOperationException("Cannot sort an empty list.");
                }

                originalList = new List<int>(Numbers);
                sortedList = new List<int>(Numbers);
                isSorted = false;

                int n = sortedList.Count;
                while (!isSorted)
                {
                    isSorted = true;
                    for (int i = 1; i < n; i++)
                    {
                        if (sortedList[i - 1] > sortedList[i])
                        {
                            Swap(ref sortedList[i - 1], ref sortedList[i]);
                            isSorted = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during sorting: {ex.Message}");
            }
        }

        // Method to swap two elements in a list
        private void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        // Method to display the sorted list
        private string DisplaySortedList()
        {
            if (sortedList != null)
            {
                return string.Join(", ", sortedList);
            }
            return "No sorted list available.";
        }
    }
}
