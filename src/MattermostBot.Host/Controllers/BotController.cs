using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MattermostBot.ArtifactoryClient;
using MattermostBot.Host.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MattermostBot.Host.Controllers
{
    [Route("[controller]/[action]")]
    public class BotController : Controller
    {
        private readonly IConfiguration _configuration;

        public BotController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<HookResponse> Hook([FromBody] HookRequest request)
        {
            var words = request.Text.Trim().Split(' ');

            if (words.Length != 2)
            {
                return new HookResponse
                {
                    Text = $"You must specify packages version. Example: \"{request.TriggerWord} 2.6.4\""
                };
            }

            var packageVersion = words.Last();

            var options = _configuration.GetSection("artifactory").Get<ArtifactoryOptions>();

            var artifactory = new Artifactory(options.Host, options.User, options.Password);

            var searchQueryBuilds = options.Packages.Select(p => 
            {
                Action<ISearchQueryBuilder> action = (ISearchQueryBuilder q) => 
                    q.WhereName().Match($"{p}*{packageVersion}*");

                return action;
            }).ToArray();

            var artifactsList = await artifactory.Search()
                .Or(searchQueryBuilds)
                .Build()
                .Execute();

            var artifacts = artifactsList.Results.Select(a => 
                new ArtifactModel(a, options.PackageNamePattern, ":white_check_mark:"))
                .GroupBy(a => a.Name)
                .Select(gr => gr.OrderByDescending(it => it.Version).First())
                .ToList();

            foreach (var notFoundPackage in options.Packages.Where(p => !artifacts.Any(a => a.Name == p)))
            {
                artifacts.Add(new ArtifactModel(notFoundPackage, ":x:"));
            }     

            return new HookResponse
            {
                Text = GetTable(artifacts)
            };
        }

        private string GetTable(List<ArtifactModel> artifacts)
        {          
            var tableBuilder = new StringBuilder();

            tableBuilder.AppendLine("| Name | Version | State | Updated |");
            tableBuilder.AppendLine("| --- | --- | --- | --- |");

            foreach (var artifact in artifacts)
            {
                tableBuilder.AppendLine($"| {artifact.Name} |" + 
                $" {artifact.Version} |" + 
                $" {artifact.Label} | {artifact?.Updated.ToString() ?? string.Empty} |");
            }

            return tableBuilder.ToString();
        }

        internal class ArtifactModel
        {
            public ArtifactModel(Artifact artifact, string packageNamePattern, string label)
            {
                if (artifact == null)
                {
                    throw new ArgumentNullException(nameof(artifact));
                }

                var versionMatches = Regex.Match(artifact.Name, packageNamePattern);

                Name = versionMatches.Groups["package"].Value;

                Version = new Version(versionMatches.Groups["version"].Value);

                Label = label;

                Updated = artifact.Updated;
            }


            public ArtifactModel(string packageName, string label)
            {
                Name = packageName;

                Label = label;
            }

            public Version Version { get; }

            public string Name { get; }
            
            public string Label { get; }

            public DateTimeOffset? Updated { get; }
        }

        public class ArtifactoryOptions
        {
            public string Host { get; set; }
            public string User { get; set; }
            public string Password { get; set; }            
            public string PackageNamePattern { get; set; }
            public List<string> Packages { get; set; } = new List<string>();
        }
    }
}