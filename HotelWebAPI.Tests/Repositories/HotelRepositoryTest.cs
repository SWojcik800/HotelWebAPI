using HotelWebApi.ApplicationCore.Entities.ApiData;
using HotelWebApi.ApplicationCore.Repositories;
using HotelWebAPI.Tests.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit;
using static Xunit.Assert;

namespace HotelWebAPI.Tests
{
    public class HotelRepositoryTest : HotelAPIDbTest
    {
        private readonly HotelRepository _sut;

        public HotelRepositoryTest() : base()
        {
            _sut = new HotelRepository(context);
        }

        [Fact]
        public async void GetAll_ReturnsAllHotelsWithAddresses()
        {
            var hotels = await _sut.GetAll();
            var firstHotelAddress = hotels[0].Address;


            IsType<List<Hotel>>(hotels);
            NotNull(firstHotelAddress);
            Equal("Generic city 1", firstHotelAddress.City);

            Equal<int>(3, hotels.Count);

        }


        [Fact]
        public async void GetById_ReturnsHotelIfExists()
        {
            var firstExisting = await context.Hotels.FirstAsync();
            var hotel = await _sut.GetById(firstExisting.Id);

            IsType<Hotel>(hotel);
            NotNull(hotel);
            Equal("hotel 1", hotel.Name);
        }

        [Fact]
        public async void GetById_ReturnsNullIfHotelDoesNotExist()
        {
            var nonExistentId = 0;
            var hotel = await _sut.GetById(nonExistentId);

            Null(hotel);
        }

        [Fact]
        public async void Create_CreatesNewHotel()
        {
            var hotelToCreate = new Hotel()
            {
                Name = "hotel 4",
                Description = "hotel 4 desc",
                Stars = 5,
                ContactEmail = "mock4@email.com",
                PhoneNumber = "192 168 004",
                Address = new Address()
                {
                    City = "Generic city 4",
                    Street = "Generic street",
                    ZipCode = "34-779"
                }
            };

            var result = await _sut.Create(hotelToCreate);

            IsType<Hotel>(result);
            Equal<Hotel>(hotelToCreate, result);

        }

        [Fact]

        public async void Update_UpdatesExistingHotel()
        {
            var hotelToUpdate = await context.Hotels.FirstAsync();
            var hotelId = hotelToUpdate.Id;
            hotelToUpdate.Name = "Updated name";

            await _sut.Update(hotelId, hotelToUpdate);

            var hotelAfterUpdate = context.Hotels.Find(hotelId);

            IsType<Hotel>(hotelAfterUpdate);
            Equal("Updated name", hotelAfterUpdate.Name);

        }

        [Fact]

        public async void Update_ReturnsNullWhenHotelIdsDontMatch()
        {
            var hotelToUpdate = await context.Hotels.FirstAsync();
            var nonExistentHotelId = hotelToUpdate.Id + 1;
            hotelToUpdate.Name = "Updated name";

            var result = await _sut.Update(nonExistentHotelId, hotelToUpdate);

            Null(result);

        }

        [Fact]

        public async void Update_ReturnsNullWhenHotelDoesNotExist()
        {
            var hotelToUpdate = await context.Hotels.FirstAsync();
            var nonExistentHotelId = 0;
            hotelToUpdate.Id = nonExistentHotelId;
            hotelToUpdate.Name = "Updated name";

            var result = await _sut.Update(nonExistentHotelId, hotelToUpdate);

            Null(result);

        }

        [Fact]

        public async void Delete_DeletesHotelIfExists()
        {
            var beforeDeletion = await context.Hotels.ToListAsync();
            var countBeforeDeletion = beforeDeletion.Count;

            var hotelToDelete = await context.Hotels.FirstAsync();
            var id = hotelToDelete.Id;

            var result = await _sut.Delete(id);

            var afterDeletion = await context.Hotels.ToListAsync();
            var countAfterDeletion = afterDeletion.Count;


            IsType<Hotel>(result);
            Equal<Hotel>(hotelToDelete, result);
            Equal<int>(countBeforeDeletion - 1, countAfterDeletion);

        }

        [Fact]

        public async void Delete_ReturnsNullIfHotelDoesNotExist()
        {
            var nonExistentId = 0;
            var result = await _sut.Delete(nonExistentId);
            Null(result);

        }
    }
}
