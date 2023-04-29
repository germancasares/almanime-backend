using Almanime.Models;
using System.Net;

namespace Almanime.Utils;

public class ErrorHandlerMiddleware
{
  private readonly RequestDelegate _next;

  public ErrorHandlerMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task Invoke(HttpContext context)
  {
    try
    {
      await _next(context);
    }
    catch (AlmNullException almException)
    {
      var response = context.Response;
      response.StatusCode = (int)HttpStatusCode.BadRequest;
      await response.WriteAsJsonAsync(new {
        almException.Field,
        almException.Rule,
        almException.Code,
      });
    }
    catch (AlmDbException almException)
    {
      var response = context.Response;
      response.StatusCode = (int)HttpStatusCode.BadRequest;
      await response.WriteAsJsonAsync(new {
        almException.Field,
        almException.Rule,
        almException.Code,
        almException.QueryParams,
      });
    }
    catch (AlmPermissionException almException)
    {
      var response = context.Response;
      response.StatusCode = (int)HttpStatusCode.BadRequest;
      await response.WriteAsJsonAsync(new {
        almException.Field,
        almException.Rule,
        almException.Code,
        almException.Permission,
        almException.UserName,
        almException.FansubName,
      });
    }
    catch (AlmValidationException almException)
    {
      var response = context.Response;
      response.StatusCode = (int)HttpStatusCode.BadRequest;
      await response.WriteAsJsonAsync(new {
        almException.Field,
        almException.Rule,
        almException.Code,
      });
    }
    catch (Exception exception)
    {
      var response = context.Response;
      response.StatusCode = (int)HttpStatusCode.BadRequest;
      await response.WriteAsJsonAsync(new {
        Exception = exception,
      });
    }
  }
}