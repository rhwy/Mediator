using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Mediator.Tests.SampleData
{
    public static class TestHelper
    {
        public static T GetNewSerializedCopyOfObject<T>(T source)
        {
            using (Stream s = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(s, source);
                s.Position = 0;
                return (T)formatter.Deserialize(s);
            }
            return default(T);
        }

       
    }
}
