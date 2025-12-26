namespace MoodleSystem.Application.Professor.ManageProfessorCoursesScreen.AddMaterial
{
    public class AddMaterialRequest
    {
        public string Name { get; init; } = null!;
        public string Url { get; init; } = null!;
        public int CourseId { get; init; }
    }
}
