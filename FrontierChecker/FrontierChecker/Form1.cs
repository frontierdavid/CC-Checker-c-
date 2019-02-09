using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using System.Threading;
using Gecko;



namespace FrontierChecker
{
    public partial class Form1 : Form
    {
        private String strMailBody { get; set; }
        private String sIP { get; set; } 
        private String bin { get; set; }

        private string ar;

        private void f()
        {
            this.dataGridView1.Rows.Remove(this.dataGridView1.Rows[0]);
        }
        private object Tarjetas()
        {
            throw new NotImplementedException();
        }
    public object borrarPrimeraLinea(string Archivo)
        {
            object obj2 = new object() ;
            try
            {
                int num2;
                StreamReader reader = new StreamReader(Archivo);
                string[] strArray = new string[2];
                int index = 0;
                do
                {
                    strArray[index] = reader.ReadLine();
                    index += 1;
                    num2 = 1;
                }
                while (index <= num2);
                {
                    this.textBox3.Text = strArray[0];
                    List<string> list = new List<string>(File.ReadAllLines(Archivo));
                    list.RemoveAt(0);
                    reader.Dispose();
                    reader.Close();
                    File.WriteAllLines(Archivo, list.ToArray());
                }
            }
            catch
            {

            }
            return obj2;
        }
        public Form1()

        {
         
            InitializeComponent();
            Xpcom.Initialize("Firefox");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            geckoWebBrowser1.Navigate("https://secure20.salvationarmy.org/donation.jsp");
  
            timer1.Start();
            lblStatus.Text = "Verificando Lista";
            lblStatus.ForeColor = Color.Lime;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ABRIR
            openFileDialog1.ShowDialog();
            txtDirectorio.Text = openFileDialog1.FileName;
            try
            {
                // =========================================================
                char Separador;
                DataTable datagrid = new DataTable();
                DataRow dr;
                //==========================================================
                lblStatus.Text = "Carga Correcta....";
                lblStatus.ForeColor = Color.Gold;

                datagrid.Columns.Add("CC");
                datagrid.Columns.Add("MES");
                datagrid.Columns.Add("ANO");
                datagrid.Columns.Add("CVV");
                dr = datagrid.NewRow();
                //=============================================================
                System.IO.StreamReader Archivo = new System.IO.StreamReader(txtDirectorio.Text);
                Separador = Convert.ToChar("|");
                while (Archivo.Peek() != -1)
                {
                    datagrid.Rows.Add(Archivo.ReadLine().Split(Separador));
                    
                }
                dataGridView1.DataSource = datagrid;
                this.Tarjetas();
                dataGridView1.CurrentRow.Selected = true;
                // =============================================================
            }
            catch(Exception)
            {

            }
            this.tContador.Start();
            //ORGANIZADOR GRID
            int tmn;
            var loopTo = dataGridView1.ColumnCount - 1;
            for (tmn = 0; tmn <= loopTo - 1; tmn++)
            {
                if(tmn == dataGridView1.ColumnCount - 1)
                {
                    dataGridView1.Columns[tmn].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                else
                {
                    dataGridView1.Columns[tmn].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }
              
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //DETENER
            lblStatus.Text = "Pausado...";
        lblStatus.ForeColor = Color.Red;
            timer1.Stop();
            timer2.Stop();
            timer3.Stop();
            timer4.Stop();
            timer5.Stop();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //LIMPIAR
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            Aprovadas.Items.Clear();
            Reprovadas.Items.Clear();
            txtDirectorio.Text = "";
            lblStatus.Text = "";
            lblAprovadas.Text = "";
            lblReprovadas.Text = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //COMENZAR A TESTEAR
            lblStatus.Text = "Testeando...";
            lblStatus.ForeColor = Color.Lime;
            timer2.Start();
            timer1.Stop();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
          
            //TIMER 2 TESTEANDO
            if (true)
            {
                try
                {
                    this.lblStatus.Text = "Testeando";
                    this.lblStatus.Text = "Testeando...";
                    lblStatus.ForeColor = Color.Lime;
                    this.dataGridView1.CurrentRow.Selected = true;
                    //DATOS DEL GATE
             
                    geckoWebBrowser1.Document.GetElementById("cardNumber").SetAttribute("value", Convert.ToString(this.dataGridView1.CurrentRow.Cells[0].Value));

                    geckoWebBrowser1.Document.GetElementById("cardCvc").SetAttribute("value", Convert.ToString(this.dataGridView1.CurrentRow.Cells[2].Value));

                    //TIMERS
                    this.timer2.Stop();
                    timer5.Start();     
                }
                catch(Exception exception1)
                {
                    Console.WriteLine("A OCURRIDO UN ERROR", exception1);
                
                }
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            timer4.Start();
            try
            {
               
            
      
                if (geckoWebBrowser1.Document.Body.InnerHtml.Contains("<a>Gift Cards</a>")) 
                {
                    this.Aprovadas.Items.Add("FRONTIER CHECKER" + this.dataGridView1.CurrentRow.Cells[0].Value + "|" + this.dataGridView1.CurrentRow.Cells[1].Value + "|" + this.dataGridView1.CurrentRow.Cells[2].Value + "|" + this.dataGridView1.CurrentRow.Cells[3].Value + "SE DEBITO 1 $ NICE");
                    this.geckoWebBrowser1.Navigate("https://www.google.com/");
                    lblStatus.Text = "Aprovada";
                    notifyIcon1.Visible = true;
                    lblStatus.ForeColor = Color.SpringGreen;
                    lblAprovadas.Text = Aprovadas.Items.Count.ToString();
                    this.f();
                    this.borrarPrimeraLinea(this.ar);
                    timer3.Stop();
                    timer1.Start();
                    timer4.Stop();
                }
                else if (geckoWebBrowser1.Document.Body.InnerHtml.Contains("Hello") )
                {
                    this.Reprovadas.Items.Add("FRONTIER CHECKER" + this.dataGridView1.CurrentRow.Cells[0].Value + "|" + this.dataGridView1.CurrentRow.Cells[1].Value + "|" + this.dataGridView1.CurrentRow.Cells[2].Value + "|" + this.dataGridView1.CurrentRow.Cells[3].Value + "NO SE DEBITO");
                    lblStatus.Text = "Reprovada";
                    this.geckoWebBrowser1.Navigate("https://www.google.com/");
                    lblStatus.ForeColor = Color.Red;
                    lblReprovadas.Text = Reprovadas.Items.Count.ToString();
                    this.f();
                    this.borrarPrimeraLinea(this.ar);
                    timer3.Stop();
                    timer1.Start();
                    timer4.Stop();
                }
            }
            catch 
            {

            }
            
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            this.geckoWebBrowser1.Navigate("google.com");
            lblStatus.Text = "Tiempo Expirado..";
            lblStatus.ForeColor = Color.Red;
            timer1.Start();
            timer4.Stop();
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            timer3.Start();
            lblStatus.Text = Convert.ToString(this.dataGridView1.CurrentRow.Cells[0].Value);
            lblStatus.ForeColor = Color.Gold;
            //////////////////////////////////// SEGUNDA PAGGINA GATEWAY 
            //var password = new Gecko.DOM.GeckoInputElement(geckoWebBrowser1.Document.GetHtmlElementById("ap_password").DomObject);
           //password.Value = @"loquendo123";

            //var clicButton = new Gecko.DOM.GeckoInputElement(geckoWebBrowser1.Document.GetHtmlElementById("signInSubmit").DomObject);
            //clicButton.Click();

            ////////////////////////////////////////////////////////
            timer5.Stop();
        }

        private void tContador_Tick(object sender, EventArgs e)
        {
            int suma = 0;
            try
            {
                var loopTo = dataGridView1.RowCount - 1;
                for (int i = 0; i <= loopTo; i++)
                {
                    if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[1].ColumnIndex)) 
                    {
                        suma+=1;
                    }

                }

                this.lblContador.Text = Convert.ToString(suma);
            }
            catch (Exception )
            {
                lblContador.Text = "0";
            }

            if ((lblContador.Text == "1"))
            {
                lblContador.Text = "0";
                this.tContador.Stop();
                this.timer1.Stop();
                this.timer2.Stop();
                this.timer3.Stop();
                lblStatus.Text = "Teste Finalizado !";
                lblStatus.ForeColor = Color.Gray;
                 
            }
        }

        private void txtDirectorio_TextChanged(object sender, EventArgs e)
        {

        }

        private void Reprovadas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
