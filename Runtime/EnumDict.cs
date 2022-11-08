using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dev.ComradeVanti.EnumDict
{

    [Serializable]
    public class EnumDict<TEnum, TData> : ISerializationCallbackReceiver
        where TEnum : Enum
    {
        
        [SerializeField] private DictEntry<TEnum, TData>[] entries =
            Array.Empty<DictEntry<TEnum, TData>>();

        private readonly Dictionary<TEnum, TData> dictionary =
            new Dictionary<TEnum, TData>();


        public TData this[TEnum key] =>
            Get(key);


        public EnumDict() { }

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

        public TData Get(TEnum key) =>
            dictionary.TryGetValue(key, out var value)
                ? value
                : default;

    }

}