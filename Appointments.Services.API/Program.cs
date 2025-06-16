using Appointments.Services.API.Data;
using Appointments.Services.API.Services;
using Appointments.Services.API.Services.IService;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Services.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<AppointmentDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("AppointmentConnection")));

            // Add AutoMapper with specific configuration
            builder.Services.AddAutoMapper(typeof(MappingConfig));

            // Configure HTTP clients
            builder.Services.AddHttpClient("UserService", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:UsersAPI"]);
            });
            builder.Services.AddHttpsRedirection(options =>
            {
                options.HttpsPort = 7071; // Port cho HTTPS
            });

            builder.Services.AddHttpClient("LawyerService", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:LawyersAPI"]);
            });

            // Add Services
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<AppointmentWithUserLawyerService>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();
       

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
          
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
