using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Поздравлятор
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>();

            var slnPath = AppDomain.CurrentDomain.BaseDirectory;
            var path = Path.Combine(slnPath, "Folder\\List.txt");

            ReadFile(ref people, path);
            PrintTable5(people);

            int i = 0;

            while (i != 7)
            {
                i = Menu();
                switch(i)
                {
                    case 1: InsEntry(ref people);break;
                    case 2: DelEntry(ref people);break;
                    case 3: PrintTable(people);break;
                    case 4: PrintTable5(people);break;
                    case 5: EditEntryName(ref people);break;
                    case 6: EditEntryBirthday(ref people);break;
                    case 7: PrintFile(path ,people);break;
                }
            }
        }

        public static int Menu()
        {
            int i;
            Console.WriteLine(" 1 - Добавить новую запись");
            Console.WriteLine(" 2 - Удалить запись");
            Console.WriteLine(" 3 - Вывести всю таблицу");
            Console.WriteLine(" 4 - Вывести ближайшие дни рождения");
            Console.WriteLine(" 5 - Изменить имя записи"); 
            Console.WriteLine(" 6 - Изменить дату рождения записи"); 
            Console.WriteLine(" 7 - Завершение работы");
            i = Convert.ToInt16(Console.ReadLine());
            while (i < 1 || i > 10)
            {
                Console.WriteLine("Данная команда отсутствует");
                i = Convert.ToInt16(Console.ReadLine());
            }
            return i;
        }


        public static void InsEntryfromFile(ref List<Person> people, string name, DateTime birthday)
        {
            people.Add(new Person(name, birthday));
            Sorted(ref people);
        }

       
        public static void InsEntry(ref List<Person> people)
        {
            people.Add(new Person());
            Sorted(ref people);
        }


        public static void DelEntry(ref List<Person> people)
        {
            Console.WriteLine("Введите номер удаляемого элемента");
            people.RemoveAt(Convert.ToInt32(Console.ReadLine())-1);
            Sorted(ref people);
        }

        public static void Sorted(ref List<Person> people)
        {
            Person temp;
            bool exit = false;
            while (!exit)
            {
                exit= true;
                if (people.Count != 1) for (int i = 0; i < people.Count-1; i++)
                {
                    if (people[i].DaysBeforeTheBirthday() > people[i + 1].DaysBeforeTheBirthday())
                    {
                        temp = people[i];
                        people[i] = people[i + 1];
                        people[i + 1] = temp;
                        exit = false;
                    }
                }
            }
        }

        public static void EditEntryName(ref List<Person> people)
        {
            Console.WriteLine("Введите номер редактируемой записи");
            int i = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"{people[i].name}\t{people[i].birthday}\t{people[i].DaysBeforeTheBirthday()}");
            people[i].name = Console.ReadLine();
        }

        public static void EditEntryBirthday(ref List<Person> people)
        {
            Console.WriteLine("Введите номер редактируемой записи");
            int i = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"{people[i].name, 20}\t{people[i].birthday,20}\t{people[i].DaysBeforeTheBirthday(), 3}");
            people[i].birthday = Convert.ToDateTime(Console.ReadLine());
            Sorted(ref people);
        }

        public static void PrintTable(List<Person> people)
        {
            for (int i = 1; i < 60; i++)
            {
                Console.Write("-");
                if (i == 59) Console.Write("\n");
            }
            Console.WriteLine("| No |        Имя         |   Дата рождения    | Дней осталось |");
            for (int i=0; i < people.Count; i++)
            {
                Console.WriteLine($"|{i+1,4:d}|{people[i].name,20}|{people[i].birthday,20}|{people[i].DaysBeforeTheBirthday(),15}|");
            }
            for (int i = 1; i < 60; i++)
            {
                Console.Write("-");
                if (i == 59) Console.Write("\n");
            }
        }

        public static void PrintTable5 (List<Person> people)
        {
            for (int i = 1; i < 60; i++)
            {
                Console.Write("-");
                if (i == 59) Console.Write("\n");
            }
            Console.WriteLine("| No |        Имя         |   Дата рождения    | Дней осталось |");
            for (int i = 0; i < people.Count && i < 5; i++)
            {
                Console.WriteLine($"|{i + 1,4:d}|{people[i].name,20}|{people[i].birthday,20}|{people[i].DaysBeforeTheBirthday(),15}|");
            }
            for (int i = 1; i < 60; i++)
            {
                Console.Write("-");
                if (i == 59) Console.Write("\n");
            }
        }

        public static void ReadFile( ref List<Person> people, string path)
        {
            StreamReader sr = new StreamReader(path);
            string line = sr.ReadLine();
            while (line != null)
            {
                string[] words = line.Split(' ');
                InsEntryfromFile(ref people, Convert.ToString(words[0]), Convert.ToDateTime(words[1]));
                line = sr.ReadLine();
            }
            sr.Close();
        }

        public static void PrintFile(string path, List<Person> people)
        {
            StreamWriter sw = new StreamWriter(path, false);
            for (int i = 0;i < people.Count;i++)
            {
                sw.WriteLine($"{people[i].name} {people[i].birthday}");
            }
            sw.Close();
        }

    }
}
