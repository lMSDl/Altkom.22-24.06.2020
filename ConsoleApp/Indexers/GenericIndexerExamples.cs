using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Indexers
{
    public class GenericIndexerExamples
    {
        class DataStore<T>
        {
            private T[] data = new T[10];

            public T this[int index]
            {
                get
                {
                    return data[index];
                }

                set
                {
                    data[index] = value;
                }
            }

            public int this[T value]
            {
                get
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        if (data[i]?.Equals(value) ?? value == null)
                            return i;
                    }
                    return -1;
                }
            }
        }


        public void Test()
        {
            var dataStore = new DataStore<string>();

            dataStore[3] = "4";
            dataStore[4] = "five";
            dataStore[5] = "VI";

            var fiveIndex = dataStore["five"];
            dataStore[fiveIndex] = "FIVE";

            for (int i = 0; i < 10; i++)
                Console.WriteLine(dataStore[i]);

            var dataStore2 = new DataStore<float>();

            dataStore2[3] = 4;
            dataStore2[4] = 5;
            dataStore2[5] = 6;

            fiveIndex = dataStore2[5f];
            dataStore2[fiveIndex] = -5;

            for (int i = 0; i < 10; i++)
                Console.WriteLine(dataStore2[i]);


        }

    }
}
