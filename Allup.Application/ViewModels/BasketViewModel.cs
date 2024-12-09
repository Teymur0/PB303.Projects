namespace Allup.Application.ViewModels
{
    public class BasketViewModel
    {
        public int Id { get; set; }
        public string ClientId { get; set; } = null!;
        public required int ProductId { get; set; }
        public ProductViewModel? Product { get; set; }
        public int Count { get; set; }
    }
    public class BasketCreateViewModel
    {
        public string ClientId { get; set; } = null!;
        public required int ProductId { get; set; }
        public int Count { get; set; }
    }
}
