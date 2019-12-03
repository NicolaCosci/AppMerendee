using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

namespace AppMerendee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> 
    public partial class MainWindow : Window 
    {
        List<Merenda> merendeDisponibili;
        public MainWindow()
        {
            InitializeComponent();
            merendeDisponibili = new List<Merenda>();
            //CaricaMerende();
        }
        private void CaricaMerende()
        {
            string line;
            StreamReader sr = new StreamReader("Merende.csv");
            sr.ReadLine(); 
            while ((line = sr.ReadLine()) != null) 
            {
                try
                {
                    Merenda m = new Merenda();
                    string[] campi = line.Split(';');  
                    string nome = campi[0]; 
                    m.Nome =  nome;
                    double prezzo = Convert.ToDouble(campi[1]);
                    m.Prezzo = prezzo;
                    merendeDisponibili.Add(m);

                }
                catch(Exception e)
                {
                    MessageBox.Show(e.ToString());
                    break;

                }
            }
            foreach(Merenda m in merendeDisponibili)
            {
                Lst_Merende.Items.Add(m);
            }
        }
        private void Aggiungi(Merenda m)
        {
            Lst_Merende.Items.Add(m);
        }
        private void Btn_aggiungi_Click(object sender, RoutedEventArgs e)
        {
            Lst_mselezionate.Items.Add(Lst_Merende.SelectedItem);
        }

        private void Btn_reset_Click(object sender, RoutedEventArgs e)
        {
            Lst_mselezionate.Items.Clear();
        }

        private void Btn_rimuovi_Click(object sender, RoutedEventArgs e)
        {
            Lst_mselezionate.Items.Remove(Lst_mselezionate.SelectedItem);
        }

        private void Btn_carica_Click(object sender, RoutedEventArgs e)
        {
            Lst_Merende.Items.Clear();
            CaricaMerende();
        }

       

        private void Btn_calcolo_Click(object sender, RoutedEventArgs e)
        {

            double PrezzoTotale = 0;
            foreach (Merenda m in Lst_mselezionate.Items)
            {
                PrezzoTotale += m.Prezzo;

            }
            Txt_spesa.Text = $"Prezzo totale : {PrezzoTotale} €";
        }

        private void Btn_stampa_Click(object sender, RoutedEventArgs e)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"ListaMerende.txt"))
            {
                file.Write($"{Txt_classe.Text} con totale spesa {Txt_spesa.Text}, per le seguenti merende:");     

                foreach (Merenda ms in Lst_mselezionate.Items)
                {
                    file.Write("\n");
                    file.Write($"{ms}");
                }

            }

            Process.Start(@"ListaMerende.txt");
        }
    }
}
