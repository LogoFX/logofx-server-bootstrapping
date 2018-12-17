using Microsoft.Extensions.DependencyInjection;
using Solid.Bootstrapping;
using Solid.Extensibility;
using Solid.Practices.Middleware;

namespace LogoFX.Server.Bootstrapping
{
    public partial class BootstrapperBase : 
        IExtensible<BootstrapperBase>, 
        IExtensible<IHaveRegistrator<IServiceCollection>>
    {
        private readonly MiddlewaresWrapper<BootstrapperBase> _concreteMiddlewaresWrapper;

        /// <inheritdoc />       
        public BootstrapperBase Use(
            IMiddleware<BootstrapperBase> middleware)
        {
            _concreteMiddlewaresWrapper.Use(middleware);
            return this;
        }

        private readonly MiddlewaresWrapper<IHaveRegistrator<IServiceCollection>> _registratorMiddlewaresWrapper;

        /// <inheritdoc />       
        public IHaveRegistrator<IServiceCollection> Use(
            IMiddleware<IHaveRegistrator<IServiceCollection>> middleware)
        {
            _registratorMiddlewaresWrapper.Use(middleware);
            return this;
        }		
    }
}
