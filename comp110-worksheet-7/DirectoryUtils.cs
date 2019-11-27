using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace comp110_worksheet_7
{
	public static class DirectoryUtils
	{
		// Return the size, in bytes, of the given file
		public static long GetFileSize(string filePath)
		{
			return new FileInfo(filePath).Length;
		}

		// Return true if the given path points to a directory, false if it points to a file
		public static bool IsDirectory(string path)
		{
			return File.GetAttributes(path).HasFlag(FileAttributes.Directory);
		}

		// Return the total size, in bytes, of all the files below the given directory
		public static long GetTotalSize(string directory)
        {
            string[] files;
            long fileSize = 0;

            files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);
            foreach(string file in files)
            {
                fileSize += GetFileSize(file);
            }

            return fileSize;
        }

		// Return the number of files (not counting directories) below the given directory
		public static int CountFiles(string directory)
		{
            string[] files;
            files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);
            return files.Length;
           

		}

		// Return the nesting depth of the given directory. A directory containing only files (no subdirectories) has a depth of 0.
		public static int GetDepth(string directory)
		{
            string[] depth;
            int depthValue = 0;

            depth = Directory.GetDirectories(directory);
            foreach(string dir in depth)
            {
                depthValue++;
            }

            return depthValue;
        }

        // Get the path and size (in bytes) of the smallest file below the given directory
        public static Tuple<string, long> GetSmallestFile(string directory)
        {
            string[] filesList;
            Tuple<string, long> size;

            size = new Tuple<string, long>("hi", 1);
            filesList = Directory.GetFiles(directory, ".", SearchOption.AllDirectories);

            string smallestFile = (from item in filesList let len = GetFileSize(item) where len > 0 orderby len ascending select item).First();
            long minSize = GetFileSize(smallestFile);

            string minSizeName = Path.GetFileName(smallestFile);

            size = new Tuple<string, long>(minSizeName, minSize);
            return size;
        }

		// Get the path and size (in bytes) of the largest file below the given directory
		public static Tuple<string, long> GetLargestFile(string directory)
		{
            string[] filesList;
            Tuple<string, long> size;

            size = new Tuple<string, long>("hi", 1);
            filesList = Directory.GetFiles(directory, ".", SearchOption.AllDirectories);

            string largestFile = (from item in filesList let len = GetFileSize(item) where len > 0 orderby len descending select item).First();
            long maxSize = GetFileSize(largestFile);

            string maxSizeName = Path.GetFileName(largestFile);

            size = new Tuple<string, long>(maxSizeName, maxSize);
            return size;
        }

		// Get all files whose size is equal to the given value (in bytes) below the given directory
		public static IEnumerable<string> GetFilesOfSize(string directory, long size)
		{
            string[] files;
            List<string> filesOfSize = new List<string>();
            files = Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories);
            long fileSize;
            foreach (string file in files)
            {
                fileSize = GetFileSize(file);
                if (fileSize == size)
                {
                    filesOfSize.Add(Path.GetFileName(file));
                }
                else
                {

                }
            }
            return filesOfSize;
        }
	}
}
