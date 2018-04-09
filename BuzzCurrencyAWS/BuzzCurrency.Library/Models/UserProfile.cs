using BuzzCurrency.Library.Enums;
using Newtonsoft.Json;

namespace BuzzCurrency.Library.Models
{
    public class UserProfile
    {
        [JsonProperty("Email")]
        public string Email { get; set; }

        [JsonProperty("EmailVerified")]
        public bool EmailVerified { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Birthdate")]
        public string Birthdate { get; set; }

        [JsonProperty("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("PhoneNumberVerified")]
        public bool PhoneNumberVerified { get; set; }

        [JsonProperty("Gender")]
        public string Gender { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }

        [JsonProperty("UserType")]
        public UserType UserType { get; set; }

        [JsonProperty("ImageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("Active")]
        public bool Active { get; set; }

        [JsonProperty("CreatedOn")]
        public string CreatedOn { get; set; }

        [JsonProperty("ModifiedOn")]
        public string ModifiedOn { get; set; }
    }
}
