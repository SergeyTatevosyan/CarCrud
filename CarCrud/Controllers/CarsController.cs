using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarCrud.Models;
using CarCrud.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarCrud.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly CarService _carService;

        public CarsController(CarService carService)
        {
            _carService = carService;
        }


        [HttpGet]
        public ActionResult<List<Car>> GetAllCars() =>
            _carService.Get();

        [HttpGet (Name = "GetCar")]
        public ActionResult<Car> Get(int id)
        {
            var car = _carService.Get(id);

            if (car == null)
            {
                return BadRequest($"В базе данных отсутсвует автомобиль с id:{id}");
            }

            return car;
        }

        [HttpPost]
        public ActionResult<Car> Create(Car car)
        {
            _carService.Create(car);

            return CreatedAtRoute("GetCar", new Car(car.Id, car.Name, car.Description), car);
        }

        [HttpPut]
        public IActionResult Update(int id, string name, string description)
        {
            if (id == 0 && String.IsNullOrEmpty(name) && String.IsNullOrEmpty(description))
            {
                return BadRequest("Отсутвует информация для обновления");
            }
            else
            {
                _carService.Update(new Car(id, name, description));
                return Ok("Информация успешно обновлена");
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var car = _carService.Get(id);

            if (car == null)
            {
                return BadRequest($"В Базе данных не найден автомобиль с id: {id} для удаления");
            }
            else
            {
                _carService.Remove(car.Id);
                return Ok($"Информация по автомобилю с id {id} успешно удалена");
            }
        }


    }
}
