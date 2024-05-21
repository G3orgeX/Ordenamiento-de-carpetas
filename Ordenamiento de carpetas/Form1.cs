using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Ordenamiento_de_carpetas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void IniciarOrden()
        {
            string rutaOrigen = @"D:\Jorge\MIs documentos\PruebaOrdenamiento";
            string rutaDestino = @"D:\Jorge\MIs documentos\PruebaOrdenamiento";
            if (txtRutaOrigen.Text != "") {
            rutaOrigen = txtRutaOrigen.Text;
            }
            if (txtRutaDestino.Text.Trim() != "") { 
                rutaDestino = txtRutaDestino.Text;
            }
              
                OrganizarArchivosPorAño(rutaOrigen, rutaDestino);

            MessageBox.Show("Archivos organizados correctamente.");
            Console.ReadLine();
        }

        static void OrganizarArchivosPorAño(string rutaOrigen, string rutaDestino)
        {
            //string[] archivos = Directory.GetFiles(rutaOrigen, "*.pdf");

            //foreach (string archivo in archivos)
            //{
            //    string nombreArchivo = Path.GetFileName(archivo);
            //    int año = ObtenerAño(nombreArchivo);
            //    if (año != -1)
            //    {
            //        string carpetaAño = Path.Combine(rutaDestino, año.ToString());
            //        if (!Directory.Exists(carpetaAño))
            //        {
            //            Directory.CreateDirectory(carpetaAño);
            //        }
            //        string nuevoRuta = Path.Combine(carpetaAño, nombreArchivo);
            //        File.Move(archivo, nuevoRuta);
            //    }
            //}
            string[] archivos = Directory.GetFiles(rutaOrigen, "*.pdf");

            foreach (string archivo in archivos)
            {
                DateTime fechaCreacion = File.GetLastWriteTime(archivo);
                int año = fechaCreacion.Year;

                string carpetaAño = Path.Combine(rutaDestino, año.ToString());
                if (!Directory.Exists(carpetaAño))
                {
                    Directory.CreateDirectory(carpetaAño);
                }
                string nuevoRuta = Path.Combine(carpetaAño, Path.GetFileName(archivo));
                File.Move(archivo, nuevoRuta);
            }
        }

        static int ObtenerAño(string nombreArchivo)
        {
            // Suponiendo que el formato del nombre es "nombre_año.pdf"
            string[] partes = nombreArchivo.Split('_');
            if (partes.Length >= 2)
            {
                string añoStr = partes[partes.Length - 1].Split('.')[0]; // Obtener el año y eliminar la extensión .pdf
                if (int.TryParse(añoStr, out int año))
                {
                    return año;
                }
            }
            return -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IniciarOrden();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtRutaOrigen.Text = BuscarRuta();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtRutaDestino.Text = BuscarRuta();
        }

        static string BuscarRuta()
        {
            string ruta = "";
            using (var dialogo = new FolderBrowserDialog())
            {
                DialogResult resultado = dialogo.ShowDialog();
                if (resultado == DialogResult.OK && !string.IsNullOrWhiteSpace(dialogo.SelectedPath))
                {
                    ruta = dialogo.SelectedPath;
                }
            }
            return ruta;
        }
    }
}

