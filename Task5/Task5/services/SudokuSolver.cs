using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.services
{
    public class SudokuSolver
    {

        private const int SIZE = 9;

        // Основной метод для запуска решения
        public bool Solve(int[,] board)
        {
            for (int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    // Если клетка пуста (обозначена 0)
                    if (board[row, col] == 0)
                    {
                        // Пробуем цифры от 1 до 9
                        for (int num = 1; num <= 9; num++)
                        {
                            if (IsSafe(board, row, col, num))
                            {
                                board[row, col] = num;

                                if (Solve(board))
                                    return true;

                                board[row, col] = 0; // backtrack
                            }
                        }

                        return false; // нет подходящих чисел
                    }
                }
            }
            PrintBoard(board);
            return true; // всё заполнено
        }

        // Проверка безопасности: нет ли конфликта в строке, столбце и 3x3 блоке
        private bool IsSafe(int[,] board, int row, int col, int num)
        {
            // Проверка строки и столбца
            for (int i = 0; i < SIZE; i++)
            {
                if (board[row, i] == num || board[i, col] == num)
                    return false;
            }

            // Проверка 3x3 блока
            int startRow = row - row % 3;
            int startCol = col - col % 3;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[startRow + i, startCol + j] == num)
                        return false;
                }
            }

            return true;
        }

        // Печать судоку
        public void PrintBoard(int[,] board)
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

    }
}
