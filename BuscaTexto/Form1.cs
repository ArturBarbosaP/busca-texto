using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace BuscaTexto
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LimparTexto()
        {
            texto.Clear(); //limpa o texto
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LimparTexto(); //limpa o texto
        }

        private void sobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, //sobre
               "Busca em Texto - 2024/1\n\nDesenvolvido por:\n72300400 - Artur Barbosa\n72300280 - Letícia Martins\nProf. Virgílio Borges de Oliveira\n\nAlgoritmos e Estruturas de Dados II\nFaculdade COTEMIG\nSomente para fins didáticos.",
               "Sobre o trabalho...",
               MessageBoxButtons.OK,
               MessageBoxIcon.Information);
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK) //se o usuário selecionar um arquivo e clicar no botão abrir
                {
                    LimparTexto(); //limpa o texto antes de abrir o arquivo
                    StreamReader stream = new StreamReader(openFileDialog.FileName); //abre o arquivo selecionado pelo usuário
                    texto.Text = stream.ReadToEnd(); //le todo o arquivo e passa o conteúdo para o texto
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro ao Abrir Arquivo", MessageBoxButtons.OK, MessageBoxIcon.Error); //se ocorrer algum erro ao abrir um arquivo, mostrar erro ao usuário
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Algoritmos
        private void SelecionarTexto(int comeco, string padrao)
        {
            try
            {
                texto.Focus(); //foco no texto
                texto.Select(comeco, padrao.Length); //selecionando no texto
                texto.SelectionBackColor = Color.Yellow; //colorindo o fundo
                if (cbx_localizarSubstituir.Checked) //se a opção de localizar e substituir estiver selecionada, irá substituir o texto marcado pela string informada pelo usuário
                    texto.Text = texto.Text.Replace(texto.SelectedText, txt_substituir.Text); //substituindo no texto
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro ao Selecionar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void forçaBrutaToolStripMenuItem_Click(object sender, EventArgs e) //algoritmo Força Bruta
        {
            try
            {
                texto.Focus(); //foco no texto
                texto.SelectAll(); //removendo a seleção anterior
                texto.SelectionBackColor = texto.BackColor;
                texto.Select(0, 0);

                string padrao = Interaction.InputBox("Digite o padrão que deseja pesquisar", "Pesquisa - Força Bruta"); //input do usuário com o padrão desejado para a pesquisa
                if (padrao == "") //se o usuário não preencher o campo de pesquisa, exibir o erro
                    throw new Exception("Insira um padrão para pesquisar!");

                int comecoSelecao; //índice do começo da seleção
                if (cbx_caseSensitive.Checked) //se a opção de diferenciar maiúsculas de minúsculas estiver habilitada
                {
                    comecoSelecao = BuscaForcaBruta.forcaBruta(padrao, texto.Text, 0); //pesquisa case sensitive começando do começo do texto e retornando o índice inicial do padrão
                    while (comecoSelecao != -1) //enquanto a pesquisa achar o padrão no texto, continuar a busca
                    {
                        SelecionarTexto(comecoSelecao, padrao); //selecionando no texto
                        comecoSelecao = BuscaForcaBruta.forcaBruta(padrao, texto.Text, comecoSelecao + padrao.Length); //pesquisa case sensitive começando do final da última pesquisa e retornando o índice inicial do padrão
                    }
                }
                else
                {   //caso esteja desabilitada
                    padrao = padrao.ToUpper(); //passando o padrao e o texto para letras maiúsculas
                    string textoUpper = texto.Text.ToUpper();
                    comecoSelecao = BuscaForcaBruta.forcaBruta(padrao, textoUpper, 0); //pesquisa case sensitive começando do começo do texto e retornando o índice inicial do padrão
                    while (comecoSelecao != -1) //enquanto a pesquisa achar o padrão no texto, continuar a busca
                    {
                        SelecionarTexto(comecoSelecao, padrao); //selecionando no texto
                        comecoSelecao = BuscaForcaBruta.forcaBruta(padrao, textoUpper, comecoSelecao + padrao.Length); //pesquisa case sensitive começando do final da última pesquisa e retornando o índice inicial do padrão
                    }
                }
                if (texto.SelectedText == "" && !cbx_localizarSubstituir.Checked) //se após a pesquisa nenhum texto estiver selecionado e a opção de localizar e substituir estiver desabilitada, o padrão não está no texto
                    throw new Exception($"Não foi possível encontrar '{padrao}'");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro ao Pesquisar - Força Bruta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rabinKarpToolStripMenuItem_Click(object sender, EventArgs e) //algoritmo Rabin Karp
        {
            try
            {
                texto.Focus(); //foco no texto
                texto.SelectAll(); //removendo a seleção anterior
                texto.SelectionBackColor = texto.BackColor;
                texto.Select(0, 0);

                string padrao = Interaction.InputBox("Digite o padrão que deseja pesquisar", "Pesquisa - Rabin Karp"); //input do usuário com o padrão desejado para a pesquisa
                if (padrao == "") //se o usuário não preencher o campo de pesquisa, exibir o erro
                    throw new Exception("Insira um padrão para pesquisar!");

                int comecoSelecao; //índice do começo da seleção
                if (cbx_caseSensitive.Checked) //se a opção de diferenciar maiúsculas de minúsculas estiver habilitada
                {
                    comecoSelecao = BuscaRabinKarp.RKSearch(padrao, texto.Text, 0); //pesquisa case sensitive começando do começo do texto e retornando o índice inicial do padrão
                    while (comecoSelecao != -1) //enquanto a pesquisa achar o padrão no texto, continuar a busca
                    {
                        SelecionarTexto(comecoSelecao, padrao); //selecionando no texto
                        comecoSelecao = BuscaRabinKarp.RKSearch(padrao, texto.Text, comecoSelecao + padrao.Length); //pesquisa case sensitive começando do final da última pesquisa e retornando o índice inicial do padrão
                    }
                }
                else
                {   //caso esteja desabilitada
                    padrao = padrao.ToUpper(); //passando o padrao e o texto para letras maiúsculas
                    string textoUpper = texto.Text.ToUpper();
                    comecoSelecao = BuscaRabinKarp.RKSearch(padrao, textoUpper, 0); //pesquisa case sensitive começando do começo do texto e retornando o índice inicial do padrão
                    while (comecoSelecao != -1) //enquanto a pesquisa achar o padrão no texto, continuar a busca
                    {
                        SelecionarTexto(comecoSelecao, padrao); //selecionando no texto
                        comecoSelecao = BuscaRabinKarp.RKSearch(padrao, textoUpper, comecoSelecao + padrao.Length); //pesquisa case sensitive começando do final da última pesquisa e retornando o índice inicial do padrão
                    }
                }
                if (texto.SelectedText == "" && !cbx_localizarSubstituir.Checked) //se após a pesquisa nenhum texto estiver selecionado e a opção de localizar e substituir estiver desabilitada, o padrão não está no texto
                    throw new Exception($"Não foi possível encontrar '{padrao}'");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro ao Pesquisar - Rabin Karp", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void kMPToolStripMenuItem_Click(object sender, EventArgs e) //algoritmo KMP
        {
            try
            {
                texto.Focus(); //foco no texto
                texto.SelectAll(); //removendo a seleção anterior
                texto.SelectionBackColor = texto.BackColor;
                texto.Select(0, 0);

                string padrao = Interaction.InputBox("Digite o padrão que deseja pesquisar", "Pesquisa - KMP"); //input do usuário com o padrão desejado para a pesquisa
                if (padrao == "") //se o usuário não preencher o campo de pesquisa, exibir o erro
                    throw new Exception("Insira um padrão para pesquisar!");

                int comecoSelecao; //índice do começo da seleção

                if (cbx_caseSensitive.Checked) //se a opção de diferenciar maiúsculas de minúsculas estiver habilitada
                {
                    comecoSelecao = BuscaKMP.KMPSearch(padrao, texto.Text, 0); //pesquisa case sensitive começando do começo do texto e retornando o índice inicial do padrão
                    while (comecoSelecao != -1) //enquanto a pesquisa achar o padrão no texto, continuar a busca
                    {
                        SelecionarTexto(comecoSelecao, padrao); //selecionando no texto
                        comecoSelecao = BuscaKMP.KMPSearch(padrao, texto.Text, comecoSelecao + padrao.Length); //pesquisa case sensitive começando do final da última pesquisa e retornando o índice inicial do padrão
                    }
                }
                else
                {   //caso esteja desabilitada
                    padrao = padrao.ToUpper(); //passando o padrao e o texto para letras maiúsculas
                    string textoUpper = texto.Text.ToUpper();
                    comecoSelecao = BuscaKMP.KMPSearch(padrao, textoUpper, 0); //pesquisa case sensitive começando do começo do texto e retornando o índice inicial do padrão
                    while (comecoSelecao != -1) //enquanto a pesquisa achar o padrão no texto, continuar a busca
                    {
                        SelecionarTexto(comecoSelecao, padrao); //selecionando no texto
                        comecoSelecao = BuscaKMP.KMPSearch(padrao, textoUpper, comecoSelecao + padrao.Length); //pesquisa case sensitive começando do final da última pesquisa e retornando o índice inicial do padrão
                    }
                }
                if (texto.SelectedText == "" && !cbx_localizarSubstituir.Checked) //se após a pesquisa nenhum texto estiver selecionado e a opção de localizar e substituir estiver desabilitada, o padrão não está no texto
                    throw new Exception($"Não foi possível encontrar '{padrao}'");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro ao Pesquisar - KMP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void boyerMooreToolStripMenuItem_Click(object sender, EventArgs e) //algoritmo Boyer Moore
        {
            try
            {
                texto.Focus(); //foco no texto
                texto.SelectAll(); //removendo a seleção anterior
                texto.SelectionBackColor = texto.BackColor;
                texto.Select(0, 0);

                string padrao = Interaction.InputBox("Digite o padrão que deseja pesquisar", "Pesquisa - Boyer Moore"); //input do usuário com o padrão desejado para a pesquisa
                if (padrao == "") //se o usuário não preencher o campo de pesquisa, exibir o erro
                    throw new Exception("Insira um padrão para pesquisar!");

                int comecoSelecao; //índice do começo da seleção

                if (cbx_caseSensitive.Checked) //se a opção de diferenciar maiúsculas de minúsculas estiver habilitada
                {
                    comecoSelecao = BuscaBoyerMoore.BMSearch(padrao, texto.Text, 0); //pesquisa case sensitive começando do começo do texto e retornando o índice inicial do padrão
                    while (comecoSelecao != -1) //enquanto a pesquisa achar o padrão no texto, continuar a busca
                    {
                        SelecionarTexto(comecoSelecao, padrao); //selecionando no texto
                        comecoSelecao = BuscaBoyerMoore.BMSearch(padrao, texto.Text, comecoSelecao + padrao.Length); //pesquisa case sensitive começando do final da última pesquisa e retornando o índice inicial do padrão
                    }
                }
                else
                {   //caso esteja desabilitada
                    padrao = padrao.ToUpper(); //passando o padrao e o texto para letras maiúsculas
                    string textoUpper = texto.Text.ToUpper();
                    comecoSelecao = BuscaBoyerMoore.BMSearch(padrao, textoUpper, 0); //pesquisa case sensitive começando do começo do texto e retornando o índice inicial do padrão
                    while (comecoSelecao != -1) //enquanto a pesquisa achar o padrão no texto, continuar a busca
                    {
                        SelecionarTexto(comecoSelecao, padrao); //selecionando no texto
                        comecoSelecao = BuscaBoyerMoore.BMSearch(padrao, textoUpper, comecoSelecao + padrao.Length); //pesquisa case sensitive começando do final da última pesquisa e retornando o índice inicial do padrão
                    }
                }
                if (texto.SelectedText == "" && !cbx_localizarSubstituir.Checked) //se após a pesquisa nenhum texto estiver selecionado e a opção de localizar e substituir estiver desabilitada, o padrão não está no texto
                    throw new Exception($"Não foi possível encontrar '{padrao}'");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message, "Erro ao Pesquisar - Boyer Moore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbx_localizarSubstituir_CheckedChanged(object sender, EventArgs e)
        {
            lbl_substituir.Visible = cbx_localizarSubstituir.Checked; //se a opção de localizar e substituir estiver ativa, irá aparecer o textbox para o usuário digitar a string que será inserida
            txt_substituir.Visible = cbx_localizarSubstituir.Checked;
        }
    }
}