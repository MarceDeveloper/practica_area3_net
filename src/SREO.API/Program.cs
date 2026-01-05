using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SREO.Domain.Interfaces;
using SREO.Application.Interfaces;
using SREO.Application.DTOs;
using SREO.Infrastructure.Repositories;
using SREO.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new() { Title = "SREO API", Version = "v1" });
// });

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEspacioRepository, EspacioRepository>();
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEspacioService, EspacioService>();
builder.Services.AddScoped<IReservaService, ReservaService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "SREO",
            ValidAudience = "SREO",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mi_clave_secreta_para_jwt_muy_larga_y_segura"))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/auth/register", async (RegisterDto registerDto, IAuthService authService) =>
{
    try
    {
        var result = await authService.RegisterAsync(registerDto);
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
});

app.MapPost("/auth/login", async (LoginDto loginDto, IAuthService authService) =>
{
    try
    {
        var result = await authService.LoginAsync(loginDto);
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
});

// Espacios endpoints
app.MapGet("/espacios", async (IEspacioService espacioService) =>
{
    var result = await espacioService.GetAllAsync();
    return Results.Ok(result);
}).RequireAuthorization();

app.MapGet("/espacios/disponibles", async (IEspacioService espacioService) =>
{
    var result = await espacioService.GetDisponiblesAsync();
    return Results.Ok(result);
}).RequireAuthorization();

app.MapGet("/espacios/{id}", async (Guid id, IEspacioService espacioService) =>
{
    var result = await espacioService.GetByIdAsync(id);
    return result != null ? Results.Ok(result) : Results.NotFound();
}).RequireAuthorization();

app.MapPost("/espacios", async (CreateEspacioDto createEspacioDto, IEspacioService espacioService) =>
{
    try
    {
        var result = await espacioService.CreateAsync(createEspacioDto);
        return Results.Created($"/espacios/{result.Id}", result);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
}).RequireAuthorization();

app.MapPut("/espacios/{id}", async (Guid id, UpdateEspacioDto updateEspacioDto, IEspacioService espacioService) =>
{
    try
    {
        await espacioService.UpdateAsync(id, updateEspacioDto);
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
}).RequireAuthorization();

app.MapDelete("/espacios/{id}", async (Guid id, IEspacioService espacioService) =>
{
    await espacioService.DeleteAsync(id);
    return Results.NoContent();
}).RequireAuthorization();

// Reservas endpoints
app.MapGet("/reservas", async (IReservaService reservaService) =>
{
    var result = await reservaService.GetAllAsync();
    return Results.Ok(result);
}).RequireAuthorization();

app.MapGet("/reservas/pendientes", async (IReservaService reservaService) =>
{
    var result = await reservaService.GetPendientesAsync();
    return Results.Ok(result);
}).RequireAuthorization();

app.MapGet("/reservas/usuario/{usuarioId}", async (Guid usuarioId, IReservaService reservaService) =>
{
    var result = await reservaService.GetByUsuarioIdAsync(usuarioId);
    return Results.Ok(result);
}).RequireAuthorization();

app.MapGet("/reservas/{id}", async (Guid id, IReservaService reservaService) =>
{
    var result = await reservaService.GetByIdAsync(id);
    return result != null ? Results.Ok(result) : Results.NotFound();
}).RequireAuthorization();

app.MapPost("/reservas", async (CreateReservaDto createReservaDto, IReservaService reservaService) =>
{
    try
    {
        var result = await reservaService.CreateAsync(createReservaDto);
        return Results.Created($"/reservas/{result.Id}", result);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
}).RequireAuthorization();

app.MapPut("/reservas/{id}/estado", async (Guid id, UpdateReservaDto updateReservaDto, IReservaService reservaService) =>
{
    try
    {
        await reservaService.UpdateEstadoAsync(id, updateReservaDto);
        return Results.NoContent();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
}).RequireAuthorization();

app.MapDelete("/reservas/{id}", async (Guid id, IReservaService reservaService) =>
{
    await reservaService.DeleteAsync(id);
    return Results.NoContent();
}).RequireAuthorization();

app.Run();
