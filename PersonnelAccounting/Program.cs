using System;

namespace PersonnelAccounting
{
    internal class Program
    {
        private static string[] _fullNames = new string[0];
        private static string[] _positions = new string[0];

        static void Main(string[] args)
        {
            bool isProgramWork = true;

            Console.WriteLine("Добро пожаловать в нашу секретную библиотеку!\nУ нас можно:");

            while (isProgramWork)
            {
                Console.WriteLine("1 - Добавить досье, 2 - Вывести все досье, 3 - Удалить одно досье, 4 - Поиск по фамилии, 5 - Выход");
                Console.WriteLine("Чтобы перейти к нужному функционалу, введите нужную цифру");
                int commandNumber = ReadIntValue();

                switch (commandNumber)
                {
                    case 1:
                        AddDossier();
                        break;
                    case 2:
                        ShowAllDossiers();
                        break;
                    case 3:
                        DeleteDossier();
                        break;
                    case 4:
                        SearchDossierByLastName();
                        break;
                    case 5:
                        isProgramWork = false;
                        break;
                    default:
                        break;
                }
            }
        }

        private static void AddDossier()
        {
            string fullName = null;
            string position = null;

            ReadData(ref fullName, ref position);

            _fullNames = Expand(_fullNames);
            _positions = Expand(_positions);

            _fullNames[_fullNames.Length - 1] = fullName;
            _positions[_positions.Length - 1] = position;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Досье добавлено");
            Console.ResetColor();
        }

        private static void ReadData(ref string fullName, ref string position)
        {
            bool isFullNameCorrect = false;

            while (isFullNameCorrect == false)
            {
                Console.WriteLine("Введите фио");
                fullName = Console.ReadLine();

                isFullNameCorrect = IsContainsFullData(fullName);

                if (isFullNameCorrect == false)
                {
                    Console.WriteLine("ФИО должно содержать фамилию, имя и отчество (при наличии)");
                }
            }

            bool isPositionCorrect = false;

            while (isPositionCorrect == false)
            {
                Console.WriteLine("Введите должность человека");
                position = Console.ReadLine().Trim();

                if (position == "")
                {
                    Console.WriteLine("Должность не может быть пустой");
                }
                else
                {
                    isPositionCorrect = true;
                }
            }
        }

        private static string[] Expand(string[] array)
        {
            string[] tempFullNames = new string[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
            {
                tempFullNames[i] = array[i];
            }

            array = tempFullNames;

            return array;
        }

        private static void ShowAllDossiers()
        {
            if (_fullNames.Length == 0)
            {
                Console.WriteLine("Ни одно досье пока не добавлено");
            }
            else
            {
                for (int i = 0; i < _fullNames.Length - 1; i++)
                {
                    Console.Write($"{i + 1} - {_fullNames[i]} {_positions[i]} ");
                }

                Console.Write($"{_fullNames.Length} - {_fullNames[_fullNames.Length - 1]} {_positions[_fullNames.Length - 1]} \n");
            }
        }

        private static void DeleteDossier()
        {
            Console.WriteLine($"В системе зарегистрировано {_fullNames.Length} досье. Укажите номер досье, которое желаете удалить");
            int numberOfDossier = ReadIntValue();
            int numberOfRemovedElement = numberOfDossier - 1;

            if (numberOfRemovedElement >= 0 && numberOfRemovedElement < _fullNames.Length && numberOfRemovedElement < _positions.Length)
            {
                _fullNames = Reduce(_fullNames, numberOfRemovedElement);
                _positions = Reduce(_positions, numberOfRemovedElement);
            }
            else
            {
                Console.WriteLine("Досье под таким номером не найдено");
            }
        }

        private static string[] Reduce(string[] array, int numberOfRemovedElement)
        {
            string[] tempFullNames = new string[array.Length - 1];

            for (int i = 0; i < numberOfRemovedElement; i++)
            {
                tempFullNames[i] = array[i];
            }

            for (int i = numberOfRemovedElement + 1; i < array.Length; i++)
            {
                tempFullNames[i - 1] = array[i];
            }

            array = tempFullNames;

            return array;
        }

        private static void SearchDossierByLastName()
        {
            Console.WriteLine("Введите желаемую фамилию");
            string lastName = Console.ReadLine();

            bool wasDossierFound = false;

            for (int i = 0; i < _fullNames.Length; i++)
            {
                if (lastName == _fullNames[i].Split()[0])
                {
                    wasDossierFound = true;
                    Console.WriteLine(_fullNames[i] + " " + _positions[i]);
                }
            }

            if (wasDossierFound == false)
            {
                Console.WriteLine("Ничего не найдено");
            }
        }

        private static int ReadIntValue()
        {
            int value = 0;
            bool isIntValue = false;

            while (isIntValue == false)
            {
                string userInput = Console.ReadLine();
                isIntValue = int.TryParse(userInput, out value);

                if (isIntValue == false)
                {
                    Console.WriteLine("Можно вводить только числа");
                }
            }

            return value;
        }

        private static bool IsContainsFullData(string fullName)
        {
            string[] words = fullName.Split(' ');
            int minimumAmountWords = 2;
            int maximumAmountWords = 3;

            return words.Length >= minimumAmountWords && words.Length <= maximumAmountWords;
        }
    }
}
