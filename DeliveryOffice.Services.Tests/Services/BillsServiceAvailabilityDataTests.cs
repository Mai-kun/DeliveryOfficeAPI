using Ahatornn.TestGenerator;
using DeliveryOffice.Core.Models;
using DeliveryOffice.DataAccess.Tests;
using DeliveryOffice.Services.Abstractions.RequestModels;
using DeliveryOffice.Services.ServiceExceptions.ForBuyer;
using DeliveryOffice.Services.ServiceExceptions.ForProduct;
using DeliveryOffice.Services.ServiceExceptions.ForSupplier;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Priority;

namespace DeliveryOffice.Services.Tests.Services;

[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
[DefaultPriority(0)]
public class BillsServiceAvailabilityDataTests : IClassFixture<SharedServiceAvailabilityDataFixture>
{
    private readonly SharedServiceAvailabilityDataFixture fixture;
    private readonly Guid billId1;
    private readonly Guid billId2;
    private readonly Guid supplierId;
    private readonly Guid buyerId;
    private readonly Guid productId;

    public BillsServiceAvailabilityDataTests(SharedServiceAvailabilityDataFixture fixture)
    {
        this.fixture = fixture;
        billId1 = new Guid("6ac53f33-ea5a-47dd-b7f2-1d33bab0a499");
        billId2 = new Guid("2b652c96-13fc-4d37-b28d-bd2ad63f58ef");
        supplierId = new Guid("50dd0a5b-cf1f-4dc8-9cdd-d01a3eafe43d");
        buyerId = new Guid("1dc2c3bf-02c9-4536-9725-8e8cb8bddb9e");
        productId = new Guid("538dede9-6eb4-4bdb-ad08-8225d36a11ab");
    }

    [Fact]
    [Priority(-100)]
    public async Task AddBillAsyncShouldWork()
    {
        var supplier = TestEntityProvider.Shared.Create<Supplier>(x => x.Id = supplierId);
        var buyer = TestEntityProvider.Shared.Create<Buyer>(x => x.Id = buyerId);
        var product = TestEntityProvider.Shared.Create<Product>(x => x.Id = productId);

        await fixture.Context.Set<Supplier>().AddAsync(supplier);
        await fixture.Context.Set<Buyer>().AddAsync(buyer);
        await fixture.Context.Set<Product>().AddAsync(product);
        await fixture.UnitOfWork.SaveChangesAsync(CancellationToken.None);

        var request1 = TestEntityProvider.Shared.Create<CreateBillRequest>(s =>
        {
            s.Id = billId1;
            s.SupplierId = supplierId;
            s.BuyerId = buyerId;
            s.Products = new List<Guid>
            {
                productId,
            };
        });
        var request2 = TestEntityProvider.Shared.Create<CreateBillRequest>(s =>
        {
            s.Id = billId2;
            s.SupplierId = supplierId;
            s.BuyerId = buyerId;
            s.Products = new List<Guid>
            {
                productId,
            };
        });

        await fixture.BillService.AddBillAsync(request1, CancellationToken.None);
        await fixture.BillService.AddBillAsync(request2, CancellationToken.None);

        var addedBills = await fixture.Reader.Read<Bill>().ToListAsync();
        addedBills.Should().HaveCount(2);
        addedBills.Select(b => b.Id).Should().Contain(new[]
        {
            billId1, billId2,
        });
    }

    [Fact]
    public async Task AddBillAsyncShouldThrowSupplierNotFoundException()
    {
        var request = TestEntityProvider.Shared.Create<CreateBillRequest>(s =>
        {
            s.Id = billId1;
            s.SupplierId = Guid.NewGuid();
            s.BuyerId = buyerId;
            s.Products = new List<Guid>
            {
                productId,
            };
        });

        var act = () => fixture.BillService.AddBillAsync(request, CancellationToken.None);
        await act.Should().ThrowAsync<SupplierNotFoundException>();
    }

    [Fact]
    public async Task AddBillAsyncShouldThrowBuyerNotFoundException()
    {
        var request = TestEntityProvider.Shared.Create<CreateBillRequest>(s =>
        {
            s.Id = billId1;
            s.SupplierId = supplierId;
            s.BuyerId = Guid.NewGuid();
            s.Products = new List<Guid>
            {
                productId,
            };
        });

        var act = () => fixture.BillService.AddBillAsync(request, CancellationToken.None);
        await act.Should().ThrowAsync<BuyerNotFoundException>();
    }

    [Fact]
    public async Task AddBillAsyncShouldThrowProductNotFoundException()
    {
        var request = TestEntityProvider.Shared.Create<CreateBillRequest>(s =>
        {
            s.Id = billId1;
            s.SupplierId = supplierId;
            s.BuyerId = buyerId;
            s.Products = new List<Guid>
            {
                Guid.NewGuid(),
            };
        });

        var act = () => fixture.BillService.AddBillAsync(request, CancellationToken.None);
        await act.Should().ThrowAsync<ProductNotFoundException>();
    }

    [Fact]
    public async Task UpdateBillAsyncShouldWork()
    {
        var updateRequest = TestEntityProvider.Shared.Create<BillRequest>(s => s.Id = billId1);

        await fixture.BillService.UpdateBillAsync(updateRequest, CancellationToken.None);

        var updatedBill = await fixture.Reader.Read<Bill>().FirstOrDefaultAsync(s => s.Id == billId1);
        updatedBill.Should().NotBeNull()
                   .And.BeEquivalentTo(updateRequest);
    }

    [Fact]
    public async Task GetAllBillsAsyncAsyncShouldReturnValue()
    {
        var result = await fixture.BillService.GetAllBillsAsync(CancellationToken.None);

        result.Should().NotBeNull()
              .And.NotBeEmpty()
              .And.HaveCount(2);
    }

    [Fact]
    public async Task GetBillByIdAsyncShouldReturnValue()
    {
        var result = await fixture.BillService.GetBillByIdAsync(billId1, CancellationToken.None);

        result.Should().NotBeNull()
              .And.BeOfType<Bill>()
              .And.BeEquivalentTo(new
              {
                  Id = billId1,
              });
    }

    [Fact]
    [Priority(100)]
    public async Task DeleteBillAsyncShouldWork()
    {
        await fixture.BillService.DeleteBillAsync(billId2, CancellationToken.None);

        var bills = await fixture.Reader.Read<Bill>().ToListAsync();
        bills.Should().HaveCount(2);
        bills.FirstOrDefault(b => b.Id == billId2 && b.IsDeleted).Should().NotBeNull();
    }
}
