using ITI.TrainSerialization;
using ITI.TrainSerialization.Interfaces;
using NUnit.Framework;

namespace ITI.TrainSerialization.Tests
{
    [TestFixture]
    class T5TestsTravelling
    {
        [Test]
        public void T1_empty_city_doesnt_return_station()
        {
            ICity c = CityFactory.CreateCity("Paris");
            Assert.That(c.FindNearestStation(0, 0), Is.Null);
        }
        [Test]
        public void T2_city_returns_a_station_no_matter_the_distance()
        {
            ICity c = CityFactory.CreateCity("Paris");
            Assert.That(c.FindNearestStation(0, 0), Is.Null);

            IStation s = c.AddStation("Opera", 0, 0);
            IStation s1 = c.FindNearestStation(int.MaxValue, int.MaxValue);
            IStation s2 = c.FindNearestStation(int.MinValue, int.MinValue);
            IStation s3 = c.FindNearestStation(0, 0);
            Assert.AreSame(s, s1);
            Assert.AreSame(s, s2);
            Assert.AreSame(s, s3);
        }
        [Test]
        public void T3_city_returns_effectively_the_closest_station()
        {
            ICity c = CityFactory.CreateCity("Paris");

            IStation s = c.AddStation("Opera", 0, 0);
            IStation s1 = c.AddStation("Chatelet", 10, 10);

            Assert.AreSame(c.FindNearestStation(1, 1), s);
            Assert.AreSame(c.FindNearestStation(-10, -10), s);
            Assert.AreSame(c.FindNearestStation(10, 10), s1);
            Assert.AreSame(c.FindNearestStation(15, 15), s1);
        }
        [TestCase(0, 0, "station1", 2, 0, "station2", 1, 0)]
        [TestCase(-2, 0, "station1", 0, 0, "station2", -1, 0)]
        [TestCase(0, 2, "station1", 0, 0, "station2", 0, 1)]
        [TestCase(-2, 2, "station1", 2, -2, "station2", 0, 0)]
        public void T4_cities_at_the_same_distance_should_return_the_left_most_top_most_one(int x1, int y1, string name1, int x2, int y2, string name2, int xPos, int yPos)
        {
            ICity c = CityFactory.CreateCity("Paris");

            IStation s1 = c.AddStation(name1, x1, y1);
            IStation s2 = c.AddStation(name2, x2, y2);

            Assert.AreSame(c.FindNearestStation(xPos, yPos), s1);
        }
        [Test]
        public void T5_can_go_where_i_am()
        {
            ICity c = CityFactory.CreateCity("Paris");

            IStation s = c.AddStation("Opera", 0, 0);

            Assert.IsTrue(c.CanGo(s, s));
        }
        [Test]
        public void T6_can_go_to_station_on_same_line()
        {
            ICity c = CityFactory.CreateCity("Paris");

            IStation s1 = c.AddStation("Opera", 0, 0);
            IStation s2 = c.AddStation("Chatelet", 10, 10);
            IStation s3 = c.AddStation("Gare de Lyon", 20, 20);

            ILine l = c.AddLine("RER A");
            l.AddBefore(s1);
            l.AddBefore(s2);
            l.AddBefore(s3);

            Assert.IsTrue(c.CanGo(s1, s3));
            Assert.IsTrue(c.CanGo(s2, s3));
            Assert.IsTrue(c.CanGo(s1, s2));
            Assert.IsTrue(c.CanGo(s3, s1));
            Assert.IsTrue(c.CanGo(s3, s2));
            Assert.IsTrue(c.CanGo(s2, s1));
        }
    }
}