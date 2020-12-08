using Microsoft.AspNetCore.Http;

namespace GoLogs.Services.Customer.Api.Application.Internals
{
    public class ScopedHttpContext
    {
        public IHttpContextAccessor Accessor { get; }

        public ScopedHttpContext(IHttpContextAccessor httpContextAccessor)
        {
            Accessor = httpContextAccessor;
        }
    }
}
