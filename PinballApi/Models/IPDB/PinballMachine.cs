using System;

namespace PinballApi.Models.IPDB
{
    [PinballDatabase(ListKeyword = "games")]
    public class PinballMachine
    {
        public string Name { get; set; }
        public PinballManufacturer Manufacturer { get; set; }
        public DateTime DateManufactured { get; set; }
        public int Players { get; set; }
        public PinballMachineType MachineType { get; set; }
        public string Theme { get; set; }
        public string Abbreviation { get; set; }

    }
}
