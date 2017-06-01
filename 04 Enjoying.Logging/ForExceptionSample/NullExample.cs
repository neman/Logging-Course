using System;
using System.Collections.Generic;
using System.Text;

namespace ForExceptionSample
{
    public class NullExample
    {

        public NullExample Create()
        {
            return new NullExample();
        }

        public NullExample GetFirst()
        {
            return this;
        }

        public NullExample GetSecond()
        {
            return null;
        }

        public NullExample GetThird()
        {
            return this;
        }


    }
}
