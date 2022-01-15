using AutoMapper;
using ExamRegistrationService.Entities;
using ExamRegistrationService.Models;

namespace ExamRegistrationService.Profiles
{
    public class ExamRegistrationProfile : Profile
    {
        public ExamRegistrationProfile()
        {
            CreateMap<ExamRegistration, ExamRegistrationDto>()
                .ForMember(
                    dest => dest.StudentName,
                    opt => opt.MapFrom(src => $"{ src.StudentFirstName } { src.StudentLastName }"))
                .ForMember(
                    dest => dest.Subject,
                    opt => opt.MapFrom(src => $"{ src.SubjectCode } { src.SubjectName }"));

            CreateMap<ExamRegistrationCreationDto, ExamRegistration>();
            CreateMap<ExamRegistrationUpdateDto, ExamRegistration>();
            CreateMap<ExamRegistration, ExamRegistration>();
        }
    }
}
