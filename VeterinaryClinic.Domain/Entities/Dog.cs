namespace VeterinaryClinic.Domain.Entities
{
    public class Dog : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Color { get; set; } = default!;
        public double TailLength { get; set; }
        public double Weight { get; set; }
    }
}
