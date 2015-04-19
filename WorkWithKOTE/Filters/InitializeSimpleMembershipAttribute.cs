using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using WorkWithKOTE.Models;

namespace WorkWithKOTE.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<UsersContext>(null);
                try
                {
                    using (var context = new UsersContext())
                    {
                        
                        if (!context.Database.Exists())
                        {
                            // Create the SimpleMembership database without Entity Framework migration schema
                           
                          ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                          DefaultValueForTour.DefaultValueForTourStatus();
                          DefaultValueForTour.DefaultValueForTourTypeOfTour();
                          DefaultValueForTour.DefaultValueForBigLogo();
                          DefaultValueForTour.Anonimus();
                           // Database.SetInitializer<UsersContext>(new TourInitialization());
                            
                        }
                    }
                    WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfiles", "UserId", "Email", autoCreateTables: true);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
                var roles = (SimpleRoleProvider)Roles.Provider;

                //Получаем провайдер членства
                var membership = (SimpleMembershipProvider)Membership.Provider;

                // Если нет в системе роли admin, создаём её
                if (!roles.RoleExists("Admin"))
                    roles.CreateRole("Admin");

                // Если нет в системе пользователя admin, создаём его(в этом месте ошибка)
                if (membership.GetUser("kote.travel@gmail.com", false) == null)
                {
                    membership.CreateUserAndAccount("kote.travel@gmail.com", "koteadmintravel");
                }

                // Если у пользователя admin нет роли admin, присваиваем ему эту роль
                if (!roles.IsUserInRole("kote.travel@gmail.com", "Admin"))
                    roles.AddUsersToRoles(new[] { "kote.travel@gmail.com" }, new[] { "Admin" });
            }

        }
    }
}
