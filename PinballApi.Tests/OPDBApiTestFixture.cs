using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using PinballApi.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace PinballApi.Tests
{
    [TestFixture]
    internal class OPDBApiTestFixture
    {
        private IOpdbApi OpdbApi;
        private const string AddamsFamilyOpdbId = "G4ODR-MLzY7";

        [SetUp]
        public void SetUp()
        {
            var t = new ConfigurationBuilder().AddUserSecrets<Settings>().Build();

            var apiToken = t["OPDBToken"];
            OpdbApi = new OPDBApi(apiToken);
        }

        [Test]
        public async Task OPDBApi_GetMachine_ShouldReturnCorrectMachine()
        {
            var machine = await OpdbApi.GetMachineInfo(AddamsFamilyOpdbId);

            Assert.That(machine.IsMachine);
            Assert.That(machine.Shortname, Is.EqualTo("TAFG"));
            Assert.That(machine.OpdbId, Is.EqualTo(AddamsFamilyOpdbId));
        }

        [Test]
        public async Task OPDBApi_GetMachineByIpdbId_ShouldReturnCorrectMachine()
        {
            var machine = await OpdbApi.GetMachineInfoByIpdbId("21");

            Assert.That(machine.IsMachine);
            Assert.That(machine.Shortname, Is.EqualTo("TAFG"));
            Assert.That(machine.OpdbId, Is.EqualTo(AddamsFamilyOpdbId));
        }

        [Test]
        public async Task OPDBApi_SearchMachines_ShouldReturnCorrectMachines()
        {
            var machine = await OpdbApi.Search("Addams");

            Assert.That(machine.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task OPDBApi_SearchMachines_WithoutAliases_ShouldReturnCorrectMachines()
        {
            var machine = await OpdbApi.Search("Addams", includeAliases: false);

            Assert.That(machine.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task OPDBApi_SearchMachines_RequireOPDB_ShouldReturnCorrectMachines()
        {
            var machine = await OpdbApi.Search("Addams", requireOpdb: false);

            Assert.That(machine.Count(), Is.EqualTo(3));
            Assert.That(machine.First().IpdbId, Is.GreaterThan(0));
        }

        [Test]
        public async Task OPDBApi_TypeAheadSearchMachines_ShouldReturnCorrectMachines()
        {
            var machine = await OpdbApi.TypeAheadSearch("Addams");

            Assert.That(machine.Count(), Is.EqualTo(3));
        }

        [Test]
        public async Task OPDBApi_Export_ShouldAllMachines()
        {
            var machine = await OpdbApi.Export();

            Assert.That(machine.Count(), Is.GreaterThan(1000));
        }
    }
}
