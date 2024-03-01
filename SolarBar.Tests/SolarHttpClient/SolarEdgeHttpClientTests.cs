using Moq;
using NUnit.Framework;
using SolarBar.HttpClient;
using SolarBar.Config;
using SolarBar.Model.Dto;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SolarBar.Model;
using SolarBar.Extensions;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;

namespace SolarBar.Tests.SolarHttpClient
{
    [TestFixture]
    public class SolarEdgeHttpClientTests
    {
        private Mock<IHttpClientFactory> _mockHttpClientFactory;
        private SolarEdgeHttpClient _client;
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;

        [SetUp]
        public void Setup()
        {
            _mockHttpClientFactory = new Mock<IHttpClientFactory>();
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            var httpClient = new System.Net.Http.HttpClient(_mockHttpMessageHandler.Object);
            _mockHttpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var config = new BarConfig { Host = "http://example.com", ApiKey = "test-api-key" };
            _client = new SolarEdgeHttpClient(config, _mockHttpClientFactory.Object);
        }

        [Test]
        public async Task GetEnergyAsync_ShouldReturnEnergyDto()
        {
            var expectedResponse = new Energy
            {
                TimeUnit = "DAY",
                Unit = "Wh",
                Values = new[]
                {
                        new MeasuredValue { date = new DateTime(2024, 2, 11, 10, 0, 0), value = 1.5f },
                        new MeasuredValue { date = new DateTime(2024, 2, 11, 11, 0, 0), value = 1.7f },
                    }
            };

            var returnedResponse = "{\"energy\":{\"timeUnit\":\"DAY\",\"unit\":\"Wh\",\"values\":[{\"date\":\"2024-02-11 10:00:00\",\"value\":1.5},{\"date\":\"2024-02-11 11:00:00\",\"value\":1.7}]}}";
            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(returnedResponse),
                })
                .Verifiable();


            var result = await _client.GetEnergyAsync("siteId", DateTime.Now, DateTime.Now, TimeUnit.DAY, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.That(result.energy.TimeUnit, Is.EqualTo(expectedResponse.TimeUnit));
            Assert.That(result.energy.Unit, Is.EqualTo(expectedResponse.Unit));
            Assert.That(result.energy.MeasuredBy, Is.EqualTo(expectedResponse.MeasuredBy));
        }
    }
}
