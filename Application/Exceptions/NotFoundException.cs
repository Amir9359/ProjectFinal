using System;

namespace Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName , object key) : base($"Entity {entityName} whith key {key} was Not Found.")
        {
            
        }
    }
}