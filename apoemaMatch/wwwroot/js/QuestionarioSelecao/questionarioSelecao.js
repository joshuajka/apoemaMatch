
let questaoTipoResposta = document.getElementById("QuestaoBase_TipoResposta");
let inputPergunta = document.getElementById("QuestaoBase_Pergunta");
let spanPergunta = document.getElementById("spanPergunta");
let divOpcaoResposta = document.getElementById("div_OpcaoResposta");
let inputOpcaoRespostaTexto1 = document.getElementById("QuestaoBase_OpcaoRespostaBase_TextoPrimeiraOpcao");
let spanOpcaoResposta1 = document.getElementById("spanOpcaoResposta1");
let inputOpcaoRespostaTexto2 = document.getElementById("QuestaoBase_OpcaoRespostaBase_TextoSegundaOpcao");
let spanOpcaoResposta2 = document.getElementById("spanOpcaoResposta2");
let questoesInput = document.getElementById("InputQuestoes");
let divQuestoes = document.getElementById("div_Questoes");
let questoesAdicionadas = [];

window.addEventListener('load', () => {

    document.getElementById("botaoAdicionarQuestao").addEventListener('click', () => {
        valideInput(perguntaEstaValida(), spanPergunta, "Pergunta é obrigatória");
        if (ehTipoRespostaOpcao()) {
            valideInput(opcaoRespostaEstaValida(), spanOpcaoResposta1, "Primeira opcão é obrigatória");
            valideInput(opcaoRespostaEstaValida(), spanOpcaoResposta2, "Segunda opcão é obrigatória");
        }

        if (questaoEstaValida())
            adicioneQuestao();
    });

    questaoTipoResposta.addEventListener('change', () => {
        if (ehTipoRespostaOpcao()) {
            divOpcaoResposta.classList.remove("d-none");
        }
        else {
            valideInput(true, spanOpcaoResposta1, "Primeira opcão é obrigatória");
            valideInput(true, spanOpcaoResposta2, "Segunda opcão é obrigatória");
            divOpcaoResposta.classList.add("d-none");
        }
    });

    //document.getElementById("botaoAdicionarOpcaoResposta").addEventListener('click', () => {
    //    opcaoRespostaEstaValida();
    //});
})

function valideInput(estaValido, span, textoValidacao) {
    if (!estaValido) {
        span.innerText = textoValidacao;
    }
    else {
        span.innerText = "";
    }
}

function inputEstaValido(input) {
    return !!input.value;
}

function ehTipoRespostaOpcao() {
    return questaoTipoResposta.value == 2 || questaoTipoResposta.value == 3;
}

function perguntaEstaValida() {
    return inputEstaValido(inputPergunta);
}

function opcaoRespostaEstaValida() {
    if (ehTipoRespostaOpcao()) {
        return inputEstaValido(inputOpcaoRespostaTexto1) &&
            inputEstaValido(inputOpcaoRespostaTexto2);
    } else {
        return true;
    }
}

function questaoEstaValida() {
    return perguntaEstaValida() && opcaoRespostaEstaValida();
}

function adicioneQuestao() {
    let opcaoResposta1 = {
        Texto: inputOpcaoRespostaTexto1.value
    }
    let opcaoResposta2 = {
        Texto: inputOpcaoRespostaTexto2.value
    }
    let opcoes = [];

    opcoes.push(opcaoResposta1);
    opcoes.push(opcaoResposta2);

    let questaoNova = {
        Pergunta: inputPergunta.value,
        TipoResposta: questaoTipoResposta.value,
        OpcoesResposta: ehTipoRespostaOpcao() ?
            opcoes
            : null
    }

    questoesAdicionadas.push(questaoNova);
    questoesInput.value = JSON.stringify(questoesAdicionadas);
    monteCardsQuestoes();
}

function monteCardsQuestoes() {
    divQuestoes.innerHTML = "";
    let questoesAtuais = JSON.parse(questoesInput.value);

    if (questoesAtuais.length) divQuestoes.classList.remove("d-none");
    else {
        divQuestoes.classList.add("d-none");
        return;
    }

    questoesAtuais.forEach(q => {
        let divCard = document.createElement('div');
        divCard.classList.add("col-md-4", "col-xs-6", "border-primary", "mb-3");

        divCard.innerHTML =
              "<div class=\"card mb-3\">"
            + "<div class=\"row g-0\">"
            + "<div class=\"col-md-12\">"
            + "<div class=\"card-header cor-apoema\"></div>"
            + "</div>"
            + "<div class=\"col-md-12\">"
            + "<div class=\"card-body\">"
            + `<p class=\"card-text\"><b>Pergunta: </b>${q.Pergunta}</p>`
            + `<p class=\"card-text\"><b>Tipo Resposta: </b>${q.TipoResposta}</p>`
            + "</div>"
            + "</div>"
            + "</div>"
            + "</div >";

        divQuestoes.appendChild(divCard);
    });
}
