using AspNetExample.Domain.Repositories;
using AspNetExample.Service.Models;
using MyDomainModel = AspNetExample.Domain.Models.MyModel;

namespace AspNetExample.Service.Services;

internal class Service : IService
{
    private readonly IRepository _repository;

    public Service(IRepository repository)
    {
        _repository = repository;
    }

    public MyModel GetModel(string data) => Convert(_repository.GetModel(data));

    private MyModel Convert(MyDomainModel domainModel) => new(domainModel.Data);
}