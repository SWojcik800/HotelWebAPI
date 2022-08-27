using HotelWebApi.ApplicationCore.Entities.ApiData;
using HotelWebApi.Contracts.Dtos;
using System.Collections.Generic;

namespace HotelWebAPI.Tests.Services.Seeders
{
    public static class HotelSeeder
    {
        public static List<HotelDto> GetHotelDtos()
        {
            return new List<HotelDto>()
            {
                new HotelDto()
            {
                Name = "hotel 4",
                Description = "hotel 4 desc",
                Stars = 5,
                City = "Generic city 4",
                Street = "Generic street",
                ZipCode = "34-780"

            },
                new HotelDto()
            {
                Name = "hotel 4",
                Description = "hotel 4 desc",
                Stars = 5,
                City = "Generic city 4",
                Street = "Generic street",
                ZipCode = "34-780"

            },
                new HotelDto()
            {
                Name = "hotel 4",
                Description = "hotel 4 desc",
                Stars = 5,
                City = "Generic city 4",
                Street = "Generic street",
                ZipCode = "34-780"

            }
        };
        }

        public static List<Hotel> GetHotels()
        {
            return new List<Hotel>()
            {
                new Hotel()
                {
                    Name = "hotel 1",
                    Description = "hotel 1 desc",
                    Stars = 3,
                    ContactEmail = "mock@email.com",
                    PhoneNumber = "192 168 001",
                    Address = new Address()
                    {
                        City = "Generic city 1",
                        Street = "Generic street",
                        ZipCode = "34-777"
                    }
                },
                new Hotel()
                {
                    Name = "hotel 2",
                    Description = "hotel 2 desc",
                    Stars = 4,
                    ContactEmail = "mock2@email.com",
                    PhoneNumber = "192 168 002",
                    Address = new Address()
                    {
                        City = "Generic city 1",
                        Street = "Generic street",
                        ZipCode = "34-778"
                    }
                },
                new Hotel()
                {
                    Name = "hotel 3",
                    Description = "hotel 3 desc",
                    Stars = 5,
                    ContactEmail = "mock3@email.com",
                    PhoneNumber = "192 168 003",
                    Address = new Address()
                    {
                        City = "Generic city 1",
                        Street = "Generic street",
                        ZipCode = "34-779"
                    }
                },
            };
        }

        public static CreateHotelDto GetCreatedHotelDto()
        {
            var createdDto =  new CreateHotelDto()
            {
                Name = "hotel 4",
                Description = "hotel 4 desc",
                Stars = 3,
                PhoneNumber = "192 168 001",
                ContactEmail = "Generic contact email",
                City = "Generic city 4",
                Street = "Generic street",
                ZipCode = "34-780"
            };

            return createdDto;
        }

        public static UpdateHotelDto GetUpdateHotelDto()
        {
            return new UpdateHotelDto()
            {
                Name = "hotel 4",
                Description = "hotel 4 desc",
                Stars = 3
            };
        }
    }
}
