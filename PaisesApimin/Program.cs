var builder = WebApplication.CreateBuilder(args);


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200");
                          policy.AllowAnyOrigin();
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                          
                      });
});
var app = builder.Build();
app.MapGet("/", () => "Hello World!");


app.MapGet("/response", async () =>
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync("https://restcountries.com/v3.1/all");
    response.EnsureSuccessStatusCode();
    string responseBody = await response.Content.ReadAsStringAsync();
    return responseBody;
});


app.UseCors(MyAllowSpecificOrigins);

app.Run();
