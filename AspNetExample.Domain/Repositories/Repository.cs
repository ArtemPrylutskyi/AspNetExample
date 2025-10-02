using AspNetExample.Domain.Models;

namespace AspNetExample.Domain.Repositories;

// Do db related things here
internal class Repository : IRepository
{
    public MyModel GetModel(string data) => new(data ?? "Template");
}