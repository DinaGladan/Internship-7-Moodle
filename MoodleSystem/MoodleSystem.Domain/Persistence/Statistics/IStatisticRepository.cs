using MoodleSystem.Domain.DTOs;
using MoodleSystem.Domain.Enumerations;
using MoodleSystem.Infrastructure.DTO;

namespace MoodleSystem.Domain.Persistence.Statistics
{
    public interface IStatisticRepository
    {
        Task<int> CountUsersAsync(DateTime startDate, DateTime endDate);
        Task<int> CountCoursesAsync(DateTime startDate, DateTime endDate);

        Task<IEnumerable<CourseStudentCountDTO>> Top3CoursesByStudentsAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<UserMessageCountDTO>> Top3UsersBySentMessagesAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<RoleCountDTO>> GetUsersCountByRole(DateTime startDate, DateTime endDate);

    }
}
