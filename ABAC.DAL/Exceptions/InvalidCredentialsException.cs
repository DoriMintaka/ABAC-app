using System;

namespace ABAC.DAL.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() : base("Invalid credentials.")
        {

        }
    }
}
