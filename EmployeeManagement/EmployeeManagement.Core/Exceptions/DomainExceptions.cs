using System.Collections.Generic;

namespace EmployeeManagement.Core.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}

public class ValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException(string message, IDictionary<string, string[]> errors) : base(message)
    {
        Errors = errors;
    }
}

public class BusinessRuleException : Exception
{
    public BusinessRuleException(string message) : base(message) { }
} 