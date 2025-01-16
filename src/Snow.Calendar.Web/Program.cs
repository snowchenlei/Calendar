using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using Snow.Calendar.Web.Common;
using Snow.Calendar.Web.Model;
using System.Globalization;
using System.Reflection;
using Snow.Calendar.Common;
using Snow.Calendar.Common.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));
builder.Services.AddTransient<Resource>();
builder.Services.AddTransient<ChineseCalendarInfo>();
builder.Services.AddTransient<IDateHelper, DateHelper>();
builder.Services.AddTransient<SolarTerm>();
builder.Services.AddTransient<IDayHelper, DayHelper>();
builder.Services.AddTransient<IBuildHtml, BuildHtml>();
builder.Services.AddTransient<ICalendarDateHelper, CalendarDateHelper>();
builder.Services.AddTransient<IHolidayHelper, HolidayHelper>();
builder.Services.AddTransient<Constellation>();
builder.Services.AddTransient<ChineseLunisolarCalendar, ChineseLunisolarCalendar>();

builder.Services.AddMemoryCache();
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

#region Swagger

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "万年历接口",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Snow",
            Email = "1007215202@qq.com"
        },
        Description = "支持节日查询，年月查询，农历查询"
    });
    var binXmlFiles = new DirectoryInfo(AppContext.BaseDirectory).GetFiles("*.xml", SearchOption.TopDirectoryOnly);
    foreach (var filePath in binXmlFiles.Select(item => item.FullName))
    {
        options.IncludeXmlComments(filePath, true);
    }
});

#endregion Swagger

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

#region Swagger

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Calendar API V1");
});

#endregion Swagger

app.MapRazorPages();
app.MapControllers();

app.Run();