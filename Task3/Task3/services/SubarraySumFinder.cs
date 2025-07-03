using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3.services
{
    public class SubarraySumFinder
    {
        /// <summary>
        /// Finds the smallest indices start and end such that
        /// the sum of elements list[start..end) equals sum.
        /// If no such subarray exists, start and end are set to 0.
        /// </summary>
        public void FindElementsForSum(List<uint> list, ulong sum, out int start, out int end)
        {
            start = 0;
            end = 0;

            // Use two pointers technique for efficient O(n) search:
            ulong currentSum = 0;
            int left = 0;

            for (int right = 0; right < list.Count; right++)
            {
                currentSum += list[right];

                // Shrink the window from the left while currentSum > sum
                while (currentSum > sum && left <= right)
                {
                    currentSum -= list[left];
                    left++;
                }

                // Check if we found the sum exactly
                if (currentSum == sum)
                {
                    start = left;
                    end = right + 1; // end is exclusive
                    return;
                }
            }

            // If no valid subarray found, start and end remain 0
        }


        public static void RunTests()
        {
            var finder = new SubarraySumFinder();

            void Test(List<uint> input, ulong sum, int expectedStart, int expectedEnd)
            {
                finder.FindElementsForSum(input, sum, out int start, out int end);
                Debug.Assert(start == expectedStart && end == expectedEnd,
                    $"Failed for sum={sum} on list=[{string.Join(",", input)}]. Expected: ({expectedStart},{expectedEnd}), Got: ({start},{end})");
                Console.WriteLine($"Passed: sum={sum} in list[{string.Join(",", input)}] => start={start}, end={end}");
            }

            // Provided examples
            Test(new List<uint> { 0, 1, 2, 3, 4, 5, 6, 7 }, 11, 5, 7);
            Test(new List<uint> { 4, 5, 6, 7 }, 18, 1, 4);
            Test(new List<uint> { 0, 1, 2, 3, 4, 5, 6, 7 }, 88, 0, 0);

            // Additional tests
            Test(new List<uint> { 1, 2, 3, 4, 5 }, 9, 1, 4); // subarray 2+3+4=9
            Test(new List<uint> { 1, 2, 3, 4, 5 }, 15, 0, 5); // whole array sum
            Test(new List<uint> { 5, 1, 3, 2, 7 }, 5, 0, 1); // first element only
            Test(new List<uint> { 5, 1, 3, 2, 7 }, 7, 4, 5); // last element only
            Test(new List<uint> { }, 0, 0, 0); // empty list, no subarray
            Test(new List<uint> { 1, 1, 1, 1, 1 }, 3, 0, 3); // first three elements
            Test(new List<uint> { 1, 1, 1, 1, 1 }, 4, 0, 4); // first four elements
            Test(new List<uint> { 1, 2, 3, 4, 5 }, 0, 0, 0); // sum=0 no subarray with sum=0 (except empty subarray disallowed)
        }

    }
}
