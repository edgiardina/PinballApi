using System;

namespace PinballApi.Models.IPDB
{
    [PinballDatabase(ListKeyword = "mfg1,mfg2")]
    public class PinballManufacturer 
    {
        public string Name { get; set; }
        //TODO: Replace this with two YEAR integer values.
        //public TimeSpan YearsOfOperation { get; set; }
        public int NumberOfGames { get; set; }

    }
}