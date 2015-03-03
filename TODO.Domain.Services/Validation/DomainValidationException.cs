using System;
using System.Collections.Generic;

namespace TODO.Domain.Services.Validation
{
    public class DomainValidationException : Exception
    {
        public List<string> ValidationErrors { get; set; }
    }
}
