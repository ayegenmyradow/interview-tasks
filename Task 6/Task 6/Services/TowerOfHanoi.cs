using System;
using System.Collections.Generic;

namespace Task_6.Services
{
    public class TowerOfHanoi
    {
        public List<string> Moves { get; } = new List<string>();

        /// <summary>
        /// Основной метод решения задачи Ханойской башни.
        /// </summary>
        /// <param name="n">Количество дисков</param>
        /// <param name="from">Исходный стержень</param>
        /// <param name="to">Целевой стержень</param>
        /// <param name="aux">Вспомогательный стержень</param>
        public void Solve(int n, char from, char to, char aux)
        {
            if (n <= 0) return;

            // Переносим n-1 дисков на вспомогательный стержень
            Solve(n - 1, from, aux, to);

            // Перемещаем самый большой диск
            Moves.Add($"Move disk {n} from rod {from} to rod {to}");

            // Переносим оставшиеся n-1 дисков на целевой стержень
            Solve(n - 1, aux, to, from);
        }

        public void PrintMoves()
        {
            foreach (var move in Moves)
            {
                Console.WriteLine(move);
            }
            Console.WriteLine($"Total moves: {Moves.Count}");
        }
    }
}
