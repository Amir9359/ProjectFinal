using System.Collections.Generic;
using System.Linq;
using Application.Interfaces.Contexts;
using AutoMapper;
using Domain.Users;
using MongoDB.Driver.Linq;

namespace Application.Users
{
    public interface IUserAddressService
    {
        List<UserAddressDto> GetAddresses(string userId);
        void AddNewAddress(AddUserAddressDto addressDto);
    }

    public class UserAddressService : IUserAddressService
    {
        private readonly IDatabaseContext _context;
        private readonly IMapper _mapper;

        public UserAddressService(IDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<UserAddressDto> GetAddresses(string userId)
        {
            var Addresses = _context.UserAddresses.Where(s => s.UserId == userId).ToList();
            var data = _mapper.Map<List<UserAddressDto>>(Addresses);
            return data;
        }

        public void AddNewAddress(AddUserAddressDto addressDto)
        {
            var Address = _mapper.Map<UserAddress>(addressDto);
            _context.UserAddresses.Add(Address);
            _context.SaveChanges();
        }
    }

    public class UserAddressDto
    {
        public int Id { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string PostalAddres { get; set; }
        public string ReciverName { get; set; }
    }
    public class AddUserAddressDto
    {
        public string State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string PostalAddres { get; set; }
        public string ReciverName { get; set; }
        public string UserId { get; set; }

    }
}