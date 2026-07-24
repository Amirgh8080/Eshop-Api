using System.Net.Http.Headers;
using System.Net.Http.Json;
using Common.AspNetCore;
using Shop.Api.ViewModels.Auth;
using Shop.Application.SiteEntities.ShippingMethods.Create;
using Shop.Application.SiteEntities.ShippingMethods.Edit;
using Shop.Query.SiteEntities.DTOs;
using Xunit;

namespace Shop.Api.IntegrationTests;

public class ShippingMethodCrudTests : IClassFixture<ApiFactory>
{
    private readonly ApiFactory _factory;

    public ShippingMethodCrudTests(ApiFactory factory)
    {
        _factory = factory;
    }

    private async Task<HttpClient> CreateAuthenticatedClientAsync()
    {
        var client = _factory.CreateClient();
        var phoneNumber = "09" + Random.Shared.Next(100_000_000, 999_999_999);

        (await client.PostAsJsonAsync("/api/Auth/register", new
        {
            phoneNumber,
            password = "Password123",
            confirmPassword = "Password123"
        })).EnsureSuccessStatusCode();

        var loginResponse = await client.PostAsJsonAsync("/api/Auth/login", new
        {
            phoneNumber,
            password = "Password123"
        });
        loginResponse.EnsureSuccessStatusCode();
        var loginResult = await loginResponse.Content.ReadFromJsonAsync<ApiResult<LoginResultDto>>();

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", loginResult!.Data!.Token);
        return client;
    }

    [Fact]
    public async Task Create_read_update_delete_shipping_method()
    {
        var client = await CreateAuthenticatedClientAsync();
        var title = $"Express-{Guid.NewGuid():N}";

        var createResponse = await client.PostAsJsonAsync("/api/ShippingMethod", new CreateShippingMethodCommand
        {
            Title = title,
            Cost = 100
        });
        createResponse.EnsureSuccessStatusCode();
        var createResult = await createResponse.Content.ReadFromJsonAsync<ApiResult>();
        Assert.True(createResult!.IsSuccess);

        var listResponse = await client.GetFromJsonAsync<ApiResult<List<ShippingMethodDto>>>("/api/ShippingMethod");
        var created = listResponse!.Data!.Single(m => m.Title == title);
        Assert.Equal(100, created.Cost);

        var getByIdResponse = await client.GetFromJsonAsync<ApiResult<ShippingMethodDto>>($"/api/ShippingMethod/{created.Id}");
        Assert.Equal(title, getByIdResponse!.Data!.Title);
        Assert.Equal(100, getByIdResponse.Data.Cost);

        var editResponse = await client.PutAsJsonAsync("/api/ShippingMethod", new EditShippingMethodCommand
        {
            Id = created.Id,
            Title = title,
            Cost = 250
        });
        editResponse.EnsureSuccessStatusCode();
        var editResult = await editResponse.Content.ReadFromJsonAsync<ApiResult>();
        Assert.True(editResult!.IsSuccess);

        var afterEditResponse = await client.GetFromJsonAsync<ApiResult<ShippingMethodDto>>($"/api/ShippingMethod/{created.Id}");
        Assert.Equal(250, afterEditResponse!.Data!.Cost);

        var deleteResponse = await client.DeleteAsync($"/api/ShippingMethod/{created.Id}");
        deleteResponse.EnsureSuccessStatusCode();
        var deleteResult = await deleteResponse.Content.ReadFromJsonAsync<ApiResult>();
        Assert.True(deleteResult!.IsSuccess);

        var afterDeleteResponse = await client.GetFromJsonAsync<ApiResult<ShippingMethodDto?>>($"/api/ShippingMethod/{created.Id}");
        Assert.Null(afterDeleteResponse!.Data);
    }
}
