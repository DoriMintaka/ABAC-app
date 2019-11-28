using System;

namespace ABAC.DAL.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException() : base("Requested entity already exists.")
        {

        }
    }
}
