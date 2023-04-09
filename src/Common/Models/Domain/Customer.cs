using System;
using Newtonsoft.Json;

namespace CodeExcercise.Common.Models.Domain
{
    public class Customer
    {
        public Customer(Guid ident, string firstname, string surname)
            :this(firstname, surname)
        {
            Ident = ident;
        }

        public Customer(string firstname, string surname)
        {
            Firstname = firstname;
            Surname = surname;
        }

        public Customer()
        {
        }

        [JsonProperty("Ident")]
        public Guid Ident { get; set; }
        
        [JsonProperty("FirstName")]
        public string Firstname { get; set; } = null!;

        [JsonProperty("Surname")] 
        public string Surname { get; set; } = null!;
    }
}