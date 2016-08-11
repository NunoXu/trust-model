﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrustModel
{
    public class TrusteeNotFoundException : Exception
    {
        public TrusteeNotFoundException() : base() { }
        public TrusteeNotFoundException(string message) : base(message) { }
        public TrusteeNotFoundException(string message, System.Exception inner) : base(message, inner) { }
        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client. 
        protected TrusteeNotFoundException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
        { }
    }
}
