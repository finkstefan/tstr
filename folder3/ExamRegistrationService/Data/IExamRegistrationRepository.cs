using ExamRegistrationService.Entities;
using System;
using System.Collections.Generic;

namespace ExamRegistrationService.Data
{
    public interface IExamRegistrationRepository
    {
        List<ExamRegistration> GetExamRegistrations(string studentIndex = null, string subjectCode = null, string subjectName = null);

        ExamRegistration GetExamRegistrationById(Guid registrationId);

        ExamRegistrationConfirmation CreateExamRegistration(ExamRegistration examRegistration);

        void UpdateExamRegistration(ExamRegistration examRegistration);

        void DeleteExamRegistration(Guid registrationId);

        bool SaveChanges();
    }
}
