//Binary Serialization To xml file - code example
//Farid Naisan 2010
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SerializationExProjCS
{
    /// <summary>
    /// This class contains methods for xml serialization of any 
    /// type of objects.
    /// </summary>
    /// <remarks></remarks>
    public class XMLSerialization
    {
        /// <summary>
        /// A generic method that can be used to serialize any type of object.
        /// The type of object is defined at method call by the client object
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="filePath">File to which data is to be srialized</param>
        /// <param name="obj">Object containing data to be serialized. This object 
        /// must be Serializable.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool SerializeToFile<T>(string filePath, T obj)
        {
            bool bok = true;

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            TextWriter writer = new StreamWriter(filePath);
            try
            {
                serializer.Serialize(writer, obj);
            }
            catch
            {
                bok = false;
            }
            finally
            {
                if (writer != null)
            
                    writer.Close();                
            }

            return bok;
        }

        /// <summary>
        /// Deserialize any xml file serialized  using the above method.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="filePath">XML file to be deserialized.</param>
        /// <returns>The object containing data read from the xml file.</returns>
        /// <remarks>Object must not have changed its structure since it was serilized calling
        /// the above method.</remarks>
        public static T DeserializeFromFile<T>(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            object obj = null;
            //to be returned with data

            TextReader reader = null;

            try
            {
                reader = new StreamReader(filePath);
                obj = (T)serializer.Deserialize(reader);
            }
            catch
            {

            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return (T)obj;
        }

    }
}
