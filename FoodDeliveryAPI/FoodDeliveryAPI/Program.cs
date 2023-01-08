
using AutoMapper;
using FoodDeliveryAPI.Common;
using FoodDeliveryAPI.Infrastructure;
using FoodDeliveryAPI.Interfaces;
using FoodDeliveryAPI.Mapping;
using FoodDeliveryAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Nominatim.API.Geocoders;
using Nominatim.API.Interfaces;
using Nominatim.API.Web;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "FoodDeliveryAPI", Version = "v1" });
        //Ovo dodajemo kako bi mogli da unesemo token u swagger prilikom testiranja
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type=ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                }
                            },
                            new string[]{}
                        }
                    });
    });


//Dodajemo semu autentifikacije i podesavamo da se radi o JWT beareru
builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
   options.TokenValidationParameters = new TokenValidationParameters //Podesavamo parametre za validaciju pristiglih tokena
   {
       ValidateIssuer = true, //Validira izdavaoca tokena
       ValidateAudience = false, //Kazemo da ne validira primaoce tokena
       ValidateLifetime = true,//Validira trajanje tokena
       ValidateIssuerSigningKey = true, //validira potpis token, ovo je jako vazno!
       ValidIssuer = "http://localhost:44309", //odredjujemo koji server je validni izdavalac
       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKey"]))//navodimo privatni kljuc kojim su potpisani nasi tokeni
   };
});


builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IVerificationRequestService, VerificationRequestService>();
builder.Services.AddScoped<IRegistrationRequestService, RegistrationService>();

//registracija db contexta u kontejneru zavisnosti, njegov zivotni vek je Scoped
builder.Services.AddDbContext<FoodDeliveryDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FoodDeliveryDbContext")));

//Registracija mapera u kontejneru, zivotni vek singleton
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();

IEmailSender emailSender = new EmailSender(configuration);


builder.Services.AddSingleton(mapper);
builder.Services.AddSingleton(emailSender);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "ProductOrigins", builder => {
        builder.WithOrigins("http://localhost:4200")//Ovde navodimo koje sve aplikacije smeju kontaktirati nasu,u ovom slucaju nas Angular front
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("ProductOrigins");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
