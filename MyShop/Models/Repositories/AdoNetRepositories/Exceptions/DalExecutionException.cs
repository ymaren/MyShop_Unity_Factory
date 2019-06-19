using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models.Repositories.AdoNetRepositories.Exceptions
{
    public class DalExecutionException : Exception
    {
        public DalExecutionException() { }

        public DalExecutionException(string message) : base(message) { }

        public DalExecutionException(string message, Exception exception) : base(message, exception) { }
    }
}