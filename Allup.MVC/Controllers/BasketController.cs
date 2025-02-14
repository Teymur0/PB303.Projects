﻿using Allup.Application.Services.Abstracts;
using Allup.Application.UI.Services.Abstracts;
using Allup.Application.UI.ViewModels;
using Allup.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Allup.MVC.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketUiService _wishlistUiService;

        public BasketController(IBasketUiService wishlistUiService)
        {
            _wishlistUiService = wishlistUiService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _wishlistUiService.GetBasketUiViewModelAsync();

            return View(model);
        }

        public async Task<IActionResult> AddBasket(int productId)
        {
            var itemsCount = await _wishlistUiService.AddBasketItem(productId);

            var result = await _wishlistUiService.GetBasketUiViewModelAsync();

            var json = JsonConvert.SerializeObject(result);

            return Json(json);
        }


        public async Task<IActionResult> AddBasketAsiman(int productId)
        {
            var itemsCount = await _wishlistUiService.AddBasketItem(productId);

            var result = await _wishlistUiService.GetBasketUiViewModelAsync();

            return PartialView("_basketModalPartial", result);
        }

        public async Task<IActionResult> Remove(int id)
        {
            var itemsCount = await _wishlistUiService.RemoveFromBasket(id);

            var result = await _wishlistUiService.GetBasketUiViewModelAsync();

            return PartialView("_basketModalPartial", result);
        }
        //    private readonly IProductService _productService;

        //    public BasketController(IProductService productService, ILanguageService languageService) : base(languageService)
        //    {
        //        _productService = productService;
        //    }

        //    public IActionResult Index()
        //    {
        //        return View();
        //    }

        //    public async Task<IActionResult> AddBasket(int productId)
        //    {
        //        var basket = Request.Cookies["basket"];
        //        var basketViewModel = new BasketViewModel();
        //        var basketCookieViewModels = new List<BasketCookieViewModel>();
        //        var basketItemViewModels = new List<BasketItemViewModel>();

        //        var languageId = await GetLanguageAsync();

        //        if (string.IsNullOrEmpty(basket))
        //        {
        //            basketCookieViewModels.Add(new BasketCookieViewModel
        //            {
        //                Count = 1,
        //                ProductId = productId
        //            });
        //        }
        //        else
        //        {
        //            basketCookieViewModels = JsonConvert.DeserializeObject<List<BasketCookieViewModel>>(basket) ?? [];

        //            if (basketCookieViewModels.Any(x => x.ProductId == productId))
        //            {
        //                var existBasketItem = basketCookieViewModels.Find(x => x.ProductId == productId);
        //                existBasketItem!.Count++;
        //            }
        //            else
        //            {
        //                basketCookieViewModels.Add(new BasketCookieViewModel
        //                {
        //                    Count = 1,
        //                    ProductId = productId
        //                });
        //            }    
        //        }

        //        Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketCookieViewModels));

        //        foreach (var item in basketCookieViewModels ?? [])
        //        {
        //            var existBasketItem = await _productService.GetAsync(x => x.Id == item.ProductId,
        //                x => x.Include(y => y.ProductTranslations!.Where(z => z.LanguageId == languageId)));

        //            if (existBasketItem == null) continue;

        //            basketItemViewModels.Add(new BasketItemViewModel
        //            {
        //                ProductId = existBasketItem.Id,
        //                Name = existBasketItem.Name,
        //                CoverImageUrl = existBasketItem.CoverImageUrl,
        //                Price = existBasketItem.Price,
        //                Count = item.Count
        //            });
        //        }

        //        //var totalAmount = basketItemViewModels.Sum(x => x.Price * x.Count);

        //        basketViewModel.Items = basketItemViewModels;
        //        //basketViewModel.TotalAmount = totalAmount;

        //        return Json(basketViewModel);
        //    }

        //    public async Task<IActionResult> Remove(int productId)
        //    {
        //        var basket = Request.Cookies["basket"];
        //        var basketViewModel = new BasketViewModel();
        //        var basketItemViewModels = new List<BasketItemViewModel>();
        //        var languageId = await GetLanguageAsync();

        //        if (string.IsNullOrEmpty(basket))
        //        {
        //            return BadRequest();
        //        }

        //        var basketCookieViewModels = JsonConvert.DeserializeObject<List<BasketCookieViewModel>>(basket);

        //        var existProduct = basketCookieViewModels.Find(x => x.ProductId == productId);

        //        if (existProduct == null)
        //            return BadRequest();

        //        basketCookieViewModels.Remove(existProduct);
        //        Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketCookieViewModels));

        //        foreach (var item in basketCookieViewModels ?? [])
        //        {
        //            var existBasketItem = await _productService.GetAsync(x => x.Id == item.ProductId,
        //                x => x.Include(y => y.ProductTranslations!.Where(z => z.LanguageId == languageId)));

        //            if (existBasketItem == null) continue;

        //            basketItemViewModels.Add(new BasketItemViewModel
        //            {
        //                ProductId = existBasketItem.Id,
        //                Name = existBasketItem.Name,
        //                CoverImageUrl = existBasketItem.CoverImageUrl,
        //                Price = existBasketItem.Price,
        //                Count = item.Count
        //            });
        //        }

        //        //var totalAmount = basketItemViewModels.Sum(x => x.Price * x.Count);

        //        basketViewModel.Items = basketItemViewModels;
        //        //basketViewModel.TotalAmount = totalAmount;
        //        return Json(basketViewModel);
        //    }
        //}
    }
}
