using MoodleSystem.Domain.Common;

namespace MoodleSystem.Domain.Entities
{
    public class Announcement : BaseEntity
    {
        public string Title { get; private set; }
        public string Content { get; private set; }
        public Course Course { get; private set; }
        public int CourseId { get; private set; }
        private Announcement() { }
        public Announcement(string title, string content, int courseId)
        {
            Title = title;
            Content = content;
            CourseId = courseId;
        }
    }
}
