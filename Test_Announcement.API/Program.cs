using Microsoft.EntityFrameworkCore;
using Test_Announcement.API.Interfaces;
using Test_Announcement.API.Mappers;
using Test_Announcement.API.Services;
using Test_Announcement.DataAccess.Context;

namespace Test_Announcement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(MapperConfig));

            builder.Services.AddDbContext<AnnouncementDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IAnnouncementDBService, AnnouncementDBService>();
            builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();

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

            UpdateDatabase(app);

            app.Run();
        }
        public static void UpdateDatabase(WebApplication app)
        {
            using (var serviceScope = app.Services.CreateScope())
            {
                AnnouncementDbContext context = serviceScope.ServiceProvider.GetService<AnnouncementDbContext>();
                context.Database.Migrate();
            }
        }
    }
}
