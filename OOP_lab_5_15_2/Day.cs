using System;
using System.IO;

namespace OOP_lab_5_15_2
{
    abstract class Day : Exhibition
    {
        private int _visitorsCount;
        private string _coment; 

        private int VisitorsCount
        {
            get => _visitorsCount;
            set => _visitorsCount = value;
        }

        private string Coment
        {
            get => _coment;
            set => _coment = value;
        }

        public virtual int ComentLength()
        {
            return _coment.Split(new char[] { '\n', '\r', ' ', ':', ';', '.', ',', '?', '!', '(', ')', '{', '}', '[', ']', '@', '#', '№', '$', '^', '%', '&', '*', '/', '|' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public Day()
        {
            base.Name = "Не вказано.";
            base.SculptorSurename = "Не вказано.";

            _visitorsCount = 0;
            _coment = "Не вказано";
        }

        public Day(string name, string sculptorSurename, int visitorsCount, string coment)
        {
            base.Name = UkrainianI(name); ;
            base.SculptorSurename = UkrainianI(sculptorSurename);

            _visitorsCount = visitorsCount;
            _coment = UkrainianI(coment);
        }

        public static void Read()
        {
            ReadBase();
            ReadKey();
        }

        private static void ReadBase()
        {
            StreamReader file = new StreamReader("base.txt");

            string[] tempStr = file.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            InitialiseBase(tempStr.Length / 4);

            for (int i = 0; i < tempStr.Length; i += 4)
            {
                Program.week[i / 4] = new Day(tempStr[i], tempStr[i + 1], int.Parse(tempStr[i + 2]), tempStr[i + 3]);
            }

            file.Close();
        }

        private static void ReadKey()
        {

        Start:

            Console.WriteLine("Додавання записiв: +");
            Console.WriteLine("Редагування записiв: E");
            Console.WriteLine("Знищення записiв: -");
            Console.WriteLine("Виведення записiв: Enter");
            Console.WriteLine("Загальна кiлькiсть вiдвiдувачiв: A");
            Console.WriteLine("Днi з максимальною кiлькiстю вiдвiдувачiв: M");
            Console.WriteLine("Записи з найбiльшою кiлькiстю слiв в коментарi: L");
            Console.WriteLine("Вихiд: Esc");

            ConsoleKey key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.OemPlus:
                    Add();
                    goto Start;

                case ConsoleKey.E:
                    Edit();
                    goto Start;

                case ConsoleKey.A:
                    Sum();
                    goto Start;

                case ConsoleKey.M:
                    Maximum();
                    goto Start;

                case ConsoleKey.L:
                    LongestComent();
                    goto Start;

                case ConsoleKey.OemMinus:
                    Remove();
                    goto Start;

                case ConsoleKey.Enter:
                    Write();
                    goto Start;

                case ConsoleKey.Escape:
                    return;

                default:
                    Console.WriteLine();
                    goto Start;
            }
        }

        private static void Add()
        {
            StreamWriter file = new StreamWriter("base.txt", true);

            Console.WriteLine("Введiть новi данi");

            Console.Write("Назва: ");

            file.WriteLine(Console.ReadLine());

            Console.Write("Прiзвище скульптора: ");

            file.WriteLine(Console.ReadLine());

        Retry:
            Console.Write("Кiлькiсть вiдвiдувачiв: ");

            try
            {
                file.WriteLine(int.Parse(Console.ReadLine()));
            }
            catch (SystemException)
            {
                Console.WriteLine("Кiлькiсть вiдвiдувачiв має бути вказана лише числом!");

                goto Retry;
            }

            Console.Write("Коментар: ");

            file.WriteLine(Console.ReadLine());

            file.Close();

            ReadBase();
        }

        private static void Remove()
        {
            Console.WriteLine();

            Write();

            Console.Write("Порядковий номер запису для видалення: ");

            bool[] remove = new bool[Program.week.Length];

            for (int i = 0; i < remove.Length; ++i)
            {
                remove[i] = false;
            }

            try
            {
                remove[int.Parse(Console.ReadLine()) - 1] = true;
            }
            catch (SystemException)
            {
                Console.WriteLine("Такого запису не iснує!");
                return;
            }

            StreamWriter file = new StreamWriter("base.txt");

            for (int i = 0; i < Program.week.Length; ++i)
            {
                if (!remove[i])
                {
                    file.WriteLine(Program.week[i].Name);
                    file.WriteLine(Program.week[i].SculptorSurename);
                    file.WriteLine(Program.week[i].VisitorsCount);
                    file.WriteLine(Program.week[i].Coment);
                }
            }

            Console.Write("Видалено.\n");

            file.Close();

            ReadBase();
        }

        private static void Edit()
        {
            Console.WriteLine();

            Write();

            Console.Write("Порядковий номер запису для редагування: ");

            bool[] edit = new bool[Program.week.Length];

            for (int i = 0; i < edit.Length; ++i)
            {
                edit[i] = false;
            }

            try
            {
                edit[int.Parse(Console.ReadLine()) - 1] = true;
            }
            catch (SystemException)
            {
                Console.WriteLine("Такого запису не iснує!");
                return;
            }

            StreamWriter file = new StreamWriter("base.txt");

            for (int i = 0; i < Program.week.Length; ++i)
            {
                if (edit[i])
                {
                    Console.WriteLine("Введiть новi данi");

                    Console.Write("Назва: ");

                    file.WriteLine(Console.ReadLine());

                    Console.Write("Прiзвище скульптора: ");

                    file.WriteLine(Console.ReadLine());

                Retry:
                    Console.Write("Кiлькiсть вiдвiдувачiв: ");

                    try
                    {
                        file.WriteLine(int.Parse(Console.ReadLine()));
                    }
                    catch (SystemException)
                    {
                        Console.WriteLine("Кiлькiсть вiдвiдувачiв має бути вказана лише числом!");

                        goto Retry;
                    }

                    Console.Write("Коментар: ");

                    file.WriteLine(Console.ReadLine());
                }
                else
                {
                    file.WriteLine(Program.week[i].Name);
                    file.WriteLine(Program.week[i].SculptorSurename);
                    file.WriteLine(Program.week[i].VisitorsCount);
                    file.WriteLine(Program.week[i].Coment);
                }
            }

            Console.Write("Змiни внесено.\n");

            file.Close();

            ReadBase();
        }

        private static void InitialiseBase(int n)
        {
            Program.week = new Day[n];
        }

        private static void Sum()
        {
            int sum = 0;

            for (int i = 0; i < Program.week.Length; ++i)
            {
                sum += Program.week[i].VisitorsCount;
            }

            Console.WriteLine("\nЗагальна кiлькiсть вiдвiдувачiв: {0}.", sum);
        }

        private static void Maximum()
        {
            int maxIndex = 0;

            for (int i = 0; i < Program.week.Length; ++i)
            {
                if (Program.week[maxIndex].VisitorsCount <= Program.week[i].VisitorsCount)
                {
                    maxIndex = i;
                }
            }

            Console.WriteLine("Днi з максимальною кiлькiстю вiдвiдувачiв:");
            Console.WriteLine(_format, "Назва", "Прiзвище скульптора", "День", "Коментар");

            for (int i = 0; i < Program.week.Length; ++i)
            {
                if (Program.week[maxIndex].VisitorsCount == Program.week[i].VisitorsCount)
                {
                    Console.WriteLine(_format, Program.week[i].Name, Program.week[i].SculptorSurename, Program.week[i].VisitorsCount, Program.week[i].Coment);
                }
            }
        }

        private static void LongestComent()
        {
            Console.WriteLine();

            int maxIndex = 0;

            for (int i = 0; i < Program.week.Length; ++i)
            {
                if (Program.week[maxIndex].ComentLength() <= Program.week[i].ComentLength())
                {
                    maxIndex = i;
                }
            }

            Console.WriteLine("Записи з найбiльшою кiлькiстю слiв в коментарi:");
            Console.WriteLine(_format, "Назва", "Прiзвище скульптора", "День", "Коментар");

            for (int i = 0; i < Program.week.Length; ++i)
            {
                if (Program.week[maxIndex].VisitorsCount == Program.week[i].VisitorsCount)
                {
                    Console.WriteLine(_format, Program.week[i].Name, Program.week[i].SculptorSurename, Program.week[i].VisitorsCount, Program.week[i].Coment);
                }
            }
        }

        private const string _format = "{0, -20} {1, -35} {2, -15} {3, -10}";

        private static void Write()
        {
            Console.WriteLine(_format, "Назва", "Прiзвище скульптора", "Кiлькiсть вiдвiдувачiв", "Коментар");

            for (int i = 0; i < Program.week.Length; ++i)
            {
                Console.WriteLine(_format, Program.week[i].Name, Program.week[i].SculptorSurename, Program.week[i].VisitorsCount, Program.week[i].Coment);
            }
        }
    }
}
