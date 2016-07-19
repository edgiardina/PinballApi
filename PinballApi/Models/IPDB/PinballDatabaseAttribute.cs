using System;

namespace PinballApi.Models.IPDB
{
    [AttributeUsage(AttributeTargets.All)]
    public class PinballDatabaseAttribute : Attribute
    {
        public string ListKeyword
        {
            get; set;
        }
    }
}
