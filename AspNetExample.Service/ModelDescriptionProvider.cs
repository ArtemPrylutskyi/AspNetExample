﻿using AspNetExample.Shared;

namespace AspNetExample.Service
{
    public class ModelDescriptionProvider : IModelDescriptionProvider
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public ModelDescriptionProvider(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

     
        public string GetDescription(Guid modelId) => $"Some data for model {modelId}: ({_dateTimeProvider.GetDateTime().ToString("o")})";
    }
}