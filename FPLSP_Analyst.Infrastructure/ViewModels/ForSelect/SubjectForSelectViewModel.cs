﻿using FPLSP_Analyst.Application.DataTransferObjects.ForSelect.Request;
using FPLSP_Analyst.Application.Interfaces.Repositories.ReadOnly;
using FPLSP_Analyst.Application.Interfaces.Services;
using FPLSP_Analyst.Application.ValueObjects.Common;
using FPLSP_Analyst.Application.ViewModels;

namespace FPLSP_Analyst.Infrastructure.ViewModels.ForSelect
{
    public class SubjectForSelectViewModel : ViewModelBase<SubjectForSelectRequest>
    {
        private readonly ISubjectReadOnlyRepository _subjectReadOnlyRepository;
        private readonly ILocalizationService _localizationService;

        public SubjectForSelectViewModel(ISubjectReadOnlyRepository subjectReadOnlyRepository, ILocalizationService localizationService)
        {
            _subjectReadOnlyRepository = subjectReadOnlyRepository;
            _localizationService = localizationService;
        }
        public override async Task HandleAsync(SubjectForSelectRequest data, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _subjectReadOnlyRepository.GetSubjectForSelect(data, cancellationToken);

                Data = result.Data!;
                Success = result.Success;
                ErrorItems = result.Errors;
                Message = result.Message;
                return;
            }
            catch
            {
                Success = false;
                ErrorItems = new[]
                {
                new ErrorItem
                {
                    Error = _localizationService["Error occurred while getting the list of subject"],
                    FieldName = string.Concat(LocalizationString.Common.FailedToGet, "list of subject")
                }
            };
            }
        }
    }
}
