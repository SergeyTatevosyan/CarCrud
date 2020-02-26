using System;
using System.Collections.Generic;
using System.Linq;
using CarCrud.Models;
using MongoDB.Driver;

namespace CarCrud.Services
{
    public class CarService
    {
        private readonly IMongoCollection<Car> _cars;

        public CarService(ICarsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _cars = database.GetCollection<Car>(settings.CarsCollectionName);
        }

        public Car Create(Car Car)
        {
            _cars.InsertOne(Car);
            return Car;
        }

        public List<Car> Get() =>
            _cars.Find(Car => true).ToList();

        public Car Get(int id) =>
            _cars.Find<Car>(Car => Car.Id == id).FirstOrDefault();

        public void Update(Car CarIn)
        {
            var existingCar = Get(CarIn.Id);
            if (existingCar == null)
            {//Если ТС не существует - создаем новую
                CarIn.Id = Get().Max(c=>c.Id) + 1;
                Create(CarIn);
            }
            else
            {//Проверяем все ли поля заполнены
                if (String.IsNullOrEmpty(CarIn.Name))
                {
                    CarIn.Name = existingCar.Name;
                }
                if (String.IsNullOrEmpty(CarIn.Description))
                {
                    CarIn.Description = existingCar.Description;
                }
                _cars.ReplaceOne(Car => Car.Id == CarIn.Id, CarIn);
            }
        }

        public void Remove(Car CarIn) =>
            _cars.DeleteOne(Car => Car.Id == CarIn.Id);

        public void Remove(int id) =>
            _cars.DeleteOne(Car => Car.Id == id);
    }
}
