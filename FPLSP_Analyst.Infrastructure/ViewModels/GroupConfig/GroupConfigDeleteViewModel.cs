﻿using AutoMapper;
using FPLSP_Analyst.Application.DataTransferObjects.GroupConfig.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadWrite;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.GroupConfig
{
    public class GroupConfigDeleteViewModel : ViewModelBase<GroupConfigDeleteRequest>
    {

        public readonly IGroupConfigReadWriteRepository _GroupConfigReadWriteRepository;
        private readonly ILocalizationService _localizationService;
        private readonly IMapper _mapper;

        public GroupConfigDeleteViewModel(IGroupConfigReadWriteRepository GroupConfigReadWriteRepository, ILocalizationService localizationService, IMapper mapper)
        {
            _GroupConfigReadWriteRepository = GroupConfigReadWriteRepository;
            _localizationService = localizationService;
            _mapper = mapper;
        }
        public override async Task HandleAsync(GroupConfigDeleteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _GroupConfigReadWriteRepository.DeleteGroupConfigAsync(request, cancellationToken);

                Success = result.Success;
                ErrorItems = result.Errors;
                Message = result.Message;
                return;
            }
            catch (Exception)
            {
                Success = false;
                ErrorItems = new[]
                    {
                    new ErrorItem
                    {
                        Error = _localizationService["Error occurred while updating the GroupConfig"],
                        FieldName = string.Concat(LocalizationString.Common.FailedToDelete, "GroupConfig")
                    }
                };
            }
        }
    }
}
