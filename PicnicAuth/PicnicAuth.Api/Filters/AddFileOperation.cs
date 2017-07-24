using System.Web.Http.Description;
using Swashbuckle.Swagger;

namespace PicnicAuth.Api.Filters
{
    internal class AddFileOperation : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.operationId != Properties.Resources.ImagePostOperationId) return;

            operation.consumes.Add(Properties.Resources.FormDataMimeType);
            operation.parameters = new[]
            {
                new Parameter
                {
                    name = Properties.Resources.ImageParameterName,
                    description = Properties.Resources.ImageParameterDescription,
                    @in = Properties.Resources.ImageParameterType,
                    required = true,
                    type = Properties.Resources.ImageDataTypeName
                }
            };
        }
    }
}