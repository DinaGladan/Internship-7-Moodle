using MoodleSystem.Application.Common.Model;
using MoodleSystem.Domain.Entities;
using MoodleSystem.Domain.Persistence.Courses;
using MoodleSystem.Infrastructure.Repositories;

namespace MoodleSystem.Application.Professor.ManageProfessorCoursesScreen.AddMaterial
{
    public class AddMaterialRequestHandler
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ICourseRepository _courseRepository;

        public AddMaterialRequestHandler(UnitOfWork unitOfWork, ICourseRepository courseRepository)
        {
            _unitOfWork = unitOfWork;
            _courseRepository = courseRepository;
        }

        public async Task<AddMaterialResponse> MaterialHandler(AddMaterialRequest req)
        {
            var professorId = CurrentUser.User!.Id;
            var course = await _courseRepository.GetByIdAsync(req.CourseId);

            if(course == null)
            {
                return new AddMaterialResponse
                {
                    Message = "Taj kolegij ne postoji",
                    Success = false
                };
            }

            if (course.ProfessorId != professorId)
            {
                return new AddMaterialResponse
                {
                    Message = "Nemate pravo objavljivat za taj kolegij",
                    Success = false
                };
            }
            
            if(!Uri.IsWellFormedUriString(req.Url, UriKind.Absolute))
            {
                return new AddMaterialResponse
                {
                    Message = "Url nije valjan",
                    Success = false
                };
            }

            var material = new Material(
                name: req.Name,
                url: req.Url,
                courseId: req.CourseId
            );

            course.Materials.Add( material );

            await _unitOfWork.SaveChangesAsync();

            return new AddMaterialResponse
            {
                Success = true
            };

        }
    }
}