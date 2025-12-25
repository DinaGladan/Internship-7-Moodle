namespace MoodleSystem.Domain.Entities
{
    public class UserCourse
    {
        public int CourseId { get; private set; }
        public Course Course { get; private set; }
        public int UserId { get; private set; }
        public User User { get; private set; }

        private UserCourse() { }

        public UserCourse(int courseId, int userId)
        {
            CourseId = courseId;
            UserId = userId;
        }
    }
}
