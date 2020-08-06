using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Autofac;
using NUnit.Framework;
using MoreLinq;
using static System.Console;

namespace S0002_DN_Singleton
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }
    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> _capitals;
        private static int instanceCount;//0
        public static int Count => instanceCount;
        private SingletonDatabase()
        {
            instanceCount++;
            WriteLine("Initializing database");

            //capitals = File.ReadAllLines("capitals.txt")
            //    .Batch(2)
            //    .ToDictionary(
            //        list => list.ElementAt(0).Trim(),
            //        list => int.Parse(list.ElementAt(1)));

            //more safe
            _capitals = File.ReadAllLines(
                Path.Combine(new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName, "capitals.txt")
                )
                .Batch(2)
                .ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1))
                    );
        }
        public int GetPopulation(string name)
        {
            return _capitals[name];
        }

        private static Lazy<SingletonDatabase> _instance =
            new Lazy<SingletonDatabase>(() => new SingletonDatabase());
        public static SingletonDatabase Instance => _instance.Value;
    }
    public class OrdinaryDatabase : IDatabase //is not singleton, only test how this work
    {
        private readonly Dictionary<string, int> _capitals;
        private static int instanceCount;//0
        public OrdinaryDatabase()
        {
            WriteLine("Initializing database");

            _capitals = File.ReadAllLines(
                    Path.Combine(new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName ?? string.Empty, "capitals.txt")
                )
                .Batch(2)
                .ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1))
                );
        }
        public int GetPopulation(string name)
        {
            return _capitals[name];
        }
    }
    public class SingletonRecordFinder
    {
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int output = 0;
            foreach (var name in names)
            {
                output += SingletonDatabase.Instance.GetPopulation(name);
            }

            return output;
        }
    }
    public class ConfigurableRecordFinder
    {
        private IDatabase _database;

        public ConfigurableRecordFinder(IDatabase database)
        {
            _database = database ?? throw new ArgumentNullException(paramName:nameof(database));
        }
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int output = 0;
            foreach (var name in names)
            {
                output += _database.GetPopulation(name);
            }
            return output;
        }
    }
    public class DummyDatabase : IDatabase
    {
        public int GetPopulation(string name)
        {
            return new Dictionary<string,int>{["alpha"]=1,["beta"]=2,["gamma"]=3}[name];
        }
    }

    [TestFixture]
    public class SingletonTests 
    {
        [Test]
        public void IsSingletonTest() 
        {
            var db = SingletonDatabase.Instance;
            var db2 = SingletonDatabase.Instance;
            Assert.That(db, Is.SameAs(db2));
            Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
        }
        [Test]
        public void SingletonTotalPopulationTest()
        {
            var rf = new SingletonRecordFinder();
            var names = new[] {"Seoul", "Mexico City"};
            int tp = rf.GetTotalPopulation(names);
            Assert.That(tp,Is.EqualTo(17500000+17400000));
        }

        [Test]
        public void ConfigurablePopulationTest()
        {
            var rf = new ConfigurableRecordFinder(new DummyDatabase());
            var names = new[] {"alpha", "gamma"};
            int tp = rf.GetTotalPopulation(names);
            Assert.That(tp,Is.EqualTo(4));
        }

        [Test]
        public void DIPopulationTest()
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<OrdinaryDatabase>()
                .As<IDatabase>()
                .SingleInstance();
            cb.RegisterType<ConfigurableRecordFinder>();
            using (var c = cb.Build())
            {
                var rf = c.Resolve<ConfigurableRecordFinder>();
            }
        }
    }
    static class Program
    {
        static void Main(string[] args)
        {
            //----------------------
            var db = SingletonDatabase.Instance;
            var city = "Tokyo";
            WriteLine($"{city} has population {db.GetPopulation(city)}");

            //---------------------- 
            var ceo = new Monostate
            {
                Name = "Adam Smith",
                Age = 55
            };

            var ceo2 = new Monostate();
            WriteLine(ceo2);
            //-----------------------
            var builder = new ContainerBuilder();
            builder.RegisterType<EventBroker>().SingleInstance();
            builder.RegisterType<Foo>();

            using (var c = builder.Build())
            {
                var foo1 = c.Resolve<Foo>();
                var foo2 = c.Resolve<Foo>();

                WriteLine($"ReferenceEquals(foo1, foo2) {ReferenceEquals(foo1, foo2)}");
                WriteLine($"ReferenceEquals(foo1.Broker, foo2.Broker) {ReferenceEquals(foo1.Broker, foo2.Broker)}");
            }
            ReadKey();
        }
    }
}
