using System;
using UnityEngine;

namespace ComradeVanti.EnumDict
{

    [Serializable]
    public struct EnumDict<TEnum, TData>
        where TEnum : Enum
    {

        [SerializeField] private Entry[] entries;
        
        

        [Serializable]
        private struct Entry
        {

            [SerializeField] private TEnum @enum;
            [SerializeField] private TData value;

            
            public TEnum Enum => @enum;

            public TData Value => value;


            public Entry(TEnum @enum, TData value)
            {
                this.@enum = @enum;
                this.value = value;
            }

        }

    }

}