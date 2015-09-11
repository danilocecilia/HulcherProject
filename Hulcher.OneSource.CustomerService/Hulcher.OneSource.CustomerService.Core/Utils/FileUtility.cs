using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Hulcher.OneSource.CustomerService.Core.Utils
{
    /// <summary>
    /// Utility class for File Manipulation
    /// </summary>
    public class FileUtility
    {
        /// <summary>
        /// Saves a file based on an input Stream
        /// </summary>
        /// <param name="inputStream">Stream that contains the input data to create a file</param>
        /// <param name="savingPath">Path where the file will be stored</param>
        public static void SaveFile(Stream inputStream, string savingPath)
        {
            using (var reader = new BinaryReader(inputStream))
            {
                int bufferSize = 1024;
                byte[] buffer = new byte[bufferSize];
                int count = 0;
                int offset = 0;

                using (var writingStream =
                    new System.IO.FileStream(savingPath, FileMode.OpenOrCreate))
                {
                    while ((count = reader.Read(buffer, offset, buffer.Length)) > 0)
                    {
                        writingStream.Write(buffer, offset, count);
                    }
                    writingStream.Flush();
                    writingStream.Close();
                }
                reader.Close();
            }
        }
    }
}
