using CodeExcercise.Common.Models.Domain;
using Newtonsoft.Json;

namespace CodeExcercise.Common.Models.DTO
{
    public class ClientCustomer
    {
        public ClientCustomer(string firstname, string surname)
        {
            Firstname = firstname;
            Surname = surname;
        }

        [JsonProperty("FirstName")]
        public string Firstname { get; set; }

        [JsonProperty("Surname")] 
        public string Surname { get; set; }

        public Customer MapToCustomer()
        {
            return new Customer(
                this.Firstname,
                this.Surname);
        }
    }
}
