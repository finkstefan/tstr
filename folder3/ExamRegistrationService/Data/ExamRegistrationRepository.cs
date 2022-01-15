using AutoMapper;
using ExamRegistrationService.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamRegistrationService.Data
{
    public class ExamRegistrationRepository : IExamRegistrationRepository
    {
        private readonly ExamRegistrationContext context;
        private readonly IMapper mapper;

        public ExamRegistrationRepository(ExamRegistrationContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<ExamRegistration> GetExamRegistrations(string studentIndex = null, string subjectCode = null, string subjectName = null)
        {
            return context.ExamRegistrations.Where(e => (studentIndex == null || e.StudentIndex == studentIndex) &&
                                                        (subjectCode == null || e.SubjectCode == subjectCode) &&
                                                        (subjectName == null || e.SubjectName == subjectName)).ToList();
        }

        public ExamRegistration GetExamRegistrationById(Guid registrationId)
        {
            return context.ExamRegistrations.FirstOrDefault (e => e.ExamRegistrationId == registrationId);
        }

        public ExamRegistrationConfirmation CreateExamRegistration(ExamRegistration examRegistration)
        {
            var createdEntity = context.Add(examRegistration);
            return mapper.Map<ExamRegistrationConfirmation>(createdEntity.Entity);
        }                
        
        public void UpdateExamRegistration(ExamRegistration examRegistration)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }

        public void DeleteExamRegistration(Guid registrationId)
        {
            var registration = GetExamRegistrationById(registrationId);
            context.Remove(registration);
        }
    }
}
