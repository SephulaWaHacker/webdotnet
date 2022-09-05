using BMW.CloudAdoption.BOM.Persistence.Context;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BMW.CloudAdoption.BOM.Modules.Bom.Handlers;
using BMW.CloudAdoption.BOM.Modules.Bom.Models.Requests;
using Microsoft.AspNetCore.Http;
using BMW.CloudAdoption.BOM.Tests.Setup;

namespace BMW.CloudAdoption.BOM.Tests.Unit
{
    [TestFixture]
    public class CreateBomTetsts: IntegrationTest
    {
        private Mock<BomInterface> _contaxtMock;
        private Mock<IMapper> _mapperMock;


        private CreateBom _handler;

        [SetUp]
        public void SetUp()
        {
            var _contaxtMock = new Mock<BomInterface>();
            var _mapperMock = new Mock<IMapper>();
            _handler = new CreateBom(_contaxtMock.Object, _mapperMock.Object);
            
        }

        [Test]
        public async Task HandleAsync_EndDateBeforeStartDate_ValidationShoudlFail()
        {
            var request = new BomRequest
            {
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now),
                VehicleId = "123"
            };

            var result = await _handler.Handle(new Modules.Bom.Commands.CreateBom(request), CancellationToken.None);
            Assert.That(result, Is.EqualTo("Start date cannot be greater than End Date"));

            Assert.IsInstanceOf<IResult>(result);

        }
    }
}
