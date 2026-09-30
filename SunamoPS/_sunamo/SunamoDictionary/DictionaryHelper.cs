namespace SunamoPS._sunamo.SunamoDictionary;

/// <summary>
/// Dictionary helpers.
/// </summary>
internal static class DictionaryHelper
{
    /// <summary>
    /// Adds the value to the list under the key; the list is created when the key is not present yet.
    /// </summary>
    internal static void AddOrCreate<TKey, TValue>(IDictionary<TKey, List<TValue>> dictionary, TKey key, TValue value) where TKey : notnull
    {
        lock (dictionary)
        {
            if (dictionary.TryGetValue(key, out var list))
            {
                list.Add(value);
            }
            else
            {
                dictionary.Add(key, new List<TValue> { value });
            }
        }
    }
}
