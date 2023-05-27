namespace VeterinaryClinic.Core.Exceptions.BadRequest400
{
    public class DuplicatedNameException : ApplicationException
    {
        public DuplicatedNameException(string name)
            : base(400, $"Dog '{name}' already exists")
        {

        }
    }
}
