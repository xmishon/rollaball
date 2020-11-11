using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace mzmeevskiy
{
    public  class Homework5 : MonoBehaviour
    {
        private void Start()
        {
            string test = "wfjc,wfj lswijweds jwej ojjfmnnbmx hjwuh293yh kj 92 dksjfa;";
            Debug.Log($"Кол-во символов 'j' в строке: {test.letterCount('j')}");

            // ---------------------------------------------------------------------------
            Debug.Log("---------------------------------");
            List<int> list = new List<int>() { 1, 2, 4, 2, 4, 5, 4, 2, 6, 1 };
            Dictionary<int, int> result = list.countFrequency();
            foreach(KeyValuePair<int, int> pair in result)
            {
                Debug.Log($"Элемент {pair.Key} встретился {pair.Value} раз.");
            }
            Debug.Log("---------------------------------");
            List<string> list2 = new List<string>() { "one", "two", "one", "four", "two", "four", "one", "one", "three" };
            Dictionary<string, int> result2 = list2.countFrequency();
            foreach(var pair in result2)
            {
                Debug.Log($"Элемент {pair.Key} встретился {pair.Value} раз.");
            }

            // ---------------------------------------------------------------------------
            Debug.Log("---------------------------------");
            Delegates.DemoThree();

        }
    }

    public static class ExtensionMethod
    {
        public static int letterCount(this string str, char ch)
        {
            int counter = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ch)
                {
                    counter++;
                }
            }
            return counter;
        }
    }

    public static class Lists
    {
        public static Dictionary<T, int> countFrequency<T>(this List<T> list)
        {
            Dictionary<T, int> result = new Dictionary<T, int>();

            List<T> aux = list.Distinct().ToList();

            foreach(T item in aux)
            {
                int num = 0;
                foreach(T externalItem in list)
                {
                    if (externalItem.Equals(item))
                    {
                        num++;
                    }
                }
                result.Add(item, num);
            }

            return result;
        }
    }

    public static class Delegates
    {
        public static void DemoOne()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                { "four", 4 },
                { "two", 2 },
                { "one", 1 },
                { "three", 3 }
            };
            var d = dict.OrderBy(delegate (KeyValuePair<string, int> pair) { return pair.Value; });
            foreach (var pair in d)
            {
                Debug.Log($"{pair.Key} - {pair.Value}");
            }
        }

        public static void DemoTwo()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                { "four", 4 },
                { "two", 2 },
                { "one", 1 },
                { "three", 3 }
            };
            var d = dict.OrderBy(pair => pair.Value);
            foreach (var pair in d)
            {
                Debug.Log($"{pair.Key} - {pair.Value}");
            }
        }

        private delegate int MyOrderDelegate(KeyValuePair<string, int> pair);
        private static MyOrderDelegate orderFunction = new MyOrderDelegate(MyOrderFunction);
        public static void DemoThree()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>()
            {
                { "four", 4 },
                { "two", 2 },
                { "one", 1 },
                { "three", 3 }
            };
            var d = dict.OrderBy(orderFunction.Invoke);
            foreach (var pair in d)
            {
                Debug.Log($"{pair.Key} - {pair.Value}");
            }
        }

        private static int MyOrderFunction(KeyValuePair<string, int> pair)
        {
            return pair.Value;
        }
    }
}
