using MoodleSystem.Application.DTO;
using MoodleSystem.Domain.Persistence.Courses;
using MoodleSystem.Domain.Persistence.Users;
using MoodleSystem.Domain.Persistence.Statistics;

namespace MoodleSystem.Application.Statistics
{
    public class StatisticsRequestHandler
    {
        private readonly IStatisticRepository _statisticRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;

        public StatisticsRequestHandler(
            IStatisticRepository statisticRepository,
            ICourseRepository courseRepository,
            IUserRepository userRepository)
        {
            _statisticRepository = statisticRepository;
            _courseRepository = courseRepository;
            _userRepository = userRepository;
        }

        public async Task<StatisticsResponse> StatisticHandler(DateTime from, DateTime to)
        {
            var topCoursesInfra = await _statisticRepository.Top3CoursesByStudentsAsync(from, to);

            var topUsersInfra = await _statisticRepository.Top3UsersBySentMessagesAsync(from, to);

            var courses = await _courseRepository.GetAllAsync();
            var users = await _userRepository.GetAllAsync();

            return new StatisticsResponse
            {
                RoleCounts = (await _statisticRepository.GetUsersCountByRole(from, to)).ToList(),
                CourseCount = await _statisticRepository.CountCoursesAsync(from, to),

                Top3Courses = topCoursesInfra
                    .Select(c => new Top3CoursesDTO
                    {
                        CourseId = c.CourseId,
                        CourseName = courses
                            .Values
                            .First(x => x.Id == c.CourseId)
                            .Name,
                        StudentCount = c.Count
                    })
                    .ToList(),

                Top3Users = topUsersInfra
                    .Select(u => new Top3UsersDTO
                    {
                        UserId = u.SenderId,
                        FullName = users
                            .Values
                            .First(x => x.Id == u.SenderId)
                            .FirstName + " " +
                            users.Values.First(x => x.Id == u.SenderId).LastName,
                        MessageCount = u.Count
                    })
                    .ToList()
            };
        }
    }
}
