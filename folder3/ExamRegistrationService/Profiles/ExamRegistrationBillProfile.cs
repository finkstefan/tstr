using AutoMapper;
using ExamRegistrationService.Entities;
using ExamRegistrationService.Models;

namespace ExamRegistrationService.Profiles
{
    public class ExamRegistrationBillProfile : Profile
    {
        public ExamRegistrationBillProfile()
        {
            CreateMap<ExamRegistrationCreationDto, ExamRegistrationBillDto>();
        }
    }
}
