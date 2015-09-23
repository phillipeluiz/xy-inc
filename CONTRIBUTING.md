# xy-inc
Serviço para consulta de CEP na base do correios através do site: http://www.buscacep.correios.com.br. Desenvolvido em C#(Sharp).

<h3>
Para configuração (instruções para execução):
</h3>

<p>Está disponível os arquivos para que o webservice seja publicado no servidor de aplicação da microsoft Internet Information Service (IIS).
</p>

<p><i>Os arquivos que devem ser publicados no IIS são:</i></p>
<ul>
<li>bin\BuscaCEP.dll
<li>Web.config
<li>WSBuscaCEP.asmx
</ul>

<p>Após publicado o webservice, deve-se publicar no servidor web os arquivos abaixo, referente a um exemplo de site que consome o serviço especificado acima.</p>

<p><i>Os arquivos que devem ser publicados no IIS são:</i></p>

<ul>
<li>bin\PaginaConsultaCEP.dll
<li>ConsultarCEP.aspx
<li>Service References
<li>web.config
</ul>

<h3>Testes</h3>

Executar a chamada para a página abaixo através de qualquer navegador de internet para realizar os testes:
<code>
http://localhost/PaginaConsultaCEP/ConsultarCEP.aspx
<code>


<h3>Fontes</h3>

<p>Fontes do webservice de consulta no site correios:
<code>
https://github.com/phillipeluiz/xy-inc/tree/master/BuscaCEP/BuscaCEP
</code>
<p>Fontes da página que consome o serviço:
<code>
https://github.com/phillipeluiz/xy-inc/tree/master/BuscaCEP/PaginaConsultaCEP
</code>







