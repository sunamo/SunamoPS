namespace SunamoPS._sunamo.SunamoDictionary;

internal class DictionaryHelper
{
    internal static void AddOrCreate<TKey, TValue>(IDictionary<TKey, List<TValue>> dictionary, TKey key, TValue value,
        bool isAvoidingDuplicates = false, Dictionary<TKey, List<string>>? stringDictionary = null) where TKey : notnull
    {
        AddOrCreate<TKey, TValue, object>(dictionary, key, value, isAvoidingDuplicates, stringDictionary);
    }

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
