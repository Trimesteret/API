using API.Enums;
using API.Models.Authentication;
using API.Models.Orders;
using API.Services.Shared;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Shared
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController
    {
        IAuthService _authorizationService;

        public OrderController(IAuthService authService)
        {
            _authorizationService = authService;
        }

        [HttpGet("/purchaseOrder")]
        public async Task<PurchaseOrder> GetPurchaseOrder()
        {
            var user = await _authorizationService.GetActiveUser();
            if (user.Role != Role.Customer && user.Role != Role.Guest)
            {
                return null;
            }

            var guest = user as Guest;

            return guest.GetOpenPurchaseOrder();
        }

    }

}
