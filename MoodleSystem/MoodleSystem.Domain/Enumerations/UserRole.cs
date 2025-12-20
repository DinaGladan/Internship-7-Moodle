using System.Text.Json.Serialization;

namespace MoodleSystem.Domain.Enumerations
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserRole
    {
        Student = 1,
        Professor = 2,
        Admin = 3
    }
}
