using Moq;
using NUnit.Framework;
using System;
using AspNetExample.Shared;

namespace AspNetExample.Service.Tests
{
    [TestFixture]
    public class ModelDescriptionProviderTests
    {
        private Mock<IDateTimeProvider> _dateTimeProviderMock = null!;
        private ModelDescriptionProvider _modelDescriptionProvider = null!;

        [SetUp]
        public void Setup()
        {
            _dateTimeProviderMock = new Mock<IDateTimeProvider>(MockBehavior.Strict);
            _modelDescriptionProvider = new ModelDescriptionProvider(_dateTimeProviderMock.Object);
        }

        [Test]
        public void GetDescription_ShouldReturnFormattedString_WithDateFromProvider()
        {
           
            var modelId = Guid.NewGuid();
            var expectedDateTime = new DateTime(2025, 10, 27, 10, 30, 0);
            _dateTimeProviderMock.Setup(p => p.GetDateTime()).Returns(expectedDateTime);

            
            var expectedString = $"Some data for model {modelId}: ({expectedDateTime.ToString("o")})";

            var actualString = _modelDescriptionProvider.GetDescription(modelId);

           
            Assert.That(actualString, Is.EqualTo(expectedString));
            _dateTimeProviderMock.Verify(p => p.GetDateTime(), Times.Once);
        }
    }
}