using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit;
using Domain.Interface;
using Moq;
using Xunit;

namespace Domain.Test;

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        //arrange
        var fixture = new Fixture()
            .Customize(new AutoMoqCustomization());
        
        var inprogress = fixture.Freeze<Mock<InProgressOrders>>();
        inprogress.SetupGet(x => x.Content).Returns(new List<Order>()
        {
            new Order("IN_PROGRESS",new List<Line>()
            {
                new Line("1","1","shirt1",4),
                new Line("2","2","shirt2",6),
            }),
            new Order("IN_PROGRESS",new List<Line>()
            {
                new Line("1","1","shirt1",7),
                new Line("2","2","shirt2",10),
            }),
            new Order("IN_PROGRESS",new List<Line>()
            {
                new Line("3","3","shirt3",1),
                new Line("4","4","shirt4",10),
            }),
        });
       
        //act
        var sut = inprogress.Object.GetTop5SoldProducts();
        
        
        //assert
        Assert.True(sut.Any(x=>x.MerchantProductNo == "1" && x.Quantity == 11));
        Assert.True(sut.Any(x=>x.MerchantProductNo == "2" && x.Quantity == 16));
        Assert.True(sut.Any(x=>x.MerchantProductNo == "3" && x.Quantity == 1));
        Assert.True(sut.Any(x=>x.MerchantProductNo == "4" && x.Quantity == 10));
    }
}