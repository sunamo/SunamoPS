namespace SunamoPS._sunamo.SunamoDictionary;

/// <summary>
/// Helper methods for working with dictionaries containing list values.
/// </summary>
internal class DictionaryHelper
{
    /// <summary>
    /// Adds a value to a dictionary of lists, creating the list if the key does not exist.
    /// </summary>
    /// <typeparam name="TKey">Type of dictionary key.</typeparam>
    /// <typeparam name="TValue">Type of values in the lists.</typeparam>
    /// <param name="dictionary">Target dictionary.</param>
    /// <param name="key">Key to add or update.</param>
    /// <param name="value">Value to add to the list.</param>
    /// <param name="isAvoidingDuplicates">Whether to skip adding if value already exists in the list.</param>
    /// <param name="stringDictionary">Optional parallel dictionary for string comparison.</param>
    internal static void AddOrCreate<TKey, TValue>(IDictionary<TKey, List<TValue>> dictionary, TKey key, TValue value,
        bool isAvoidingDuplicates = false, Dictionary<TKey, List<string>>? stringDictionary = null) where TKey : notnull
    {
        AddOrCreate<TKey, TValue, object>(dictionary, key, value, isAvoidingDuplicates, stringDictionary);
    }

    /// <summary>
    /// Adds a value to a dictionary of lists with collection key support.
    /// </summary>
    /// <typeparam name="TKey">Type of dictionary key.</typeparam>
    /// <typeparam name="TValue">Type of values in the lists.</typeparam>
    /// <typeparam name="TCollectionElement">Element type when key is a collection.</typeparam>
    /// <param name="dictionary">Target dictionary.</param>
    /// <param name="key">Key to add or update.</param>
    /// <param name="value">Value to add to the list.</param>
    /// <param name="isAvoidingDuplicates">Whether to skip adding if value already exists.</param>
    /// <param name="stringDictionary">Optional parallel dictionary for string comparison.</param>
    internal static void AddOrCreate<TKey, TValue, TCollectionElement>(IDictionary<TKey, List<TValue>> dictionary, TKey key, TValue value,
        bool isAvoidingDuplicates = false, Dictionary<TKey, List<string>>? stringDictionary = null) where TKey : notnull
    {
        var isComparingWithString = stringDictionary != null;

        if (key is IList && typeof(TCollectionElement) != typeof(object))
        {
            var keyEnumerable = key as IList<TCollectionElement>;
            var isContained = false;
            foreach (var item in dictionary)
            {
                var entryKey = item.Key as IList<TCollectionElement>;
                if (entryKey != null && keyEnumerable != null && entryKey.SequenceEqual(keyEnumerable)) isContained = true;
            }

            if (isContained)
            {
                foreach (var item in dictionary)
                {
                    var entryKey = item.Key as IList<TCollectionElement>;
                    if (entryKey != null && keyEnumerable != null && entryKey.SequenceEqual(keyEnumerable))
                    {
                        if (isAvoidingDuplicates)
                            if (item.Value.Contains(value))
                                return;
                        item.Value.Add(value);
                    }
                }
            }
            else
            {
                List<TValue> newList = new() { value };
                dictionary.Add(key, newList);

                if (isComparingWithString && stringDictionary != null)
                {
                    List<string> newStringList = new() { value?.ToString() ?? string.Empty };
                    stringDictionary.Add(key, newStringList);
                }
            }
        }
        else
        {
            var shouldAdd = true;
            lock (dictionary)
            {
                if (dictionary.ContainsKey(key))
                {
                    if (isAvoidingDuplicates)
                    {
                        if (dictionary[key].Contains(value))
                            shouldAdd = false;
                        else if (isComparingWithString && stringDictionary != null)
                            if (stringDictionary[key].Contains(value?.ToString() ?? string.Empty))
                                shouldAdd = false;
                    }

                    if (shouldAdd)
                    {
                        var existingList = dictionary[key];
                        existingList?.Add(value);

                        if (isComparingWithString && stringDictionary != null)
                        {
                            var existingStringList = stringDictionary[key];
                            existingStringList?.Add(value?.ToString() ?? string.Empty);
                        }
                    }
                }
                else
                {
                    List<TValue> newList = new() { value };
                    dictionary.Add(key, newList);

                    if (isComparingWithString && stringDictionary != null)
                    {
                        List<string> newStringList = new() { value?.ToString() ?? string.Empty };
                        stringDictionary.Add(key, newStringList);
                    }
                }
            }
        }
    }
}
