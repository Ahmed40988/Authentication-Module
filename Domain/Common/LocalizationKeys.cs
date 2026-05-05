using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public static class LocalizationKeys
    {
        public const string Required = "CommonRequired";
        public const string Invalid = "CommonInvalid";
        public const string NotFound = "CommonNotFound";
        public const string CannotDelete = "CommonCannotDelete";

        public const string InvalidStatus = "EmployeeInvalidStatus";
        public const string InvalidQuantity = "ProductInvalidQuantity";
        public const string NotEnoughStock = "ProductNotEnoughStock";
        public const string InvalidDate = "InvalidDate";
        public const string AlreadyExists = "AlreadyExists";
    }
}
