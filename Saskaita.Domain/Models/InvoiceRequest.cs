namespace Saskaita.Domain.Models
{
    public class InvoiceRequest
    {
        public string? BuyerCompanyName { get; set; }
        public string? BuyerAddress { get; set; }
        public string? BuyerVatCode { get; set; }
        public string? BuyerCompanyCode { get; set; }
        public string? BuyerCountry { get; set; }
        public string? SellerCompanyName { get; set; }
        public string? SellerAddress { get; set; }
        public string? SellerVatCode { get; set; }
        public string? SellerCompanyCode { get; set; }
        public string? SellerCountry { get; set; }
        public string? ServiceName { get; set; }
        public string? SellerBank { get; set; }
        public string? SellerBankAccountNumber { get; set; }
        public int Units { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
