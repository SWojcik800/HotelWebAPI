﻿using System;

namespace HotelWebApi.ApplicationCore.Common.Exceptions
{
    public class ForbidException : Exception
    {
        public ForbidException(string message) : base(message)
        {

        }
    }
}
