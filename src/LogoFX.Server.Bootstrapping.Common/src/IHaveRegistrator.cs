namespace LogoFX.Server.Bootstrapping.Common
{
    public interface IHaveRegistrator<TDependencyRegistrator>
    {
        TDependencyRegistrator Registrator { get; }
    }
}
