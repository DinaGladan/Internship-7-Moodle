using Microsoft.EntityFrameworkCore;
using MoodleSystem.Domain.Entities;
using MoodleSystem.Domain.Enumerations;

namespace MoodleSystem.Infrastructure.Persistence
{
    public static class DataBaseSeed
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User(
                    firstName: "Lolek",
                    lastName: "Bokic",
                    email: "admin@fesb.hr",
                    password: "ananas"
                )
                {
                    Id = 1,
                    Role = UserRole.Admin
                },
                new User(
                    firstName: "Boban",
                    lastName: "Porke",
                    email: "profesorr@fesb.hr",
                    password: "ananas"
                )
                {
                    Id = 2,
                    Role = UserRole.Professor
                },
                new User(
                    firstName: "Ivica",
                    lastName: "Porke",
                    email: "elprofesorr@fesb.hr",
                    password: "ananas"
                )
                {
                    Id = 3,
                    Role = UserRole.Professor
                },
                new User(
                    firstName: "Dina",
                    lastName: "Gladan",
                    email: "dinastud@fesb.hr",
                    password: "ananas"
                )
                {
                    Id = 4,
                    Role = UserRole.Student
                },
                new User(
                    firstName: "Duje",
                    lastName: "Nincevic",
                    email: "dujestud@fesb.hr",
                    password: "ananas"
                )
                {
                    Id = 5,
                    Role = UserRole.Student
                }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course(
                    name: "Programiranje",
                    professorId: 2
                )
                {
                    Id = 1
                },
                new Course(
                    name: "Programiranje za Unix",
                    professorId: 2
                )
                {
                    Id = 2
                },
                new Course(
                    name: "Programiranje za Internet",
                    professorId: 3
                )
                {
                    Id = 3
                }
            );

            modelBuilder.Entity<UserCourse>().HasData(
                new
                {
                    CourseId = 1,
                    UserId = 4
                },
                new
                {
                    CourseId = 1,
                    UserId = 5
                }
            );

            modelBuilder.Entity<Material>().HasData(
                new Material(
                    name: "Materijal1",
                    courseId: 1,
                    url: "https://url1.hr"
                )
                {
                    Id = 1
                },
                new Material(
                    name: "Materijal2",
                    courseId: 2,
                    url: "https://url2.hr"
                )
                {
                    Id = 2
                },
                new Material(
                    name: "Materijal3",
                    courseId: 3,
                    url: "https://url3.hr"
                )
                {
                    Id = 3
                }
            );

            modelBuilder.Entity<PrivateMessage>().HasData(
                new PrivateMessage(
                    content: "Sadrzaj privatne poruke 1",
                    senderId: 1,
                    receiverId: 2
                )
                {
                    Id= 1
                },
                new PrivateMessage(
                    content: "Sadrzaj privatne poruke 2",
                    senderId: 1,
                    receiverId: 3
                )
                {
                    Id = 2
                },
                new PrivateMessage(
                    content: "Sadrzaj privatne poruke 3",
                    senderId: 2,
                    receiverId: 1
                )
                {
                    Id = 3
                }
            );

            modelBuilder.Entity<Announcement>().HasData(
                new Announcement(
                    title: "Kolokvij1",
                    content: "sadrzaj 1 annon",
                    courseId: 1
                )
                {
                    Id = 1
                },
                new Announcement(
                    title: "Kolokvij2",
                    content: "sadrzaj 2 annon",
                    courseId: 2
                )
                {
                    Id = 2
                },
                new Announcement(
                    title: "Kolokvij3",
                    content: "sadrzaj 3 annon",
                    courseId: 1
                )
                {
                    Id = 3
                }
            );
        }
    }
}
