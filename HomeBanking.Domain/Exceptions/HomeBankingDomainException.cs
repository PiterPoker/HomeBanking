using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBanking.Domain.Exceptions
{
    /// <summary>
    /// Exception type for domain exceptions
    /// </summary>
    public class HomeBankingDomainException : Exception
    {
        public HomeBankingDomainException()
        { }

        public HomeBankingDomainException(string message)
            : base(message)
        { }

        public HomeBankingDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
