﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://stackoverflow.com/questions/5716423/c-sharp-sortable-collection-which-allows-duplicate-keys
/// <summary>
/// Comparer for comparing two keys, handling equality as beeing greater
/// Use this Comparer e.g. with SortedLists or SortedDictionaries, that don't allow duplicate keys
/// </summary>
/// <typeparam name="TKey"></typeparam>
public class DuplicateDescKeyComparer<TKey>
				:
			 IComparer<TKey> where TKey : IComparable
{
	#region IComparer<TKey> Members

	public int Compare(TKey x, TKey y) {
		int result = x.CompareTo(y);

		if (result == 0)
			return 1; // Handle equality as being greater. Note: this will break Remove(key) or
		else          // IndexOfKey(key) since the comparer never returns 0 to signal key equality
			return -result;
	}

	#endregion
}

public static class MathExt
{
	public static float map(float value, float leftMin, float leftMax, float rightMin, float rightMax) {
		return rightMin + (value - leftMin) * (rightMax - rightMin) / (leftMax - leftMin);
	}
}