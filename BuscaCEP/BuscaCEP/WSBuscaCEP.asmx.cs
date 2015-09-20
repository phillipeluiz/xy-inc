using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.Net;
using System.IO;
using System.Text.RegularExpressions;



namespace BuscaCEP
{
    /// <summary>
    /// Summary description for WSBuscaCEP
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WSBuscaCEP : System.Web.Services.WebService
    {

        [WebMethod]
        /// <summary>
        /// Método utilizado para buscar endereço fornecendo como parametro o código CEP
        /// </summary>
        public List<Endereco> endereco_por_CEP(String strCEP)
        {
            try
            {

                //Realiza conexão com a página do correios, informando o CEP que deve ser consultado
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.buscacep.correios.com.br/servicos/dnec/consultaLogradouroAction.do?Metodo=listaLogradouro&CEP=" + strCEP + "&TipoConsulta=cep");
                request.Method = "POST";
                //Obtem a resposta do lado do servidor.
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                //captura a cadeia de valores correspondentes ao código HTML que o servidor respondeu
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                //converte para String
                string strWebPage = reader.ReadToEnd();

                //Lista será utilizada para armazenar os resultados da consulta
                List<Endereco> lstEndereco = new List<Endereco>();

                if (strWebPage.IndexOf("<font color=\"black\">CEP NAO ENCONTRADO</font>") > 0)
                {
                    Endereco objEndereco = new Endereco();
                    objEndereco.cep = "CEP não encontrado!";
                    lstEndereco.Add(objEndereco);
                    objEndereco = null;
                    return lstEndereco;


                }
                else
                {
                    Endereco objEndereco = new Endereco();
                    objEndereco.cep = strCEP;
                    objEndereco.logradouro = Regex.Match(strWebPage, "<td width=\"268\" style=\"padding: 2px\">(.*)</td>").Groups[1].Value;
                    objEndereco.bairro = Regex.Matches(strWebPage, "<td width=\"140\" style=\"padding: 2px\">(.*)</td>")[0].Groups[1].Value;
                    objEndereco.cidade = Regex.Matches(strWebPage, "<td width=\"140\" style=\"padding: 2px\">(.*)</td>")[1].Groups[1].Value;
                    objEndereco.estado = Regex.Match(strWebPage, "<td width=\"25\" style=\"padding: 2px\">(.*)</td>").Groups[1].Value;
                    lstEndereco.Add(objEndereco);
                    objEndereco = null;
                    return lstEndereco;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro , favor realizar o preechimento correto!");
            }
             
        }

        [WebMethod]
        /// <summary>
        /// Método utilizado para buscar CEP fornecendo como parametro o Estado, a Cidade e o Logradouro
        /// </summary>
        public List<Endereco> CEP_por_Endereco(String strUF, String strCidade, String strLogradouro)
        {

            try
            {
                //Realiza conexão com a página do correios, informando o CEP que deve ser consultado
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://www.buscacep.correios.com.br/servicos/dnec/consultaLogradouroAction.do?Metodo=listaLogradouro&TipoConsulta=logradouro&UF=" + strUF + "&Localidade=" + strCidade + "&Logradouro=" + strLogradouro + "");

                //Obtem a resposta do lado do servidor.
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                //captura a cadeia de valores correspondentes ao código HTML que o servidor respondeu
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                //converte para String
                string strWebPage = reader.ReadToEnd();

                //Lista será utilizada para armazenar os resultados da consulta
                List<Endereco> lstEndereco = new List<Endereco>();

                //Faz uso de expressão regular para verificar se o CEP não foi encontrado
                if (strWebPage.IndexOf("<font color=\"black\">CEP NAO ENCONTRADO</font>") > 0)
                {
                    Endereco objEndereco = new Endereco();
                    objEndereco.cep = "CEP não encontrado!";
                    lstEndereco.Add(objEndereco);
                    objEndereco = null;
                    return lstEndereco;
                }
                else
                {
                    Int16 QtdRegistros = 0;
                    String strQtdRegistros = Regex.Match(strWebPage, "<b>(.*) Logradouro").Groups[1].Value;
                    if (strQtdRegistros != "")
                    { QtdRegistros = Convert.ToInt16(strQtdRegistros); }
                    else
                    { QtdRegistros = 0; };

                    if (QtdRegistros > 1)
                    {
                        for (int j = 0; j <= QtdRegistros - 1; j++)
                        {
                            Endereco objEndereco = new Endereco();
                            objEndereco.cep = Regex.Matches(strWebPage, "<td width=\"65\" style=\"padding: 2px\">(.*)</td>")[j].Groups[1].Value;
                            objEndereco.logradouro = Regex.Matches(strWebPage, "<td width=\"268\" style=\"padding: 2px\">(.*)</td>")[j].Groups[1].Value;
                            objEndereco.bairro = Regex.Matches(strWebPage, "<td width=\"140\" style=\"padding: 2px\">(.*)</td>")[j].Groups[1].Value;
                            objEndereco.cidade = Regex.Matches(strWebPage, "<td width=\"140\" style=\"padding: 2px\">(.*)</td>")[j + 1].Groups[1].Value;
                            objEndereco.estado = Regex.Matches(strWebPage, "<td width=\"25\" style=\"padding: 2px\">(.*)</td>")[j].Groups[1].Value;
                            lstEndereco.Add(objEndereco);
                            objEndereco = null;
                        }

                        return lstEndereco;
                    }
                    else
                    {
                        Endereco objEndereco = new Endereco();
                        objEndereco.cep = Regex.Match(strWebPage, "<td width=\"65\" style=\"padding: 2px\">(.*)</td>").Groups[1].Value;
                        objEndereco.logradouro = Regex.Match(strWebPage, "<td width=\"268\" style=\"padding: 2px\">(.*)</td>").Groups[1].Value;
                        objEndereco.bairro = Regex.Matches(strWebPage, "<td width=\"140\" style=\"padding: 2px\">(.*)</td>")[0].Groups[1].Value;
                        objEndereco.cidade = Regex.Matches(strWebPage, "<td width=\"140\" style=\"padding: 2px\">(.*)</td>")[1].Groups[1].Value;
                        objEndereco.estado = Regex.Match(strWebPage, "<td width=\"25\" style=\"padding: 2px\">(.*)</td>").Groups[1].Value;
                        lstEndereco.Add(objEndereco);
                        objEndereco = null;
                        return lstEndereco;
                    }

                }
            }
            catch (Exception e )
            { throw new Exception("Erro , favor realizar o preechimento correto!"); }


        }



    }
}
