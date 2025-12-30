using MoodleSystem.Application.Professor.MyCourses;
using MoodleSystem.Application.Professor.ProfessorCoursesScreen;
using MoodleSystem.Application.Student.MyCourses;
using MoodleSystem.Application.Student.StudentCoursesScreen;

namespace MoodleSystem.Console.Actions
{
    public class ProfessorActions
    {
        private readonly MyCoursesProfessorRequestHandler _myCoursesProfessorRequestHandler;
        private readonly ProfessorCoursesScreenRequestHandler _professorCoursesScreenRequestHandler;

        public ProfessorActions (MyCoursesProfessorRequestHandler myCoursesProfessorRequestHandler, ProfessorCoursesScreenRequestHandler professorCoursesScreenRequestHandler)
        {
            _myCoursesProfessorRequestHandler = myCoursesProfessorRequestHandler;
            _professorCoursesScreenRequestHandler = professorCoursesScreenRequestHandler;
        }

        public async Task<MyCoursesProfessorResponse> GetMyCoursesAsync()
        {
            return await _myCoursesProfessorRequestHandler.ProfessorHandler();
        }

        public async Task<ProfessorCoursesScreenResponse> GetCourseScreenAsync(int courseId)
        {
            var req = new ProfessorCoursesScreenRequest { CourseId = courseId };
            return await _professorCoursesScreenRequestHandler.CoursesHandler(req);
        }
    }
}
