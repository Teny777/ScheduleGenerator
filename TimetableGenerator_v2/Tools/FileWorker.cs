using System;
using Generator.Singleton;
using Microsoft.Win32;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;

namespace Generator.Tools
{
    public class FileWorker
    {
        private static string _filename = null;
        private static readonly BinaryFormatter _bf = new BinaryFormatter();

        public static void OpenGeneratorFile()
        {
            if (ChooseFileToOpen())
            {
                OpenFile();
            }
        }

        public static void SaveGeneratorFile()
        {
            if (!string.IsNullOrEmpty(_filename))
            {
                SaveFile();
            }
            else
            {
                SaveGeneratorFileAs();
            }
            
        }

        public static void SaveGeneratorFileAs()
        {
            if (ChooseFilePathToSave())
            {
                SaveFile();
            }
        }

        private static bool ChooseFileToOpen()
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "Файлы генератора расписаний (*.gtf)|*.gtf",
                RestoreDirectory = true
            };

            if (ofd.ShowDialog() == true)
            {
                _filename = ofd.FileName;
                return true;
            }

            return false;
        }
        
        private static bool ChooseFilePathToSave()
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "Файлы генератора расписаний (*.gtf)|*.gtf",
                RestoreDirectory = true
            };
            if (sfd.ShowDialog() == true)
            {
                _filename = sfd.FileName;
                return true;
            }
            return false;
        }

        private static void OpenFile()
        {
            using FileStream fs = new FileStream(_filename, FileMode.Open);
            try
            {
                Data.Instance = (Data)_bf.Deserialize(fs);
                Garbage.FillRestrictions(Data.Instance.Restrictions);
            }
            catch (Exception e)
            {
                MessageBox.Show("Ошибка открытия файла");
            }
        }


        private static void SaveFile()
        {
            using FileStream fs = new FileStream(_filename, FileMode.Create);
            _bf.Serialize(fs, Data.Instance);
            MessageBox.Show("Сохранено успешно!");
        }
    }
}
