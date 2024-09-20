using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Newtonsoft.Json.Linq;

namespace ViaCepApp
{
    public partial class MainPage : ContentPage
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnConsultarClicked(object sender, EventArgs e)
        {
            string cep = cepEntry.Text?.Trim();

            if (string.IsNullOrEmpty(cep) || cep.Length != 8)
            {
                resultadoLabel.Text = "Digite um CEP válido com 8 dígitos.";
                return;
            }

            try
            {
                string url = $"https://viacep.com.br/ws/{cep}/json/";
                string response = await httpClient.GetStringAsync(url);

                var endereco = JObject.Parse(response);

                if (endereco["erro"] != null)
                {
                    resultadoLabel.Text = "CEP não encontrado.";
                }
                else
                {
                    resultadoLabel.Text = $"Endereço: {endereco["logradouro"]}, {endereco["bairro"]}, {endereco["localidade"]} - {endereco["uf"]}";
                }
            }
            catch (Exception ex)
            {
                resultadoLabel.Text = $"Erro: {ex.Message}";
            }
        }
    }
}

