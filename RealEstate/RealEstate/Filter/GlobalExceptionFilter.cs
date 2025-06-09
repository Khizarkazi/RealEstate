using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RealEstate.Models;

namespace RealEstate.Filter
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var errorModel = new ErrorViewModel
            {
                ExceptionMessage = context.Exception.Message,
                ExceptionType = context.Exception.GetType().Name,
                ExceptionTime = DateTime.Now,
                Path = context.HttpContext.Request.Path
            };

            var result = new ViewResult
            {
                ViewName = "~/Views/Shared/ErrorPage.cshtml",
                ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<ErrorViewModel>(
                    context.HttpContext.RequestServices.GetService(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.IModelMetadataProvider)) as Microsoft.AspNetCore.Mvc.ModelBinding.IModelMetadataProvider,
                    context.ModelState
                )
                {
                    Model = errorModel
                }
            };

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}
