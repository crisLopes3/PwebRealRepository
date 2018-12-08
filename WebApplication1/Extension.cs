using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml.Serialization;
using WebApplication1.Models;

namespace WebApplication1
{
    public static class Extension
    {
        public static Dictionary<Ramo, string> GetRamoDescricoes()
        {
            var valuesAndDescriptions = new Dictionary<Ramo, string>();
   
            Type enumType = typeof(Ramo);

            var enumValues = enumType.GetEnumValues();

            foreach (Ramo value in enumValues)
            {
                NewMethod(valuesAndDescriptions, enumType, value);
            }
            return valuesAndDescriptions;
        }

        private static void NewMethod(Dictionary<Ramo, string> valuesAndDescriptions, Type enumType, Ramo value)
        {
            MemberInfo memberInfo =
                enumType.GetMember(value.ToString()).First();

            var descriptionAttribute =
                memberInfo.GetCustomAttribute<DescriptionAttribute>();

            if (descriptionAttribute != null)
            {
                valuesAndDescriptions.Add(value,descriptionAttribute.Description);
            }
            else
            {
                valuesAndDescriptions.Add(value, value.ToString());
            }
        }

    }
}