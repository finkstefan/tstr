using ExamRegistrationService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExamRegistrationService.Data
{
    public class ExamRegistrationMockRepository : IExamRegistrationRepository
    {
        public static List<ExamRegistration> ExamRegistrations { get; set; } = new List<ExamRegistration>();

        public ExamRegistrationMockRepository()
        {
            FillData();
        }

        private void FillData()
        {
            ExamRegistrations.AddRange(new List<ExamRegistration>
            {
                new ExamRegistration
                {
                    ExamRegistrationId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                    StudentId = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
                    StudentFirstName = "Petar",
                    StudentLastName = "Petrović",
                    StudentIndex = "IT1/2020",
                    StudentCurrentSemester = 1,
                    StudentEnrolledYear = 1,
                    SubjectId = Guid.Parse("21ad52f8-0281-4241-98b0-481566d25e4f"),
                    SubjectCode = "S12020",
                    SubjectName = "Subject 1",
                    SubjectTerm = 1,
                    SubjectSemester = 1,
                    SubjectExamTime = DateTime.Parse("2020-11-15T09:00:00")
                },
                new ExamRegistration
                {
                    ExamRegistrationId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                    StudentId = Guid.Parse("32cd906d-8bab-457c-ade2-fbc4ba523029"),
                    StudentFirstName = "Marko",
                    StudentLastName = "Marković",
                    StudentIndex = "IT2/2019",
                    StudentCurrentSemester = 1,
                    StudentEnrolledYear = 2,
                    SubjectId = Guid.Parse("9d8bab08-f442-4297-8ab5-ddfe08e335f3"),
                    SubjectCode = "S22020",
                    SubjectName = "Subject 2",
                    SubjectTerm = 1,
                    SubjectSemester = 2,
                    SubjectExamTime = DateTime.Parse("2020-11-15T09:00:00")
                }
            });
        }

        public List<ExamRegistration> GetExamRegistrations(string studentIndex = null, string subjectCode = null, string subjectName = null)
        {
            return (from e in ExamRegistrations
                    where string.IsNullOrEmpty(studentIndex) || e.StudentIndex == studentIndex &&
                          string.IsNullOrEmpty(subjectCode) || e.SubjectCode == subjectCode &&
                          string.IsNullOrEmpty(subjectName) || e.SubjectName == subjectName
                    select e).ToList();
        }

        public ExamRegistration GetExamRegistrationById(Guid registrationId)
        {
            return ExamRegistrations.FirstOrDefault(e => e.ExamRegistrationId == registrationId);
        }

        public ExamRegistrationConfirmation CreateExamRegistration(ExamRegistration examRegistration)
        {
            examRegistration.ExamRegistrationId = Guid.NewGuid();
            ExamRegistrations.Add(examRegistration);
            var exam = GetExamRegistrationById(examRegistration.ExamRegistrationId);

            return new ExamRegistrationConfirmation
            {
                ExamRegistrationId = exam.ExamRegistrationId,
                StudentIndex = exam.StudentIndex,
                StudentFirstName = exam.StudentFirstName,
                StudentLastName = exam.StudentLastName,
                SubjectCode = exam.SubjectCode,
                SubjectName = exam.SubjectName,
                ExamTime = exam.SubjectExamTime
            };
        }

        public void DeleteExamRegistration(Guid registrationId)
        {
            ExamRegistrations.Remove(ExamRegistrations.FirstOrDefault(e => e.ExamRegistrationId == registrationId));
        }

        public void UpdateExamRegistration(ExamRegistration examRegistration)
        {
            var exam = GetExamRegistrationById(examRegistration.ExamRegistrationId);

            exam.StudentId = examRegistration.StudentId;
            exam.StudentFirstName = examRegistration.StudentFirstName;
            exam.StudentLastName = examRegistration.StudentLastName;
            exam.StudentIndex = examRegistration.StudentIndex;
            exam.StudentEnrolledYear = examRegistration.StudentEnrolledYear;
            exam.StudentCurrentSemester = examRegistration.StudentCurrentSemester;
            exam.SubjectId = examRegistration.SubjectId;
            exam.SubjectCode = examRegistration.SubjectCode;
            exam.SubjectName = examRegistration.SubjectName;
            exam.SubjectSemester = examRegistration.SubjectSemester;
            exam.SubjectTerm = examRegistration.SubjectTerm;
            exam.SubjectExamTime = examRegistration.SubjectExamTime;
        }

        //Koristi se za Update kako u kontroleru ne bi morali da menjamo logiku pri zameni repozitorijuma
        public bool SaveChanges()
        {            
            return true;
        }
    }
}
