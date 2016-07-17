using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Jsonsong.Api.Product.Common;
using Jsonsong.Dal.Common.MongoDb;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Serialization;
using Swashbuckle.Swagger.Model;
using Swashbuckle.SwaggerGen.Generator;

namespace Jsonsong.Api.Product
{
    public class Startup
    {
        public static string Tmp1 { get; set; }


        private readonly IHostingEnvironment _hostingEnv;

        public Startup(IHostingEnvironment env)
        {
            _hostingEnv = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            if (File.Exists("d:/config/global.json"))
            {
                builder.AddJsonFile("d:/config/global.json", optional: true, reloadOnChange: true);
            }

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();

            DataBaseManager.ConnectionString = Configuration["ConnectionString:MongoDB"];
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);


            services.AddMvc()
                .AddJsonOptions(
                    options =>
                    {
                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        options.SerializerSettings.Converters.Add(new MongoJsonConverter());
                    });
            services.AddSwaggerGen(c =>
            {
                c.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Swashbuckle Sample API",
                    Description = "A sample API for testing Swashbuckle",
                    TermsOfService = "Some terms ..."
                });

                c.OperationFilter<AssignOperationVendorExtensions>();
            });

            if (_hostingEnv.IsDevelopment())
            {
                services.ConfigureSwaggerGen(c => { c.IncludeXmlComments(GetXmlCommentsPath()); });
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // loggerFactory.AddProvider(new MongoLoggerProvider());

            app.UseApplicationInsightsRequestTelemetry();
            app.UseApplicationInsightsExceptionTelemetry();

            app.UseMvc();

            app.UseSwagger((httpRequest, swaggerDoc) => { swaggerDoc.Host = httpRequest.Host.Value; });
            app.UseSwaggerUi();
        }


        private static string GetXmlCommentsPath()
        {
            var app = PlatformServices.Default.Application;
            var path = Path.Combine(app.ApplicationBasePath, app.ApplicationName + ".xml");
            Tmp1 = path;
            return path;
        }
    }


    public class AssignOperationVendorExtensions : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            operation.Extensions.Add("x-purpose", "test");
        }
    }
}