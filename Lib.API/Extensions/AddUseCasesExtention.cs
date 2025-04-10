using Lib.Application.UseCases.Books;

namespace Lib.API.Extensions
{
    public static class AddUseCasesExtention
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            var assembly = typeof(AddBookUseCase).Assembly;

            var useCaseTypes = assembly.GetTypes()
                .Where(t => t.Name.EndsWith("UseCase") && t.IsClass && !t.IsAbstract);

            var useCaseInterfaceTypes = assembly.GetTypes()
                .Where(t => t.Name.EndsWith("UseCase") && t.IsInterface);

            foreach (var implementationType in useCaseTypes)
            {
                var implementedInterfaces = implementationType.GetInterfaces()
                    .Where(i => useCaseInterfaceTypes.Contains(i));

                foreach (var interfaceType in implementedInterfaces)
                {
                    services.AddScoped(interfaceType, implementationType);
                }
            }
        }
    }
}
