using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DND_Character_Sheet.Useful_Methods
{
    public static class AccessClassProperties
    {
        public static object GetNestedPropertyValue(object obj, string name)
        {
            foreach (string part in name.Split('.'))
            {
                object?[] index = null;
                CheckForIndexedProperty(part, index);

                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, index);
            }
            return obj;
        }

        public static bool SetNestedPropertyValue(object obj, string name, object value)
        {
            if (obj == null) { return false; }

            string[] bits = name.Split('.');
            for (int i = 0; i < bits.Length - 1; i++)
            {
                object?[] index = null;
                CheckForIndexedProperty(bits[i], index);
                PropertyInfo propertyToGet = obj.GetType().GetProperty(bits[i]);
                if (propertyToGet == null) { return false; }

                obj = propertyToGet.GetValue(obj, index);
                if (obj == null) { return false; }
            }
            PropertyInfo propertyToSet = obj.GetType().GetProperty(bits.Last());
            if (propertyToSet == null) { return false; }

            object?[] finalIndex = null;
            CheckForIndexedProperty(bits[bits.Length - 1], finalIndex);

            if (propertyToSet.PropertyType.Name.ToLower().Contains("int"))
                propertyToSet.SetValue(obj, int.Parse(string.Concat(value.ToString().Where(x => char.IsDigit(x)))), null);
            else
                propertyToSet.SetValue(obj, value, finalIndex);

            return true;
        }

        private static void CheckForIndexedProperty(string propertyName, object?[] index)
        {
            if (propertyName.Contains("["))
            {
                index ??= new object?[1];
                index.Append(int.Parse(propertyName.Substring(
                    propertyName.IndexOf("[") + 1, propertyName.IndexOf("]") - propertyName.IndexOf("[") - 1)));
            }
        }
    }
}
