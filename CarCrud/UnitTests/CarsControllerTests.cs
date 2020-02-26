using System;
using System.Linq;
using CarCrud.Controllers;
using CarCrud.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CarCrud.UnitTests
{
    public class CarsControllerTests
    {

        [Fact]
        public void PartialCarUpdate()
        {
            // Arrange
            var services = new Services.CarService(new CarsDatabaseSettings
                                            {
                                                CarsCollectionName = "Cars",
                                                ConnectionString = "mongodb://localhost:27017",
                                                DatabaseName = "CarsDb"
                                            });
            CarsController controller = new CarsController(services);

            // Act
            var carFromDB = services.Get().First();
            controller.Update(carFromDB.Id, "", carFromDB.Description);
            // Assert
            var updateCarFromDB = services.Get(carFromDB.Id);
            Assert.Equal(carFromDB.Name, updateCarFromDB.Name);
            controller.Update(carFromDB.Id, carFromDB.Name, carFromDB.Description);
        }
    }
}
