using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dev.ComradeVanti.EnumDict
{

    /// <summary>
    ///     Serializes a value for each entry in an enum
    /// </summary>
    /// <typeparam name="TEnum">The enum type</typeparam>
    /// <typeparam name="TData">The value type</typeparam>
    [Serializable]
    public class EnumDict<TEnum, TData> : ISerializationCallbackReceiver
        where TEnum : Enum
    {

        [SerializeField] private DictEntry<TEnum, TData>[] entries =
            Array.Empty<DictEntry<TEnum, TData>>();

        private readonly Dictionary<TEnum, TData> dictionary =
            new Dictionary<TEnum, TData>();


        /// <summary>
        ///     Get the stored value for the given key
        /// </summary>
        /// <remarks>
        ///     This returns the default value for TData if no value was ever
        ///     specified
        /// </remarks>
        /// <param name="key">The key to get</param>
        public TData this[TEnum key] =>
            Get(key);


        /// <summary>
        ///     Creates a new enum-dict with the default value for TData on each key
        /// </summary>
        public EnumDict() { }

        /// <summary>
        ///     Creates a new enum-dict with the specified key-value pairs. Missing keys
        ///     will get the default value for TData
        /// </summary>
        /// <param name="entries">The preset entries</param>
        public EnumDict(IDictionary<TEnum, TData> entries)
        {
            foreach (var keyValuePair in entries)
                dictionary.Add(keyValuePair.Key, keyValuePair.Value);
        }


        public void OnBeforeSerialize() =>
            entries = EnumUtil.GetEnumValues<TEnum>()
                              .Select(key =>
                              {
                                  var value = this[key];
                                  return new DictEntry<TEnum, TData>(key, value);
                              }).ToArray();

        public void OnAfterDeserialize()
        {
            dictionary.Clear();
            foreach (var entry in entries)
                dictionary.Add(entry.Key, entry.Value);
        }

        /// <summary>
        ///     Get the stored value for the given key
        /// </summary>
        /// <remarks>
        ///     This returns the default value for TData if no value was ever
        ///     specified
        /// </remarks>
        /// <param name="key">The key to get</param>
        /// <returns>The value</returns>
        public TData Get(TEnum key) =>
            dictionary.TryGetValue(key, out var value)
                ? value
                : default;

    }

}