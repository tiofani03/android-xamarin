using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace productDemo.Feature.Demo.pages
{
    public class Helper
    {
        public static T DeepCopy<T>(T obj)
        {
            // Periksa apakah obj adalah objek yang dapat disalin
            if (!typeof(T).IsSerializable) throw new ArgumentException("The type must be serializable.", nameof(obj));

            // Jika obj adalah null, kembalikan null
            if (ReferenceEquals(obj, null)) return default;

            // Buat stream untuk menulis objek ke dalamnya
            using (var memoryStream = new MemoryStream())
            {
                // Buat objek formatter biner untuk serialisasi objek
                IFormatter formatter = new BinaryFormatter();
                formatter.Context = new StreamingContext(StreamingContextStates.Clone);

                // Serialisasi objek ke dalam memoryStream
                formatter.Serialize(memoryStream, obj);

                // Pindahkan pointer posisi stream kembali ke awal
                memoryStream.Seek(0, SeekOrigin.Begin);

                // Deserialisasi objek dari memoryStream dan kembalikan salinannya
                return (T)formatter.Deserialize(memoryStream);
            }
        }
    }
}