using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace ODataOnPremise
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options => options.EnableEndpointRouting = false);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddOData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});

            var model = GetEdmModel2();
            //var model = GetEdmModel();

            app.UseMvc(option => {
                option.MaxTop(null).Select().SkipToken().Count().Filter().OrderBy().Expand();
                option.MapODataServiceRoute("odata", "odata", model);
            });
        }

        private IEdmModel GetEdmModel2()
        {           
            var builder = new CustomModelBuilder();
            IEdmModel model = builder
                //.BuildAddressType()
                //.BuildCategoryType()
                //.BuildCustomerType()
                .BuildWeatherForecastType()
                .BuildDefaultContainer()
                .BuildCustomerSet()
                .GetModel();

            MemoryStream stream = new MemoryStream();
            InMemoryMessage message = new InMemoryMessage { Stream = stream };
            ODataMessageWriterSettings settings = new ODataMessageWriterSettings();
            settings.ODataUri = new ODataUri
            {
                ServiceRoot = new Uri("https://localhost:44329/odata/odata.svc/")
            };



            // write metadata payload
            //ODataMessageWriter writer = new ODataMessageWriter((IODataResponseMessage)message, settings,model);
            //writer.WriteMetadataDocument();


            // write services documents payload
            // One way
            //ODataMessageWriter writer = new ODataMessageWriter((IODataResponseMessage)message, settings);
            //ODataServiceDocument serviceDocument = new ODataServiceDocument();
            //serviceDocument.EntitySets = new[]
            //{
            //    new ODataEntitySetInfo
            //    {
            //        Name = "WeatherForecast",
            //        Title = "WeatherForecast",
            //        Url = new Uri("WeatherForecast", UriKind.Relative)
            //    },

            //};
            //writer.WriteServiceDocument(serviceDocument);

            // Second way
            ODataMessageWriter writer = new ODataMessageWriter((IODataResponseMessage)message, settings);
            ODataServiceDocument serviceDocument = model.GenerateServiceDocument();
            writer.WriteServiceDocument(serviceDocument);


            // write entity response
            //ODataMessageWriter writer = new ODataMessageWriter((IODataResponseMessage)message, settings,model);
            //IEdmEntitySet entitySet = model.FindDeclaredEntitySet("WeatherForecast");
            //ODataWriter odataWriter = writer.CreateODataResourceSetWriter(entitySet);
            //ODataResourceSet set = new ODataResourceSet();
            //odataWriter.WriteStart(set);
            //odataWriter.WriteEnd();


            // generating file

            message.Stream.Seek(0, SeekOrigin.Begin);
            StreamReader reader = new StreamReader(message.Stream);
            var path = Path.Combine(AppContext.BaseDirectory, "servicesdocument.xml");
            File.WriteAllText(path, reader.ReadToEnd());
                                 

            return model;           

        }

        //private IEdmModel GetEdmModel()
        //{
        //    var edmBuilder = new ODataConventionModelBuilder();
        //    //edmBuilder.EntitySet<WeatherForecast>("WeatherForecast");
        //    var weather  = edmBuilder.EntitySet<WeatherForecast>("WeatherForecast");

        //    weather
        //        .EntityType
        //        .Collection
        //        .Function("OverideGrades")
        //        .ReturnsCollectionFromEntitySet<Grade>("Grade");



        //    return edmBuilder.GetEdmModel();


        //}
    }
}
