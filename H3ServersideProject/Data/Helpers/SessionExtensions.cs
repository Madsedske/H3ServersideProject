using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace H3ServersideProject.Helpers
{
    public static class SessionExtensions 
    { 
        public static void SetObjectAsJson(this ISession session, string key, object value) 
        { 
            session.SetString(key, JsonConvert.SerializeObject(value)); 
        } 
        public static T? GetObjectFromJson<T>(this ISession session, string key) 
        { 
            var value = session.GetString(key); 
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value); 
        }



        //[HttpGet("set/{data}")]
        //public IActionResult setsession(string data)
        //{
        //    HttpContext.Session.SetString("keyname", data);
        //    return Ok("session data set");
        //}

        //[HttpGet("get")]
        //public IActionResult getsessiondata()
        //{
        //    var sessionData = HttpContext.Session.GetString("keyname");
        //    return Ok(sessionData);
        //}
    }
}
