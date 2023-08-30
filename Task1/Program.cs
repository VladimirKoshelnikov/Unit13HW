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
        
        //Метод позволяющий измерить скорость выполнения функции
        public static void MeasuringSpeed(string message, ActionForMeasuring action)
        {
            var stopWatch = Stopwatch.StartNew();
            action();
            Console.Write($"{message}: {stopWatch.Elapsed.TotalMilliseconds} мс");
            Console.WriteLine();
            stopWatch.Stop();
        }

        // Будем искать слово "посимпатичнее" потому что в Обломове оно точно где-то в середине
        static void Main(string[] args) 
        {
            // Считываем текстовый файл
            string path = @"C:\\skill\\Unit13HW\\Task1\\Book.txt";
            string text = File.ReadAllText(path);
            char[] marks = { ' ', '\n', '\r' };

            // Создаем списки
            List<string> bookTextList = new List<string>();
            LinkedList<string> bookTextLinkedList = new LinkedList<string>();
            
            // Отделяем все слова от пунктуационных знаков
            var noPunctuationText = new string(text.Where(c => !char.IsPunctuation(c)).ToArray());
            string[] words = noPunctuationText.Split(marks);
            foreach (string word in words)
            {
                if (word != "")
                {
                    // Добавляем в списки все слова
                    bookTextList.Add(word.ToLower());
                    bookTextLinkedList.AddLast(word.ToLower());
                }
            }

            // Измеряем скорость вставок
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