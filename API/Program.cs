var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();



app.MapGet("/api/generate", () =>
{
    Random _random = new Random();
    int randomNumber = _random.Next(1, 101);

    var response = new Result
    {
        data = new Data { Number = randomNumber },
        
    };
    return response;
})
.WithName("GetRandomNumber")
.WithDescription("Generates a random number from 1 - 100")
.Produces<Result>(200)
.WithOpenApi();

app.Run();




internal class Result
{
    public Data data { get; set; }
}

internal class Data {
    public int Number { get; set; }
     }
