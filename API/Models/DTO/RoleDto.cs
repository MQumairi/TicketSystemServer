using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace API.Models.DTO
{
    public class RoleDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
        public string color { get; set; }
        public List<UserDto> roleUsers { get; set; }
        public List<UserDto> userList {get; set;}

    }
}