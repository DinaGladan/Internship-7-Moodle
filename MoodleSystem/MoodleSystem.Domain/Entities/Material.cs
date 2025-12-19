using MoodleSystem.Domain.Common;

namespace MoodleSystem.Domain.Entities
{
    public class Material : BaseEntity
    {
        public string Name { get; private set; }
        public int CourseId { get; private set; }
        public string Url { get; private set; }
        public Course Course { get; private set; }
        private Material() { }
        public Material(string name, int courseId, string url)
        {
            Name = name;
            CourseId = courseId;
            Url = url;
        }
    }
}
