using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

[ApiController]
public class BaseController : ControllerBase
{
    private static readonly JsonSerializerOptions _jsonOptions =
        new JsonSerializerOptions
        {
            PropertyNamingPolicy = null
        };

    protected IActionResult ApiSuccess<T>(T data, string message)
    {
        var response = new PayrollApis.Models.Common.ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };

        return new JsonResult(response, _jsonOptions);
    }

    protected IActionResult ApiError(string message)
    {
        var response = new PayrollApis.Models.Common.ApiResponse<object>
        {
            Success = false,
            Message = message,
            Data = null
        };

        return new JsonResult(response, _jsonOptions);
    }
}