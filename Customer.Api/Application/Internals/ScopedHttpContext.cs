// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using Microsoft.AspNetCore.Http;

namespace GoLogs.Services.Customer.Api.Application.Internals
{
    public class ScopedHttpContext
    {
        public ScopedHttpContext(IHttpContextAccessor httpContextAccessor)
        {
            Accessor = httpContextAccessor;
        }

        public IHttpContextAccessor Accessor { get; }
    }
}
