// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.ComponentModel;

namespace AgencyPro.Core.Extensions
{
    public static class EnumExtensions
        // ReSharper restore CheckNamespace
    {
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[]) field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        public static string GetName(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            return field.Name;
        }


        //SomeType value = SomeType.Grapes;
        //bool isGrapes = value.Is(SomeType.Grapes); //true
        //bool hasGrapes = value.Has(SomeType.Grapes); //true

        //value = value.Add(SomeType.Oranges);
        //value = value.Add(SomeType.Apples);
        //value = value.Remove(SomeType.Grapes);

        //bool hasOranges = value.Has(SomeType.Oranges); //true
        //bool isApples = value.Is(SomeType.Apples); //false
        //bool hasGrapes = value.Has(SomeType.Grapes); //false

        public static bool Has<T>(this Enum type, T value)
        {
            try
            {
                return ((int) (object) type & (int) (object) value) == (int) (object) value;
            }
            catch
            {
                return false;
            }
        }

        public static bool Is<T>(this Enum type, T value)
        {
            try
            {
                return (int) (object) type == (int) (object) value;
            }
            catch
            {
                return false;
            }
        }


        public static T Add<T>(this Enum type, T value)
        {
            try
            {
                return (T) (object) ((int) (object) type | (int) (object) value);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format(
                        "Could not append value from enumerated type '{0}'.",
                        typeof(T).Name
                    ), ex);
            }
        }


        public static T Remove<T>(this Enum type, T value)
        {
            try
            {
                return (T) (object) ((int) (object) type & ~(int) (object) value);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    string.Format(
                        "Could not remove value from enumerated type '{0}'.",
                        typeof(T).Name
                    ), ex);
            }
        }
    }
}