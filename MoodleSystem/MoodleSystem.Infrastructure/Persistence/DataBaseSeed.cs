using Microsoft.EntityFrameworkCore;
using MoodleSystem.Domain.Abstractions.Services;
using MoodleSystem.Domain.Entities;
using MoodleSystem.Domain.Enumerations;

namespace MoodleSystem.Infrastructure.Persistence
{
    public static class DataBaseSeed
    {
        public static async Task InitializeAsync(MoodleDbContext context)
        {
            if (context.Users.Any())
                return;

            var admin = new User(
                firstName: "Lolek",
                lastName: "Bokic",
                email: "admin@fesb.hr",
                passwor:
            );
            admin.SetRole(UserRole.Admin);

            var professor = new User(
                firstName: "Ana",
                lastName: "Lokic",
                email: "prof@fesb.hr",
                passwordHash:
            );

            professor.SetRole(UserRole.Professor);

            var student1 = new User(
                firstName: "Dina",
                lastName: "Gladan",
                email: "student1@fesb.hr",
                password:
            );

            var student2 = new User(
                firstName: "Duje",
                lastName: "Ivkovic",
                email: "student2@fesb.hr",
            );

            context.Users.AddRange(admin, professor, student1, student2);
            await context.SaveChangesAsync();

            var course1 = new Course(
                name: "Programiranje za Internet",
                professorId: professor.Id
            );

            var course2 = new Course(
                name: "Baze podataka",
                professorId: professor.Id
            );

            var course3 = new Course(
                name: "Programiranje",
                professorId: professor.Id
            );

            context.Courses.AddRange(course1, course2, course3);
            await context.SaveChangesAsync();

            var material1 = new Material(
                name: "Materijal1",
                courseId: course1.Id,
                url: "https://url1.hr"
                );

            var material2 = new Material(
                name: "Materijal2",
                courseId: course1.Id,
                url: "https://url2.hr"
                );

            var material3 = new Material(
                name: "Materijal3",
                courseId: course1.Id,
                url: "https://url3.hr"
                );

            var userCourse1 = new UserCourse(
                courseId: course2.Id,
                userId: student1.Id
                );

            var userCourse2 = new UserCourse(
                courseId: course1.Id,
                userId: student1.Id
                );

            var userCourse3 = new UserCourse(
                courseId: course1.Id,
                userId: student2.Id
                );

            context.UserCourses.AddRange(userCourse1, userCourse2, userCourse3);
            await context.SaveChangesAsync();

            context.Materials.AddRange(material1, material2, material3);
            await context.SaveChangesAsync();

            var privateMessage1 = new PrivateMessage(
                content: "prva poruka",
                receiverId: student1.Id,
                senderId: student2.Id
                );

            var privateMessage2 = new PrivateMessage(
                content: "druga poruka",
                receiverId: professor.Id,
                senderId: student2.Id
                );

            var privateMessage3 = new PrivateMessage(
                content: "treca poruka",
                receiverId: admin.Id,
                senderId: professor.Id
                );

            context.PrivateMessages.AddRange(privateMessage1, privateMessage2, privateMessage3);
            await context.SaveChangesAsync();

            var announcement1 = new Announcement(
                title: "Test1",
                content: "sadrzaj 1 annon",
                courseId: course1.Id
                );
            var announcement2 = new Announcement(
                title: "Test2",
                content: "sadrzaj 2 annon",
                courseId: course1.Id
                );
            var announcement3 = new Announcement(
                title: "Test3",
                content: "sadrzaj 3 annon",
                courseId: course2.Id
                );

            context.Announcements.AddRange(announcement1, announcement2, announcement3);
            await context.SaveChangesAsync();

        }

    }
}
