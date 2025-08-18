
using FluentValidation.Results;

namespace OrderingApplication.Exceptions;

public class ValidationException : ApplicationException
{
    public IDictionary<string , string[]> Errors { get; set; }

    public ValidationException() : base("One or more validations errors occurred")
    {
        Errors = new Dictionary<string , string[]>();   
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures.GroupBy(f => f.PropertyName, e => e.ErrorMessage).
                            ToDictionary(fg => fg.Key, fg => fg.ToArray());
    }

}
