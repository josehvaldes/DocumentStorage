using DocuStorage.Common.Data.Services;
using DocuStorage.Data.Dapper.Services;
using DocuStorage.DataContent.S3;
using DocuStorage.DAzure.DStorage;
using DocuStorageAD.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();

var services = builder.Services;

services.AddScoped<IDocumentService, DocumentService>();
services.AddScoped<IGroupService, GroupService>();

services.AddScoped<IUserDataService, UserDataDpService>();
services.AddScoped<IGroupDataService, GroupDataDpService>();
services.AddScoped<IDocumentDataService, DocumentDataDpService>();
services.AddScoped<IDocumentContentService, DocumentContentDpService>();
services.AddScoped<ISqlDataProvider, SqlDapperProvider>();
services.AddScoped<IBackup, ContainerBackup>();
services.AddScoped<IMirror<DocumentEntity>, DocumentTableMirror>();

services.AddSingleton<IS3Cache, RedisCache>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

//Use this for docker deployment
app.Run();

//Use this for VS debugging
//app.Run("http://localhost:4001");
