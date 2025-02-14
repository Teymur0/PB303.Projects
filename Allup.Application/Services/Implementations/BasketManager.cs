﻿using Allup.Application.Services.Abstracts;
using Allup.Application.UI.Services.Abstracts;
using Allup.Application.UI.Services.Implementations;
using Allup.Application.ViewModels;
using Allup.Domain.Entities;
using Allup.Persistence.Context;
using AutoMapper;
using Core.Persistence.Repositories;
using Microsoft.EntityFrameworkCore.Query;
using System.Globalization;
using System.Linq.Expressions;

namespace Allup.Application.Services.Implementations
{
    public class BasketManager : CrudManager<BasketViewModel, Basket, BasketCreateViewModel>, IBasketService
    {
        private readonly EfRepositoryBase<Basket, AppDbContext> _repository;
        private readonly ICookieService _cookieService;
        private readonly ExternalApiService _externalApiService;

        public BasketManager(EfRepositoryBase<Basket, AppDbContext> repository, IMapper mapper, ICookieService cookieService, ExternalApiService externalApiService) : base(repository, mapper)
        {
            _repository = repository;
            _cookieService = cookieService;
            _externalApiService = externalApiService;
        }
        public override async Task<BasketViewModel> CreateAsync(BasketCreateViewModel createViewModel)
        {
            var existItem = await _repository.GetAsync(x => x.ClientId == createViewModel.ClientId && x.ProductId == createViewModel.ProductId);

            if (existItem != null)
                 existItem.Count++;

            var createdWishlistItem = await base.CreateAsync(createViewModel);

            return createdWishlistItem;
        }

        public override async Task<List<BasketViewModel>> GetAllAsync(Expression<Func<Basket, bool>>? predicate = null, Func<IQueryable<Basket>, IOrderedQueryable<Basket>>? orderBy = null, Func<IQueryable<Basket>, IIncludableQueryable<Basket, object>>? include = null)
        {
            var wishListItems = await base.GetAllAsync(predicate, orderBy, include);

            var currency = await _cookieService.GetCurrencyAsync();

            var coefficient = await _externalApiService.GetCurrencyCoefficient(currency.CurrencyCode ?? "azn");
            var culture = new CultureInfo(currency.IsoCode ?? "az-az");

            foreach (var item in wishListItems)
            {
                if (item.Product == null) continue;

                item.Product.FormattedPrice = (item.Product.Price / coefficient).ToString("C", culture);
            }

            return wishListItems;
        }
    }
}
