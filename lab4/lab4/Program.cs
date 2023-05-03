namespace lab4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? dir;
            if (args.Length == 0)
            {
                Console.Write("Enter directory path: (cur for current) ");
                dir = Console.ReadLine();
                if(dir == null)
                {
                    Environment.Exit(1);
                }
                if (dir.Contains("cur"))
                {
                    dir = Directory.GetCurrentDirectory();
                }
            }
            else
            {
                string[] types = new string[args.Length - 1];
                Array.Copy(args, 1, types, 0, args.Length - 1);
                if (args.Contains("?"))
                {
                    Console.WriteLine("This is help mode");
                    Console.WriteLine("Press Enter to continue:");
                    Console.ReadLine();
                    Environment.Exit(0);
                }
                dir = args[0];
            }

            if (!Directory.Exists(dir))
            {
                Console.WriteLine();
                Console.WriteLine("Error 1 (dir doesn't exist)");
                Console.WriteLine("Press Enter to continue:");
                Console.ReadLine();
                Environment.Exit(1);
            }

            int count = 0;
            int hiddenCount = 0;
            int readOnlyCount = 0;
            int archivedCount = 0;

            foreach (string subDir in Directory.GetDirectories(dir, "*", SearchOption.AllDirectories))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(subDir);
                FileInfo[] files = dirInfo.GetFiles("*.*", SearchOption.AllDirectories);
                int fileCount = files.Length;
                int fileHiddenCount = 0;
                int fileReadOnlyCount = 0;
                int fileArchivedCount = 0;
                foreach (FileInfo file in files)
                {
                    count++;
                    if ((file.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                    {
                        hiddenCount++;
                        fileHiddenCount++;
                    }
                    if ((file.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        readOnlyCount++;
                        fileReadOnlyCount++;
                    }
                    if ((file.Attributes & FileAttributes.Archive) == FileAttributes.Archive)
                    {
                        archivedCount++;
                        fileArchivedCount++;
                    }
                }
                Console.WriteLine(dirInfo.Name);
                Console.WriteLine($"Total files: {fileCount}");
                Console.WriteLine($"Hidden files: {fileHiddenCount}");
                Console.WriteLine($"Read-only files: {fileReadOnlyCount}");
                Console.WriteLine($"Archived files: {fileArchivedCount}");
                Console.WriteLine();
            }

            FileInfo[] rootFiles = new DirectoryInfo(dir).GetFiles("*.*", SearchOption.TopDirectoryOnly);
            int rootFileCount = rootFiles.Length;
            int rootHiddenCount = 0;
            int rootReadOnlyCount = 0;
            int rootArchivedCount = 0;
            foreach (FileInfo file in rootFiles)
            {
                count++;
                if ((file.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                {
                    hiddenCount++;
                    rootHiddenCount++;
                }
                if ((file.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    readOnlyCount++;
                    rootReadOnlyCount++;
                }
                if ((file.Attributes & FileAttributes.Archive) == FileAttributes.Archive)
                {
                    archivedCount++;
                    rootArchivedCount++;
                }
            }
            Console.WriteLine($"Total files: {count}");
            Console.WriteLine($"Hidden files: {hiddenCount}");
            Console.WriteLine($"Read-only files: {readOnlyCount}");
            Console.WriteLine($"Archived files: {archivedCount}");

            Console.WriteLine("Press Enter to continue:");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}