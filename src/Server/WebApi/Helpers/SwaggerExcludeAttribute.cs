namespace WebApi.Helpers
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class SwaggerExcludeAttribute : Attribute { }
}
