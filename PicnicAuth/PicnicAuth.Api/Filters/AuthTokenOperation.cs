using System.Collections.Generic;
using System.Net;
using System.Web.Http.Description;
using PicnicAuth.Database.Models.Authentication;
using Swashbuckle.Swagger;

namespace PicnicAuth.Api.Filters
{
    internal class AuthTokenOperation : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            swaggerDoc.paths.Add(Properties.Resources.AuthTokenRoute, new PathItem
            {
                post = new Operation
                {
                    tags = new List<string> {Properties.Resources.TokenEndpointName},
                    consumes = new List<string>
                    {
                        Properties.Resources.UrlEncodedContentType
                    },
                    summary = Properties.Resources.TokenEndpointDescription,
                    responses = new Dictionary<string, Response>
                    {
                        {
                            ((int) HttpStatusCode.OK).ToString(),
                            new Response
                            {
                                description = Properties.Resources.TokenGetString,
                                schema = schemaRegistry.GetOrRegister(typeof(TokenResponse))
                            }
                        },
                        {
                            ((int) HttpStatusCode.BadRequest).ToString(),
                            new Response
                            {
                                description = Properties.Resources.InvalidRequestString,
                                schema = schemaRegistry.GetOrRegister(typeof(TokenErrorResponse))
                            }
                        }
                    },
                    parameters = new List<Parameter>
                    {
                        new Parameter
                        {
                            type = typeof(string).Name.ToLower(),
                            name = Properties.Resources.GrantTypeName,
                            required = true,
                            @in = Properties.Resources.FormDataParameterTypeName,
                            description = Properties.Resources.GrantTypeParameterDescription
                        },
                        new Parameter
                        {
                            type = typeof(string).Name.ToLower(),
                            name = Properties.Resources.UsernameParameterName,
                            required = false,
                            @in = Properties.Resources.FormDataParameterTypeName
                        },
                        new Parameter
                        {
                            type = typeof(string).Name.ToLower(),
                            name = Properties.Resources.PasswordParameterName,
                            required = false,
                            @in = Properties.Resources.FormDataParameterTypeName
                        }
                    }
                }
            });
        }
    }
}