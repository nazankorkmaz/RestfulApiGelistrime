namespace RestfulCohort.Api;

public class Startup{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();

        services.AddScoped<IBookService, FakeBookService>(); // dependency ınjection

    }

    public void Configure(IApplicationBuilder app,IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();

        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        app.UseMiddleware<LoggingMiddleware>(); // Global Logging Middleware

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
    
}