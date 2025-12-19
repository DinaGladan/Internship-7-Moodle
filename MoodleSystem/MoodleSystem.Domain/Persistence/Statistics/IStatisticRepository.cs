namespace MoodleSystem.Domain.Persistence.Statistics
{
    public interface IStatisticRepository
    {
        Task<int> CountUsersAsync(DateTime startDate, DateTime endDate);
        Task<int> CountCoursesAsync(DateTime startDate, DateTime endDate);

        Task<IEnumerable<(int CourseId, int Count)>> Top3CoursesByStudentsAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<(int UserId, int Count)>> Top3UsersBySentMessagesAsync(DateTime startDate, DateTime endDate);

    }
}
