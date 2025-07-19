﻿using System.Net.Http.Headers;

namespace SEG.Api.Seguridad.Middlewares
{
    public class MiddlewareManejadorTokens : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MiddlewareManejadorTokens(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer", ""));
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
