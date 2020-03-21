using System.Text.RegularExpressions;
using Xunit;

namespace MattermostBot.ArtifactoryClient.Tests
{
    public class ArtifactoryTests
    {
        private readonly IArtifactory _artifactory = new Artifactory("", "", "");

        [Fact]
        public void Build_valid_body_request_when_search_by_name_equality()
        {
            var result = _artifactory.Search()
                .WhereName().Equal("expected")
                .Build();

            Assert.Equal("items.find({ \"name\": { \"$eq\": \"expected\" } })", result.Body);
        }

        [Fact]
        public void Build_valid_body_request_when_search_by_name_not_equality()
        {
            var result = _artifactory.Search()
                .WhereName().NotEqual("expected")
                .Build();

            Assert.Equal("items.find({ \"name\": { \"$ne\": \"expected\" } })", result.Body);
        }

        [Fact]
        public void Build_valid_body_request_when_search_by_name_matching()
        {
            var result = _artifactory.Search()
                .WhereName().Match("expected.*")
                .Build();

            Assert.Equal("items.find({ \"name\": { \"$match\": \"expected.*\" } })", result.Body);
        }

        [Fact]
        public void Build_valid_body_request_when_search_by_repository_equality()
        {
            var result = _artifactory.Search()
                .WhereRepository().Equal("expected")
                .Build();

            Assert.Equal("items.find({ \"repo\": { \"$eq\": \"expected\" } })", result.Body);
        }

        [Fact]
        public void Build_valid_body_request_when_search_by_repository_not_equality()
        {
            var result = _artifactory.Search()
                .WhereRepository().NotEqual("expected")
                .Build();

            Assert.Equal("items.find({ \"repo\": { \"$ne\": \"expected\" } })", result.Body);
        }

        [Fact]
        public void Build_valid_body_request_when_search_by_repository_matching()
        {
            var result = _artifactory.Search()
                .WhereRepository().Match("expected.*")
                .Build();

            Assert.Equal("items.find({ \"repo\": { \"$match\": \"expected.*\" } })", result.Body);
        }

        [Fact]
        public void Build_valid_body_request_when_search_by_name_and_repository()
        {
            var result = _artifactory.Search()
                .WhereName().Match("expected-name.*")
                .WhereRepository().Match("expected-repo.*")
                .Build();

            var expectedResult = "items.find({ \"name\": { \"$match\": \"expected-name.*\" } }, " + 
            "{ \"repo\": { \"$match\": \"expected-repo.*\" } })";

            Assert.Equal(expectedResult, result.Body);
        }

        [Fact]
        public void Build_valid_body_request_when_search_by_name_or_repository()
        {
            var result = _artifactory.Search()
                .Or(q => q.WhereName().Match("expected-name.*"), 
                    q => q.WhereRepository().Match("expected-repo.*"))               
                .Build();

            var expectedResult = "items.find({ \"$or\": [ { \"name\": { \"$match\": \"expected-name.*\" } }, " + 
            "{ \"repo\": { \"$match\": \"expected-repo.*\" } } ] })";

            Assert.Equal(expectedResult, result.Body);
        }

        [Fact]
        public void Test()
        {
            var packagePattern = @"(?<package>.*)-(?<version>\d+.\d+.\d+).zip";

            var match = Regex.Match("packageName-2.6.4.zip", packagePattern);

            Assert.Equal("packageName", match.Groups["package"].Value);  
            Assert.Equal("2.6.4", match.Groups["version"].Value);        
        }
    }
}
