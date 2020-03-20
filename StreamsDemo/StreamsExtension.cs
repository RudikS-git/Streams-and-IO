using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace StreamsDemo
{
    // C# 6.0 in a Nutshell. Joseph Albahari, Ben Albahari. O'Reilly Media. 2015
    // Chapter 15: Streams and I/O
    // Chapter 6: Framework Fundamentals - Text Encodings and Unicode
    // https://msdn.microsoft.com/ru-ru/library/system.text.encoding(v=vs.110).aspx

    public static class StreamsExtension
    {

        #region Public members

        #region TODO: Implement by byte copy logic using class FileStream as a backing store stream .

        public static int ByByteCopy(string sourcePath, string destinationPath)
        {
            byte[] byteArray;

            using (FileStream fileStream = new FileStream(sourcePath, FileMode.Open))
            {
                byteArray = new byte[fileStream.Length];

                fileStream.Read(byteArray, 0, byteArray.Length);
            }

            using (FileStream fileStreamSecond = new FileStream(destinationPath, FileMode.Create))
            {
                fileStreamSecond.Write(byteArray, 0, byteArray.Length);
            }

            return byteArray.Length;

        }

        #endregion

        #region TODO: Implement by byte copy logic using class MemoryStream as a backing store stream.

        public static int InMemoryByByteCopy(string sourcePath, string destinationPath)
        {
            // TODO: step 1. Use StreamReader to read entire file in string

            // TODO: step 2. Create byte array on base string content - use  System.Text.Encoding class

            // TODO: step 3. Use MemoryStream instance to read from byte array (from step 2)

            // TODO: step 4. Use MemoryStream instance (from step 3) to write it content in new byte array

            // TODO: step 5. Use Encoding class instance (from step 2) to create char array on byte array content

            // TODO: step 6. Use StreamWriter here to write char array content in new file

            MemoryStream memoryStream;

            using (StreamReader streamReader = new StreamReader(sourcePath, Encoding.UTF8))
            {
                byte [] byteArray = Encoding.Default.GetBytes(streamReader.ReadToEnd());
                memoryStream = new MemoryStream(byteArray);
            }

            using(StreamWriter streamWriter = new StreamWriter(destinationPath, false, Encoding.Default))
            {
                streamWriter.Write(Encoding.Default.GetChars(memoryStream.ToArray()));
            }

            return (int)memoryStream.Length;
        }

        #endregion

        #region TODO: Implement by block copy logic using FileStream buffer.

        public static int ByBlockCopy(string sourcePath, string destinationPath)
        {
            int length;

            using(FileStream fileStream = new FileStream(sourcePath, FileMode.Open))
            {
                using (FileStream fileStreamSecond = new FileStream(destinationPath, FileMode.Create))
                {
                    fileStream.CopyTo(fileStreamSecond);
                    length = (int)fileStreamSecond.Length;
                }
            }

            return length;
        }

        #endregion

        #region TODO: Implement by block copy logic using MemoryStream.

        public static int InMemoryByBlockCopy(string sourcePath, string destinationPath)
        {
            // TODO: Use InMemoryByByteCopy method's approach
            MemoryStream memoryStream;

            using (FileStream fileStream = new FileStream(sourcePath, FileMode.Open))
            {
                byte [] byteArray = new byte[fileStream.Length];
                fileStream.Read(byteArray, 0, byteArray.Length);

                memoryStream = new MemoryStream(byteArray);
            }

            using (FileStream fileStreamSecond = new FileStream(destinationPath, FileMode.Create))
            {
                fileStreamSecond.Write(memoryStream.ToArray(), 0, (int)memoryStream.Length);
            }

            return (int)memoryStream.Length;
        }

        #endregion

        #region TODO: Implement by block copy logic using class-decorator BufferedStream.

        public static int BufferedCopy(string sourcePath, string destinationPath)
        {
            byte[] byteArray;

            using (FileStream fileStream = new FileStream(sourcePath, FileMode.Open))
            {
                using (BufferedStream bufferedStream = new BufferedStream(fileStream))
                {
                    byteArray = new byte[fileStream.Length];
                    bufferedStream.Read(byteArray, 0, byteArray.Length);
                }
            }

            using (FileStream fileStreamSecond = new FileStream(destinationPath, FileMode.Create))
            {
                fileStreamSecond.Write(byteArray, 0, byteArray.Length);
            }

            return byteArray.Length;
        }

        #endregion

        #region TODO: Implement by line copy logic using FileStream and classes text-adapters StreamReader/StreamWriter

        public static int ByLineCopy(string sourcePath, string destinationPath)
        {
            int count = 0;

            using (StreamReader streamReader = new StreamReader(sourcePath))
            {
                using(StreamWriter streamWriter = new StreamWriter(destinationPath))
                {
                    string textLine;

                    while ((textLine = streamReader.ReadLine()) != null)
                    {
                        streamWriter.WriteLine(textLine);
                        count++;
                    }
                }
            }

            return count;
        }

        #endregion

        #region TODO: Implement content comparison logic of two files 

        public static bool IsContentEquals(string sourcePath, string destinationPath)
        {
            byte[] byteArrayFirst;
            byte[] byteArraySecond;

            using (FileStream fileStream = new FileStream(sourcePath, FileMode.Open))
            {
                byteArrayFirst = new byte[fileStream.Length];
                fileStream.Read(byteArrayFirst, 0, byteArrayFirst.Length);
            }

            using (FileStream fileStreamSecond = new FileStream(destinationPath, FileMode.Open))
            {
                byteArraySecond = new byte[fileStreamSecond.Length];
                fileStreamSecond.Read(byteArraySecond, 0, byteArraySecond.Length);
            }

            if(byteArrayFirst.Length != byteArraySecond.Length)
            {
                if(byteArraySecond[byteArraySecond.Length - 1] != '\n')
                {
                    return false;
                }
            }

            for(int i = 0; i < byteArrayFirst.Length; i++)
            {
                if(byteArrayFirst[i] != byteArraySecond[i])
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #endregion

        #region Private members

        #region TODO: Implement validation logic

        private static void InputValidation(string sourcePath, string destinationPath)
        {
            if(!File.Exists(sourcePath))
            {
                throw new ArgumentException("Source path does not exist");
            }

            if ((destinationPath == null) || (destinationPath.IndexOfAny(Path.GetInvalidPathChars()) != -1))
            {
                throw new ArgumentException("Destination path does not correct");
            }
            else
            {
                try
                {
                    var tempFileInfo = new FileInfo(destinationPath);
                }
                catch (NotSupportedException)
                {

                }
            }
        }

        #endregion

        #endregion

    }
}
