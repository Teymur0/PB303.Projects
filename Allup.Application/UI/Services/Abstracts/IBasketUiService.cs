using Allup.Application.UI.ViewModels;

namespace Allup.Application.UI.Services.Abstracts;

public interface IBasketUiService
{
    Task<BasketUiViewModel> GetBasketUiViewModelAsync();
    Task<int> AddBasketItem(int productId);
    Task<int> RemoveFromBasket(int id);
}