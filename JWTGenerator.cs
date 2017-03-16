using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Newtonsoft.Json.Linq;
using JWT;
using Newtonsoft.Json;

namespace WebsiteAlive.COM.JWTGenerator
{
    [ComVisible(true)]
    [Guid("1aee9ae3-4302-4462-a612-039b9201e6bb")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("WebsiteAlive.COM.JWTGenerator")]
    public class JWTGenerator : JWTComInterface
    {
        string secret = "jfUR43@fls8sdfKJGNKJK09NHbbf43566df";
        public JWTGenerator()
        {
           
        }
        [ComVisible(true)]
        public string getJWTSession(string jwttoken)
        {
            string outputPayLoad = "";
            string output = ""; 
            try
            {
                outputPayLoad = JWT.JsonWebToken.Decode(jwttoken, secret, true);
            }
            catch(Exception ex)
            {
                dynamic outputNotValid = new { IsValid = false };
                output = JsonConvert.SerializeObject(outputNotValid);
                return output; 
            }
            JObject jsonPayload = JObject.Parse(outputPayLoad);
            dynamic outputIsValid = new {
                IsValid = true,
                objectref = jsonPayload.GetValue("objectref").ToString(),
                groupid = Convert.ToInt32(jsonPayload.GetValue("groupid")),
                operatorid = Convert.ToInt32(jsonPayload.GetValue("operatorid")),
                sessionid = Convert.ToInt32(jsonPayload.GetValue("sessionid"))
            };
            output = JsonConvert.SerializeObject(outputIsValid);
            return output;

        }

        [ComVisible(true)]
        public string setJWTSession(string objectref, int groupid, int operatorid, int sessionid)
        {
            var utc0 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var issueTime = DateTime.Now;

            var iat = (int)issueTime.Subtract(utc0).TotalSeconds;
            var exp = (int)issueTime.AddDays(3).Subtract(utc0).TotalSeconds; // Expiration time 3 days from creation of token

            var payload = new Dictionary<string, object>
            {
                {   "objectref",    objectref       },
                {   "groupid",      groupid         },
                {   "operatorid",   operatorid      },
                {   "sessionid",    sessionid       },
                {   "exp",          exp             },
                {   "iat",          iat             }
            };
            //string secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
            byte[] toBytes = Encoding.ASCII.GetBytes(secret);
            var token = JWT.JsonWebToken.Encode(payload, toBytes, new JWT.JwtHashAlgorithm()); 
            return token;
        }


    }
}
