
namespace OrderingApplication.Exceptions;

public class NotFoundException : ApplicationException
{

    public NotFoundException(string name, object key) : base($"Entity \"{name}\" and \"{key}\" was not found")
    {

    }


}
