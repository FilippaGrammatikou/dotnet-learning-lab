using BookReviewApp;
using BookReviewApp.DATA;
using BookReviewApp.Interfaces;
using BookReviewApp.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// every time the application requests a seed instance it will create a new one
builder.Services.AddTransient<Seed>();
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Add Dependency Injection for our Repository
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewerRepository, ReviewerRepository>();


builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// call class within program.cs to populate the DB when the API starts
using (var scope = app.Services.CreateScope())
{
    var seed = scope.ServiceProvider.GetRequiredService<Seed>();
    seed.SeedDataContext();
}

app.Run();
