namespace VeterinaryClinic.Core.Installers
{
    public interface IOrderedInstaller : IInstaller
    {
        int Order { get; }
    }
}
