using Dapper;
using Hangfire;
using Microsoft.Owin;
using Owin;
using PandaJob.Controllers;
using System;
using System.Configuration;
using System.Data.SqlClient;

[assembly: OwinStartup(typeof(PandaJob.App_Start.Startup))]
namespace PandaJob.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage(ConfigurationManager.ConnectionStrings["PandaJob"].ConnectionString);
           
            app.UseHangfireDashboard("/PandaJob", new DashboardOptions
            {
                Authorization = new[] { new AuthorizationFilter() }
            });
            app.UseHangfireServer();

            //每2分钟执行一次
            RecurringJob.AddOrUpdate(() => ExecuteJob1(), "*/2 * * * *"); 
        }

        public void ExecuteJob1() 
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PandaJob"].ConnectionString))
            {
                string sql = "insert into Person(Name, CreateTime) values(@Name, getdate())";
                var param = new
                {
                    Name = "张三" + DateTime.Now.Minute,
                };
                conn.Execute(sql, param);
            }
        }
    }
}