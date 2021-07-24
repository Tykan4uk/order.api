using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Moq;
using OrderApi.Data;
using OrderApi.Data.Entities;
using OrderApi.DataProviders.Abstractions;
using OrderApi.Models;
using OrderApi.Models.Responses;
using OrderApi.Services;
using OrderApi.Services.Abstractions;
using Xunit;

namespace OrderApi.UnitTests.Services
{
    public class OrderServiceTest
    {
        private readonly IOrderService _orderService;

        private readonly Mock<IOrderProvider> _orderProvider;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IDbContextWrapper<OrdersDbContext>> _dbContextWrapper;
        private readonly Mock<ILogger<OrderService>> _logger;

        private readonly PagingDataResult _pagingDataResultsSuccess = new PagingDataResult()
        {
            Orders = new List<OrderEntity>()
            {
                new OrderEntity()
                {
                    Products = new List<ProductEntity>()
                    {
                        new ProductEntity()
                        {
                            Name = "TestName"
                        }
                    }
                }
            },
            TotalRecords = 1
        };
        private readonly List<ProductEntity> _productEntitiesListSuccess = new List<ProductEntity>()
        {
            new ProductEntity()
            {
                Name = "TestName"
            }
        };
        private readonly List<ProductEntity> _productEntitiesListFailed = new List<ProductEntity>()
        {
        };
        private readonly List<ProductModel> _productModelsListSuccess = new List<ProductModel>()
        {
            new ProductModel()
            {
                Name = "TestName"
            }
        };
        private readonly List<ProductModel> _productModelsListFailed = new List<ProductModel>()
        {
        };
        private readonly PagingDataResult _pagingDataResultsFailed = new PagingDataResult()
        {
        };
        private readonly GetByPageResponse _getByPageResponseSuccess = new GetByPageResponse()
        {
            Orders = new List<OrderModel>()
            {
                new OrderModel()
                {
                    UserId = "TestUserId"
                }
            },
            TotalRecords = 1
        };
        private readonly GetByPageResponse _getByPageResponseFailed = new GetByPageResponse()
        {
            Orders = null,
            TotalRecords = 1
        };
        private readonly OrderEntity _orderEntitySuccess = new OrderEntity()
        {
            UserId = "TestUserId"
        };
        private readonly OrderModel _orderModelSuccess = new OrderModel()
        {
            UserId = "TestUserId"
        };
        private readonly OrderEntity _orderEntityFailed = new OrderEntity()
        {
        };
        private readonly OrderModel _orderModelFailed = new OrderModel()
        {
        };
        private readonly string _testOrderIdSuccess = "testOrderIdSuccess";
        private readonly string _testOrderIdFailed = "testOrderIdFailed";
        private readonly string _testUserIdSuccess = "testUserIdSuccess";
        private readonly string _testUserIdFailed = "testUserIdFailed";

        public OrderServiceTest()
        {
            _orderProvider = new Mock<IOrderProvider>();
            _mapper = new Mock<IMapper>();
            _dbContextWrapper = new Mock<IDbContextWrapper<OrdersDbContext>>();
            _logger = new Mock<ILogger<OrderService>>();

            _orderProvider.Setup(s => s.GetByPageAsync(
                It.Is<string>(i => i.Contains(_testUserIdSuccess)),
                It.Is<int>(i => i == 1),
                It.Is<int>(i => i == 10))).ReturnsAsync(_pagingDataResultsSuccess);

            _orderProvider.Setup(s => s.GetByPageAsync(
                It.Is<string>(i => i.Contains(_testUserIdFailed)),
                It.Is<int>(i => i == 1000),
                It.Is<int>(i => i == 10000))).ReturnsAsync(_pagingDataResultsFailed);

            _orderProvider.Setup(s => s.GetByIdAsync(
                It.Is<string>(i => i.Contains(_testUserIdSuccess)),
                It.Is<string>(i => i.Contains(_testOrderIdSuccess)))).ReturnsAsync(_productEntitiesListSuccess);

            _orderProvider.Setup(s => s.GetByIdAsync(
                It.Is<string>(i => i.Contains(_testUserIdFailed)),
                It.Is<string>(i => i.Contains(_testOrderIdFailed)))).ReturnsAsync(_productEntitiesListFailed);

            _orderProvider.Setup(s => s.AddAsync(
                It.Is<string>(i => i.Contains(_testUserIdSuccess)),
                It.Is<List<ProductEntity>>(i => i.Equals(_productEntitiesListSuccess)))).ReturnsAsync(_orderEntitySuccess);

            _orderProvider.Setup(s => s.AddAsync(
                It.Is<string>(i => i.Contains(_testUserIdFailed)),
                It.Is<List<ProductEntity>>(i => i.Equals(_productEntitiesListFailed)))).ReturnsAsync(_orderEntityFailed);

            _mapper.Setup(s => s.Map<OrderModel>(
                It.Is<OrderEntity>(i => i.Equals(_orderEntitySuccess)))).Returns(_orderModelSuccess);

            _mapper.Setup(s => s.Map<OrderModel>(
                It.Is<OrderEntity>(i => i.Equals(_orderEntityFailed)))).Returns(_orderModelFailed);

            _mapper.Setup(s => s.Map<List<ProductModel>>(
                It.Is<List<ProductEntity>>(i => i.Equals(_productEntitiesListSuccess)))).Returns(_productModelsListSuccess);

            _mapper.Setup(s => s.Map<List<ProductModel>>(
                It.Is<List<ProductEntity>>(i => i.Equals(_productEntitiesListFailed)))).Returns(_productModelsListFailed);

            _mapper.Setup(s => s.Map<List<ProductEntity>>(
                It.Is<List<ProductModel>>(i => i.Equals(_productModelsListSuccess)))).Returns(_productEntitiesListSuccess);

            _mapper.Setup(s => s.Map<List<ProductEntity>>(
                It.Is<List<ProductModel>>(i => i.Equals(_productModelsListFailed)))).Returns(_productEntitiesListFailed);

            _mapper.Setup(s => s.Map<GetByPageResponse>(
                It.Is<PagingDataResult>(i => i.Equals(_pagingDataResultsSuccess)))).Returns(_getByPageResponseSuccess);

            _mapper.Setup(s => s.Map<GetByPageResponse>(
                It.Is<PagingDataResult>(i => i.Equals(_pagingDataResultsFailed)))).Returns(_getByPageResponseFailed);

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransaction()).Returns(dbContextTransaction.Object);

            _orderService = new OrderService(_dbContextWrapper.Object, _orderProvider.Object, _mapper.Object, _logger.Object);
        }

        [Fact]
        public async Task GetByPageAsync_Success()
        {
            // arrange
            var testPage = 1;
            var testPageSize = 10;

            // act
            var result = await _orderService.GetByPageAsync(_testUserIdSuccess, testPage, testPageSize);

            // assert
            result.Should().NotBeNull();
            result.Orders.Should().NotBeNull();
            result.TotalRecords.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetByPageAsync_Failed()
        {
            // arrange
            var testPage = 1000;
            var testPageSize = 10000;

            // act
            var result = await _orderService.GetByPageAsync(_testUserIdFailed, testPage, testPageSize);

            // assert
            result.Should().NotBeNull();
            result.Orders.Should().BeNull();
            result.TotalRecords.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task GetByIdAsync_Success()
        {
            // arrange

            // act
            var result = await _orderService.GetByIdAsync(_testUserIdSuccess, _testOrderIdSuccess);

            // assert
            result.Should().NotBeNull();
            result.Products.Should().NotBeNull();
            result.Products[0].Name.Should().Be("TestName");
        }

        [Fact]
        public async Task GetByIdAsync_Failed()
        {
            // arrange

            // act
            var result = await _orderService.GetByIdAsync(_testUserIdFailed, _testOrderIdFailed);

            // assert
            result.Should().NotBeNull();
            result.Products.Should().BeEmpty();
        }

        [Fact]
        public async Task AddAsync_Success()
        {
            // arrange

            // act
            var result = await _orderService.AddAsync(_testUserIdSuccess, _productModelsListSuccess);

            // assert
            result.Should().NotBeNull();
            result.Order.Should().NotBeNull();
            result.Order.UserId.Should().Be("TestUserId");
        }

        [Fact]
        public async Task AddAsync_Failed()
        {
            // arrange

            // act
            var result = await _orderService.AddAsync(_testUserIdFailed, _productModelsListFailed);

            // assert
            result.Should().NotBeNull();
            result.Order.Should().NotBeNull();
            result.Order.UserId.Should().BeNull();
        }
    }
}
