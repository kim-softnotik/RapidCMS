﻿using System;
using System.Linq;
using System.Threading.Tasks;
using RapidCMS.Common.ActionHandlers;
using RapidCMS.Common.Data;
using RapidCMS.Common.Enums;
using TestLibrary.Entities;
using TestLibrary.Repositories;

#nullable enable

namespace TestServer.ActionHandlers
{
    public class CreateButtonActionHandler : IButtonActionHandler
    {
        private readonly AzureTableStorageRepository _repository;

        public CreateButtonActionHandler(AzureTableStorageRepository repository)
        {
            _repository = repository;
        }

        public CrudType GetCrudType()
        {
            return CrudType.Refresh;
        }

        public async Task InvokeAsync(string? parentId, string? id, object? customData)
        {
            var i = 0;
            var max = Convert.ToInt64(customData);

            do
            {
                await _repository.InsertAsync(parentId, new AzureTableStorageEntity()
                {
                    Description = $"New New New {i}",
                    Title = $"Item {i}"
                }, default);
            }
            while (++i < max);
        }

        public bool IsCompatibleWithView(ViewContext viewContext)
        {
            return viewContext.Usage.HasFlag(UsageType.List);
        }

        public bool ShouldConfirm()
        {
            return true;
        }
    }
}
