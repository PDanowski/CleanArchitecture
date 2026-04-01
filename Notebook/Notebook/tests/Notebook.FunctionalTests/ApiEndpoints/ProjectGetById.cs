using System.Net;
using Newtonsoft.Json;
using Notebook.Web;
using Notebook.Web.Endpoints.ProjectEndpoints;
using Xunit;

namespace Notebook.FunctionalTests.ApiEndpoints
{
    [Collection("Sequential")]
    public class ProjectGetById : IClassFixture<CustomWebApplicationFactory<WebMarker>>
    {
        private readonly HttpClient _client;

        public ProjectGetById(CustomWebApplicationFactory<WebMarker> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ReturnsSeedProjectGivenId1()
        {
            var response = await _client.GetAsync(GetProjectByIdRequest.BuildRoute(1));
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<GetProjectByIdResponse>(json)!;

            Assert.Equal(1, result.Id);
            Assert.Equal(SeedData.TestProject1.Name, result.Name);
            Assert.Equal(3, result.Items.Count);
        }

        [Fact]
        public async Task ReturnsNotFoundGivenId0()
        {
            string route = GetProjectByIdRequest.BuildRoute(0);
            var response = await _client.GetAsync(route);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
