
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.DAL.EF;
using Todo.DAL.Models;
using Todo.DAL.Interfaces;

namespace Todo.DAL
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDalDependencies(this IServiceCollection serviceCollection)
        {
            return
                serviceCollection.AddScoped(typeof(IBaseRespository<>), typeof(BaseRepository<>))
                .AddScoped(typeof(IIdentityManager<>), typeof(IdentityManager<>))
                .AddIdentityCore<User>().AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().Services;
                               
            ;
        }
    }
}
