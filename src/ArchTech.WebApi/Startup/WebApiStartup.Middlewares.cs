using ArchTech.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace ArchTech.WebApi.Startup;

public static partial class WebApiStartup
{
    public static WebApplication UseDefaults(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.UseMiddleware<ExceptionMiddleware>();
        app.MapControllers();
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseSwagger(options => options.SerializeAsV2 = false);
        app.UseSwaggerUI();

        if (!app.Environment.IsDevelopment()) return app;
        
        app.UseCors(options => options
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());
        
        app.UseDeveloperExceptionPage();
        
        return app;
    }
}