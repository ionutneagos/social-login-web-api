namespace WebApi.Models.Auth
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;

    public class GoogleUserRequest
    {
        public const string PROVIDER = "google";

        [JsonProperty("idToken")]
        [Required]
        public string IdToken { get; set; }

    }
}