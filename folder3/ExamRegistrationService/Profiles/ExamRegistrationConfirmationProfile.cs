using AutoMapper;
using ExamRegistrationService.Entities;
using ExamRegistrationService.Models;

namespace ExamRegistrationService.Profiles
{
    public class ExamRegistrationConfirmationProfile : Profile
    {
        public ExamRegistrationConfirmationProfile()
        {
            CreateMap<ExamRegistrationConfirmation, ExamRegistrationConfirmationDto>()
                .ForMember(
                    dest => dest.Student,
                    opt => opt.MapFrom(src => $"{ src.StudentIndex } { src.StudentFirstName } { src.StudentLastName }"))
                .ForMember(
                    dest => dest.Subject,
                    opt => opt.MapFrom(src => $"{ src.SubjectCode } { src.SubjectName }"));

            CreateMap<ExamRegistration, ExamRegistrationConfirmation>();
        }
    }
}

