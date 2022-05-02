using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gmail_Services
{
    public class Startup
    {
        IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // cau hinh: MailSettings,du lieu duoc khoi tao tu doi tuong mailsettings
            // No duoc inject vao cac doi tuong khac khi cac doi tuong khac can su dung den no
            services.AddOptions();
            var mailsettings = _configuration.GetSection("MailSettings");
            services.Configure<MailSettings>(mailsettings); 

            services.AddTransient<SendMailService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                endpoints.MapGet("/sendmail", async context => {
                  var message = await  Gmail_Services.MailUtills.SendMail("hoangvanhunghd96@gmail.com", "justforyou96hd@gmail.com", "Try Gmail services in ASP.Core", "first step to be better");
                    await context.Response.WriteAsync(message);
                });
                endpoints.MapGet("/sendgmail", async context => {
                  var message = await  Gmail_Services.MailUtills.SendGmail("hoangvanhunghd96@gmail.com", "justforyou96hd@gmail.com", "Try Gmail services in ASP.Core", "first step to be better", "hoangvanhunghd96@gmail.com","Anh123hd");
                    await context.Response.WriteAsync(message);
                });

                endpoints.MapGet("/sendMailService", async context => {
                  var sendMailService = context.RequestServices.GetService<SendMailService>();
                  var mailContent = new MailContent();
                  mailContent.To ="justforyou96hd@gmail.com";
                  mailContent.Subject ="Test mailServices";
                  mailContent.Body="you will be better yesterday";
                  
                 var result =  await sendMailService.SendMail(mailContent);

                    await context.Response.WriteAsync(result);
                });
            });
        }
    }
}
