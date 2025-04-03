using Lib.Application.UseCases.Books;

namespace Lib.API.Extensions
{
    public static class AddUseCasesExtention
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            var assembly = typeof(AddBookUseCase).Assembly;

            var useCasesTypes = assembly.GetTypes()
                .Where(t => t.Name.EndsWith("UseCase") && t.IsClass && !t.IsAbstract);

            foreach (var type in useCasesTypes)
            {
                services.AddScoped(type);
            }
        }
    }
}
