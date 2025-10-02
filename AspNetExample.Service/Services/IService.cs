using AspNetExample.Service.Models;

namespace AspNetExample.Service.Services;

public interface IService
{
    MyModel GetModel(string data);
}