using System;
using System.Linq;

namespace Микрокроссворд   //Сурен и Саня делали вместе
{
    class Program
    {
        static void Main(string[] args)
        {
            //================================================================================
            //ДАНО
            string strVertWord = "ПОЛУХА";
            string[] strHorWords = { "БУЛКА", "ПЛЮЩ", "ПОТУК", "БУМ", "СМЕХ","АКСОЛ" };
            //================================================================================

            string[] strUsedWords = ArrangeWords(strVertWord, strHorWords);
            Print(strVertWord, strUsedWords);
        }

        static string[] ArrangeWords(string strVertWord, string[] strHorWords)
        {
            //Подпрограмма выбирает слова, подходящие для подстановки под ту или иную букву вертикального слова. Сначала берутся
            //те слова, которые содержат только одну букву из слова. После того, как слово выбирается под определённую букву, для 
            //всех других слов, содержащих ту же букву, становится на один доступный вариант меньше. Пример:
            //    Для буквы "У" в слове "ЛОПУХ" подходят "БУМ" и "БУЛКА". Выбирается первое слово, так как в нем только одна буква, 
            //    содержащаяся в слове "ЛОПУХ". Так как на букву "У" уже было выбрано слово, в слове "БУЛКА" остается только одна буква,
            //    содержащаяся в слове "ЛОПУХ", а именно "Л" и т.д.

            string[] strUsedWords = new string[strVertWord.Length]; //Массив с числом элементов, равным длине вертикального слова

            bool running = true;
            while (running)
            {
                bool flag = false; //Переменная, показывающая, убрал ли цикл в этой итерации слово
                int firstMetInd = 0; //Если слово единственное, содержит букву, индекс слова запишется сюда
                int count;

                //Подбор слов под буквы
                for (int i = 0; i < strHorWords.Length; i++)
                {
                    count = 0;
                    firstMetInd = 0;
                    for (int j = 0; j < strVertWord.Length; j++)
                    {
                        char letterVert = strVertWord[j];
                        if (strHorWords[i].Contains(letterVert))
                        {
                            count++;
                            firstMetInd = j;
                        }
                    }
                    if (count == 1 && strUsedWords[firstMetInd] == null)
                    {
                        strUsedWords[firstMetInd] = strHorWords[i];
                        strHorWords[i] = "";
                        flag = true;
                    }
                }



                //Подбор слов по количеству слов на букву
                for (int i = 0; i < strVertWord.Length; i++)
                {
                    char letter = strVertWord[i];
                    count = 0;
                    for (int j = 0; j < strHorWords.Length; j++)
                    {
                        if (strHorWords[j].Contains(letter))
                        {
                            count++; //Сколько слов подходит для той или иной буквы
                            firstMetInd = j;
                        }
                    }

                    if (count == 1 && strUsedWords[strVertWord.IndexOf(letter)] == null)
                    {
                        //Если для буквы возможно только одно слово, оно убирается из исходного массива и добавляется в итоговый массив
                        strUsedWords[strVertWord.IndexOf(letter)] = strHorWords[firstMetInd];
                        strHorWords[firstMetInd] = "";
                        flag = true;
                    }
                    else if (count == 0)
                    {
                        running = false; //Для буквы вертикального слова не осталось возможных слов
                    }
                }

                //Программа зашла в тупик (Не осталось слов, в которых была бы только одна подходящая буква)
                if (!flag) 
                {
                    for (int i = 0; i < strUsedWords.Length; i++)
                    {
                        if (strUsedWords[i] == null)
                        {
                            running = true;
                            for (int j = 0; j < strHorWords.Length; j++)
                            {
                                if (strHorWords[j] != "" && strHorWords[j].Contains(strVertWord[i]))
                                {
                                    strUsedWords[i] = strHorWords[j];
                                    strHorWords[j] = "";
                                    flag = true;
                                    break;
                                }
                            }
                        }
                        if (flag) break;
                    }
                }
                if (strUsedWords.Contains(null))
                    running = true;
            }
            return strUsedWords;
        }

        static void Print(string strVertWord, string[] strHorWords)//Вывод
        {
            int i;
            int[] LetNumber = new int[strHorWords.Length];
            for (i = 0; i < LetNumber.Length; i++)
            {
                LetNumber[i] = strHorWords[i].IndexOf(strVertWord[i]);
            }
            int MaxLength = LetNumber.Max();
            for (i = 0; i < LetNumber.Length; i++)
            {
                for (int j = 0; j < MaxLength - LetNumber[i]; j++)
                    Console.Write(" ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(strHorWords[i].Substring(0, strHorWords[i].IndexOf(strVertWord[i])));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(strHorWords[i][strHorWords[i].IndexOf(strVertWord[i])]);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(strHorWords[i].Substring(strHorWords[i].IndexOf(strVertWord[i])+1));
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}