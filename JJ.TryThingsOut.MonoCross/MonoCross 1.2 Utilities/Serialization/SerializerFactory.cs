using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Reflection;
using System.Runtime.Serialization;

namespace MonoCross.Utilities.Serialization
{
    public static class SerializerFactory
    {
        /// <summary>
        /// returns an instance of the default <see cref="ISerializer"/> (i.e. <see cref="SerializerXml"/> ).
        /// </summary>
        /// <typeparam name="T">Type for which to support serialization</typeparam>
        /// <returns></returns>
        public static ISerializer<T> Create<T>() 
        {
            // default ISerializer to XML
            return Create<T>( SerializationFormat.XML );
        }

        /// <summary>
        /// returns an instance of <see cref="ISerializer"/> for given <see cref="SerializationFormat"/>.
        /// </summary>
        /// <typeparam name="T">Type for which to support serialization</typeparam>
        /// <param name="format">Format for which to provide serialization (i.e. <see cref="SerializationFormat"/> )</param>
        /// <returns></returns>
        public static ISerializer<T> Create<T>(SerializationFormat format)
        {
            switch ( format )
            {
                case SerializationFormat.XML:
                    return new SerializerXml<T>();
                case SerializationFormat.JSON:
                    return new SerializerJson<T>();
            }
            return null;
        }
        

        //public virtual XmlSerializer GetObjectSerializer()
        //{
        //    Type[] subtypes = GetSerializationTypes( typeof( T ) );
        //    return subtypes.Count() > 0 ? new XmlSerializer( typeof( T ), subtypes ) : new XmlSerializer( typeof( T ) );
        //}
        //public virtual XmlSerializer GetListSerializer()
        //{
        //    Type[] subtypes = GetSerializationTypes( typeof( T ) );
        //    return subtypes.Count() > 0 ? new XmlSerializer( typeof( List<T> ), subtypes ) : new XmlSerializer( typeof( List<T> ) );
        //}

        //private Type[] GetSerializationTypes( Type type )
        //{
        //    List<Type> types = new List<Type>();
        //    PropertyInfo[] props = type.GetProperties();
        //    foreach ( PropertyInfo prop in props )
        //    {
        //        if ( prop.PropertyType.Module.Name != "mscorlib.dll" )
        //        {
        //            types.Add( prop.PropertyType );
        //            GetSerializationTypes( prop.PropertyType );
        //        }
        //    }
        //    return types.ToArray();
        //}


    }
}
