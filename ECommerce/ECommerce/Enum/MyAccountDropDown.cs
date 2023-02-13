
namespace ECommerce
{
    public enum MyAccountDropDown
    {
        [Description("account/account")]
        Dashboard,
        [Description("account/register")]
        Register,
        [Description("account/login")]
        Login,
        [Description("account/order")]
        MyOrder,
        [Description("account/return/add")]
        Return,
        [Description("information/tracking")]
        Tracking,
        [Description("account/voucher")]
        MyVoucher,
        [Description("account/logout")]
        Logout
    }
}