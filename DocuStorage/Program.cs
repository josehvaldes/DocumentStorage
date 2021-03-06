using DocuStorage.Helpers;
using DocuStorage.Services;
using FluentValidation.AspNetCore;
using System.Reflection;
using DocuStorage.Common.Data.Services;
using DocuStorage.Data.Dapper.Services;
using DocuStorage.DataContent.S3;
using DocuStorage.DAzure.DStorage;

var builder = WebApplication.CreateBuilder(args);

{
    var services = builder.Services;
    services.AddCors();
    services.AddControllers()
                .AddFluentValidation(options =>
                {
                    // Validate child properties and root collection elements
                    options.ImplicitlyValidateChildProperties = true;
                    options.ImplicitlyValidateRootCollectionElements = true;
                    // Automatic registration of validators in assembly
                    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });

    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
    

    services.AddScoped<IUserService, UserService>();
    services.AddScoped<IDocumentService, DocumentService>();
    services.AddScoped<IGroupService, GroupService>();

    services.AddScoped<IUserDataService, UserDataDpService>();
    services.AddScoped<IGroupDataService, GroupDataDpService>();
    services.AddScoped<IDocumentDataService, DocumentDataDpService>();
    services.AddScoped<IDocumentContentService, DocumentContentDpService>();
    services.AddScoped<ISqlDataProvider, SqlDapperProvider>();
    services.AddScoped<IBackup, ContainerBackup>();
    services.AddScoped<IMirror<DocumentEntity>, DocumentTableMirror>();
    services.AddScoped<IBackupDocumentsService, BackupDocumentsService>();

    services.AddSingleton<IS3Cache, RedisCache>();
    
}

var app = builder.Build();

{
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
}

//Use this for VS debugging
//app.Run("http://localhost:4000");

//Use this for docker deployment
app.Run();