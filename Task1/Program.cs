using System;
using System.IO;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace Task1
{
    class Program
    {

        public delegate void ActionForMeasuring();

        public static void MeasuringSpeed(string message, ActionForMeasuring action)
        {
            var stopWatch = Stopwatch.StartNew();
            action();
            Console.Write($"{message}: {stopWatch.Elapsed.TotalMilliseconds} мс");
            Console.WriteLine();
            stopWatch.Stop();
        }

        //посимпатичнее
        static void Main(string[] args) 
        {
            string path = @"C:\\skill\\Unit13HW\\Task1\\Book.txt";
            string text = File.ReadAllText(path);
            char[] marks = { ' ', '\n', '\r' };


            List<string> bookTextList = new List<string>();
            LinkedList<string> bookTextLinkedList = new LinkedList<string>();

            var noPunctuationText = new string(text.Where(c => !char.IsPunctuation(c)).ToArray());
            string[] words = noPunctuationText.Split(marks);
            HashSet<string> uniqueWords = new HashSet<string>();
            foreach (string word in words)
            {
                if (word != "")
                {
                    bookTextList.Add(word.ToLower());
                    bookTextLinkedList.AddLast(word.ToLower());
                }
            }

            // запускаем новый таймер
            MeasuringSpeed("Время затраченное на добавление слова в начало списка",() => bookTextList.Insert(0,"СловоВНачалеСписка"));
            
            MeasuringSpeed("Время затраченное на добавление слова в начало связанного списка",() => bookTextLinkedList.AddFirst("СловоВНачалеСвязанногоСписка"));
            
            MeasuringSpeed("Время затраченное на добавление слова в середину списка", () => bookTextList.Insert(80000, "СловоВСерединеСписка"));
            
            LinkedListNode<string> centralWord = bookTextLinkedList.Find("посимпатичнее");
            MeasuringSpeed("Время затраченное на добавление слова в середину связанного списка", () => bookTextLinkedList.AddAfter(centralWord, "СловоВСерединеСвязанногоСписка"));

            MeasuringSpeed("Время затраченное на добавление слова в конец списка", () => bookTextList.Add("СловоВКонцеСписка"));

            MeasuringSpeed("Время затраченное на добавление слова в конец связанного списка", () => bookTextLinkedList.AddLast("СловоВКонцеСвязанногоСписка"));
           
            Console.ReadKey();
        }
    }
}