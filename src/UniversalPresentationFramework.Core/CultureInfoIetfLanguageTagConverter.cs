﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Wodsoft.UI
{
    /// <summary>
    /// CultureInfoIetfLanguageTagConverter - Type converter for converting instances of other types to and from CultureInfo.
    /// </summary>
    /// <remarks>
    /// This class differs in two ways from System.ComponentModel.CultureInfoConverter, the default type converter 
    /// for the CultureInfo class. First, it uses a string representation based on the IetfLanguageTag property 
    /// rather than the Name property (i.e., RFC 3066 rather than RFC 1766). Second, when converting from a string, 
    /// the properties of the resulting CultureInfo object depend only on the string and not on user overrides set 
    /// in Control Panel. This makes it possible to create documents the appearance of which do not depend on 
    /// local settings.
    /// </remarks>
    public class CultureInfoIetfLanguageTagConverter : TypeConverter
    {
        //-------------------------------------------------------------------
        //
        //  Public Methods
        //
        //-------------------------------------------------------------------

        #region Public Methods

        /// <summary>
        /// CanConvertFrom - Returns whether or not this class can convert from a given type.
        /// </summary>
        /// <returns>
        /// bool - True if this converter can convert from the provided type, false if not.
        /// </returns>
        /// <param name="typeDescriptorContext"> The ITypeDescriptorContext for this call. </param>
        /// <param name="sourceType"> The Type being queried for support. </param>
        public override bool CanConvertFrom(ITypeDescriptorContext? typeDescriptorContext, Type sourceType)
        {
            // We can only handle strings.
            return sourceType == typeof(string);
        }

        /// <summary>
        /// CanConvertTo - Returns whether or not this class can convert to a given type.
        /// </summary>
        /// <returns>
        /// bool - True if this converter can convert to the provided type, false if not.
        /// </returns>
        /// <param name="typeDescriptorContext"> The ITypeDescriptorContext for this call. </param>
        /// <param name="destinationType"> The Type being queried for support. </param>
        public override bool CanConvertTo(ITypeDescriptorContext? typeDescriptorContext, Type? destinationType)
        {
            // We can convert to an InstanceDescriptor or to a string.
            return destinationType == typeof(InstanceDescriptor) ||
                destinationType == typeof(string);
        }

        /// <summary>
        /// ConvertFrom - Attempt to convert to a CultureInfo from the given object
        /// </summary>
        /// <returns>
        /// A CultureInfo object based on the specified culture name.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// An ArgumentNullException is thrown if the example object is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// An ArgumentException is thrown if the example object is not null and is not a valid type
        /// which can be converted to a CultureInfo.
        /// </exception>
        /// <param name="typeDescriptorContext">The ITypeDescriptorContext for this call.</param>
        /// <param name="cultureInfo">The CultureInfo which is respected when converting.</param>
        /// <param name="source">The object to convert to a CultureInfo.</param>
        public override object ConvertFrom(ITypeDescriptorContext? typeDescriptorContext,
                                           CultureInfo? cultureInfo,
                                           object source)
        {
            if (source is string cultureName)
            {
                return CultureInfo.GetCultureInfoByIetfLanguageTag(cultureName);
            }

            throw GetConvertFromException(source);
        }

        /// <summary>
        /// ConvertTo - Attempt to convert a CultureInfo to the given type
        /// </summary>
        /// <returns>
        /// The object which was constructed.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// An ArgumentNullException is thrown if the example object is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// An ArgumentException is thrown if the example object is not null and is not a CultureInfo,
        /// or if the destinationType isn't one of the valid destination types.
        /// </exception>
        /// <param name="typeDescriptorContext"> The ITypeDescriptorContext for this call. </param>
        /// <param name="cultureInfo"> The CultureInfo which is respected when converting. </param>
        /// <param name="value"> The double to convert. </param>
        /// <param name="destinationType">The type to which to convert the CultureInfo. </param>
        public override object? ConvertTo(ITypeDescriptorContext ?typeDescriptorContext,
                                         CultureInfo? cultureInfo,
                                         object? value,
                                         Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }

            if ( value is CultureInfo culture)
            {
                if (destinationType == typeof(string))
                {
                    return culture.IetfLanguageTag;
                }
                else if (destinationType == typeof(InstanceDescriptor))
                {
                    MethodInfo method = typeof(CultureInfo).GetMethod(
                        "GetCultureInfo",
                        BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public,
                        null, // use default binder
                        new Type[] { typeof(string) },
                        null  // default binder doesn't use parameter modifiers
                        )!;

                    return new InstanceDescriptor(method, new object[] { culture.Name });
                }
            }

            throw GetConvertToException(value, destinationType);
        }
        #endregion 
    }
}
