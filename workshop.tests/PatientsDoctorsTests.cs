using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace workshop.tests;

public class Tests
{

    [Test]
    public async Task PatientEndpointStatusOk()
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/surgery/patients");

        // Assert
        Assert.That(response.StatusCode == System.Net.HttpStatusCode.OK);
    }

    [Test]
    public async Task DoctorEndpointStatusOk()
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/surgery/doctors");

        // Assert
        Assert.That(response.StatusCode == System.Net.HttpStatusCode.OK);
    }

    [Test]
    public async Task PatientIdEndPointStatusOk()
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        var client = factory.CreateClient();

        // Act
        int id = 1;
        string requestUrl = $"/surgery/appointmentsbypatient/{id}";
        var response = await client.GetAsync(requestUrl);

        // Assert
        Assert.That(response.StatusCode == System.Net.HttpStatusCode.OK);
    }

    [Test]
    public async Task PatientIdEndPointStatusNotFound()
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        var client = factory.CreateClient();

        // Act
        int id = 100000;
        string requestUrl = $"/surgery/appointmentsbypatient/{id}";
        var response = await client.GetAsync(requestUrl);

        // Assert
        Assert.That(response.StatusCode == System.Net.HttpStatusCode.NotFound);
    }

    [Test]
    public async Task DoctorIdEndPointStatusOk()
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        var client = factory.CreateClient();

        // Act
        int id = 1;
        string requestUrl = $"/surgery/appointmentsbydoctor/{id}";
        var response = await client.GetAsync(requestUrl);

        // Assert
        Assert.That(response.StatusCode == System.Net.HttpStatusCode.OK);
    }

    [Test]
    public async Task DoctorIdEndPointStatusNotFound()
    {
        // Arrange
        var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder => { });
        var client = factory.CreateClient();

        // Act
        int id = 100000;
        string requestUrl = $"/surgery/appointmentsbydoctor/{id}";
        var response = await client.GetAsync(requestUrl);

        // Assert
        Assert.That(response.StatusCode == System.Net.HttpStatusCode.NotFound);
    }
}