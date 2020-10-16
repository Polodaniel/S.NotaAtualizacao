var DivDadosGerais = "#DadosGerais";
var DivVisaoGeral = "#VisaoGeral";
var DivDetalhes = "#Detalhes";
var DivAnaliseAjuste = "#AnaliseAjuste";
var DivImagens = "#Imagens";

function OcultarDadosIniciais()
{
    $(DivVisaoGeral).hide(10);
    $(DivDetalhes).hide(10);
    $(DivAnaliseAjuste).hide(10);
    $(DivImagens).hide(10);
}

function BotaoDadosGeraisProximo()
{
    $(DivDadosGerais).hide(300);
    $(DivVisaoGeral).show(300);
    $(DivDetalhes).hide(300);
    $(DivAnaliseAjuste).hide(300);
    $(DivImagens).hide(300);
}

function BotaoVisaoGeralAnterior()
{
    $(DivDadosGerais).show(300);
    $(DivVisaoGeral).hide(300);
    $(DivDetalhes).hide(300);
    $(DivAnaliseAjuste).hide(300);
    $(DivImagens).hide(300);
}

function BotaoVisaoGeralProximo() {
    $(DivDadosGerais).hide(300);
    $(DivVisaoGeral).hide(300);
    $(DivDetalhes).show(300);
    $(DivAnaliseAjuste).hide(300);
    $(DivImagens).hide(300);
}

function BotaoDetalhesAnterior() {
    $(DivDadosGerais).hide(300);
    $(DivVisaoGeral).show(300);
    $(DivDetalhes).hide(300);
    $(DivAnaliseAjuste).hide(300);
    $(DivImagens).hide(300);
}

function BotaoDetalhesProximo() {
    $(DivDadosGerais).hide(300);
    $(DivVisaoGeral).hide(300);
    $(DivDetalhes).hide(300);
    $(DivAnaliseAjuste).show(300);
    $(DivImagens).hide(300);
}

function BotaoAnaliseAjusteAnterior() {
    $(DivDadosGerais).hide(300);
    $(DivVisaoGeral).hide(300);
    $(DivDetalhes).show(300);
    $(DivAnaliseAjuste).hide(300);
    $(DivImagens).hide(300);
}


function BotaoAnaliseAjusteProximo() {
    $(DivDadosGerais).hide(300);
    $(DivVisaoGeral).hide(300);
    $(DivDetalhes).hide(300);
    $(DivAnaliseAjuste).hide(300);
    $(DivImagens).show(300);
}

function BotaoImagensAnterior() {
    $(DivDadosGerais).hide(300);
    $(DivVisaoGeral).hide(300);
    $(DivDetalhes).hide(300);
    $(DivAnaliseAjuste).show(300);
    $(DivImagens).hide(300);
}