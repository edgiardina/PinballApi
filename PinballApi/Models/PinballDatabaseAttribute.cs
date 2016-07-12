using System;

namespace PinballApi.Models
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
