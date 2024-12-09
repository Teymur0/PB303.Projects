using Allup.Application.ViewModels;

namespace Allup.Application.UI.ViewModels;

public class BasketUiViewModel
{
    public List<BasketViewModel>? Items { get; set; }
    public int Count { get; set; }
    public decimal TotalAmount { get; set; }
}