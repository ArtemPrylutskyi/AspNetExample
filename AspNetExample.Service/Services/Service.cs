using AspNetExample.Domain.Repositories;
using MyDomainModel = AspNetExample.Domain.Models.MyModel;
using MyModel = AspNetExample.Service.Models.MyModel;

namespace AspNetExample.Service.Services
{
    public class Service : IService
    {
        private readonly IRepository _repository;
        private readonly IModelDescriptionProvider _modelDescriptionProvider;

        public Service(IRepository repository, IModelDescriptionProvider modelDescriptionProvider)
        {
            _repository = repository;
            _modelDescriptionProvider = modelDescriptionProvider;
        }

        public MyModel? CreateModel(string data) => Convert(_repository.CreateModel(data));

        public MyModel? GetModel(Guid id)
        {
            var modelFromRepo = _repository.GetModel(id);

            
            if (modelFromRepo == null)
            {
                return null;
            }

            var model = Convert(modelFromRepo);
            string description = _modelDescriptionProvider.GetDescription(id);

            return model with { Description = description };
        }

        public MyModel? UpdateModel(MyModel data) => Convert(_repository.UpdateModel(Convert(data)));

        public bool DeleteModel(Guid id) => _repository.DeleteModel(id);

        private MyDomainModel? Convert(MyModel? model) => model is null ? null : new(model.Data, model.Id);
        private MyModel? Convert(MyDomainModel? model) => model is null ? null : new(model.Data, model.Id);
    }
}