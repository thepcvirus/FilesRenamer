using System;
using System.IO;

namespace FileRenamerProject
{
    public class FilesController
    {
        string NoErrorConstant = "";
        string FolderURL;
        string[] AllFilesInFolder;
        string[] FilesToBeRenamed;
        string[] DublicatedFiles;
        string[] RenamedFilesBackupNames;
        int LastIndexToBeChecked;
        public FilesController(string FolderURL)
        {
            this.FolderURL = FolderURL;
            RefreshURLFiles();
        }

        void UpdateFileNamesWithSlices(string Slice)
        {
            LastIndexToBeChecked = 0;
            foreach (string FileName in AllFilesInFolder)
            {
                if (FileName.Contains(Slice))
                {
                    FilesToBeRenamed[LastIndexToBeChecked] = FileName;
                    RenamedFilesBackupNames[LastIndexToBeChecked] = FileName;
                    LastIndexToBeChecked++;
                }
            }
        }

        bool CheckIfNewNameExist(string NewName)
        {
            foreach (string FileName in AllFilesInFolder)
            {
                if (FileName == NewName)
                {
                    return true;
                }
            }
            return false;
        }

        public void RefreshURLFiles()
        {
            AllFilesInFolder = Directory.GetFiles(FolderURL);
            FilesToBeRenamed = new string[AllFilesInFolder.Length];
            RenamedFilesBackupNames = new string[AllFilesInFolder.Length];
            DublicatedFiles = new string[AllFilesInFolder.Length];
            LastIndexToBeChecked = 0;
        }

        public void UpdateURL()
        {
            Console.WriteLine("Enter The New Folder URL that you want to rename it's files:");
            String FolderURL = Console.ReadLine();
            this.FolderURL = FolderURL;
            RefreshURLFiles();
        }

        public bool ReplaceSlice()
        {
            Console.WriteLine("Enter The Slice you want to remove:");
            String Slice = Console.ReadLine();
            Console.WriteLine("Enter The New Slice you want to Insert:");
            String NewSlice = Console.ReadLine();

            bool DublicateErrorTrigger = false;
            UpdateFileNamesWithSlices(Slice);
            if (LastIndexToBeChecked == 0)
            {
                Console.WriteLine("No Files to be Renamed!");
                return false;
            }


            for (int i = 0; i < LastIndexToBeChecked; i++)
            {
                Console.WriteLine(FilesToBeRenamed[i]);
            }
            Console.WriteLine("These are the files that You want to rename, Are you sure want to rename them?(Yes // No)");
            if (!Console.ReadLine().Contains("Yes"))
            {
                Console.WriteLine("Renaming Cancelled!");
                return false;
            }

            for (int i = 0; i < LastIndexToBeChecked; i++)
            {
                if (!CheckIfNewNameExist(FilesToBeRenamed[i].Replace(Slice, NewSlice)))
                {
                    System.IO.File.Move(FilesToBeRenamed[i], FilesToBeRenamed[i].Replace(Slice, NewSlice));
                    DublicatedFiles[i] = NoErrorConstant;
                }
                else
                {
                    DublicateErrorTrigger = true;
                    DublicatedFiles[i] = FilesToBeRenamed[i];
                }
            }

            if (DublicateErrorTrigger)
            {
                Console.WriteLine("There are dublicates for these files if they got renamed:");
                for (int i = 0; i < LastIndexToBeChecked; i++)
                {
                    if (!DublicatedFiles[i].Equals(NoErrorConstant))
                    {
                        Console.WriteLine(DublicatedFiles[i]);
                    }
                }
                Console.WriteLine("Do you want to replace them?(Yes // No)");
                if (Console.ReadLine().Contains("Yes"))
                {
                    for (int i = 0; i < LastIndexToBeChecked; i++)
                    {
                        if (!DublicatedFiles[i].Equals(NoErrorConstant))
                        {
                            System.IO.File.Delete(FilesToBeRenamed[i].Replace(Slice, NewSlice));
                            System.IO.File.Move(FilesToBeRenamed[i], FilesToBeRenamed[i].Replace(Slice, NewSlice));
                        }
                    }
                    Console.WriteLine("Dublicated Files Replaced!");
                }
                else
                {
                    Console.WriteLine("Dublicated Files Skipped!");
                }
            }
            Console.WriteLine("Files Renamed Succesfully");
            return true;
        }

        public bool RemoveSlice()
        {
            Console.WriteLine("Enter The Slice you want to remove:");
            String Slice = Console.ReadLine();

            bool DublicateErrorTrigger = false;
            UpdateFileNamesWithSlices(Slice);
            if (LastIndexToBeChecked == 0)
            {
                Console.WriteLine("No Files to be Renamed!");
                return false;
            }


            for (int i = 0; i < LastIndexToBeChecked; i++)
            {
                Console.WriteLine(FilesToBeRenamed[i]);
            }
            Console.WriteLine("These are the files that You want to rename, Are you sure want to rename them?(Yes // No)");
            if (!Console.ReadLine().Contains("Yes"))
            {
                Console.WriteLine("Renaming Cancelled!");
                return false;
            }

            for (int i = 0; i < LastIndexToBeChecked; i++)
            {
                if (!CheckIfNewNameExist(FilesToBeRenamed[i].Remove(FilesToBeRenamed[i].IndexOf(Slice), Slice.Length)))
                {
                    System.IO.File.Move(FilesToBeRenamed[i], FilesToBeRenamed[i].Remove(FilesToBeRenamed[i].IndexOf(Slice), Slice.Length));
                    DublicatedFiles[i] = NoErrorConstant;
                }
                else
                {
                    DublicateErrorTrigger = true;
                    DublicatedFiles[i] = FilesToBeRenamed[i];
                }
            }

            if (DublicateErrorTrigger)
            {
                Console.WriteLine("There are dublicates for these files if they got renamed:");
                for (int i = 0; i < LastIndexToBeChecked; i++)
                {
                    if (!DublicatedFiles[i].Equals(NoErrorConstant))
                    {
                        Console.WriteLine(DublicatedFiles[i]);
                    }
                }
                Console.WriteLine("Do you want to replace them?(Yes // No)");
                if (Console.ReadLine().Contains("Yes"))
                {
                    for (int i = 0; i < LastIndexToBeChecked; i++)
                    {
                        if (!DublicatedFiles[i].Equals(NoErrorConstant))
                        {
                            System.IO.File.Delete(FilesToBeRenamed[i].Remove(FilesToBeRenamed[i].IndexOf(Slice), Slice.Length));
                            System.IO.File.Move(FilesToBeRenamed[i], FilesToBeRenamed[i].Remove(FilesToBeRenamed[i].IndexOf(Slice), Slice.Length));
                        }
                    }
                    Console.WriteLine("Dublicated Files Replaced!");
                }
                else
                {
                    Console.WriteLine("Dublicated Files Skipped!");
                }
            }
            Console.WriteLine("Files Renamed Succesfully");
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to File Renamer Project");
            Console.WriteLine("Enter The Folder URL that you want to rename it's files:");
            String FileURl = Console.ReadLine();
            FilesController FC = new FilesController(FileURl);
            String Choice;
            while (true)
            {
                Console.WriteLine("1-Remove a slice from files name. \n2-Remove a slice from files name and Replace it with another Slice.\n3-Change Folder URL.\n4-Exit the program.");
                Choice = Console.ReadLine();
                switch (Choice)
                {
                    case "1":
                        FC.RemoveSlice();
                        break;
                    case "2":
                        FC.ReplaceSlice();
                        break;
                    case "3":
                        FC.UpdateURL();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Undefined Input !");
                        break;
                }
                FC.RefreshURLFiles();
            }
        }
    }
}
