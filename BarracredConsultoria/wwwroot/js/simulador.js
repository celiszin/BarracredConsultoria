document.addEventListener('DOMContentLoaded', function () {
    // Constantes financeiras
    const taxaSelicAnual = 0.1075; 
    const taxaMensal = Math.pow(1 + taxaSelicAnual, 1/12) - 1; 

    // Utilitário de formatação
    function formatarMoeda(valor) {
        return valor.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
    }

    // Elementos da Calculadora por Objetivo
    const inputValor = document.getElementById('inputValor');
    const displayValor = document.getElementById('displayValor');
    const inputTempo = document.getElementById('inputTempo');
    const displayTempo = document.getElementById('displayTempo');
    const calcMensal = document.getElementById('calcMensal');
    const calcAcumulado = document.getElementById('calcAcumulado');
    const calcRendimento = document.getElementById('calcRendimento');
    const textViabilidade = document.getElementById('textViabilidade');

    function calcularPorObjetivo() {
        const valorObjetivo = parseFloat(inputValor.value);
        const meses = parseInt(inputTempo.value);

        displayValor.innerText = formatarMoeda(valorObjetivo);
        displayTempo.innerText = meses + " Meses";

        const pmt = (valorObjetivo * taxaMensal) / (Math.pow(1 + taxaMensal, meses) - 1);
        const totalInvestido = pmt * meses;
        const rendimento = valorObjetivo - totalInvestido;

        calcMensal.innerText = pmt.toLocaleString('pt-BR', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
        calcAcumulado.innerText = formatarMoeda(valorObjetivo);
        calcRendimento.innerText = "+ " + formatarMoeda(rendimento);

        atualizarMensagemViabilidade(meses);
    }

    function atualizarMensagemViabilidade(meses) {
        if (meses <= 12) {
            textViabilidade.innerText = "Meta de curto prazo agressiva! Vai exigir disciplina, mas o resultado vem rápido.";
        } else if (meses <= 48) {
            textViabilidade.innerText = "Excelente! Neste prazo intermediário, os juros compostos já começam a dar um bom empurrão no seu dinheiro.";
        } else {
            textViabilidade.innerText = "Planejamento fantástico! No longo prazo, grande parte do seu sonho será pago apenas pelos rendimentos.";
        }
    }


    inputValor.addEventListener('input', calcularPorObjetivo);
    inputTempo.addEventListener('input', calcularPorObjetivo);
    
    calcularPorObjetivo();
});