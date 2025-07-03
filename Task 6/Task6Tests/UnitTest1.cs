using NUnit.Framework;

namespace Task6Tests
{
    [TestFixture]
    public class TowerOfHanoiTests
    {
        [Test]
        public void TestHanoi8Disks()
        {
            var hanoi = new Task_6.Services.TowerOfHanoi();
            hanoi.Solve(8, 'A', 'C', 'B'); // A - исходный, C - целевой, B - вспомогательный

            int expectedMoves = (int)Math.Pow(2, 8) - 1;
            Assert.AreEqual(expectedMoves, hanoi.Moves.Count, "Количество ходов должно соответствовать минимальному количеству: 2^n - 1");

            // Проверка первой и последней инструкции
            Assert.IsTrue(hanoi.Moves[0].Contains("диск 1"));
            Assert.IsTrue(hanoi.Moves[^1].Contains("диск 1"));
        }
    }

}