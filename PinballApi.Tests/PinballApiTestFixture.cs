using NUnit.Framework;

namespace PinballApi.Tests
{
    [TestFixture]
    public class PinballApiTestFixture
    {
        PinballMachineApi api = new PinballMachineApi();

        [Test]
        public void PinballMachines_ShouldReturnAllPinballMachines()
        {
            var machines = api.GetAllPinballMachines();

            Assert.That(machines, Is.Not.Empty);
        }

        [Test]
        public void PinballManufacturers_ShouldReturnAllManufacturers()
        {
            var manufacturers = api.GetAllPinballManufacturers();

            Assert.That(manufacturers, Is.Not.Empty);
        }

        [Test]
        public void PinballManufacturers_ShouldReturnSpecificManufacturers()
        {
            var name = "Stern Pinball, Incorporated";
            var manufacturer = api.GetPinballManufacturerByName(name);

            Assert.That(manufacturer, Is.Not.Null);
            Assert.That(manufacturer.Name.Contains(name));
        }

        [Test]
        public void PinballApi_ShouldClearCache()
        {
            var manufacturers = api.GetAllPinballManufacturers();

            Assert.DoesNotThrow(() => api.ClearCache());            
        }
    }
}
