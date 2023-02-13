
namespace ECommerce
{
    public enum TablePrice
    {
        [Description("Sub-Total:")]
        SubTotal,
        [Description("Flat Shipping Rate:")]
        FlatShippingRate,
        [Description("Eco Tax")]
        EcoTax,
        [Description("VAT")]
        VAT,
        [Description("Total:")]
        Total
    }
}