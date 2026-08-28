using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using PinballApi.Models.MatchPlay.Opdb;
using System.Linq;
using System.Threading.Tasks;

namespace PinballApi.Tests
{
    /// <summary>
    /// The OPDB and PinTips endpoints that Match Play took over from opdb.org.
    /// </summary>
    [TestFixture]
    internal class MatchPlayOpdbTestFixture
    {
        private const string AddamsFamilyGroupOpdbId = "G4ODR";
        private const string AddamsFamilyGoldOpdbId = "G4ODR-MLzY7";
        private const string BeatlesPlatinumAliasOpdbId = "G0l8P-M85d9-A1ZNY";
        private const int BlackPyramidArenaId = 1;

        private MatchPlayApi matchPlayApi;

        [SetUp]
        public void SetUp()
        {
            var t = new ConfigurationBuilder().AddUserSecrets<Settings>().Build();

            var apiToken = t["MatchPlayApiToken"];
            matchPlayApi = new MatchPlayApi(apiToken);
        }

        [Test]
        public async Task MatchPlayApi_GetOpdbEntry_ShouldReturnMachine()
        {
            var entry = await matchPlayApi.GetOpdbEntry(AddamsFamilyGoldOpdbId);

            Assert.Multiple(() =>
            {
                Assert.That(entry.OpdbId, Is.EqualTo(AddamsFamilyGoldOpdbId));
                Assert.That(entry.OpdbGroup, Is.EqualTo(AddamsFamilyGroupOpdbId));
                Assert.That(entry.ShortName, Is.EqualTo("TAFG"));
                Assert.That(entry.EntryType, Is.EqualTo(OpdbEntryType.Machine));
                Assert.That(entry.IsMachine, Is.True);
                Assert.That(entry.PhysicalMachine, Is.True);
                Assert.That(entry.IpdbId, Is.EqualTo(21));
                Assert.That(entry.Year, Is.EqualTo(1994));
                Assert.That(entry.Manufacturer.Name, Is.EqualTo("Bally"));
            });
        }

        [Test]
        public async Task MatchPlayApi_GetOpdbEntry_ShouldReturnMachineGroup()
        {
            var entry = await matchPlayApi.GetOpdbEntry(AddamsFamilyGroupOpdbId);

            Assert.Multiple(() =>
            {
                Assert.That(entry.OpdbId, Is.EqualTo(AddamsFamilyGroupOpdbId));
                Assert.That(entry.OpdbMachine, Is.Null);
                Assert.That(entry.EntryType, Is.EqualTo(OpdbEntryType.MachineGroup));
                Assert.That(entry.IsMachineGroup, Is.True);
                Assert.That(entry.PhysicalMachine, Is.False);
            });
        }

        [Test]
        public async Task MatchPlayApi_GetOpdbEntry_ShouldReturnAlias()
        {
            var entry = await matchPlayApi.GetOpdbEntry(BeatlesPlatinumAliasOpdbId);

            Assert.Multiple(() =>
            {
                Assert.That(entry.OpdbId, Is.EqualTo(BeatlesPlatinumAliasOpdbId));
                Assert.That(entry.EntryType, Is.EqualTo(OpdbEntryType.Alias));
                Assert.That(entry.IsAlias, Is.True);
                Assert.That(entry.OpdbMachine, Is.EqualTo("G0l8P-M85d9"));
            });
        }

        [Test]
        public async Task MatchPlayApi_GetOpdbEntry_WithoutIncludes_ShouldOmitPeopleAndImages()
        {
            var entry = await matchPlayApi.GetOpdbEntry(AddamsFamilyGoldOpdbId);

            Assert.Multiple(() =>
            {
                Assert.That(entry.People, Is.Null);
                Assert.That(entry.Images, Is.Null);
            });
        }

        [Test]
        public async Task MatchPlayApi_GetOpdbEntry_WithPeople_ShouldReturnCredits()
        {
            var entry = await matchPlayApi.GetOpdbEntry(AddamsFamilyGoldOpdbId, includePeople: true);

            Assert.That(entry.People, Is.Not.Empty);
            Assert.That(entry.People.Any(person => person.Name == "Pat Lawlor" && person.Role == "design"), Is.True);
        }

        [Test]
        public async Task MatchPlayApi_GetOpdbEntry_WithImages_ShouldReturnImageUrls()
        {
            var entry = await matchPlayApi.GetOpdbEntry(AddamsFamilyGoldOpdbId, includeImages: true);

            Assert.That(entry.Images, Is.Not.Empty);

            var image = entry.Images.First();

            Assert.Multiple(() =>
            {
                Assert.That(image.Urls.Small, Does.StartWith("https://"));
                Assert.That(image.Urls.Medium, Does.StartWith("https://"));
                Assert.That(image.Urls.Large, Does.StartWith("https://"));
                Assert.That(image.Sizes.Large.Width, Is.GreaterThan(0));
                Assert.That(image.Sizes.Large.Height, Is.GreaterThan(0));
            });
        }

        [Test]
        public async Task MatchPlayApi_GetOpdbChangelog_ShouldReturnChanges()
        {
            var changelog = await matchPlayApi.GetOpdbChangelog();

            Assert.That(changelog, Is.Not.Empty);
            Assert.That(changelog.Any(change => change.Action == OpdbChangelogAction.Move), Is.True);
            Assert.That(changelog.All(change => change.Action != OpdbChangelogAction.Unknown), Is.True);
            Assert.That(changelog.All(change => !string.IsNullOrWhiteSpace(change.OpdbIdDeleted)), Is.True);
        }

        [Test]
        public async Task MatchPlayApi_GetPinTipsByOpdbId_ShouldReturnTips()
        {
            var result = await matchPlayApi.GetPinTipsByOpdbId(AddamsFamilyGroupOpdbId);

            Assert.Multiple(() =>
            {
                Assert.That(result.PinTips, Is.Not.Empty);
                Assert.That(result.OpdbInfo.Name, Is.EqualTo("The Addams Family"));
                Assert.That(result.PinTips.First().VoteTotal, Is.GreaterThan(0));
                Assert.That(result.PinTips.First().Text, Is.Not.Empty);
            });
        }

        [Test]
        public async Task MatchPlayApi_GetPinTipsByArenaId_ShouldReturnTips()
        {
            var result = await matchPlayApi.GetPinTipsByArenaId(BlackPyramidArenaId);

            Assert.That(result.PinTips, Is.Not.Empty);
            Assert.That(result.OpdbInfo.Name, Is.Not.Empty);
        }

        [Test]
        [Category("DataExport")]
        public async Task MatchPlayApi_GetOpdbExport_ShouldReturnEveryEntry()
        {
            var entries = await matchPlayApi.GetOpdbExport();

            Assert.That(entries.Count, Is.GreaterThan(1000));

            var addams = entries.Single(entry => entry.OpdbId == AddamsFamilyGoldOpdbId);

            Assert.Multiple(() =>
            {
                Assert.That(addams.ShortName, Is.EqualTo("TAFG"));
                Assert.That(addams.EntryType, Is.EqualTo(OpdbEntryType.Machine));
                Assert.That(entries.Any(entry => entry.EntryType == OpdbEntryType.MachineGroup), Is.True);
                Assert.That(entries.Any(entry => entry.EntryType == OpdbEntryType.Alias), Is.True);
                Assert.That(entries.All(entry => entry.EntryType != OpdbEntryType.Unknown), Is.True);
            });
        }

        [Test]
        [Category("DataExport")]
        public async Task MatchPlayApi_GetOpdbSlimExport_ShouldReturnEveryEntry()
        {
            var entries = await matchPlayApi.GetOpdbSlimExport();

            Assert.That(entries.Count, Is.GreaterThan(1000));

            var addams = entries.Single(entry => entry.OpdbId == AddamsFamilyGoldOpdbId);

            Assert.Multiple(() =>
            {
                Assert.That(addams.ManufacturerName, Is.EqualTo("Bally"));
                Assert.That(addams.EntryType, Is.EqualTo(OpdbEntryType.Machine));
                Assert.That(addams.PrimaryBackglassImage.Urls.Small, Does.StartWith("https://"));
            });
        }

        [Test]
        [Category("DataExport")]
        public async Task MatchPlayApi_GetPinTipsExport_ShouldReturnEveryTip()
        {
            var tips = await matchPlayApi.GetPinTipsExport();

            Assert.That(tips.Count, Is.GreaterThan(1000));

            var tip = tips.First();

            Assert.Multiple(() =>
            {
                Assert.That(tip.OpdbId, Is.Not.Empty);
                Assert.That(tip.Text, Is.Not.Empty);
                Assert.That(tip.CreatedAt.Year, Is.GreaterThan(2000));
            });
        }

        [TestCase("G4ODR", "4ODR", null, null, OpdbEntryType.MachineGroup)]
        [TestCase("G4ODR-MLzY7", "4ODR", "LzY7", null, OpdbEntryType.Machine)]
        [TestCase("G0l8P-M85d9-A1ZNY", "0l8P", "85d9", "1ZNY", OpdbEntryType.Alias)]
        public void OpdbIdParts_Parse_ShouldSplitId(string opdbId, string group, string machine, string alias, OpdbEntryType entryType)
        {
            var parts = OpdbIdParts.Parse(opdbId);

            Assert.Multiple(() =>
            {
                Assert.That(parts.Group, Is.EqualTo(group));
                Assert.That(parts.Machine, Is.EqualTo(machine));
                Assert.That(parts.Alias, Is.EqualTo(alias));
                Assert.That(parts.EntryType, Is.EqualTo(entryType));
                Assert.That(parts.GroupId, Is.EqualTo("G" + group));
                Assert.That(parts.ToString(), Is.EqualTo(opdbId));
            });
        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("4ODR")]
        [TestCase("G4ODR-LzY7")]
        [TestCase("G4ODR-MLzY7-1ZNY")]
        [TestCase("G4ODR MLzY7")]
        public void OpdbIdParts_TryParse_ShouldRejectInvalidId(string opdbId)
        {
            Assert.That(OpdbIdParts.TryParse(opdbId, out var parts), Is.False);
            Assert.That(parts, Is.Null);
        }
    }
}
