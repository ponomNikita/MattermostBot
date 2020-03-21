using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MattermostBot.ArtifactoryClient
{
    public class Artifactory : IArtifactory
    {
        private string _host;
        private string _user;
        private string _password;

        public Artifactory(string host, string user, string password)
        {
            _host = host;
            _user = user;
            _password = password;
        }

        public async Task<ArtifactsList> Execute(ISearchQuery query)
        {
            using (var handler = new HttpClientHandler 
            { 
                Credentials = new NetworkCredential(_user, _password)
            })
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(_host);

                var content = new StringContent(query.Body);

                try
                {
                    var response = await client.PostAsync("/artifactory/api/search/aql", content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    
                    if (response.IsSuccessStatusCode) 
                    {
                        try
                        {                           
                            var result = JsonConvert.DeserializeObject<ArtifactsList>(responseContent);

                            return result;
                        }
                        catch (Exception e)
                        {
                            throw new ArtifactoryClientException("Can't deserialize response content", e);
                        }
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        throw new ArtifactoryClientException($"BadRequest: {responseContent}", response.StatusCode);
                    }
                    else if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        throw new ArtifactoryClientException($"Unauthorized: {responseContent}", response.StatusCode);
                    }
                    else
                    {
                        throw new ArtifactoryClientException(responseContent, response.StatusCode);
                    }
                }
                catch (Exception e)
                {
                    throw new ArtifactoryClientException("Enable to process request", e);
                }
            }
        }

        public ISearchQueryBuilder Search()
        {
            return new SearchQueryBuilder(this);
        }
    }
}
