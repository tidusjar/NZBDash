#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: MappingHelper.cs
//  Created By: Jamie Rees
// 
//  Permission is hereby granted, free of charge, to any person obtaining
//  a copy of this software and associated documentation files (the
//  "Software"), to deal in the Software without restriction, including
//  without limitation the rights to use, copy, modify, merge, publish,
//  distribute, sublicense, and/or sell copies of the Software, and to
//  permit persons to whom the Software is furnished to do so, subject to
//  the following conditions:
//  
//  The above copyright notice and this permission notice shall be
//  included in all copies or substantial portions of the Software.
//  
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//  LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//  OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//  WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  ***********************************************************************
#endregion
using System;
using System.Linq;

namespace NZBDash.Common.Mapping
{
    public static class MappingHelper
    {
        /// <summary>
        /// Maps the matching properties from one type to the other by name.
        /// <para>If the name does not match then it will not be mapped.</para>
        /// <para>This class was intended to be used in a class that derives from "IValueInjection" </para>
        /// </summary>
        /// <param name="target">The target object.</param>
        /// <param name="source">The source object.</param>
        /// <exception cref="System.InvalidCastException"> If there is no cast type then this will be thrown</exception>
        public static void MapMatchingProperties(object target, object source)
        {
            var sourceProps = source.GetType().GetProperties();
            var targetProps = target.GetType().GetProperties();

            for (var i = 0; i < targetProps.Count(); i++)
            {
                var sourceProperty = sourceProps.FirstOrDefault(x => String.Equals(x.Name, targetProps[i].Name, StringComparison.InvariantCultureIgnoreCase));
                if (sourceProperty != null)
                {
                    var objVal = GetPropValue(source, sourceProperty.Name);

                    var fullProperty = target.GetType().GetProperty(targetProps[i].Name);
                    var newType = fullProperty.PropertyType;
                    object castType = null;

                    if (newType == typeof(int))
                    {
                        castType = (int)objVal;
                    }
                    else if (newType == typeof(bool))
                    {
                        castType = (bool)objVal;
                    }
                    else if (newType == typeof(string))
                    {
                        castType = (string)objVal;
                    }
                    else if (newType == typeof(long))
                    {
                        castType = (long)objVal;
                    }

                    if (castType == null)
                    {
                        throw new InvalidCastException(string.Format("Could not find the correct cast type for property: {0}, type: {1}", targetProps[i].Name, fullProperty.PropertyType));
                    }

                    var val = castType;
                    SetPropValue(target, val, targetProps[i].Name);
                }
            }
        }

        private static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        private static void SetPropValue(object target, object value, string propName)
        {
            var prop = target.GetType().GetProperty(propName);
            prop.SetValue(target, value);
        }
    }
}
