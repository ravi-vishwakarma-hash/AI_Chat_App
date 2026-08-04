using AI_Chat_App.Core.Interfaces;
using AI_Chat_App.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// register the HttpClient 
builder.Services.AddHttpClient();

// register the AI services

//builder.Services.AddKeyedTransient<IChatCompletionService>("ForumSpeed", (sp, key) =>
//{
//    var groqApiKey = builder.Configuration["GroqApiKey"];


//    return new GroqChatCompletionService("llama-3.1-70b", groqApiKey);
//});

builder.Services.AddKeyedTransient<IChatCompletionService>("Google", (sp, key) =>
{
    var google = builder.Configuration.GetSection("AIModels:Google");
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();

    return new GoogleGeminiChatCompletionService(httpClientFactory.CreateClient(), google["Model"]!, google["ApiKey"]!);
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
