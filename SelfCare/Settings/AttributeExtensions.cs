// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Reflection;

namespace SelfCare.Settings
{
    /// <summary>
    /// Helpful extensions for attributes.
    /// </summary>
    internal static class AttributeExtensions
    {
        /// <summary>
        /// Attempts to retrieve a custom attribute stored with a MemberInfo
        /// instance.
        /// </summary>
        /// <typeparam name="T">The type of attribute to retrieve.</typeparam>
        /// <param name="member">The MemberInfo instance.</param>
        /// <param name="attribute">An out attribute reference.</param>
        /// <returns>Returns true if the attribute is found.</returns>
        public static bool TryGetAttribute<T>(this MemberInfo member, out T attribute)
            where T : Attribute
        {
            Type type = typeof(T);
            attribute = null;

            object[] attributes = member.GetCustomAttributes(type, false);
            if (attributes.Length > 0)
            {
                attribute = attributes[0] as T;
                return (attribute != null);
            }

            return false;
        }
    }
}