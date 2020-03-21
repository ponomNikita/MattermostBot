using System;
using System.Net;

namespace MattermostBot.ArtifactoryClient
{
    public class ArtifactoryClientException : Exception
    {
        public ArtifactoryClientException(string message, Exception ex) 
            : base(message, ex)
        {

        }
        public ArtifactoryClientException(string message, HttpStatusCode statusCode) 
            : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; }
    }
}
