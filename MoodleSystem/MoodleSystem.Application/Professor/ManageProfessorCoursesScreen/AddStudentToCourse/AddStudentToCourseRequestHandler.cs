using MoodleSystem.Application.Common.Model;
using MoodleSystem.Domain.Entities;
using MoodleSystem.Domain.Enumerations;
using MoodleSystem.Domain.Persistence.Common;
using MoodleSystem.Domain.Persistence.Courses;
using MoodleSystem.Domain.Persistence.Users;

namespace MoodleSystem.Application.Professor.ManageProfessorCoursesScreen.AddStudentToCourse
{
    public class AddStudentToCourseRequestHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddStudentToCourseRequestHandler( IUnitOfWork unitOfWork, ICourseRepository courseRepository, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _courseRepository = courseRepository;
            _userRepository = userRepository;
        }

        public async Task<AddStudentToCourseResponse> StudentCourseHandler(AddStudentToCourseRequest req)
        {
            var professorId = CurrentUser.User!.Id;

            var course = await _courseRepository.GetByIdAsync(req.CourseId);

            if(course == null)
            {
                return new AddStudentToCourseResponse
                {
                    Message = "Kolegij s tim Idem ne postoji.",
                    Success = false
                };
            }

            if(course.ProfessorId != req.CourseId)
            {
                return new AddStudentToCourseResponse
                {
                    Message = "Ovaj kolegij nije pod vasim vlasnistvom",
                    Success = false
                };
            }

            var student = await _userRepository.GetByIdAsync(req.StudentId);

            if(student == null || student.Role != UserRole.Student)
            {
                return new AddStudentToCourseResponse
                {
                    Message = "Zeljena osoba nije student",
                    Success = false
                };
            }

            if(course.Enrollments.Any(e => e.User.Id == req.StudentId))
            {
                return new AddStudentToCourseResponse
                {
                    Message = "Zeljena osoba je vec upisana na ovaj kolegij",
                    Success = false
                };
            }

            var userCourse = new UserCourse(
                courseId : req.CourseId,
                userId :req.StudentId
                );

            course.Enrollments.Add( userCourse );

            await _unitOfWork.SaveChangesAsync();
            return new AddStudentToCourseResponse { 
                Success = true
            };
        }
    }
} 