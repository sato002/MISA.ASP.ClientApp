using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ASP.ClientApp.Models.Exceptions
{
    public class NotFoundSessionIdException : Exception
    {
    }

    public class NotFoundProcessIdException : Exception
    {
    }

    public class InvalidCaptchaException : Exception
    {
    }

    public class InvalidIdentityException : Exception, IBreakRetryException
    {
    }

    public class UnRecognizedMainPageException : Exception
    {
    }

    public class InvalidTaxCodeException : Exception, IBreakRetryException
    {

    }

    public class PasswordExpiredException : Exception, IBreakRetryException
    {
    }

    public class UnResolvedCaptchaException : Exception
    {
    }
}
