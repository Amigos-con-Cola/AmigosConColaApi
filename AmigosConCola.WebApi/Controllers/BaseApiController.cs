using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace AmigosConCola.WebApi.Controllers;

public class BaseApiController : ControllerBase
{
    [NonAction]
    public IActionResult ValidationErrors(List<Error> errors)
    {
        foreach (var error in errors)
            ModelState.AddModelError(error.Code, error.Description);
        return ValidationProblem(ModelState);
    }
}