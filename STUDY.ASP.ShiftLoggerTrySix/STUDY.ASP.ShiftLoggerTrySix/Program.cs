var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/person/{id:int}", (int id) =>
{
    return new PersonRecord(id, "Tim", "Corey");
});

app.MapPost("/person", (PersonRecord person) =>
{
    return person;
});

app.Run();
record PersonRecord(int Id, string FirstName, string LastName);

