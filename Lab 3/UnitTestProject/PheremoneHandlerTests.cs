using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using TravelingSalesMan_AntColony;

namespace UnitTestProject
{
    [TestClass]
    public class PheremoneHandlerTests
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void PheremoneOnUnvisitedEdge()
        {
            PheremoneHandler pheremoneHandler = new PheremoneHandler();

            Location location1 = new Location(1, 1, 2);
            Location location2 = new Location(2, 3, 4);

            Assert.AreEqual(10.0, pheremoneHandler.GetPheremone(location1, location2));
        }

        [TestMethod]
        public void EvaporatePheremoneOnUnvisitedEdge()
        {
            PheremoneHandler pheremoneHandler = new PheremoneHandler();

            // update with empty list of ants, this would only trigger evaporation of pheremones
            pheremoneHandler.Update(new List<IAnt>());

            Location location1 = new Location(1, 1, 2);
            Location location2 = new Location(2, 3, 4);

            Assert.AreEqual(8.0, pheremoneHandler.GetPheremone(location1, location2));
        }

        [TestMethod]
        public void EvaporatePheremoneTwiceOnUnvisitedEdge()
        {
            PheremoneHandler pheremoneHandler = new PheremoneHandler();

            // update with empty list of ants, this would only trigger evaporation of pheremones
            pheremoneHandler.Update(new List<IAnt>());
            pheremoneHandler.Update(new List<IAnt>());

            Location location1 = new Location(1, 1, 2);
            Location location2 = new Location(2, 3, 4);

            Assert.AreEqual(6.4, pheremoneHandler.GetPheremone(location1, location2), 0.001);
        }

        [TestMethod]
        public void OneAntHasWalkedOnce()
        {
            PheremoneHandler pheremoneHandler = new PheremoneHandler();

            Location location1 = new Location(1, 1, 1);
            Location location2 = new Location(2, 1, 3);
            var visited = new List<Location>(){ location1, location2 };

            Mock antMock = new Mock<IAnt>();
            antMock.SetReturnsDefault<List<Location>>(visited);
            antMock.SetReturnsDefault<double>(2);

            var ants = new List<IAnt>();
            ants.Add((IAnt)antMock.Object);

            // update with list of 1 ant, this would only trigger evaporation of pheremones
            pheremoneHandler.Update(ants);
            
            double actual = pheremoneHandler.GetPheremone(location1, location2);
            Assert.AreEqual(8.5, actual, 0.001);
        }

        [TestMethod]
        public void SeveralAntsHasWalked()
        {
            PheremoneHandler pheremoneHandler = new PheremoneHandler();

            Location location1 = new Location(1, 1, 1);
            Location location2 = new Location(2, 1, 3);
            
            var visited = new List<Location>() { location1, location2 };

            Mock antMock1 = new Mock<IAnt>();
            antMock1.SetReturnsDefault<List<Location>>(visited);
            antMock1.SetReturnsDefault<double>(2);

            Mock antMock2 = new Mock<IAnt>();
            antMock2.SetReturnsDefault<List<Location>>(visited);
            antMock2.SetReturnsDefault<double>(3);

            var ants = new List<IAnt>();
            ants.Add((IAnt)antMock1.Object);
            ants.Add((IAnt)antMock2.Object);

            // update with list of 2 ants, this would only trigger evaporation of pheremones
            pheremoneHandler.Update(ants);

            double actual = pheremoneHandler.GetPheremone(location1, location2);
            
            Assert.AreEqual(8.833, actual, 0.001);
        }

        [TestMethod]
        public void AntsWalkingDifferentPaths()
        {
            PheremoneHandler pheremoneHandler = new PheremoneHandler();

            Location location1 = new Location(1, 1, 1);
            Location location2 = new Location(2, 1, 3);
            Location location3 = new Location(3, 1, 6);

            var visited1 = new List<Location>() { location1, location2 };
            var visited2 = new List<Location>() { location1, location3 };

            Mock antMock1 = new Mock<IAnt>();
            antMock1.SetReturnsDefault<List<Location>>(visited1);
            antMock1.SetReturnsDefault<double>(2);

            Mock antMock2 = new Mock<IAnt>();
            antMock2.SetReturnsDefault<List<Location>>(visited2);
            antMock2.SetReturnsDefault<double>(3);

            var ants = new List<IAnt>();
            ants.Add((IAnt)antMock1.Object);
            ants.Add((IAnt)antMock2.Object);

            // update with list of 2 ants, this would only trigger evaporation of pheremones
            pheremoneHandler.Update(ants);

            double actual = pheremoneHandler.GetPheremone(location1, location2);

            Assert.AreEqual(8.5, actual, 0.001);
        }
    }
}
