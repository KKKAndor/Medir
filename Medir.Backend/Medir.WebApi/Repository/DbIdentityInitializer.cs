using Medir.Persistence;

namespace Medir.WebApi.Repository
{
    public class DbIdentityInitializer
    {
        public static void Initialize(RepositoryContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
