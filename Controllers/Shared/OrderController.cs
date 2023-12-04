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

    }

}
