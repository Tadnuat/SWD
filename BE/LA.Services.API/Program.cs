using LA.Services.API.Data;
using LA.Services.API.Services.IService;
using LA.Services.API.Services;
using Microsoft.EntityFrameworkCore;

namespace LA.Services.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Đăng ký Service
            builder.Services.AddScoped<ILawyerService, LawyerService>();
            builder.Services.AddScoped<ILawyerDiplomaService, LawyerDiplomaService>();
            builder.Services.AddScoped<IWorkSlotService, WorkSlotService>();



            // Add services to the container.

            builder.Services.AddControllers();
            //Add DbContext

          

            //        builder.Services.AddDbContext<AppointmentDbContext>(options =>
            //options.UseSqlServer(builder.Configuration.GetConnectionString("AppointmentDbConnection")));

            builder.Services.AddDbContext<LawyerDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("LawyerDbConnection")));



            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //AutoMapper
            builder.Services.AddAutoMapper(typeof(MappingConfig));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:5173")
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            // Use CORS
            app.UseCors("AllowFrontend");

            app.MapControllers();
            //ApplyMigrations(app); // Áp dụng migration khi ứng dụng khởi động
            app.Run();

            //tự động áp dụng các migration vào database khi ứng dụng khởi động
            //void ApplyMigrations(IApplicationBuilder app)
            //{
            //    using (var scope = app.ApplicationServices.CreateScope())
            //    {
            //        var dbContext = scope.ServiceProvider.GetRequiredService<LawyerDbContext>();
            //        dbContext.Database.Migrate();
            //    }
            //}
        }
    }
}
