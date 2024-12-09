using Allup.Application.Services.Abstracts;
using Allup.Application.UI.Services.Abstracts;
using Allup.Application.UI.ViewModels;
using Allup.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Allup.Application.UI.Services.Implementations
{
    public class BasketUiManager : IBasketUiService
    {
        private readonly IBasketService _basketService;
        private readonly ICookieService _cookieService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BasketUiManager(IBasketService basketService, ICookieService cookieService, IHttpContextAccessor httpContextAccessor)
        {
            _basketService = basketService;
            _cookieService = cookieService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<int> AddBasketItem(int productId)
        {
            string clientId;
            if (!_httpContextAccessor.HttpContext.User.Identity!.IsAuthenticated)
                clientId = _cookieService.GetBrowserId();
            else
                clientId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var wishListCreateViewModel = new BasketCreateViewModel { ClientId = clientId, ProductId = productId ,Count=1};

            await _basketService.CreateAsync(wishListCreateViewModel);

            var itemsCount = (await _basketService.GetAllAsync(x => x.ClientId == clientId)).Count;

            return itemsCount;
        }

        public async Task<BasketUiViewModel> GetBasketUiViewModelAsync()
        {
            var clientId = _cookieService.GetBrowserId();
            var language = await _cookieService.GetLanguageAsync();

            var basketItems = await _basketService.GetAllAsync(x => x.ClientId == clientId,
                include: x => x.Include(y => y.Product)
                .ThenInclude(p => p!.ProductTranslations!
                .Where(pt => pt.LanguageId == language.Id)));

            var count=basketItems.Count();
            decimal sum = 0;
            foreach (var basketItem in basketItems)
            {
                sum+= basketItem.Product.Price;
            }
            var vm = new BasketUiViewModel
            {
                Items = basketItems,
                Count = count,
                TotalAmount = sum,

                
            };

            return vm;
        }

        public async Task<int> RemoveFromBasket(int id)
        {
            var clientId = _cookieService.GetBrowserId();

            await _basketService.Remove(id);

            var itemsCount = (await _basketService.GetAllAsync(x => x.ClientId == clientId)).Count;

            return itemsCount;
        }
    }
}
