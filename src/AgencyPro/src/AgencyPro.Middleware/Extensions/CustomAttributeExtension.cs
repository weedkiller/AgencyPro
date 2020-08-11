// // Copyright (c) Rod Johnson & IdeaFortune. All rights reserved.
// // Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using System;
using System.Linq;
using System.Reflection;

namespace AgencyPro.Middleware.Extensions
{
    /// <summary>
    /// </summary>
    public class CustomAttributeExtension
    {
        /// <summary>
        ///     Tries the get attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="memberInfo">The member information.</param>
        /// <param name="customAttribute">The custom attribute.</param>
        /// <returns></returns>
        public static bool TryGetAttribute<T>(MemberInfo memberInfo, out T customAttribute) where T : Attribute
        {
            var attributes = memberInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();
            if (attributes == null)
            {
                customAttribute = null;
                return false;
            }

            customAttribute = (T) attributes;
            return true;
        }
    }

    public static class AttributeExtensions
    {
        public static TValue GetAttributeValue<TAttribute, TValue>(
            this Type type,
            Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            var attribute = type.GetTypeInfo().GetCustomAttribute<TAttribute>();
            return attribute != null ? valueSelector(attribute) : default;
        }
    }
}