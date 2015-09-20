using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace PaginaConsultaCEP
{
    public partial class ConsultarCEP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }

        protected void btnConsultarEnderecoPorCEP_Click(object sender, EventArgs e)
        {
            try
            {
                WSBuscaCEP.WSBuscaCEPSoapClient wsCEP = new WSBuscaCEP.WSBuscaCEPSoapClient("WSBuscaCEPSoap");

                WSBuscaCEP.Endereco[] arrEndereco = wsCEP.endereco_por_CEP(txtCEP.Text);

                grdResultado.DataSource = arrEndereco;
                grdResultado.DataBind();
            }
            catch (Exception ex)
            { 
                msgErro.Text = ex.Message.ToString();
                grdResultado.DataBind();
            }

        }

        protected void btnConsultaCEPporEndereco_Click(object sender, EventArgs e)
        {
            try
            {
            WSBuscaCEP.WSBuscaCEPSoapClient wsCEP = new WSBuscaCEP.WSBuscaCEPSoapClient("WSBuscaCEPSoap");

            WSBuscaCEP.Endereco[] arrEndereco = wsCEP.CEP_por_Endereco(txtEstado.Text, txtCidade.Text, txtLogradouro.Text);

            grdResultado.DataSource = arrEndereco;
            grdResultado.DataBind();
                }
            catch (Exception ex)
            { msgErro.Text = ex.Message.ToString();
              grdResultado.DataBind();
            }

        }


    }
}