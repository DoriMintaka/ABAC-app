using System;

namespace ABAC.DAL.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException() : base("Forbidden.")
        {

        }
    }
}
