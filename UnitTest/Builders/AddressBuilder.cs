
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Orders;

namespace UnitTest.Builders
{
    public class AddressBuilder
    {
        private Address _address;
        public string TestCity => "Tehran";
        public string TestState => "ValiAsr";
        public string TestZipCode => "275842154";
        public string TestPostalAddress => "Negar Tower";
        public string TestReciverName => "Amir Hossain";


        public AddressBuilder()
        {
            _address = WithDefaultValues();
        }

        public Address Build()
        {
            return _address;
        }
        private Address WithDefaultValues()
        {
            _address = new Address(TestState ,TestCity, TestZipCode, TestPostalAddress , TestReciverName);
            return _address;
        }
    }
}
