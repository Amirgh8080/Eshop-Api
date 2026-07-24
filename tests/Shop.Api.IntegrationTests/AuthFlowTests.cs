using System.Net.Http.Json;
using Common.AspNetCore;
using Shop.Api.ViewModels.Auth;
using Xunit;

namespace Shop.Api.IntegrationTests;

public class AuthFlowTests : IClassFixture<ApiFactory>
{
    private readonly HttpClient _client;

    public AuthFlowTests(ApiFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Register_then_login_issues_a_jwt_token()
    {
        var phoneNumber = "09" + Random.Shared.Next(100_000_000, 999_999_999);

        var registerResponse = await _client.PostAsJsonAsync("/api/Auth/register", new
        {
            phoneNumber,
            password = "Password123",
            confirmPassword = "Password123"
        });
        registerResponse.EnsureSuccessStatusCode();
        var registerResult = await registerResponse.Content.ReadFromJsonAsync<ApiResult>();
        Assert.NotNull(registerResult);
        Assert.True(registerResult!.IsSuccess);

        var loginResponse = await _client.PostAsJsonAsync("/api/Auth/login", new
        {
            phoneNumber,
            password = "Password123"
        });
        loginResponse.EnsureSuccessStatusCode();
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<ApiResult<LoginResultDto>>();

        Assert.NotNull(loginResult);
        Assert.True(loginResult!.IsSuccess);
        Assert.False(string.IsNullOrWhiteSpace(loginResult.Data!.Token));
        Assert.False(string.IsNullOrWhiteSpace(loginResult.Data.RefreshToken));
    }

    [Fact]
    public async Task Login_with_wrong_password_fails()
    {
        var phoneNumber = "09" + Random.Shared.Next(100_000_000, 999_999_999);

        var registerResponse = await _client.PostAsJsonAsync("/api/Auth/register", new
        {
            phoneNumber,
            password = "Password123",
            confirmPassword = "Password123"
        });
        registerResponse.EnsureSuccessStatusCode();

        var loginResponse = await _client.PostAsJsonAsync("/api/Auth/login", new
        {
            phoneNumber,
            password = "WrongPassword123"
        });
        loginResponse.EnsureSuccessStatusCode();
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<ApiResult<LoginResultDto>>();

        Assert.NotNull(loginResult);
        Assert.False(loginResult!.IsSuccess);
    }
}
