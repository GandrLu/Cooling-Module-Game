using System.Collections.Generic;
using UnityEngine;

/// Some helping methods.
public static class ExtensionMethods
{
    /// <summary>
    /// Remaps a float number from one range to another, taken from: 
    /// https://forum.unity.com/threads/re-map-a-number-from-one-range-to-another.119437/
    /// </summary>
    /// <returns>The remapped value</returns>
    public static float Map(this float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (from - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
    }

    /// <summary>
    /// Remaps a int number from one range to another, taken from: 
    /// https://forum.unity.com/threads/re-map-a-number-from-one-range-to-another.119437/
    /// </summary>
    /// <returns>The remapped value</returns>
    public static int Map(this int from, int fromMin, int fromMax, int toMin, int toMax)
    {
        return (from - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
    }

    /// <summary>
    /// Collects all children objects with a certain tag. Taken from:
    /// https://answers.unity.com/questions/893966/how-to-find-child-with-tag.html
    /// </summary>
    /// <returns>An array of objects</returns>
    public static T[] FindComponentsInChildrenWithTag<T>(this GameObject parent, string tag, bool forceActive = false) where T : Component
    {
        if (parent == null) { throw new System.ArgumentNullException(); }
        if (string.IsNullOrEmpty(tag) == true) { throw new System.ArgumentNullException(); }
        List<T> list = new List<T>(parent.GetComponentsInChildren<T>(forceActive));
        if (list.Count == 0) { return null; }

        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i].CompareTag(tag) == false)
            {
                list.RemoveAt(i);
            }
        }
        return list.ToArray();
    }

    /// <summary>
    /// Collects a children object with a certain tag. Taken from:
    /// https://answers.unity.com/questions/893966/how-to-find-child-with-tag.html
    /// </summary>
    /// <returns>An object</returns>
    public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag, bool forceActive = false) where T : Component
    {
        if (parent == null) { throw new System.ArgumentNullException(); }
        if (string.IsNullOrEmpty(tag) == true) { throw new System.ArgumentNullException(); }

        T[] list = parent.GetComponentsInChildren<T>(forceActive);
        foreach (T t in list)
        {
            if (t.CompareTag(tag) == true)
            {
                return t;
            }
        }
        return null;
    }
}
