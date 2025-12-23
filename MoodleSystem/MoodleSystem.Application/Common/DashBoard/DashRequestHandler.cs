using MoodleSystem.Application.Common.Model;
using MoodleSystem.Domain.Enumerations;

namespace MoodleSystem.Application.Common.DashBoard
{
    public class DashRequestHandler
    {
        public DashResponse DashHandler()
        {
            if (!CurrentUser.IsLoggedIn)
                return new DashResponse { 
                    Type = "None"
                };

            switch( CurrentUser.User!.Role )
            {
                case UserRole.Student:
                    return new DashResponse {
                        Type = "Student"
                    };
                case UserRole.Professor:
                    return new DashResponse
                    {
                        Type = "Professor"
                    };
                case UserRole.Admin:
                    return new DashResponse
                    {
                        Type = "Admin"
                    };
                default:
                    return new DashResponse
                    {
                        Type = "None"
                    };
            };
        }
    }
}