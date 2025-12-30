using MoodleSystem.Application.Common.Model;
using MoodleSystem.Domain.Entities;
using MoodleSystem.Domain.Persistence.Common;
using MoodleSystem.Domain.Persistence.Courses;

namespace MoodleSystem.Application.Professor.ManageProfessorCoursesScreen.AddAnnouncement
{
    public class AddAnnouncementRequestHandler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseRepository _courseRepository;

        public AddAnnouncementRequestHandler(IUnitOfWork unitOfWork, ICourseRepository courseRepository)
        {
            _unitOfWork = unitOfWork;
            _courseRepository = courseRepository;
        }

        public async Task<AddAnnouncementResponse> AnnouncementHandler(AddAnnouncementRequest req)
        {
            var professorId = CurrentUser.User!.Id;
            var course = await _courseRepository.GetByIdAsync(req.CourseId);

            if(course == null)
            {
                return new AddAnnouncementResponse
                {
                    Message = "Taj kolegij ne postoji",
                    Success = false
                };
            }

            if(course.ProfessorId != professorId)
            {
                return new AddAnnouncementResponse
                {
                    Message = "Nemate pravo objavljivat za taj kolegij",
                    Success = false
                };
            }

            var announcement = new Announcement(
                title: req.Title,
                content: req.Content,
                courseId: req.CourseId
            );

            await _courseRepository.AddAnnouncementAsync( announcement );
            await _unitOfWork.SaveChangesAsync();

            return new AddAnnouncementResponse
            {
                Success = true
            };
        }
        
    }
}