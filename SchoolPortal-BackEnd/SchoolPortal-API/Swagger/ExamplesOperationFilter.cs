using System;
using System.Collections.Generic;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SchoolPortal_API.Swagger
{
    public class ExamplesOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var path = context.ApiDescription.RelativePath ?? string.Empty; // e.g. api/Class

            // Request examples
            if (operation.RequestBody != null && operation.RequestBody.Content.TryGetValue("application/json", out var mediaType))
            {
                if (path.StartsWith("api/Class", StringComparison.OrdinalIgnoreCase))
                {
                    mediaType.Example = BuildClassDtoExample();
                }
                else if (path.StartsWith("api/Section", StringComparison.OrdinalIgnoreCase))
                {
                    mediaType.Example = BuildSectionDtoExample();
                }
                else if (path.StartsWith("api/ClassSection", StringComparison.OrdinalIgnoreCase))
                {
                    mediaType.Example = BuildClassSectionDtoExample();
                }
            }

            // Response examples (200/201)
            foreach (var kvp in operation.Responses)
            {
                if (kvp.Value.Content.TryGetValue("application/json", out var respMedia))
                {
                    if (path.StartsWith("api/ClassSection", StringComparison.OrdinalIgnoreCase))
                    {
                        respMedia.Example ??= BuildClassSectionResponseExample();
                    }
                    else if (path.StartsWith("api/Class", StringComparison.OrdinalIgnoreCase))
                    {
                        respMedia.Example ??= BuildClassResponseExample();
                    }
                    else if (path.StartsWith("api/Section", StringComparison.OrdinalIgnoreCase))
                    {
                        respMedia.Example ??= BuildSectionResponseExample();
                    }
                }
            }
        }

        private static IOpenApiAny BuildClassDtoExample()
        {
            return new OpenApiObject
            {
                ["name"] = new OpenApiString("Grade 1"),
                ["examAssessment"] = new OpenApiString("Annual"),
                ["orderBy"] = new OpenApiInteger(1),
                ["companyId"] = new OpenApiString(Guid.Empty.ToString()),
                ["schoolId"] = new OpenApiString(Guid.Empty.ToString()),
                ["isActive"] = new OpenApiBoolean(true)
            };
        }

        private static IOpenApiAny BuildClassResponseExample()
        {
            return new OpenApiObject
            {
                ["id"] = new OpenApiString(Guid.NewGuid().ToString()),
                ["name"] = new OpenApiString("Grade 1"),
                ["examAssessment"] = new OpenApiString("Annual"),
                ["orderBy"] = new OpenApiInteger(1),
                ["companyId"] = new OpenApiString(Guid.NewGuid().ToString()),
                ["schoolId"] = new OpenApiString(Guid.NewGuid().ToString()),
                ["isActive"] = new OpenApiBoolean(true)
            };
        }

        private static IOpenApiAny BuildSectionDtoExample()
        {
            return new OpenApiObject
            {
                ["name"] = new OpenApiString("A"),
                ["companyId"] = new OpenApiString(Guid.Empty.ToString()),
                ["schoolId"] = new OpenApiString(Guid.Empty.ToString()),
                ["isActive"] = new OpenApiBoolean(true)
            };
        }

        private static IOpenApiAny BuildSectionResponseExample()
        {
            return new OpenApiObject
            {
                ["id"] = new OpenApiString(Guid.NewGuid().ToString()),
                ["name"] = new OpenApiString("A"),
                ["companyId"] = new OpenApiString(Guid.NewGuid().ToString()),
                ["schoolId"] = new OpenApiString(Guid.NewGuid().ToString()),
                ["isActive"] = new OpenApiBoolean(true)
            };
        }

        private static IOpenApiAny BuildClassSectionDtoExample()
        {
            return new OpenApiObject
            {
                ["classMasterId"] = new OpenApiString(Guid.Empty.ToString()),
                ["sectionMasterId"] = new OpenApiString(Guid.Empty.ToString()),
                ["locationId"] = new OpenApiString(Guid.Empty.ToString()),
                ["companyId"] = new OpenApiString(Guid.Empty.ToString()),
                ["schoolId"] = new OpenApiString(Guid.Empty.ToString()),
                ["isActive"] = new OpenApiBoolean(true)
            };
        }

        private static IOpenApiAny BuildClassSectionResponseExample()
        {
            return new OpenApiObject
            {
                ["id"] = new OpenApiString(Guid.NewGuid().ToString()),
                ["classMasterId"] = new OpenApiString(Guid.NewGuid().ToString()),
                ["className"] = new OpenApiString("Grade 1"),
                ["sectionMasterId"] = new OpenApiString(Guid.NewGuid().ToString()),
                ["sectionName"] = new OpenApiString("A"),
                ["locationId"] = new OpenApiString(Guid.NewGuid().ToString()),
                ["companyId"] = new OpenApiString(Guid.NewGuid().ToString()),
                ["schoolId"] = new OpenApiString(Guid.NewGuid().ToString()),
                ["isActive"] = new OpenApiBoolean(true)
            };
        }
    }
}
