using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamRegistrationService.Models.Exceptions
{
    public class BillingException : Exception
    {
        public BillingException(string message) : base(message)
        {

        }
    }
}
