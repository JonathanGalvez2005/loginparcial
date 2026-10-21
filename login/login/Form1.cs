using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace login
{
    public partial class Frm_usuario : Form
    {
        private const string CS = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\Users\Estudiante\Desktop\parcialLogin\login\login\usuarios_usss121926.mdf;Integrated Security = True";
        private const string TABLA = "[dbo].[Table]";
        private DataTable dt = new DataTable();
        private int pos = 0;


        public Frm_usuario()
        {
            InitializeComponent();
        }

        private void CargarDatos()
        {
            try
            {
                dt.Clear();
                using (SqlConnection cn = new SqlConnection(CS))
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM " + TABLA + " ORDER BY idUsuario", cn))
                {
                    da.Fill(dt);
                }
                dataGridView1.DataSource = dt;

                if (dt.Rows.Count > 0)
                    MostrarRegistro(Math.Min(pos, dt.Rows.Count - 1));
                else
                    LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }

        private void MostrarRegistro(int nuevaPos)
        {
            if (dt.Rows.Count == 0) { LimpiarCampos(); return; }
            if (nuevaPos < 0) nuevaPos = 0;
            if (nuevaPos > dt.Rows.Count - 1) nuevaPos = dt.Rows.Count - 1;
            pos = nuevaPos;

            DataRow r = dt.Rows[pos];
            txtUsuario.Text = r["usuario"].ToString();
            txtClave.Text = r["clave"].ToString();
            txtConfirmar.Text = r["clave"].ToString();
            txtNombre.Text = r["nombre"].ToString();
            txtDireccion.Text = r["direccion"].ToString();
            txtTelefono.Text = r["telefono"].ToString();

            dataGridView1.ClearSelection();
            dataGridView1.Rows[pos].Selected = true;
        }

        private void LimpiarCampos()
        {
            txtUsuario.Clear();
            txtClave.Clear();
            txtConfirmar.Clear();
            txtNombre.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtUsuario.Focus();
        }

        private bool Validar()
        {
            string usuario = txtUsuario.Text.Trim();
            string clave = txtClave.Text;

            if (usuario.Length < 6 || usuario.Length > 16)
            {
                MessageBox.Show("El usuario debe tener entre 6 y 16 caracteres.");
                txtUsuario.Focus();
                return false;
            }
            if (!clave.Any(char.IsLetter) || !clave.Any(char.IsDigit))
            {
                MessageBox.Show("La clave debe contener letras y numeros.");
                txtClave.Focus();
                return false;
            }
            if (clave != txtConfirmar.Text)
            {
                MessageBox.Show("La clave y la confirmacion no son iguales.");
                txtConfirmar.Focus();
                return false;
            }
            if (txtNombre.Text.Trim() == "")
            {
                MessageBox.Show("Ingrese el nombre.");
                txtNombre.Focus();
                return false;
            }
            return true;
        }

        private void AgregarParametros(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@usuario", txtUsuario.Text.Trim());
            cmd.Parameters.AddWithValue("@clave", txtClave.Text);
            cmd.Parameters.AddWithValue("@nombre", txtNombre.Text.Trim());
            cmd.Parameters.AddWithValue("@direccion", txtDireccion.Text.Trim());
            cmd.Parameters.AddWithValue("@telefono", txtTelefono.Text.Trim());
        }
        private void Frm_usuario_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
