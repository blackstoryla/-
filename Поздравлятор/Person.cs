using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Поздравлятор
{
    internal class Person
    {
        public string name;
        public DateTime birthday;

        public Person()
        {
            Console.WriteLine("Введите имя");
            this.name = Console.ReadLine();
            Console.WriteLine("Введите дату рождения");
            this.birthday = Convert.ToDateTime(Console.ReadLine());
        }
        public Person(string name, DateTime birthday)
        {
            this.name = name;
            this.birthday = birthday;
        }

        public int DaysBeforeTheBirthday()
        {
            DateTime temp = DateTime.Today;
            DateTime data = new DateTime();
            if (temp.Month > this.birthday.Month || (temp.Month == this.birthday.Month && this.birthday.Day < temp.Day))
            {
                data = new DateTime(temp.Year + 1, this.birthday.Month, this.birthday.Day);
            }
            else {
                data = new DateTime(temp.Year, this.birthday.Month, this.birthday.Day); 
            }

            return (data - temp).Days;
        }
    }
}
