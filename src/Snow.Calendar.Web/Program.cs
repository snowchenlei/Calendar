using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllers();

#region Swagger

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "�������ӿ�",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Snow",
            Email = "1007215202@qq.com"
        },
        Description = "֧�ֽ��ղ�ѯ�����²�ѯ��ũ����ѯ"
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

app.Run();
