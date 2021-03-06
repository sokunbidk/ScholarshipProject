using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScholarshipManagement.Data.ApplicationContext;
using ScholarshipManagement.Data.Repositories;
using ScholarshipManagement.Data.Interfaces;
using ScholarshipManagement.Data.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ScholarshipManagement.Web.UI
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
            services.AddControllersWithViews();
            services.AddDbContext<SchoolDbContext>(options =>
                options.UseMySQL(Configuration.GetConnectionString("SchoolDbContext"))
                );

            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<IJamaatRepository, JamaatRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICircuitRepository, CircuitRepository>();
            services.AddScoped<IJamaatRepository, JamaatRepository>();


            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ICircuitService, CircuitService>();
            services.AddScoped<IJamaatService, JamaatService>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(config =>
                {
                    config.LoginPath = "/user/login";
                    config.Cookie.Name = "Scholarship";
                });


            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseHttpsRedirection();
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
        });
        }
    }
}
