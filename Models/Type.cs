using System.Text.Json.Serialization;

namespace WebApi.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Type
    {
        Student = 1,

        Teacher = 2,

        Admin = 0,
    }
}