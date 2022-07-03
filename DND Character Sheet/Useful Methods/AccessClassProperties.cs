using DND_Character_Sheet.Models.Serialize_Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                CheckForIndexedProperty(part, ref index);

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
            for (int i = 0; i < bits.Length; i++)
            {
                object?[] index = null;
                CheckForIndexedProperty(bits[i], ref index);
                var x = obj.GetType().GetProperties();
                var y = GetPropertyNameWithoutIndex(bits, i);
                //var a = ObservableCollection<object>() { };
                //var z = ((IObservable<object>)obj)[1];
                PropertyInfo propertyToGet = obj.GetType().GetProperty(GetPropertyNameWithoutIndex(bits, i));
                if (propertyToGet == null) { return false; }
                //index = null;
                //index = new object?[] { 2 };

                obj = propertyToGet.GetValue(obj, index);
                //obj = propertyToGet.GetValue(obj, null);

                //var test = (Array)propertyToGet


                //obj = propertyToGet.GetValue(obj, null);
                //obj = (PropertyInfo)obj.GetType().getva .GetValue(obj, index);
                if (obj == null) { return false; }
            }
            PropertyInfo propertyToSet = obj.GetType().GetProperty(bits.Last());
            if (propertyToSet == null) { return false; }

            object?[] finalIndex = null;
            CheckForIndexedProperty(bits[bits.Length - 1], ref finalIndex);

            if (propertyToSet.PropertyType.Name.ToLower().Contains("int"))
                propertyToSet.SetValue(obj, int.Parse(string.Concat(value.ToString().Where(x => char.IsDigit(x)))), null);
            else
                propertyToSet.SetValue(obj, value, finalIndex);

            return true;
        }

        private static string GetPropertyNameWithoutIndex(string[] bits, int i)
            => bits[i].Contains("[") ? bits[i].Remove(bits[i].IndexOf("["), bits[i].IndexOf("]") - bits[i].IndexOf("[") + 1) : bits[i];


        private static void CheckForIndexedProperty(string propertyName, ref object?[] index)
        {
            if (propertyName.Contains("["))
            {
                //index ??= new object?[1];
                index = new object?[] {int.Parse(propertyName.Substring(
                    propertyName.IndexOf("[") + 1, propertyName.IndexOf("]") - propertyName.IndexOf("[") - 1))};
                //bits
            }
        }
    }
}
