using System;

namespace PinballApi.Models.IPDB
{
    [PinballDatabase(ListKeyword = "mfgAM,mfgNZ")]
    public class PinballManufacturer 
    {
        public string Name { get; set; }
        //TODO: Replace this with two YEAR integer values.
        //public TimeSpan YearsOfOperation { get; set; }
        public int NumberOfGames { get; set; }

    }
}