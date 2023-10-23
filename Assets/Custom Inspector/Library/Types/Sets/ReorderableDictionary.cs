using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CustomInspector
{
    [Serializable]
    public class ReorderableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] List<SerializableKeyValuePair> keyValuePairs = new();

        public ReorderableDictionary() : base()
        { }
        public ReorderableDictionary(IDictionary<TKey, TValue> dictionary) : base(dictionary)
        {
            keyValuePairs = dictionary.Select(_ => new SerializableKeyValuePair(_.Key, _.Value)).ToList();
        }
        public ReorderableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection) : base(collection)
        {
            keyValuePairs = collection.Select(_ => new SerializableKeyValuePair(_.Key, _.Value)).ToList();
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            List<SerializableKeyValuePair> values = this.Select(_ => new SerializableKeyValuePair(_.Key, _.Value)).ToList();
        }
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            base.Clear();
            foreach (var item in keyValuePairs)
            {
                if(item.isValid)
                {
                    this.TryAdd(item.key, item.value); //this can fail, because deserialize can happen before first onGUI (that marks invalid)
                }
            }
        }

        [System.Serializable]
        public class SerializableKeyValuePair
        {
            public TKey key;
            public TValue value;

            public bool isValid = true;

            public SerializableKeyValuePair(TKey key, TValue value)
            {
                this.key = key;
                this.value = value;
            }
        }
    }
}
