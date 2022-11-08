using System;
using System.Collections.Generic;
using UnityEngine;

namespace Dev.ComradeVanti.EnumDict
{

    [Serializable]
    public struct DictEntry<TEnum, TValue>
    {

        [SerializeField] private TEnum key;
        [SerializeField] private TValue value;


        public TEnum Key => key;

        public TValue Value => value;


        public DictEntry(KeyValuePair<TEnum, TValue> kv)
        {
            key = kv.Key;
            value = kv.Value;
        }

        public DictEntry(TEnum key, TValue value)
        {
            this.key = key;
            this.value = value;
        }

    }

}