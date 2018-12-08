using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1
{

    public static class SessionExtensions
    {
        public static void Set<T>(this HttpSessionStateBase session, string key, T value)
        {
            session[key] = value;
        }
        public static T Get<T>(this HttpSessionStateBase session, string key)
        {
            T value;
            try {
                value = (T)session[key];
            }
            catch(NullReferenceException)
            {
                value = default(T);
            }
            return value;
            //return (T)session[key];
        }
    }
}
