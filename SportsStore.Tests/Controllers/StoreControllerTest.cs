using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models.Domain;
using SportsStore.Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportsStore.Tests.Controllers
{
    public class StoreControllerTest
    {
        private readonly StoreController _storeController;
        private readonly Mock<IProductRepository> _productRepository;
        private readonly DummyApplicationDbContext _context;

        public StoreControllerTest()
        {
            _context = new DummyApplicationDbContext();
            _productRepository = new Mock<IProductRepository>();
            _storeController = new StoreController(_productRepository.Object);
        }

        [Fact]
        public void Index_PassesOnlineProductsToViewViaModel()
        {
            _productRepository.Setup(p => p.GetByAvailability(new List<Availability> { Availability.ShopAndOnline, Availability.OnlineOnly }))
                .Returns(_context.ProductsOnline);
            var result = Assert.IsType<ViewResult>(_storeController.Index());
            var model = Assert.IsAssignableFrom<IEnumerable<Product>>(result.Model);
            Assert.Equal(10, model.Count());
        }
    }
}
