using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PerfectBuild.Model;
//using System;
//using System.Linq;

//namespace PerfectBuild.Data
//{
//    /*Пока не понимаю, как выполнять миграции для двух разных контекстов: основной базы и базы Identity.
//     Объединил данный под одним контекстом, наследованым от IdentityDbContext<User>
//     */
//    public class IdentityContext : IdentityDbContext<User>
//    {
//        public IdentityContext(DbContextOptions options) : base(options) { }
//    }
//}
