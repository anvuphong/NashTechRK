using System.Diagnostics;

namespace MiddlewareAssignment.Middlewares
{
    public class LoginMiddleware
    {
        private readonly RequestDelegate _next;
        public LoginMiddleware(RequestDelegate next)
        {
            this._next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            using var buffer = new MemoryStream();
            var request = context.Request;
            var response = context.Response;
            var stream = response.Body;
            response.Body = buffer;
            await _next(context);
            Debug.WriteLine("Request content type: {0} {1} {2} {3} {4}", request.Scheme, request.Host, request.QueryString, request.Body, request.Path);
            string outputString = string.Format("Request content type: Scheme {0} Host {1} QueryString {2} Body {3} Path {4}", request.Scheme, request.Host, request.QueryString, request.Body, request.Path);
            using (StreamWriter writer = new StreamWriter("Log.txt", true))
            {
                writer.WriteLine(outputString);
            }
            buffer.Position = 0;
            await buffer.CopyToAsync(stream);
        }
    }
}