using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VeterinaryClinic.Core.Validation;
using VeterinaryClinic.Domain.Entities;

namespace VeterinaryClinic.DataAccess.EntityConfigurations
{
    public class DogConfiguration : IEntityTypeConfiguration<Dog>
    {
        public void Configure(EntityTypeBuilder<Dog> builder)
        {
            builder.Property(a => a.Name).HasMaxLength(FieldsValidation.Dog.DogNameMaxLength);
            builder.Property(a => a.Color).HasMaxLength(FieldsValidation.Dog.DogColorMaxLength);
        }
    }

}
