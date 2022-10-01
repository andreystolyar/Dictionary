using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dictionary.Models;

namespace Dictionary.Services
{
    static class DictService
	{
        static DictService()
        {
            string? _lastPath = null;

            if (File.Exists(settings))
			{
                using StreamReader sr = new(settings);

                _lastPath = sr.ReadLine();
            }

			Path = _lastPath ?? "dictionary.txt";
		}
        const string settings = "settings";

		static readonly List<Entry> _dict = new();

		static readonly List<Entry> _learningList = new();

        static int _selectNumber = 10;

        public static void AddWord(Entry e)
        {
            _dict.Add(e);
        }

        public static List<Entry> GetWords()
        {
            return _dict;
        }

        public static void RemoveWords(int index)
        {
            _dict.RemoveAt(index);
        }


        public static void FillLearningList(IList learnList, bool shuffle)
        {
            _learningList.Clear();

            foreach (var item in learnList)
            {
                _learningList.Add((Entry)item);
            }

            //Алгоритм рандомизации Саттоло
            if (shuffle)
            {
				Random r = new();
				int j, i = _learningList.Count;
                while (i > 1)
                {
                    i--;
                    j = r.Next(i);

                    (_learningList[i], _learningList[j]) =
						(_learningList[j], _learningList[i]);
                }
            }
        }
        public static List<Entry> GetLearningList()
        {
            return _learningList;
        }

        public static string SelectNumber
        {
            get
            {
                _selectNumber = (_selectNumber > _dict.Count)
                    ? _dict.Count
                    : _selectNumber;

                return _selectNumber.ToString();
            }
            set
            {
                if(!int.TryParse(value, out _selectNumber))
                {
                    _selectNumber = 10;
                }
                if(_selectNumber >= _dict.Count)
                {
                    _selectNumber = _dict.Count;
                }
            }
        }
        public static string Path { get; private set; }
        public static bool Init()
        {
            if (File.Exists(Path))
            {
                using StreamReader sr = new(Path);

				_dict.Clear();

				if(int.TryParse(sr.ReadLine(), out int n))
				{
					_dict.Capacity = n;
				}
				else
				{
					sr.DiscardBufferedData();
					sr.BaseStream.Position = 0;
				}

				string? line;
				string[] sublines;

				while((line = sr.ReadLine()) != null)
				{
					if(line.Contains(':'))
					{
						sublines = line.Split(':', 2);

						_dict.Add(new Entry
						{ Word = sublines[0], Translation = sublines[1] });
					}
				}
				return true;
            }
			return false;
		}

		public static void Create()
        {
            Path = string.Empty;
            _dict.Clear();
        }

        public static void Open(string filePath)
        {
			Path = filePath;

			Init();
		}

		public static void Save()
        {
            Save(Path);
        }
        public static void Save(string savePath)
        {
            using StreamWriter sw = new(savePath);
            sw.WriteLine(_dict.Count);
            foreach (var entry in _dict)
            {
                sw.WriteLine($"{entry.Word}:{entry.Translation}");
            }
            Path = savePath;
        }
        public static void UpdateSettings()
        {
            using StreamWriter sw = new(settings, false);
            sw.WriteLine(Path);
        }
	}
}
