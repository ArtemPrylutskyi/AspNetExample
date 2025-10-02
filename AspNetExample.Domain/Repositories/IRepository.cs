using AspNetExample.Domain.Models;

namespace AspNetExample.Domain.Repositories;

public interface IRepository
{
    MyModel GetModel(string data);
}