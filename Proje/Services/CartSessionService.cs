using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Proje.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proje.Services
{
    public class CartSessionService : ICartSessionService
    {
        IHttpContextAccessor _httpContextAccessor;
        public CartSessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Cart GetCart()
        {
            Cart cartToCheck= _httpContextAccessor.HttpContext.Session.GetObject<Cart>("cart");
            if(cartToCheck==null)
            {
                _httpContextAccessor.HttpContext.Session.setObject("cart", new Cart());
                cartToCheck= _httpContextAccessor.HttpContext.Session.GetObject<Cart>("cart");

            }
            return cartToCheck;
        }

        public void SetCart(Cart cart)
        {
            _httpContextAccessor.HttpContext.Session.setObject("cart",cart);
        }
    }
}
