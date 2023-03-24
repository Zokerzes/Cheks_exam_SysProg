using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cheks
{
    internal class MainModel:INPC
    {
        private string _selectedDirectory;
        public string SelectedDirectory
        {
            get { return _selectedDirectory; }
            set { _selectedDirectory = value;
                OnPropertyChenged();
            }
        }
        //listBlockWord

        List<string> _forBiddenWords;
        public List<string> ForBiddenWords
        {
            get { return _forBiddenWords; }
        }

        public bool LoadBiddenWords (string path = "")
        {
            _forBiddenWords = File.ReadLines(path).ToList();
            OnPropertyChenged(nameof(ForBiddenWords));
            return true;
        }

        public bool ScanDirectory()
        {
            Directory.GetFiles(_selectedDirectory, "*.txt")
        }

    }
}
