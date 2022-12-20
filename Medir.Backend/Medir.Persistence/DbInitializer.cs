using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medir.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(MedirDbContext context)
        {            
            context.Database.EnsureCreated();
        }
    }
}
