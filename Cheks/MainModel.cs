using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheks
{
    internal class MainModel : INPC
    {
        const string FORB_FILE = "BlokList.txt";
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

            }
        }

    }
}
