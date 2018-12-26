using Solid.Practices.Middleware;

// ReSharper disable once CheckNamespace
namespace Solid.Extensibility
{
    //TODO: Move to packages
    public class ExtensibilityAspect<T> :
        IAspect,
        IExtensible<T> where T : class
    {
        private readonly T _bootstrapper;

        public ExtensibilityAspect(T bootstrapper)
        {
            _bootstrapper = bootstrapper;
            _middlewaresWrapper = new MiddlewaresWrapper<T>(_bootstrapper);
        }

        private readonly MiddlewaresWrapper<T> _middlewaresWrapper;

        /// <inheritdoc />       
        public T Use(
            IMiddleware<T> middleware)
        {
            _middlewaresWrapper.Use(middleware);
            return _bootstrapper;
        }

        public void Initialize()
        {
            MiddlewareApplier.ApplyMiddlewares(_bootstrapper, _middlewaresWrapper.Middlewares);
        }

        public string Id => $"Extensibility.{typeof(T).Name}";
        public string[] Dependencies => new[] { "Modularity", "Discovery", "Platform" };
    }
}