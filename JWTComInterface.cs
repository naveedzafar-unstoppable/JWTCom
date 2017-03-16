using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteAlive.COM.JWTGenerator
{
    [ComVisible(true)]
    [Guid("1aee9ae3-4302-4462-a612-039b9201e6aa")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface JWTComInterface
    {
        string setJWTSession(string objectref, int groupid, int operatorid, int sessionid);
        string getJWTSession(string jwtToken);
    }
}
