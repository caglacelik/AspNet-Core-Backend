﻿using Core;
using Core.DTOs;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var Id = context.ActionArguments.Values.FirstOrDefault();
            if (Id == null)
            {
                await next();
                return;
            }
            var id = (int)Id;
            var anyEntity = await _service.AnyAsync(x=> x.Id == id);

            if (anyEntity)
            {
                await next.Invoke();
                return;
            }

            context.Result= new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"{typeof(T).Name} not found"));
        }
    }
}
