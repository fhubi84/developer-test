using FluentAssertions;
using Moq;
using Taxually.TechnicalTest.Core.Commands.Requests;
using Taxually.TechnicalTest.Core.Handlers;
using Taxually.TechnicalTest.Core.Interfaces;
using Taxually.TechnicalTest.Core.Processors;

namespace Taxually.TechnicalTest.Tests
{
    public class VatRegistrationHandlerTest
    {
        private readonly VatRegistrationHandler? handler = null;

        public VatRegistrationHandlerTest()
        {
            var httpClient = new Mock<IHttpClient>();
            httpClient.Setup(c => c.PostAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));

            var queueClient = new Mock<IQueueClient>();
            queueClient.Setup(c => c.EnqueueAsync(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(true));

            var vatRequestProcessorFactory = new Mock<IVatRequestProcessorFactory>();
            vatRequestProcessorFactory.Setup(f => f.GetVatRequestProcessor("GB")).Returns(new BritishVatRequestProcessor(httpClient.Object));
            vatRequestProcessorFactory.Setup(f => f.GetVatRequestProcessor("FR")).Returns(new FrenchVatRequestProcessor(queueClient.Object));
            vatRequestProcessorFactory.Setup(f => f.GetVatRequestProcessor("DE")).Returns(new GermanVatRequestProcessor(queueClient.Object));

            handler = new VatRegistrationHandler(vatRequestProcessorFactory.Object);
        }

        [Theory]
        [InlineData("GB")]
        [InlineData("FR")]
        [InlineData("DE")]
        public async Task Given_Valid_Country_Code_Should_Return_True(string country)
        {
           var result = await handler!.Handle(new VatRegistrationRequest() 
           {
               CompanyId = "1",
               CompanyName = "A",
               Country = country 
           });

           result.Success.Should().Be(true);
        }

        [Theory]
        [InlineData("XX")]
        public async Task Given_Invalid_Country_Code_Should_Return_False(string country)
        {
            var result = await handler!.Handle(new VatRegistrationRequest()
            {
                CompanyId = "1",
                CompanyName = "A",
                Country = country
            });

            result.Success.Should().Be(false);
        }
    }
}