using Demo.NetCore2._2;
using EntityFrameworkCore.DbContextScope.NetCore;
using Numero3.EntityFramework.Demo.DatabaseContext.NetCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Numero3.EntityFramework.Demo.BusinessLogicServices.NetCore
{
    public class UserCreditScoreService {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;

        public UserCreditScoreService(IDbContextScopeFactory dbContextScopeFactory) {
            _dbContextScopeFactory = dbContextScopeFactory ?? throw new ArgumentNullException("dbContextScopeFactory");
        }

        public void UpdateCreditScoreForAllUsers(IIdentity identity) {
            /*
			 * Demo of DbContextScope + parallel programming.
			 */

            using (var dbContextScope = _dbContextScopeFactory.Create()) {
                //-- Get all users
                var dbContext = dbContextScope.DbContexts.Get<UserManagementDbContext, IIdentity>(identity);
                var userIds = dbContext.Users.Select(u => u.Id).ToList();

                Console.WriteLine("Found {0} users in the database. Will calculate and store their credit scores in parallel.", userIds.Count());

                //-- Calculate and store the credit score of each user
                // We're going to imagine that calculating a credit score of a user takes some time. 
                // So we'll do it in parallel.

                // You MUST call SuppressAmbientContext() when kicking off a parallel execution flow 
                // within a DbContextScope. Otherwise, this DbContextScope will remain the ambient scope
                // in the parallel flows of execution, potentially leading to multiple threads
                // accessing the same DbContext instance.
                using (_dbContextScopeFactory.SuppressAmbientContext()) {
                    Parallel.ForEach(userIds, (t) => UpdateCreditScore(t, identity));
                }

                // Note: SaveChanges() isn't going to do anything in this instance since all the changes
                // were actually made and saved in separate DbContextScopes created in separate threads.
                dbContextScope.SaveChanges();
            }
        }

        public void UpdateCreditScore(Guid userId, IIdentity _identity) {
            using (var dbContextScope = _dbContextScopeFactory.Create()) {
                var dbContext = dbContextScope.DbContexts.Get<UserManagementDbContext, IIdentity>(_identity);
                var user = dbContext.Users.Find(userId);
                if (user == null)
                    throw new ArgumentException(String.Format("Invalid userId provided: {0}. Couldn't find a User with this ID.", userId));

                // Simulate the calculation of a credit score taking some time
                var random = new Random(Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(random.Next(300, 1000));

                user.CreditScore = random.Next(1, 100);
                dbContextScope.SaveChanges();
            }
        }
    }
}
