using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace AppMenuOrden
{
    public partial class Form1 : Form
    {
        Dictionary<int, KeyValuePair<string, string>> datos;
        public Form1()
        {
            datos = new Dictionary<int, KeyValuePair<string, string>>();

            // simular datos de la base de datos
            datos.Add(1, new KeyValuePair<string, string>("Papas sencillas", "20.00"));
            datos.Add(2, new KeyValuePair<string, string>("Papas grandes", "30.00"));
            datos.Add(3, new KeyValuePair<string, string>("Hamburguesa sencilla", "40.00"));
            datos.Add(4, new KeyValuePair<string, string>("Coca-Cola 1/2 Lt", "25.00"));
            datos.Add(5, new KeyValuePair<string, string>("Tacos al pastor", "35.00"));
            datos.Add(6, new KeyValuePair<string, string>("Orden Enchiladas 3 U", "60.00"));
            datos.Add(7, new KeyValuePair<string, string>("Pupusa Mixta Unidad", "25.00"));
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var dato in datos)
            {
                Button btn = new Button()
                {
                    Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Regular, GraphicsUnit.Point, 0),
                    ForeColor = Color.RoyalBlue,
                    Size = new Size(287, 104),
                    Tag = dato.Key,
                    Text = $"{dato.Value.Key}\n{dato.Value.Value}",
                    UseVisualStyleBackColor = true
                };

                btn.Click += btnMenu_Click;

                flowPanel.Controls.Add(btn);
            }
        }



        private void btnMenu_Click(object sender, EventArgs e)
        {
            Button botoncito = sender as Button;
            int id = Convert.ToInt32(botoncito.Tag.ToString());

            // consultar a la base de datos con el id de la linea anterior
            var prod = datos.Where(x => x.Key == id).First();

            //var prod = (from p in ctx.menu
            //            where p.id = id).firstordefault();

            dataGridView1.Rows.Add(prod.Value.Key, prod.Value.Value, 1, prod.Value.Value);
            calcularTotales();
        }

        void calcularTotales()
        {
            double total = 0;
            foreach (DataGridViewRow fila in dataGridView1.Rows)
            {
                total += Convert.ToDouble(fila.Cells[3].Value);
            }

            textBox1.Text = total.ToString();
            textBox2.Text = (total * 0.15).ToString();
            textBox3.Text = (total + (total * 0.15)).ToString();
        }

        private void DataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int cant = Convert.ToInt32( Interaction.InputBox("Ingrese la cantidad: ") );
                double precio = Convert.ToDouble(dataGridView1.CurrentRow.Cells[1].Value);
                dataGridView1.CurrentRow.Cells[2].Value = cant;
                dataGridView1.CurrentRow.Cells[3].Value = cant * precio;

                calcularTotales();
            }
            catch (Exception)
            {

            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            int indice = dataGridView1.CurrentRow.Index;
            dataGridView1.Rows.RemoveAt(indice);

            calcularTotales();
        }
    }
}
