﻿using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Filter;
using RealEstate.RepoDAL;
using RealEstate.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
});


// Register database context
builder.Services.AddDbContext<RealEstateContext>
    (
        options => options.UseSqlServer
        (
            builder.Configuration.GetConnectionString("con")
        )
    );

builder.Services.AddScoped<IPropertiesRepo, PropertyServices>();

builder.Services.AddScoped<IUserCountRepo, UserCountService>();
builder.Services.AddScoped<IPropertyRepo, PropertyReportService>();

builder.Services.AddScoped<ILeaseAgreementRepo,LeaseAgreementServices>();

builder.Services.AddScoped<ITransactionRepo,TransactionService >();
builder.Services.AddScoped<IInquiryRepo, InquiryService>();

// ✅ Register session services
builder.Services.AddDistributedMemoryCache(); // Required
builder.Services.AddSession(); // Required



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession(); // ✅ Make sure this comes before app.UseAuthorization()

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=StartingPage}/{action=Index}/{id?}");
app.Run();
