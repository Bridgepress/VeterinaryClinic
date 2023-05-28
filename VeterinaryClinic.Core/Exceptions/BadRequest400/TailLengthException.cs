namespace VeterinaryClinic.Core.Exceptions.BadRequest400
{
    public class TailLengthException : ApplicationException
    {
        public TailLengthException()
            : base(400, $"Tail height is a negative number")
        {

        }
    }
}
