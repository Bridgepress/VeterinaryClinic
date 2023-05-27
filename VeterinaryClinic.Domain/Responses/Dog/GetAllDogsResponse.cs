namespace VeterinaryClinic.Domain.Responses.Dog
{
    public class GetAllDogsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Color { get; set; } = default!;
        public double TailLength { get; set; }
        public double Weight { get; set; }
    }
}
