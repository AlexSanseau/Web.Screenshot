
using System;

namespace Alx.Web
{
    public class DeviceSizeException : Exception
    {
        public DeviceSizeException(Devices device)
            : base("Could not get device size for: " + device)
        { }
    }
}
