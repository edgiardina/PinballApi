using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using PinballApi.Models.IPDB;

namespace PinballApi
{
    /// <summary>
    /// Wrapper around ipdb.org pinball machine data
    /// </summary>
    public class PinballMachineApi
    {
        private const string ipdbBaseUrl = "http://ipdb.org/lists.cgi?anonymously=true&list=";

        private IEnumerable<PinballMachine> pinballMachines;
        private IEnumerable<PinballManufacturer> pinballManufacturers;

        /// <summary>
        /// Clear out the cached values for Pinball Machines and Pinball Manufacturers
        /// </summary>
        public void ClearCache()
        {
            pinballMachines = null;
            pinballManufacturers = null;
        }

        /// <summary>
        /// Get pinball machines from
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PinballMachine> GetAllPinballMachines()
        {
            if (this.pinballMachines != null)
                return this.pinballMachines;

            var pinballMachines = new List<PinballMachine>();
            var htmlNodes = GetIpdbListData<PinballMachine>();

            foreach (var node in htmlNodes)
            {
                DateTime dateManufactured;
                PinballMachineType machineType;
                int players = 0;

                var tds = node.SelectNodes("td").ToArray();
                DateTime.TryParse(tds[2].InnerText, out dateManufactured);
                Enum.TryParse(tds[4].InnerText, out machineType);
                int.TryParse(tds[3].InnerText, out players);

                pinballMachines.Add(
                    new PinballMachine
                    {
                        Name = tds[0].InnerText,
                        Manufacturer = GetPinballManufacturerByName(tds[1].InnerText),
                        DateManufactured = dateManufactured,
                        Players = players,
                        MachineType = machineType,
                        Theme = tds[5].InnerText
                        //TODO: Lazy Load Abbreviations
                    });
            }

            this.pinballMachines = pinballMachines;
            return pinballMachines;
        }

        public IEnumerable<PinballManufacturer> GetAllPinballManufacturers()
        {
            if (this.pinballManufacturers != null)
                return this.pinballManufacturers;

            var pinballManufacturers = new List<PinballManufacturer>();
            var htmlNodes = GetIpdbListData<PinballManufacturer>();

            foreach (var node in htmlNodes)
            {
                int numOfGames = 0;

                var tds = node.SelectNodes("td").ToArray();

                int.TryParse(tds[2].InnerText, out numOfGames);

                pinballManufacturers.Add(
                    new PinballManufacturer
                    {
                        Name = tds[0].InnerText,
                        NumberOfGames = numOfGames
                    });
            }

            this.pinballManufacturers = pinballManufacturers;
            return pinballManufacturers;
        }

        public PinballManufacturer GetPinballManufacturerByName(string name)
        {
            if (this.pinballManufacturers == null)
                GetAllPinballManufacturers();

            return this.pinballManufacturers.FirstOrDefault(n => TransformNameToManufacturerKey(n.Name) == TransformNameToManufacturerKey(name));
        }

        // strip out (a.k.a. name) string from manufacturer if provided
        private string TransformNameToManufacturerKey(string name)
        {
            return (name.Contains("(") ? name.Substring(0, name.LastIndexOf("(")) : name)
                                          .Trim();
        }

        private IEnumerable<HtmlNode> GetIpdbListData<T>()
        {
            List<HtmlNode> nodes = new List<HtmlNode>();

            if (!typeof(T).IsDefined(typeof(PinballDatabaseAttribute), false))
                throw new InvalidOperationException("Type provided for GetIpdbListData is not decorated with a PinballDatabaseAttribute");

            var list = ((PinballDatabaseAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(PinballDatabaseAttribute))).ListKeyword;

            var listKeywordCollection = list.Split(',');

            foreach (var listKeyword in listKeywordCollection)
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(ipdbBaseUrl + listKeyword.Trim());

                //First table row contains the headers. ignore.         
                nodes.AddRange(doc.DocumentNode.SelectNodes("//table/tr").Skip(1));
            }

            return nodes;
        }
    }
}
