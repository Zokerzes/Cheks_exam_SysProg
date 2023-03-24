using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cheks
{
    internal class MainModel : INPC
    {
        const string FORB_FILE = "BlokList.txt";
        const string COPY_DIRECT_NAME = "Forbfidden Files";

        public MainModel()
        {
            LoadBiddenWords();
        }

        private string _selectedDirectory;
        public string SelectedDirectory
        {
            get { return _selectedDirectory; }
            set
            {
                _selectedDirectory = value;
                OnPropertyChenged();
            }
        }
        //listBlockWord


        public ObservableCollection<string> ForBiddenWords { get; set; }



        private int _progress = 0;

        public int Progress
        {
            get { return _progress; }
        }

        // загрузить Запрещёные слова
        public bool LoadBiddenWords()
        {
            ForBiddenWords = new ObservableCollection<string>
            (
               File.ReadLines(FORB_FILE).ToList()
            );
            OnPropertyChenged(nameof(ForBiddenWords));
            return true;
        }

        // сохранить запрещённые слова
        public bool SaveBiddenWords()
        {
            File.WriteAllLines(FORB_FILE, ForBiddenWords);
            return true;
        }


        //найти все текстовые файлы из выбраной папки
        public async Task ScanDirectory()
        {
            //Найти все текстовые файлы
            List<string> files = Directory
                .GetFiles(_selectedDirectory, "*.txt", SearchOption
                                                        .AllDirectories).ToList();
            //чтение
            foreach (var file in files)
            {

                using (StreamReader stream = new StreamReader(file))
                {
                    string line;
                    bool find = false;
                    while ((line = await stream.ReadLineAsync()) != null && !find)
                    {
                        foreach (var word in ForBiddenWords)
                        {
                            if (line
                                .ToLower()
                                .Contains(word.ToLower()))
                            {
                                find = true;
                                var fi = new FileInfo(file);
                                File.Copy(file, Environment.GetFolderPath(Environment
                                    .SpecialFolder.MyDocuments) + $"\\{COPY_DIRECT_NAME}\\{fi.Name}_forbidden");
                                break;
                            }
                        }


                    }
                }

            }
        }
        //Заменить все слова на звездочки
        public async Task ReplaceTextFiles()
        {
            List<string> files = Directory.GetFiles(Environment.GetFolderPath(Environment
                                    .SpecialFolder.MyDocuments) + $"\\{COPY_DIRECT_NAME}").ToList();
            foreach (var file in files)
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string line;
                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        foreach (var word in ForBiddenWords)
                        {
                            if (line.ToLower()
                               .Contains(word.ToLower()))
                            {
                                var replasedLine = Regex.Replace(line, word, new string('*', word.Length), RegexOptions.IgnoreCase);

                                using (StreamWriter sw = new StreamWriter(sr.BaseStream))
                                {
                                    sw.WriteLine(replasedLine);
                                }
                            }
                        }

                    }
                }
            }

        }
    }
}
