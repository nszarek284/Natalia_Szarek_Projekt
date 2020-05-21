using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Natalia_Szarek_Projekt
{
    class ViewModel : VMBase
    {
        private string tekst = null;

        private ObservableCollection<Tuple<string, int>> unikalneSlowa;

        public RelayCommand Zaladuj
        {
            get;
            private set;
        }

        public RelayCommand Wyczysc
        {
            get;
            private set;
        }

        public RelayCommand Licz
        {
            get;
            private set;
        }

        public string Tekst
        {
            get => tekst;
            set
            {
                tekst = value;
                OnPropertyChanged(nameof(Tekst));
            }
        }

        public ObservableCollection<Tuple<string, int>> UnikalneSlowa
        {
            get;
            set;
        }
        = new ObservableCollection<Tuple<string, int>>();

        public void ZaladujTekst(object sciezkaDoPliku)
        {
            string sciezka = (string)sciezkaDoPliku;
            if(File.Exists(sciezka))
            {
                Tekst = File.ReadAllText(sciezka);
            }
        }

        public void LiczSlowa()
        {
            string tekstPomocniczy = tekst.ToLower();
            List<string> slowaList = new List<string>();
            List<Tuple<string, int>> liczoneSlowa = new List<Tuple<string, int>>();

            string[] slowa = tekstPomocniczy.Split(' ', '.', ',');

            tekstPomocniczy = " " + tekstPomocniczy + " ";

            foreach (string s in slowa)
            {
                if (s != "" && s != " ")
                {
                    slowaList.Add(s);
                }
            }

            slowaList.Sort();
            MakeUniqueWordList(slowaList);

            foreach (string s in slowaList)
            {
                KMP(tekstPomocniczy, s);
            }

            UnikalneSlowa = new ObservableCollection<Tuple<string, int>>(UnikalneSlowa);

            OnPropertyChanged(nameof(UnikalneSlowa));
        }

        public void MakeUniqueWordList(List<string> SlowaList)
        {
            for (int i = 1; i < SlowaList.Count; i++)
            {
                if (SlowaList[i - 1] == SlowaList[i])
                {
                    SlowaList.RemoveAt(i - 1);
                    i--;
                }
            }
        }
        public void KMP(string tekst, string ciag)
        {
            int j = 0;
            int count = 0;



            for (int i = 1; i < tekst.Length - 1;)
            {
                if (ciag[j] == tekst[i])
                {
                    j++;
                    i++;
                }

                if (j == ciag.Length)
                {
                    if (tekst[i - j - 1] == ' ' && tekst[i] == ' ')
                    {
                        count++;
                    }
                    if (j != 0)
                        j--;
                }

                else if (i < tekst.Length && ciag[j] != tekst[i])
                {
                    if (j != 0)
                    {
                        j--;
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            UnikalneSlowa.Add(new Tuple<string, int>(ciag, count));

            Console.WriteLine(ciag + " " + count);
        }

        public ViewModel()
        {
            Zaladuj = new RelayCommand(arg => ZaladujTekst(arg));
            Licz = new RelayCommand(arg => LiczSlowa());
        }
    }
}
