using Alisio.Putman.UtilMethods.ErrorManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Alisio.Putman.UtilMethods
{
    /// <summary>
    /// This class adds methods to the <seealso cref="System.Type"/> class.
    /// </summary>
    public static class UMTypeT
    {
        private static UMErrorHandler ErrorMessage { get; set; }

        /// <summary>
        /// This method returns the error. If everything was succesful this will return null.
        /// </summary>
        /// <returns>string</returns>

        public static String GetErrorMessage()
        {
            if (ErrorMessage != null)
                return ErrorMessage.Error;

            return null;
        }

        /// <summary>
        /// This methods executes a method and returns a value if that methods returns one.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x">The class the method exists</param>
        /// <param name="assemblyName">The assembly you execute this method from.</param>
        /// <param name="methodName">The method name that is in specified class.</param>
        /// <param name="paramaters">The parameters the specefied method has.</param>
        /// <returns>object</returns>
        public static object GetValueMethod<T>(T x, string assemblyName, string methodName, object[] paramaters)
        {
            //int integer = (int)GetValueMethod(typeof(A), "getint", new object[] { });
            try
            {
                BindingFlags flags = (BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Static);
                Type type = Type.GetType(assemblyName);

                if (type != null)
                    foreach (MethodInfo method in type.GetMethods(flags))
                        if (method.Name == methodName)
                            return method.Invoke(Activator.CreateInstance(type), paramaters);
            }
            catch (Exception e)
            {
                ErrorMessage = new UMErrorHandler();
                ErrorMessage.Error = e.Message;
                return null;
            }

            return null;
        }

        /// <summary>
        /// This methods executes a method and returns the field.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="x">The class the method exists</param>
        /// <param name="assemblyName">The assembly you execute this method from.</param>
        /// <param name="fieldName"></param>
        /// <returns>object</returns>
        public static object GetValueField<T>(T x, string assemblyName, string fieldName)
        {
            try
            {
                BindingFlags flags = (BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Static);
                Type type = Type.GetType(assemblyName);

                if (type != null)
                    foreach (FieldInfo field in type.GetFields(flags))
                        if (field.Name == fieldName)
                            return field.GetValue(Activator.CreateInstance(type));
            }
            catch (Exception e)
            {
                ErrorMessage = new UMErrorHandler();
                ErrorMessage.Error = e.Message;
                return null;
            }

            return null;
        }
    }
}
