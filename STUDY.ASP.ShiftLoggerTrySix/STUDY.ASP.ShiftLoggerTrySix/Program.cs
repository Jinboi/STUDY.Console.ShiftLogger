using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

var shifts = new List<Shift>();

app.MapGet("/shifts", () => shifts);
app.MapGet("/shifts/{id}", Results<Ok<Shift>,NotFound> (int id) =>
{
    var targetShift = shifts.SingleOrDefault(t => id == t.Id);
    return targetShift is null
        ? TypedResults.NotFound()
        : TypedResults.Ok(targetShift);
});
app.MapPost("/shifts", (Shift shift) =>
{
    shifts.Add(shift);
    return TypedResults.Created("/shifts/{id}", shift);
});


app.MapDelete("/shifts/{id}", (int id) =>
{
    shifts.RemoveAll(t => id == t.Id);
    return TypedResults.NoContent();
});

app.Run();
public record Shift(int Id, int EmployeeId, DateTime ClockIn, DateTime ClockOut, bool IsDoneWorking);