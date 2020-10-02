using UnityEngine;

/// <summary>
/// Extensions for arrays
/// </summary>
public static class ArrayExtensions
{
    #region GetRandom
    /// <summary>
    /// Gets a random entry from the entire array
    /// </summary>
    /// <typeparam name="T">The type (deduced from the array parameter)</typeparam>
    /// <param name="array">The array</param>
    /// <returns>A random object. If the array is null or empty, returns default(T)</returns>
    public static T GetRandom<T>(this T[] array)
    {
        // Empty array, return default value
        if (array == null || array.Length == 0)
            return default;

        // Only one object, return it
        if (array.Length == 1)
            return array[0];

        // Return a random entry from the entire array
        return array[Random.Range(0, array.Length)];
    }
    #endregion
}
