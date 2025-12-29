using MoodleSystem.Application.Student.MyCourses;
using MoodleSystem.Application.Student.StudentCoursesScreen;

namespace MoodleSystem.Console.Actions
{
    public class StudentActions
    {
        private readonly MyCoursesStudentRequestHandler _myCoursesStudentRequestHandler;
        private readonly StudentCorsesScreenRequestHandler _studentCorsesScreenRequestHandler;

        public StudentActions( MyCoursesStudentRequestHandler myCoursesStudentRequestHandler, StudentCorsesScreenRequestHandler studentCoursesScreenRequestHandler)
        {
            _myCoursesStudentRequestHandler = myCoursesStudentRequestHandler;
            _studentCorsesScreenRequestHandler = studentCoursesScreenRequestHandler;
        }

        public async Task<MyCoursesStudentResponse> GetMyCoursesAsync()
        {
            return await _myCoursesStudentRequestHandler.StudentHandler();
        }

        public async Task<StudentCorsesScreenResponse> GetCourseScreenAsync(int courseId)
        {
            var req = new StudentCoursesScreenRequest { CourseId = courseId };
            return await _studentCorsesScreenRequestHandler.StudentHandler(req);
        }

    }
}