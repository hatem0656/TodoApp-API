using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Core.IServices;
using Todo.Core.Services;
using Todo.DAL;

namespace Todo.Core
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection serviceCollection)
        {
            return
                serviceCollection
                .AddScoped<ITodoService, TodoService>()
                .AddScoped<IUserService, UserService>()
                .AddDalDependencies();
        }
    }
}
