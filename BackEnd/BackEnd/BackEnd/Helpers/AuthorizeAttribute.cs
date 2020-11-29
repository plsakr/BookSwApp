using System;
using BackEnd.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BackEnd.Helpers
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeUserAttribute: Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var email = (string) context.HttpContext.Items["Email"];
            var role = (string) context.HttpContext.Items["Role"];
            
            if (email == null || role == "LIBRARIAN")
                context.Result = new JsonResult(new {message = "Unauthorized"}) {StatusCode = StatusCodes.Status401Unauthorized};
        }
    }
    
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeLibrarianAttribute: Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var email = (string) context.HttpContext.Items["User"];
            var role = (string) context.HttpContext.Items["Role"];
            
            if (email == null || role == "USER")
                context.Result = new JsonResult(new {message = "Unauthorized"}) {StatusCode = StatusCodes.Status401Unauthorized};

        }
    }
}