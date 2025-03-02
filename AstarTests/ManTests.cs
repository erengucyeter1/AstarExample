using Microsoft.VisualStudio.TestTools.UnitTesting;
using AstarExample;

namespace AstarExample.Tests
{
    [TestClass]
    public class ManTests
    {
        [TestMethod]
        public void CalculateMannhattanDistance_SamePosition_ShouldReturnZero()
        {
            // Arrange
            var position = new Position(0, 0);
            var man1 = new Man(position);
            var man2 = new Man(position);

            // Act
            int distance = man1.CalculateMannhattanDistance(man2);

            // Assert
            Assert.AreEqual(0, distance);
        }

        [TestMethod]
        public void CalculateMannhattanDistance_DifferentPositions_ShouldReturnCorrectDistance()
        {
            // Arrange
            var position1 = new Position(0, 0);
            var position2 = new Position(3, 4);
            var man1 = new Man(position1);
            var man2 = new Man(position2);

            // Act
            int distance = man1.CalculateMannhattanDistance(man2);

            // Assert
            Assert.AreEqual(7, distance); // 3 + 4 = 7
        }

        [TestMethod]
        public void CalculateMannhattanDistance_AdjacentPositions_ShouldReturnOne()
        {
            // Arrange
            var position1 = new Position(0, 0);
            var position2 = new Position(0, 1);
            var man1 = new Man(position1);
            var man2 = new Man(position2);

            // Act
            int distance = man1.CalculateMannhattanDistance(man2);

            // Assert
            Assert.AreEqual(1, distance);
        }
    }
}
