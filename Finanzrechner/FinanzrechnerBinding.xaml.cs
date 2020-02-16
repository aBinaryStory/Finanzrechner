using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Finanzrechner
{
    /// <summary>
    /// Interaktionslogik für FinanzrechnerBinding.xaml
    /// </summary>
    public partial class FinanzrechnerBinding : Window, INotifyPropertyChanged
    {
        public int MaxKredit => 50000;
        public int MinKredit => 10000;
        public int MaxJahre => 120;
        public int MinJahre => 10;
        public int MaxZinssatz => 10;
        public int MinZinssatz => 1;

        private int _kredit;

        public int Kredit
        {
            get { return _kredit; }
            set 
            { 
                if(SetProperty(ref _kredit, value))
                {
                    RaisePropertyChanged(nameof(Rate)); //oder SliderChangedEvent -> Notify(Rate)
                }
            }
        }

        private int _laufzeit;

        public int Laufzeit
        {
            get { return _laufzeit; }
            set 
            { 
                if (SetProperty(ref _laufzeit, value))
                {
                    RaisePropertyChanged(nameof(Rate));
                }
            }
        }

        private double _zinssatz;

        public double Zinssatz
        {
            get { return _zinssatz; }
            set
            {
                if (SetProperty(ref _zinssatz, value))
                {
                    RaisePropertyChanged(nameof(Rate));
                }
            }
        }

        public double Rate
        {
            get 
            { 
                double zwischenSumme = Math.Pow(1 + (Zinssatz / 1200), Laufzeit);
                //Teilen durch Null verhindern
                return zwischenSumme == 1 ? 0 : Kredit * (((Zinssatz / 1200) * zwischenSumme) / (zwischenSumme - 1));
            }
        }

        public FinanzrechnerBinding()
        {
            Kredit = MinKredit;
            Laufzeit = MinJahre;
            Zinssatz = MinZinssatz;
            InitializeComponent();
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

            storage = value;
            RaisePropertyChanged(propertyName);

            return true;
        }

        protected void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }
        #endregion
    }
}
