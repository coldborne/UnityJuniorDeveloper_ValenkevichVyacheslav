﻿using System;

namespace Aquarium
{
    public enum Commands : byte
    {
        First = 1,
        Second,
        Third,
        Fourth
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            Aquarium aquarium = new Aquarium();

            aquarium.Init();

            bool isAquariumWithoutDamage = true;

            while (isAquariumWithoutDamage)
            {
                aquarium.ShowAllFish();

                Console.WriteLine("Выберите действие");
                Console.WriteLine($"{(int)Commands.First} - Добавить рыбку");
                Console.WriteLine($"{(int)Commands.Second} - Вынуть рыбку");
                Console.WriteLine($"{(int)Commands.Third} - Пропустить итерацию");
                Console.WriteLine($"{(int)Commands.Fourth} - Сломать аквариум");

                int userInput = UserUtils.ReadCommand();

                aquarium.ReduceFishAge();

                aquarium.CleanAquariumOfDeadFish();

                switch (userInput)
                {
                    case (int)Commands.First:
                        aquarium.TryAddFish();
                        break;
                    case (int)Commands.Second:
                        aquarium.TryPullOutRandomFish();
                        break;
                    case (int)Commands.Third:
                        break;
                    case (int)Commands.Fourth:
                        isAquariumWithoutDamage = false;
                        break;
                }
            }
        }
    }
}