﻿using System.Xml;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace mzmeevskiy
{
    public class BinarySerializationData<T> : IData<T>
    {
        private static BinaryFormatter _formatter;

        public BinarySerializationData()
        {
            _formatter = new BinaryFormatter();
        }

        public void Save(T data, string path = null)
        {
            if (data == null && !string.IsNullOrEmpty(path)) return;
            if (!typeof(T).IsSerializable) return;
            using (var fs = new FileStream(path, FileMode.Create))
            {
                _formatter.Serialize(fs, data);
            }
        }

        public T Load(string path)
        {
            T result;
            if (!File.Exists(path)) return default(T);
            using (var fs = new FileStream(path, FileMode.Open))
            {
                result = (T)_formatter.Deserialize(fs);
            }

            return result;
        }
    }
}