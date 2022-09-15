using Microsoft.AspNetCore.OData;
using BinaryCityProject.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDbContext<BinaryCity_DbContext>(options => options.UseSqlServer("Data Source=NM-NB-10024638;Initial Catalog=BinaryCityDb;Integrated Security=True;MultipleActiveResultSets=True"));
builder.Services.AddControllers().AddOData(opt => opt.AddRouteComponents("odata", GetEdmModel()).Filter().Select());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseMvc(routeBuilder =>
//{
//    routeBuilder.Select().Filter();
//    routeBuilder.MapODataServiceRoute("odata", "odata", GetEdmModel());
//    routeBuilder.MaxTop(null).Expand().Select().Filter().OrderBy().Count();

//});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();

    builder.EntitySet<Client>("Client");
    builder.EntitySet<Contact>("Contact");
    builder.EntitySet<Client_Contact_List>("Client_Contact_List");
    builder.EntitySet<Contact_Client_List>("Contact_Client_List");

    return builder.GetEdmModel();
}