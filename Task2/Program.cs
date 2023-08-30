using System;
using System.IO;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace Task2
{
    class Program
    {
        public class WordCounter
        {
            public WordCounter(string value, int number)
            {
                Value = value;
                Number = number;
            }

            public string Value { get; set; }
            public int Number { get; set; }
        }

        static void Main(string[] args)
        {
            // Считываем текстовый файл
            string path = @"C:\\skill\\Unit13HW\\Task1\\Book.txt";
            string text = File.ReadAllText(path);

            // Удаляем лишние знаки препинания и разбиваем на слова
            char[] marks = { ' ', '\n', '\r' };
            var noPunctuationText = new string(text.Where(c => !char.IsPunctuation(c)).ToArray());
            string[] words = noPunctuationText.Split(marks);
            
            // Находим все уникальные слова в книге
            HashSet<string> uniqueWords = new HashSet<string>();
            foreach (string word in words)
            {
                if (word != "")
                {
                    // Сводим все низкий регистр
                    uniqueWords.Add(word.ToLower());
                }
            }

            // Проходимся по всей книге находим количество повторений для каждого слова
            List<WordCounter> topWords = new List<WordCounter>();
            foreach (string word in uniqueWords)
            {
                int num = 0;
                foreach (string element in words)
                {
                    if (element == word)
                    {
                        num++;
                    }
                }
                topWords.Add(new WordCounter(word, num));
            }
            // Сортируем список по возрастанию 
            topWords.Sort((x, y) => x.Number.CompareTo(y.Number));

            // Выводим самые частые слова
            int countWords = topWords.Count;
            Console.WriteLine("Список самых частовстречающихся слов в книге:");
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"{i})\t{topWords[countWords - i].Value}\t-\t{topWords[countWords - i].Number} повторений");
            }

            Console.ReadKey();
        }
    }
}