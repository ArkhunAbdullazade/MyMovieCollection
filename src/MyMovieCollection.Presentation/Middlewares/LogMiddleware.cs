using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http.Extensions;
using MyMovieCollection.Core.Models;
using MyMovieCollection.Core.Repositories;

namespace MyMovieCollection.Presentation
.Middlewares;
public class LogMiddleware : IMiddleware
{
    private readonly IDataProtector dataProtector;
    private readonly ILogRepository repository;
    private bool IsLoggingEnabled { get; set; }
    public LogMiddleware(IDataProtectionProvider dataProtectionProvider, IConfiguration configuration, ILogRepository repository)
    {
        this.dataProtector = dataProtectionProvider.CreateProtector("MyMovieCollectionPurpose");
        this.IsLoggingEnabled = configuration.GetSection("IsLoggingEnabled").Get<bool>();
        this.repository = repository;
    }
    
    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {        
        if (IsLoggingEnabled) 
        {
            #pragma warning disable CS8604 // Possible null reference argument.
            int? userId = httpContext.Request.Cookies["Authorize"] is null ? null : Convert.ToInt16(dataProtector.Unprotect(httpContext.Request.Cookies["Authorize"]));
            #pragma warning restore CS8604 // Possible null reference argument.

            var url = httpContext.Request.GetDisplayUrl();

            var methodType = httpContext.Request.Method;

            var statusCode = httpContext.Response.StatusCode;

            //Request Body
            var request = httpContext.Request;
            string? requestContent = null;

            if (request.Method == HttpMethods.Post && request.ContentLength > 0)
            {
                request.EnableBuffering();

                var buffer = new byte[Convert.ToInt32(request.ContentLength)];

                await request.Body.ReadAsync(buffer, 0, buffer.Length);

                requestContent = Encoding.UTF8.GetString(buffer);

                request.Body.Position = 0;
            }

            //Response Body
            var originalBodyStream = httpContext.Response.Body;
            string? responseContent = null;

            using (var memoryStream = new MemoryStream())
            {
                httpContext.Response.Body = memoryStream;

                await next(httpContext);

                httpContext.Response.Body = originalBodyStream;

                memoryStream.Seek(0, SeekOrigin.Begin);

                responseContent = await new StreamReader(memoryStream).ReadToEndAsync();
            }
            
            var newLog = new Log 
            {
                UserId = userId,
                Url = url,
                MethodType = methodType,
                StatusCode = statusCode,
                RequestBody = requestContent,
                ResponseBody = responseContent,
            };

            await this.repository.CreateAsync(newLog);
        }
        
        await next.Invoke(httpContext);
    }
}