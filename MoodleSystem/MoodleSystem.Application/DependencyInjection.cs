using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoodleSystem.Application.Admin.ChangeUserRole;
using MoodleSystem.Application.Admin.DeleteUser;
using MoodleSystem.Application.Admin.UpdateUserEmail;
using MoodleSystem.Application.Common.DashBoard;
using MoodleSystem.Application.Common.DashBoard.Admin;
using MoodleSystem.Application.Common.DashBoard.Professor;
using MoodleSystem.Application.Common.DashBoard.Student;
using MoodleSystem.Application.Common.Model.LogIn;
using MoodleSystem.Application.Common.Model.Register;
using MoodleSystem.Application.PrivateMsgs.ChatScreen;
using MoodleSystem.Application.PrivateMsgs.MyPrivateMessages;
using MoodleSystem.Application.PrivateMsgs.NewMessage;
using MoodleSystem.Application.Professor.ManageProfessorCoursesScreen.AddAnnouncement;
using MoodleSystem.Application.Professor.ManageProfessorCoursesScreen.AddMaterial;
using MoodleSystem.Application.Professor.ManageProfessorCoursesScreen.AddStudentToCourse;
using MoodleSystem.Application.Professor.MyCourses;
using MoodleSystem.Application.Professor.ProfessorCoursesScreen;
using MoodleSystem.Application.Student.MyCourses;
using MoodleSystem.Application.Student.StudentCoursesScreen;
using MoodleSystem.Infrastructure;

namespace MoodleSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructure(configuration);

            services.AddScoped<LogInRequestHandler>();
            services.AddScoped<RegisterRequestHandler>();

            services.AddScoped<DashRequestHandler>();
            services.AddScoped<DashAdminRequestHandler>();
            services.AddScoped<DashProfessorRequestHandler>();
            services.AddScoped<DashStudentRequestHandler>();

            services.AddScoped<ChangeUserRoleRequestHandler>();
            services.AddScoped<DeleteUserAdminRequestHandler>();
            services.AddScoped<UpdateUserEmailAdminRequestHandler>();

            services.AddScoped<ChatScreenRequestHandler>();
            services.AddScoped<MyPrivateMessagesRequestHandler>();
            services.AddScoped<NewMessageRequestHandler>();
            services.AddScoped<NewMessageUsersRequestHandler>();

            services.AddScoped<AddAnnouncementRequestHandler>();
            services.AddScoped<AddMaterialRequestHandler>();
            services.AddScoped<AddStudentToCourseRequestHandler>();
            services.AddScoped<MyCoursesProfessorRequestHandler>();
            services.AddScoped<ProfessorCoursesScreenRequestHandler>();

            services.AddScoped<MyCoursesStudentRequestHandler>();
            services.AddScoped<StudentCorsesScreenRequestHandler>();

            return services;

        }
    }
}