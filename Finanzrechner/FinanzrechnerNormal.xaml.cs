using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Finanzrechner
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class FinanzRechnerNormal : Window
    {
        public FinanzRechnerNormal()
        {
            InitializeComponent();
            lblKredit.Content = sldKredit.Value.ToString();
            lblZinssatz.Content = sldZins.Value.ToString();
            lblLaufzeit.Content = sldLaufzeit.Value.ToString();
            RateBerechnen();
        } 

        private void Kreditsumme_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(IsInitialized)
            {
                lblKredit.Content = ((Slider)sender).Value.ToString();
                RateBerechnen();
            }
        }

        private void Zinzsatz_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(IsInitialized)
            {
                lblZinssatz.Content = ((Slider)sender).Value;
                RateBerechnen();
            }
        }

        private void Laufzeit_Changed(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(IsInitialized)
            {
                lblLaufzeit.Content = ((Slider)sender).Value;
                RateBerechnen();
            }
        }

        private void RateBerechnen()
        {
            double zwischenSumme = Math.Pow(1 + (sldZins.Value / 1200), sldLaufzeit.Value);
            //Teilen durch Null verhindern
            var kredit = zwischenSumme == 1 ? 0 : sldKredit.Value * (((sldZins.Value / 1200) * zwischenSumme) / (zwischenSumme - 1));

            rRate.Text = String.Format("{0:0.00}", kredit);          
        }
    }
}
