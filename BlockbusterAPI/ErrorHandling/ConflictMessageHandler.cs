using BlockbusterAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockbusterAPI.ErrorHandling
{
    public static class ConflictMessageHandler
    {
        public static ConflictMessageError BuildUserConflictError(int error)
        {
            if (error == 3)
                return new ConflictMessageError() { Message = "Username and Email already exists." };
            else if (error == 2)
                return new ConflictMessageError() { Message = "Email already exists." };

            return new ConflictMessageError() { Message = "Username already exists." };
        }
    }
}
