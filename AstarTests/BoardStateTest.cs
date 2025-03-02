using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AstarExample.Tests
{
    [TestClass]
    public class BoardStateTests
    {
        [TestMethod]
        public void CalculateH_ShouldReturnCorrectHeuristicValue()
        {
            // Arrange
            var startMen = new Man[,]
            {
                { new Man(new Position(0, 0)) { Value = "A" }, new Man(new Position(0, 1)) { Value = "B" }, new Man(new Position(0, 2)) { Value = "C" } },
                { new Man(new Position(1, 0)) { Value = "D" }, new Man(new Position(1, 1)) { Value = "E" }, new Man(new Position(1, 2)) { Value = "F" } },
                { new Man(new Position(2, 0)) { Value = "G" }, new Man(new Position(2, 1)) { Value = "H" }, new Man(new Position(2, 2)) { Value = "I" } }
            };

            var goalMen = new Man[,]
            {
                { new Man(new Position(0,0)) { Value = "G" }, new Man(new Position(0, 1)) { Value = "B" }, new Man(new Position(0, 2)) { Value = "C" } },
                { new Man(new Position(1, 0)) { Value = "D" }, new Man(new Position(1, 1)) { Value = "E" }, new Man(new Position(1, 2)) { Value = "F" } },
                { new Man(new Position(2, 0)) { Value = "A" }, new Man(new Position(2, 1)) { Value = "H" }, new Man(new Position(2, 2)) { Value = "I" } }
            };



            var startState = new BoardState(startMen, null, null);
            var goalState = new BoardState(goalMen, null, null);

            // Act
            int heuristicValue = startState.CalculateH(goalState);

            // Assert
            Assert.AreEqual(4, heuristicValue); // Expected heuristic value based on Manhattan distance
        }



        [TestMethod]
        public void GetMan_ShouldReturnCorrectMan_WhenValueExists()
        {
            // Arrange
            var men = new Man[,]
            {
                { new Man(new Position(0, 0)) { Value = "A" }, new Man(new Position(0, 1)) { Value = "B" }, new Man(new Position(0, 2)) { Value = "C" } },
                { new Man(new Position(1, 0)) { Value = "D" }, new Man(new Position(1, 1)) { Value = "E" }, new Man(new Position(1, 2)) { Value = "F" } },
                { new Man(new Position(2, 0)) { Value = "G" }, new Man(new Position(2, 1)) { Value = "H" }, new Man(new Position(2, 2)) { Value = "I" } }
            };

            var boardState = new BoardState(men, null, null);

            // Act
            var man = boardState.GetMan("E");

            // Assert
            Assert.IsNotNull(man);
            Assert.AreEqual("E", man.Value);
            Assert.AreEqual(1, man.Position.X);
            Assert.AreEqual(1, man.Position.Y);
        }

        [TestMethod]
        public void GetMan_ShouldReturnNull_WhenValueDoesNotExist()
        {
            // Arrange
            var men = new Man[,]
            {
                { new Man(new Position(0, 0)) { Value = "A" }, new Man(new Position(0, 1)) { Value = "B" }, new Man(new Position(0, 2)) { Value = "C" } },
                { new Man(new Position(1, 0)) { Value = "D" }, new Man(new Position(1, 1)) { Value = "E" }, new Man(new Position(1, 2)) { Value = "F" } },
                { new Man(new Position(2, 0)) { Value = "G" }, new Man(new Position(2, 1)) { Value = "H" }, new Man(new Position(2, 2)) { Value = "I" } }
            };

            var boardState = new BoardState(men, null, null);

            // Act
            var man = boardState.GetMan("Z");

            // Assert
            Assert.IsNull(man);
        }

        [TestMethod]
        public void GetMan_ShouldReturnCorrectMan_WhenValueIsEmptyString()
        {
            // Arrange
            var men = new Man[,]
            {
                { new Man(new Position(0, 0)) { Value = "" }, new Man(new Position(0, 1)) { Value = "B" }, new Man(new Position(0, 2)) { Value = "C" } },
                { new Man(new Position(1, 0)) { Value = "D" }, new Man(new Position(1, 1)) { Value = "E" }, new Man(new Position(1, 2)) { Value = "F" } },
                { new Man(new Position(2, 0)) { Value = "G" }, new Man(new Position(2, 1)) { Value = "H" }, new Man(new Position(2, 2)) { Value = "I" } }
            };

            var boardState = new BoardState(men, null, null);

            // Act
            var man = boardState.GetMan("");

            // Assert
            Assert.IsNotNull(man);
            Assert.AreEqual("", man.Value);
            Assert.AreEqual(0, man.Position.X);
            Assert.AreEqual(0, man.Position.Y);
        }


        [TestMethod]
        public void ExchangeManLocations_ValidPositions_ExchangesCorrectly()
        {
            // Arrange
            var initialBoard = new Man[,]
            {
                { new Man(new Position(0, 0)) { Value = "" }, new Man(new Position(0, 1)) { Value = "B" }, new Man(new Position(0, 2)) { Value = "C" } },
                { new Man(new Position(1, 0)) { Value = "D" }, new Man(new Position(1, 1)) { Value = "E" }, new Man(new Position(1, 2)) { Value = "F" } },
                { new Man(new Position(2, 0)) { Value = "G" }, new Man(new Position(2, 1)) { Value = "H" }, new Man(new Position(2, 2)) { Value = "I" } }
            };


            var boardState = new BoardState(initialBoard, null, null);
            var source = new Position(1, 1);
            var destination = new Position(0,1);

            // Act
            var newState = boardState.ExchangeManLocations(source, destination);

            // Assert
            Assert.AreEqual(new Position(1, 1), newState.Men[1, 1].Position);
            Assert.AreEqual(new Position(0, 1), newState.Men[0,1].Position);
        }

        [TestMethod]
        public void ExchangeManLocations_SamePosition_NoChange()
        {
            // Arrange
            var initialBoard = new Man[2, 2]
            {
                { new Man(new Position(1, 1)), new Man(new Position(1, 2)) },
                { new Man(new Position(2, 1)), new Man(new Position(2, 2)) }
            };
            var boardState = new BoardState(initialBoard, null, null);
            var source = new Position(1, 1);
            var destination = new Position(1, 1);

            // Act
            var newState = boardState.ExchangeManLocations(source, destination);

            // Assert
            Assert.AreEqual(new Position(1, 1), newState.Men[0, 0].Position);
        }



        [TestMethod]
        public void TestHashSetWithBoardState()
        {
            // Arrange
            var men1 = new Man[3, 3]
            {
                { new Man(new Position(0, 0)) { Value = "4" }, new Man(new Position(0, 1)) { Value = "1" }, new Man(new Position(0, 2)) { Value = "3" } },
                { new Man(new Position(1, 0)) { Value = "2" }, new Man(new Position(1, 1)) { Value = "" }, new Man(new Position(1, 2)) { Value = "5" } },
                { new Man(new Position(2, 0)) { Value = "7" }, new Man(new Position(2, 1)) { Value = "8" }, new Man(new Position(2, 2)) { Value = "6" } }
            };
            var boardState1 = new BoardState(men1, null, null) { F = 5 };

            var men2 = new Man[3, 3]
            {
                { new Man(new Position(0, 0)) { Value = "4" }, new Man(new Position(0, 1)) { Value = "1" }, new Man(new Position(0, 2)) { Value = "3" } },
                { new Man(new Position(1, 0)) { Value = "2" }, new Man(new Position(1, 1)) { Value = "" }, new Man(new Position(1, 2)) { Value = "5" } },
                { new Man(new Position(2, 0)) { Value = "7" }, new Man(new Position(2, 1)) { Value = "8" }, new Man(new Position(2, 2)) { Value = "6" } }
            };
            var boardState2 = new BoardState(men2, null, null) { F = 10 };

            var hashSet = new HashSet<BoardState>(new BoardStateEqualityComparer());
            // Act
            hashSet.Add(boardState1);
            bool contains = hashSet.Contains(boardState2);

            // Assert
            Assert.IsTrue(contains, "HashSet should contain boardState2 because Men values are the same.");
        }
    }
}


