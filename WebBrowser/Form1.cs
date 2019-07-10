using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowser
{
    public partial class Form1 : Form
    {
        //Postavljamo parametre
        String Url = string.Empty;

        //Konstruktor
        public Form1()
        {
            InitializeComponent();
            Url = "https://duckduckgo.com/html/";
            myBrowser();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //Metoda koja se obavlja po ucitavanju stranice
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //Ako ima prethodne strane u istoriji omoguci dugme za povratak
            if (webBrowser1.CanGoBack)
            {
                toolStripButton1.Enabled = true;
            }
            else
            {
                toolStripButton1.Enabled = false;
            }

            //Ako ima sledece stranice omoguci dugme za napred
            if (webBrowser1.CanGoForward)
            {
                toolStripButton3.Enabled = true;
            }
            else
            {
                toolStripButton3.Enabled = false;
            }

            //Supresuj poruke o JS greskama
            webBrowser1.ScriptErrorsSuppressed = true;

            //Ispisi poruku u status labelu o uspesnom ucitavanju stranice
            toolStripStatusLabel1.Text = "Done";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Onemogucavamo prilikom startovanja forme dugmad za Next & Previous
            toolStripButton1.Enabled = false; //Next dugme
            toolStripButton3.Enabled = false; //Previous dugme

        }

        //Metoda za loadiranje i prikazivanje web stranica u browseru
        private void myBrowser()
        {
            //Ako combo box nije prazan, Url je jednak tom tekstu
            if (toolStripComboBox1.Text != "") Url = toolStripComboBox1.Text;
            //Preko Navigate metode ucitavamo sadrzaj stranice
            webBrowser1.Navigate(Url);

            //Pravimo dinamiku tako sto hvatamo promene na aplikaciji
            webBrowser1.ProgressChanged +=
                new WebBrowserProgressChangedEventHandler(webpage_ProgressChanged);
            webBrowser1.DocumentTitleChanged +=
                new EventHandler(webpage_DocumentTitleChanged);
            webBrowser1.StatusTextChanged +=
                new EventHandler(webpage_StatusTextChanged);
            webBrowser1.Navigated +=
                new WebBrowserNavigatedEventHandler(webpage_Navigated);
            webBrowser1.DocumentCompleted +=
                new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
        }

        //DEPRECATED
        ////Metoda koja se obavlja po ucitavanju stranice
        //private void webpage_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        //{
        //    //Ako ima prethodne strane u istoriji omoguci dugme za povratak
        //    if (webBrowser1.CanGoBack) toolStrip1.Enabled = true;
        //    else toolStripButton1.Enabled = false;

        //    //Ako ima sledece stranice omoguci dugme za napred
        //    if (webBrowser1.CanGoForward) toolStripButton3.Enabled = true;
        //    else toolStripButton3.Enabled = false;

        //    //Ispisi poruku u status labelu o uspesnom ucitavanju stranice
        //    toolStripStatusLabel1.Text = "Done";
        //}

        //Metoda koja dohvata Title stranice
        private void webpage_DocumentTitleChanged(object sender, EventArgs e)
        {
            //Dohvata Title stranice
           //this.Text = this.Text + " - " + webBrowser1.DocumentTitle.ToString();

            this.Text = "Nemo - " + webBrowser1.DocumentTitle.ToString();
        }

        private void webpage_StatusTextChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = webBrowser1.StatusText;
        }

        //Metoda koja upravlja status barom
        private void webpage_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Maximum = (int)e.MaximumProgress;
            toolStripProgressBar1.Value = ((int)e.CurrentProgress < 0 ||
            (int)e.MaximumProgress < (int)e.CurrentProgress) ?
            (int)e.MaximumProgress : (int)e.CurrentProgress;
        }

        //Metoda koja dohvata Url stranice i prikazuje ga u combo boxu
        private void webpage_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            toolStripComboBox1.Text = webBrowser1.Url.ToString();
        }

        //Metoda koja vrsi refresh stranice klikom na dugme
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //Refresh metoda radi tako sto ona proverava server za novu verziju stranice
            webBrowser1.Refresh();
        }

        //Metoda koja omogucava rad Forward dugmeta
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            myBrowser();
        }
        //Metoda koja omogucava rad Forward dugmeta
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            //Ide na sledecu stranicu ako postoji
            webBrowser1.GoForward();
        }

        //Metoda koja omogucava rad Back dugmeta
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //Ide na prethodnu stranicu
            webBrowser1.GoBack();
        }

        //Metoda koja omogucava rad Home dugmeta
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(Url);
        }
        //Metoda koja omogucava rad Print dugmeta
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            //Otvara print preview
            webBrowser1.ShowPrintPreviewDialog();
        }
    }
}
