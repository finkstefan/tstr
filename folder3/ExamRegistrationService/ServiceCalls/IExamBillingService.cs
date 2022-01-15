using ExamRegistrationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamRegistrationService.ServiceCalls
{
    public interface IExamBillingService
    {
        public bool BillStudentAccount(ExamRegistrationBillDto bill);
    }
}
