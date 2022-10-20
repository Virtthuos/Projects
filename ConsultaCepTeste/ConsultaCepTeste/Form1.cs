using RestSharp;
using System;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace ConsultaCepTeste
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void ConsultaBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var client = new RestClient(String.Format("https://viacep.com.br/ws/{0}/json/", consultaCepTextBox.Text));

                var request = new RestRequest();

                var response = await client.GetAsync(request);

                string json = response.Content;

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    MessageBox.Show("Houve um problema ao consultar o CEP...");
                }

                else
                {
                    DadosRetornoAPI dadosRetornoAPI = new JavaScriptSerializer().Deserialize<DadosRetornoAPI>(json);

                    cepTextBox.Text = dadosRetornoAPI.Cep;
                    logradouroTextBox.Text = dadosRetornoAPI.Logradouro;
                    complementoTextBox.Text = dadosRetornoAPI.Complemento;
                    bairroTextBox.Text = dadosRetornoAPI.Bairro;
                    LocalidadeTextBox.Text = dadosRetornoAPI.Localidade;
                    UfTextBox.Text = dadosRetornoAPI.Uf;
                    ibgeTextBox.Text = dadosRetornoAPI.Ibge;
                    giaTextBox.Text = dadosRetornoAPI.Gia;
                    dddTextBox.Text = dadosRetornoAPI.Ddd;
                    siafiTextBox.Text = dadosRetornoAPI.Siafi;

                    if (cepTextBox.Text == "")
                    {
                        MessageBox.Show("Houve um problema ao consultar o CEP...");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("CEP inválido!", ex.Message);
            }
        }
    }
}
