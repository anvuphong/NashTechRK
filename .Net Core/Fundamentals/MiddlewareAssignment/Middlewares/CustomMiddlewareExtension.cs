namespace MiddlewareAssignment.Middlewares
{
    public static class CustomMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder){
            return builder.UseMiddleware<LoginMiddleware>();
        }
    }
}