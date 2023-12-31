﻿using Ads.Web.DataContexts;
using Ads.Web.Entities;
using Ads.Web.Services.AdService.Concrete.Queries;
using Ads.Web.Services.AdService.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Ads.Web.Services.AdService.Concrete
{
    public class AdService : IAdService
    {
        private readonly AdDbContext _efDbContext;
        private const int PageSize = 10;
        private List<ValidationResult> _validationProblems = new List<ValidationResult>();

        public AdService(AdDbContext efDbContext)
        {
            _efDbContext = efDbContext;
        }

        public bool HasValidationProblems
        {
            get => _validationProblems.Any();
        }

        public IDictionary<string, string[]> ValidationProblems
        {
            get => _validationProblems
               .SelectMany(
                   collectionSelector: l => l.MemberNames,
                   resultSelector: (errorMessage, memberName) => new { errorMessage = errorMessage.ErrorMessage ?? string.Empty, memberName })
               .GroupBy(
                   keySelector: e => e.errorMessage,
                   elementSelector: e => e.memberName)
               .ToDictionary(
                    keySelector: g => g.Key,
                    elementSelector: g => g.ToArray());
        }

        public GetAdsResponseDto GetAds(GetAdsRequestDto? adsRequestDto)
        {
            int pageStartsZero = adsRequestDto?.Page - 1 ?? 0;
            var query = _efDbContext.Ads
                .SortAdBy(
                    propertyName: adsRequestDto?.SortName,
                    asc: adsRequestDto?.SortAsc ?? true)
                .Select(a => new GetAdsResponseItemDto
                {
                    Id = a.AdId,
                    Name = a.Name,
                    Price = a.Price,
                    CreatedUtc = a.DateCreatedUtc.ToString(),
                    ImgUrl = a.ImageUrls
                        .Where(i => i.IsMain)
                        .Select(i => i.Url)
                        .FirstOrDefault()
                })
                .Skip(count: pageStartsZero * PageSize)
                .Take(count: PageSize);

            return new GetAdsResponseDto
            {
                Page = 1 + pageStartsZero,
                Items = query
                    .ToList()
            };
        }

        public CreateAdResponseDto? CreateAd(CreateAdRequestDto createAdRequestDto)
        {
            if (!ValidateObject(createAdRequestDto))
            {
                return null;
            }
            if (createAdRequestDto.ImageUrls
                .Where(img => string.IsNullOrEmpty(img))
                .Any())
            {
                AddValidationError(
                    errorMessage: "images have empty value",
                    memberName: nameof(createAdRequestDto.ImageUrls));
                return null;
            }
            Ad newAd = new Ad
            {
                DateCreatedUtc = DateTime.UtcNow,
                Name = createAdRequestDto.Name ?? string.Empty,
                Description = createAdRequestDto.Description,
                Price = createAdRequestDto.Price,
                ImageUrls = createAdRequestDto.ImageUrls
                    .Select((s, i) => new ImageUrl
                    {
                        Url = s,
                        IsMain = i == 0
                    })
                    .ToList()
            };
            _efDbContext.Ads.Add(newAd);
            _efDbContext.SaveChanges();
            return new CreateAdResponseDto { Id = newAd.AdId };
        }

        private bool ValidateObject(object instance)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(
                instance: instance,
                serviceProvider: null,
                items: null);
            bool res = Validator.TryValidateObject(
                instance: instance,
                validationContext: context,
                validationResults: results,
                validateAllProperties: true);
            if (results.Any())
            {
                _validationProblems.AddRange(results);
            }
            return res;
        }

        private void AddValidationError(string errorMessage, string? memberName = null)
        {
            _validationProblems.Add(
                new ValidationResult(
                    errorMessage: errorMessage,
                    memberNames: !string.IsNullOrEmpty(memberName)
                        ? new[] { memberName }
                        : null));
        }

        public GetAdItemResponseDto? GetAdItem(int id, GetAdItemRequestDto? getAdItemRequestDto)
        {
            if (getAdItemRequestDto != null
                && !ValidateObject(getAdItemRequestDto))
            {
                return null;
            }
            bool includeDescription = getAdItemRequestDto?.Fields?.Contains("description") ?? false;
            bool includeImages = getAdItemRequestDto?.Fields?.Contains("images") ?? false;

            return _efDbContext.Ads
                    .Select(a => new GetAdItemResponseDto
                    {
                        Id = a.AdId,
                        Name = a.Name,
                        Description = includeDescription
                                ? a.Description
                                : null,
                        CreatedUtc = a.DateCreatedUtc.ToString(),
                        ImageUrls = includeImages
                            ? a.ImageUrls
                                .OrderByDescending(i => i.IsMain)
                                .Select(i => i.Url)
                            : a.ImageUrls
                                .Where(i => i.IsMain)
                                .Select(i => i.Url)
                                .Take(1)
                    })
                    .SingleOrDefault(a => a.Id == id);
        }
    }
}
