using LessonMonitor.BusinessLogic;
using LessonMonitor.Core.Repositories;
using LessonMonitor.Core.Services;
using LessonMonitor.DataAccess.MSSQL;
using LessonMonitor.DataAccess.MSSQL.Repositories;
using LessonMonitor.GitHubClientApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace LessonMonitor.API
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public string[] AllKeys { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<ApiMappingProfile>();
                cfg.AddProfile<DataAccessMappingProfile>();
                cfg.AddProfile<BusinessLogicMappingProfile>();
            });

            services.AddScoped<IHomeworksRepository, HomeworksRepository>();
            services.AddScoped<IHomeworksService, HomeworksService>();

            services.AddScoped<IMembersRepository, MembersRepository>();
            services.AddScoped<IMembersService, MembersService>();

            services.AddScoped<ILessonsRepository, LessonsRepository>();
            services.AddScoped<ILessonsService, LessonsService>();

            services.AddGitHubApiClient(Configuration);

            services.AddDbContext<LessonMonitorDbContext>(builder =>
            {
                builder.UseSqlServer(Configuration.GetConnectionString("LessonMonitorDb"));
            });

            services.AddControllers();

          
            services.AddAutoMapper(cfg =>
            {
                //cfg.AddMaps(typeof(Startup).Assembly, typeof(LessonMonitorDbContext).Assembly);

                cfg.AddProfile<ApiMappingProfile>();
                cfg.AddProfile<DataAccessMapperProfile>();

            });

            //services.AddSingleton<IGitHubService, GitHubService>();
            //services.AddSingleton<IGitHubRepository, GitHubRepository>();

            services.AddScoped<IHomeworksService, HomeworksService>();
            services.AddScoped<IHomeworksRepository, HomeworksRepository>();

            services.AddTransient<IMembersService, MembersService>();
            services.AddTransient<IMembersRepository, MembersRepository>();
            
            services.AddTransient<IQuestionsService, QuestionsService>();
            services.AddTransient<IQuestionsRepository, QuestionsRepository>();

            services.AddScoped<ILessonsService, LessonsService>();
            services.AddScoped<ILessonsRepository, LessonsRepository>();

            services.AddSingleton<IResponseBodyRepository, ResponseBodyRepository>();

            services.AddDbContext<LMonitorDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("LessonMonitorDbMain"));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LessonMonitor.API", Version = "v1" });

                var filePath = Path.Combine(System.AppContext.BaseDirectory, "LessonMonitor.API.xml");

                c.IncludeXmlComments(filePath);
            });
        }

        public void Configure([NotNull] IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LessonMonitor.API v1"));
            }

            ErrorMessageRegistry.ReBuild();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<MyMiddlewareComponent>();

            app.Use( async (httpContext, next) =>
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.contoso.com/");

                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                WebHeaderCollection myWebHeaderCollection = myHttpWebResponse.Headers;

                await using StreamWriter file = new StreamWriter("HeaderLines.txt", true);

                for (int i = 0; i < myWebHeaderCollection.Count; i++)
                {
                    var header = myWebHeaderCollection.GetKey(i);

                    string[] values = myWebHeaderCollection.GetValues(header);

                    if (values.Length > 0)
                    {
                        for (int j = 0; j < values.Length; j++)
                        {
                            file.WriteLine(values[j]);
                        } 
                    }
                }

                string[] headers = myWebHeaderCollection.AllKeys;

                foreach (string header in headers)
                {
                    file.WriteLine(myWebHeaderCollection.Get(header));
                }

                file.WriteLine(myWebHeaderCollection);

                myWebHeaderCollection.Clear();

                myHttpWebResponse.Close();

                var task = next();

                await task;
            });

            app.UseAuthorizationMiddleware();

            app.UseResponseBodyMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
