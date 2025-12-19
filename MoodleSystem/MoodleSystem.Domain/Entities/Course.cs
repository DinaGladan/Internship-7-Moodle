using MoodleSystem.Domain.Common;

namespace MoodleSystem.Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; private set; }
        public int ProfessorId { get; private set; }
        public User Professor { get; private set; }

        public ICollection<UserCourse> Enrollments { get; private set; } = new List<UserCourse>();
        public ICollection<Announcement> Announcements { get; private set; } = new List<Announcement>();
        public ICollection<Material> Materials { get; private set; } = new List<Material>();

        private Course() { }
        public Course(string name, int professorId)
        {
            Name = name;
            ProfessorId = professorId;
        }
    }

}


