using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Automat.Domain.Product.Dtos;
using Automat.Infrastructure.Db.Context.Mongo;
using Automat.Infrastructure.Db.Repository;
using MongoDB.Driver;

namespace Automat.Domain.Product.Db
{
    public class ProductMongoRepository : MongoRepositoryBase<Models.Product, Guid>, IProductRepository
    {
        public ProductMongoRepository(IMongoDbContext dbContext) : base(dbContext)
        {
            InitializeProducts();
        }

        public async Task<IEnumerable<Models.Product>> GetProductsBySkuListAsync(IEnumerable<string> skuList, CancellationToken cancellationToken)
        {
            var filterBuilder = Builders<Models.Product>.Filter;
            var filter = filterBuilder.In(product => product.Sku, skuList);
            return await (await Collection.FindAsync(filter, cancellationToken: cancellationToken)).ToListAsync(cancellationToken: cancellationToken);
        }

        private void InitializeProducts()
        {
            DeleteManyAsync(_ => true).Wait();

            var products = new List<Models.Product>
            {
                Models.Product.Load(new ProductDomainDto
                {
                    Name = "Filter Coffee",
                    Sku = "HDFC1",
                    AvailableQuantity = 5,
                    Price = 15,
                    Category = new CategoryDomainDto
                    {
                        Name = "Drink",
                        SubCategory = new CategoryDomainDto
                        {
                            Name = "Hot Drink"
                        }
                    }

                }),
                Models.Product.Load(new ProductDomainDto
                {
                    Name = "Lemonade",
                    Sku = "CDL1",
                    AvailableQuantity = 5,
                    Price = 5,
                    Category = new CategoryDomainDto
                    {
                        Name = "Drink",
                        SubCategory = new CategoryDomainDto
                        {
                            Name = "Cold Drink"
                        }
                    }

                }),
                Models.Product.Load(new ProductDomainDto
                {
                    Name = "White Sugar",
                    Sku = "SWS1",
                    AvailableQuantity = 20,
                    Price = 0,
                    Category = new CategoryDomainDto
                    {
                        Name = "Sugar",
                        RelatedCategories = new List<CategoryDomainDto>
                        {
                            new CategoryDomainDto
                            {
                                Name = "Hot Drink"
                            }
                        }
                    }
                }),
                Models.Product.Load(new ProductDomainDto
                {
                    Name = "Biscuit",
                    Sku = "FB1",
                    AvailableQuantity = 10,
                    Price = 3,
                    Category = new CategoryDomainDto
                    {
                        Name = "Food"
                    }
                }),
                Models.Product.Load(new ProductDomainDto
                {
                    Name = "Chocolate",
                    Sku = "FC1",
                    AvailableQuantity = 10,
                    Price = 6,
                    Category = new CategoryDomainDto
                    {
                        Name = "Food"
                    }
                }),
            };

            InsertManyAsync(products).Wait();
        }
    }
}