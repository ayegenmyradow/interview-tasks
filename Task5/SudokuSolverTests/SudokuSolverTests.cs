using NUnit.Framework;

[TestFixture]
public class SudokuSolverTests
{
    [Test]
    public void TestSudokuSolver()
    {
        int[,] board = new int[9, 9]
        {
            {5,3,0, 0,7,0, 0,0,0},
            {6,0,0, 1,9,5, 0,0,0},
            {0,9,8, 0,0,0, 0,6,0},

            {8,0,0, 0,6,0, 0,0,3},
            {4,0,0, 8,0,3, 0,0,1},
            {7,0,0, 0,2,0, 0,0,6},

            {0,6,0, 0,0,0, 2,8,0},
            {0,0,0, 4,1,9, 0,0,5},
            {0,0,0, 0,8,0, 0,7,9}
        };

        var solver = new Task5.services.SudokuSolver();
        bool solved = solver.Solve(board);

        Assert.IsTrue(solved, "Sudoku should be solvable");
        Assert.IsTrue(IsValidSolution(board), "Solution should be valid");
    }

    private bool IsValidSolution(int[,] board)
    {
        // Проверка каждой строки, столбца и блока 3x3
        for (int i = 0; i < 9; i++)
        {
            var row = new bool[10];
            var col = new bool[10];
            var box = new bool[10];

            for (int j = 0; j < 9; j++)
            {
                int rowVal = board[i, j];
                int colVal = board[j, i];
                int boxVal = board[(i / 3) * 3 + j / 3, (i % 3) * 3 + j % 3];

                if (row[rowVal] || col[colVal] || box[boxVal])
                    return false;

                row[rowVal] = col[colVal] = box[boxVal] = true;
            }
        }

        return true;
    }
}
