using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DND_Character_Sheet.Useful_Methods
{
    public static class AccessClassProperties
    {
        public static object GetNestedPropertyValue(object obj, string name)
        {
            foreach (string part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        public static void SetNestedPropertyValue(object obj, string name, object value)
        {
            //PropertyInfo info = default(PropertyInfo);
            //object o = obj;
            //var properties = name.Split('.');
            //foreach (string part in name.Split('.'))
            //{
            //    info = o.GetType().GetProperty(part);
            //    o = info.GetValue(o, null);
            //}


            //var newInfo = o.GetType().GetProperty(properties[properties.Length - 1]);
            //newInfo.SetValue(obj, value, null);


            //info.SetValue(obj, value, null);


            string[] bits = name.Split('.');
            for (int i = 0; i < bits.Length - 1; i++)
            {
                PropertyInfo propertyToGet = obj.GetType().GetProperty(bits[i]);
                obj = propertyToGet.GetValue(obj, null);
            }
            PropertyInfo propertyToSet = obj.GetType().GetProperty(bits.Last());

            if (propertyToSet.PropertyType.Name.ToLower().Contains("int"))
            {
                propertyToSet.SetValue(obj, int.Parse(string.Concat(value.ToString().Where(x => char.IsDigit(x)))), null);
            }
            else
                propertyToSet.SetValue(obj, value, null);
            //propertyToSet.SetValue(obj, value, null);
        }
    }
}
