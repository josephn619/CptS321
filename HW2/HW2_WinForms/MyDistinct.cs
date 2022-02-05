﻿// <copyright file="MyDistinct.cs" company="Adam Nassar 11588762">
// Copyright (c) Adam Nassar 11588762. All rights reserved.
// </copyright>

namespace HW2_WinForms.MyDistinct
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Calculates distinct integers in 3 different ways.
    /// </summary>
    public class MyDistinct
    {
        /// <summary>
        /// Calculates distinct integers through use of a HashSet.
        /// </summary>
        /// <param name="targetList">targetList</param>
        /// <returns>distinctIntegers.</returns>
        public static int HashSet(List<int> targetList)
        {
            // HashSet is filled with only unique numbers, so my initial approach was unnecessary
            HashSet<int> hashList = new HashSet<int>(targetList);

            return hashList.Count();

            // int unique = 0;

            // HashSet<int> hashList = new HashSet<int>(targetList);

            // List<int> tempList = new List<int>(hashList.Count());

            // foreach (int item in hashList)
            // {
            //     if (!tempList.Contains(item))
            //     {
            //         tempList.Add(item);
            //         unique++;
            //     }
            // }

            // return unique;
        }

        /// <summary>
        /// Calculates distinct integers with O(1) storage complexity.
        /// </summary>
        /// <param name="targetList">targetList.</param>
        /// <returns>distinctIntegers.</returns>
        public static int LowMemory(List<int> targetList)
        {
            int unique = 0; // unique integers in list

            for (int i = 0; i < targetList.Count(); i++)
            {
                // FindLastIndex takes Predicate<T> as parameter and finds last index of instance
                // This is a lambda function. The x == targetList[i] says if the test element is equal to our current element
                // The last == i essentially says if the last index is our current index, then there are no duplicates
                if (targetList.FindLastIndex(x => x == targetList[i]) == i)
                {
                    unique++;
                }
            }

            return unique;
        }

        /// <summary>
        /// Calculates distinct integers through list's sort method.
        /// </summary>
        /// <param name="targetList">targetList.</param>
        /// <returns>distinctIntegers.</returns>
        public static int ListSort(List<int> targetList)
        {
            int unique = 1; // ListSort will always be 1 off since it starts at i=1, therefor unique starts at 1

            targetList.Sort();

            // Can't start at i = 0, because that will end up out of range
            for (int i = 1; i < targetList.Count(); i++)
            {
                if (targetList[i] != targetList[i - 1])
                {
                    unique++;
                }
            }

            return unique;
        }
    }
}
