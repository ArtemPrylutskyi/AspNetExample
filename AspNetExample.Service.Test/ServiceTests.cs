using Moq;
using NUnit.Framework;
using System;
using AspNetExample.Domain.Repositories;
using AspNetExample.Service.Services;
using DomainModel = AspNetExample.Domain.Models.MyModel;
using ServiceModel = AspNetExample.Service.Models.MyModel;

namespace AspNetExample.Service.Tests
{
    [TestFixture]
    public class ServiceTests
    {
        
        private Mock<IRepository> _repositoryMock = null!;
        private Mock<IModelDescriptionProvider> _modelDescriptionProviderMock = null!;
        private Services.Service _service = null!;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IRepository>(MockBehavior.Strict);
            _modelDescriptionProviderMock = new Mock<IModelDescriptionProvider>(MockBehavior.Strict);
            _service = new Services.Service(_repositoryMock.Object, _modelDescriptionProviderMock.Object);
        }

        [Test]
        public void CreateModel_ShouldCallRepository_AndReturnMappedModel()
        {
            // Arrange
            var inputData = "new data";
            var domainModel = new DomainModel(inputData, Guid.NewGuid());
            _repositoryMock.Setup(r => r.CreateModel(inputData)).Returns(domainModel);

           
            var result = _service.CreateModel(inputData);

           
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(domainModel.Id));
            Assert.That(result.Data, Is.EqualTo(domainModel.Data));
            Assert.That(result.Description, Is.Null); 
            _repositoryMock.Verify(r => r.CreateModel(inputData), Times.Once);
        }

        [Test]
        public void GetModel_WhenModelExists_ShouldReturnModelWithDescription()
        {
           
            var modelId = Guid.NewGuid();
            var domainModel = new DomainModel("test data", modelId);
            var description = "This is a description.";

            _repositoryMock.Setup(r => r.GetModel(modelId)).Returns(domainModel);
            _modelDescriptionProviderMock.Setup(p => p.GetDescription(modelId)).Returns(description);

            var result = _service.GetModel(modelId);

            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(modelId));
            Assert.That(result.Data, Is.EqualTo(domainModel.Data));
            Assert.That(result.Description, Is.EqualTo(description));
            _repositoryMock.Verify(r => r.GetModel(modelId), Times.Once);
            _modelDescriptionProviderMock.Verify(p => p.GetDescription(modelId), Times.Once);
        }

        [Test]
        public void GetModel_WhenModelDoesNotExist_ShouldReturnNull()
        {
            
            var modelId = Guid.NewGuid();
            _repositoryMock.Setup(r => r.GetModel(modelId)).Returns((DomainModel)null);

            
            var result = _service.GetModel(modelId);

            
            Assert.That(result, Is.Null);
            _repositoryMock.Verify(r => r.GetModel(modelId), Times.Once);
            _modelDescriptionProviderMock.Verify(p => p.GetDescription(It.IsAny<Guid>()), Times.Never);
        }

        [Test]
        public void UpdateModel_ShouldCallRepository_AndReturnUpdatedModel()
        {
            
            var serviceModel = new ServiceModel("updated data", Guid.NewGuid(), null);
            var domainModel = new DomainModel(serviceModel.Data, serviceModel.Id);

            _repositoryMock.Setup(r => r.UpdateModel(It.Is<DomainModel>(d => d.Id == serviceModel.Id && d.Data == serviceModel.Data)))
                         .Returns(domainModel);

            
            var result = _service.UpdateModel(serviceModel);

            
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(serviceModel.Id));
            Assert.That(result.Data, Is.EqualTo(serviceModel.Data));
            _repositoryMock.Verify(r => r.UpdateModel(It.IsAny<DomainModel>()), Times.Once);
        }

        [Test]
        public void DeleteModel_ShouldCallRepository_AndReturnResult()
        {
            
            var modelId = Guid.NewGuid();
            _repositoryMock.Setup(r => r.DeleteModel(modelId)).Returns(true);

           
            var result = _service.DeleteModel(modelId);

            Assert.That(result, Is.True);
            _repositoryMock.Verify(r => r.DeleteModel(modelId), Times.Once);
        }
    }
}