using Microsoft.EntityFrameworkCore;
using MyBackend.Data;
using MyBackend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Cargar las variables de entorno
Env.Load();

builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS
app.UseCors("AllowAllOrigins");

app.UseAuthentication();  // Asegúrate de que esto esté antes de UseAuthorization
app.UseAuthorization();

// Endpoint para login
app.MapPost("/api/auth/login", async (ApplicationDbContext db, UsuarioLogin usuarioLogin) =>
{
    var usuario = await db.Usuarios.FirstOrDefaultAsync(u => u.Email == usuarioLogin.Email && u.Password == usuarioLogin.Password);

    if (usuario == null)
    {
        return Results.Unauthorized();
    }

    var claims = new[]
    {
        new Claim(ClaimTypes.Name, usuario.Email),
        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString())
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    var token = new JwtSecurityToken(
        issuer: builder.Configuration["Jwt:Issuer"],
        audience: builder.Configuration["Jwt:Audience"],
        claims: claims,
        expires: DateTime.Now.AddMinutes(30),
        signingCredentials: creds);

    return Results.Ok(new
    {
        token = new JwtSecurityTokenHandler().WriteToken(token),
        expiration = token.ValidTo
    });
});

// Endpoints for Usuarios
app.MapGet("/api/usuarios", async (ApplicationDbContext db) =>
    await db.Usuarios.ToListAsync());

app.MapPost("/api/usuarios", async (ApplicationDbContext db, Usuario usuario) =>
{
    db.Usuarios.Add(usuario);
    await db.SaveChangesAsync();
    return Results.Created($"/api/usuarios/{usuario.Id}", usuario);
});

// Endpoints for Thumbnails
app.MapGet("/api/thumbnails", async (ApplicationDbContext db) =>
    await db.Thumbnails.ToListAsync());

app.MapPost("/api/thumbnails", async (ApplicationDbContext db, Thumbnail thumbnail) =>
{
    db.Thumbnails.Add(thumbnail);
    await db.SaveChangesAsync();
    return Results.Created($"/api/thumbnails/{thumbnail.Id}", thumbnail);
});

// Endpoint para registro
app.MapPost("/api/auth/register", async (ApplicationDbContext db, UsuarioLogin usuarioLogin) =>
{
    var usuarioExistente = await db.Usuarios.FirstOrDefaultAsync(u => u.Email == usuarioLogin.Email);

    if (usuarioExistente != null)
    {
        return Results.BadRequest("Usuario ya existe.");
    }

    var nuevoUsuario = new Usuario
    {
        Email = usuarioLogin.Email,
        Password = usuarioLogin.Password,
        FechaCreacion = DateTime.Now // Establecer fecha de creación
    };

    db.Usuarios.Add(nuevoUsuario);
    await db.SaveChangesAsync();

    return Results.Ok("Usuario registrado con éxito.");
});

// Endpoints for Favorites
app.MapGet("/api/favorites", async (ApplicationDbContext db) =>
    await db.Favorites.ToListAsync());

app.MapPost("/api/favorites", async (ApplicationDbContext db, Favorite favorite) =>
{
    if (favorite.UserId == 0)
    {
        return Results.BadRequest("UserId is required.");
    }

    db.Favorites.Add(favorite);
    await db.SaveChangesAsync();
    return Results.Created($"/api/favorites/{favorite.Id}", favorite);
});

app.MapDelete("/api/favorites/{id}", async (ApplicationDbContext db, int id) =>
{
    var favorite = await db.Favorites.FindAsync(id);
    if (favorite == null)
    {
        return Results.NotFound();
    }

    db.Favorites.Remove(favorite);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
