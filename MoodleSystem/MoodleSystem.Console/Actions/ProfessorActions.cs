using MoodleSystem.Application.Professor.ManageProfessorCoursesScreen.AddAnnouncement;
using MoodleSystem.Application.Professor.ManageProfessorCoursesScreen.AddMaterial;
using MoodleSystem.Application.Professor.ManageProfessorCoursesScreen.AddStudentToCourse;
using MoodleSystem.Application.Professor.MyCourses;
using MoodleSystem.Application.Professor.ProfessorCoursesScreen;
using MoodleSystem.Domain.Enumerations;
using MoodleSystem.Domain.Persistence.Users;

namespace MoodleSystem.Console.Actions
{
    public class ProfessorActions
    {
        private readonly MyCoursesProfessorRequestHandler _myCoursesProfessorRequestHandler;
        private readonly ProfessorCoursesScreenRequestHandler _professorCoursesScreenRequestHandler;
        private readonly AddAnnouncementRequestHandler _addAnnouncementRequestHandler;
        private readonly AddMaterialRequestHandler _addMaterialRequestHandler;
        private readonly AddStudentToCourseRequestHandler _addStudentToCourseRequestHandler;
        private readonly IUserRepository _userRepository;


        public ProfessorActions(MyCoursesProfessorRequestHandler myCoursesProfessorRequestHandler, ProfessorCoursesScreenRequestHandler professorCoursesScreenRequestHandler, AddAnnouncementRequestHandler addAnnouncementRequestHandler, AddMaterialRequestHandler addMaterialRequestHandler, AddStudentToCourseRequestHandler addStudentToCourseRequestHandler, IUserRepository userRepository)
        {
            _myCoursesProfessorRequestHandler = myCoursesProfessorRequestHandler;
            _professorCoursesScreenRequestHandler = professorCoursesScreenRequestHandler;
            _addAnnouncementRequestHandler = addAnnouncementRequestHandler;
            _addMaterialRequestHandler = addMaterialRequestHandler;
            _addStudentToCourseRequestHandler = addStudentToCourseRequestHandler;
            _userRepository = userRepository;
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

        public async Task<AddAnnouncementResponse> AddAnnouncement(int courseId, string title, string content)
        {
            var req = new AddAnnouncementRequest
            {
                CourseId = courseId,
                Title = title,
                Content = content
            };

            return await _addAnnouncementRequestHandler.AnnouncementHandler(req);
        }

        public async Task<AddMaterialResponse> AddMaterial(int courseId, string name, string url)
        {
            var req = new AddMaterialRequest
            {
                CourseId = courseId,
                Name = name,
                Url = url
            };

            return await _addMaterialRequestHandler.MaterialHandler(req);
        }

        public async Task<AddStudentToCourseResponse> AddStudent(int courseId, int studentId)
        {
            var req = new AddStudentToCourseRequest
            {
                CourseId = courseId,
                StudentId = studentId
            };
            return await _addStudentToCourseRequestHandler.StudentCourseHandler(req);
        }

        public async Task<List<(int Id, string FullName)>> GetAllStudentsAsync(){

            var students =await _userRepository.GetByRoleAsync(UserRole.Student);

            return students.OrderBy(s => s.LastName)
                .ThenBy(s => s.FirstName)
                .Select(s =>(s.Id, $"{s.FirstName} {s.LastName}"))
                .ToList();
        }
    }
}
