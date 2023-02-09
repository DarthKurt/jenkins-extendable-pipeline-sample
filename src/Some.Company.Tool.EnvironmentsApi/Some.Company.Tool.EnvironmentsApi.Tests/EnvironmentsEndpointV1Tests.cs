using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Some.Company.Tool.EnvironmentsApi.Tests;

[TestFixture]
[FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
internal sealed class EnvironmentsEndpointV1Tests
{
    private readonly TestWebApplicationFactory<Program> _factory;
    private readonly HttpClient _httpClient;

    public EnvironmentsEndpointV1Tests()
    {
        _factory = new TestWebApplicationFactory<Program>();
        _httpClient = _factory.CreateClient();
    }

    private static IEnumerable<object[]> InvalidObjects => new List<object[]>
    {
        new object[] { new Environment { Title = string.Empty, Description = "Test description" }, "Name is empty" },
        new object[] { new Environment { Title = "P", Description = "Test description" }, "Name length < 3" }
    };

    [TestCaseSource(nameof(InvalidObjects))]
    public async Task Post_should_return_validation_problems_for_invalid_objects(
        Environment environment, string errorMessage)
    {
        var response = await _httpClient.PostAsJsonAsync("/v1/environment", environment);
        var problemResult = await response.Content.ReadFromJsonAsync<HttpValidationProblemDetails>();

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        problemResult?.Errors.Should().NotBeNull();
        problemResult?.Errors.Should().ContainSingle(errorMessage);
    }
    
    [Test]
    public async Task Post_should_return_Created_result()
    {
        var post = new Environment
        {
            Title = "Test title",
            Description = "Test description"
        };
        
        var expected = post with { Id = 1 };
        
        var response = await _httpClient.PostAsJsonAsync("/v1/environment", post);
    
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    
        var environments = await _httpClient.GetFromJsonAsync<List<Environment>>("/v1/environment");
    
        environments.Should().ContainSingle();
        environments.Should().Contain(expected);
    }
}