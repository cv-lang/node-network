using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.NodeNetwork.Test
{
    public class TestService : ITestService
    {
        public int Ping(int dataToResend)
        {
            return dataToResend;
        }
    }
}
