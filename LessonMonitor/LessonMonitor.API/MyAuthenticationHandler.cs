﻿using LessonMonitor.API.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace LessonMonitor.API
{
    public class MyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public MyAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var random = new Random();

            var bodyStream = Context.Request.Body;
            var userCredentials = await JsonSerializer.DeserializeAsync<UserCredentials>(bodyStream);

            var isSuccesss = random.Next(0, 2) == 1;

            if (isSuccesss)
            {
                var claimsPrincipal = new ClaimsPrincipal();

                var claims = new Claim[]
                {
                    new Claim("userId", "12345"),
                    new Claim(ClaimTypes.Name, "Test")
                };

                claimsPrincipal.AddIdentity(new ClaimsIdentity(claims, "Custom"));

                Context.User = claimsPrincipal;

                return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, AuthenticationSchema.DEFAULT_SCHEMA));
            }
            else
            {
                return AuthenticateResult.Fail(new Exception("Authentication Failed"));
            }
        }
    }
}
