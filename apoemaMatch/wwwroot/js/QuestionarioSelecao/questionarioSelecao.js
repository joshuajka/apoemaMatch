
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

window.addEventListener('load', () => {
    monteCardsQuestoes();

    document.getElementById("botaoAdicionarQuestao").addEventListener('click', () => {
        valideInput(perguntaEstaValida(), spanPergunta, "Critério é obrigatório");
        if (ehTipoRespostaOpcao()) {
            valideInput(opcaoRespostaEstaValida(), spanOpcaoResposta1, "Primeiro atributo é obrigatório");
            valideInput(opcaoRespostaEstaValida(), spanOpcaoResposta2, "Segundo atributo é obrigatório");
        }

        if (questaoEstaValida())
            adicioneQuestao();
    });

    questaoTipoResposta.addEventListener('change', () => {
        if (ehTipoRespostaOpcao()) {
            divOpcaoResposta.classList.remove("d-none");
        }
        else {
            valideInput(true, spanOpcaoResposta1, "Primeiro atributo é obrigatório");
            valideInput(true, spanOpcaoResposta2, "Segundo atributo é obrigatório");
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
        Ordem: 1,
        Texto: inputOpcaoRespostaTexto1.value
    }
    let opcaoResposta2 = {
        Ordem: 2,
        Texto: inputOpcaoRespostaTexto2.value
    }
    let opcoes = [];

    opcoes.push(opcaoResposta1);
    opcoes.push(opcaoResposta2);

    let questoesAdicionadas = JSON.parse(questoesInput.value || '[]');
    let questaoNova = {
        Ordem: questoesAdicionadas.length + 1,
        Pergunta: inputPergunta.value,
        TipoResposta: questaoTipoResposta.value,
        TextoTipoResposta: questaoTipoResposta.options[questaoTipoResposta.selectedIndex].text,
        OpcoesResposta: ehTipoRespostaOpcao() ?
            opcoes
            : null
    }

    questoesAdicionadas.push(questaoNova);
    questoesInput.value = JSON.stringify(questoesAdicionadas);
    limpeCamposInputQuestao();
    monteCardsQuestoes();
}

function monteCardsQuestoes() {
    divQuestoes.innerHTML = "";
    let questoesAtuais = JSON.parse(questoesInput.value || '[]');

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
            + "<div class=\"card-header cor-apoema\" style=\"text-align: center;\">"
            + "<p class=\"card-text\">"
            + "<h5 class=\"card-title\">"
            + `Critério ${q.Ordem}`
            + "</h5>"
            + "</p>"
            + "</div>"
            + "</div>"
            + "<div class=\"col-md-12\">"
            + "<div class=\"card-body\">"
            + `<p class=\"card-text\"><b>Critério: </b>${q.Pergunta}</p>`
            + `<p class=\"card-text\"><b>Tipo de critério: </b>${q.TextoTipoResposta}</p>`
            + "</div>"
            + "</div>"
            + "<div class=\"col-md-12\">"
            + "<div class=\"card-footer\">"
            + "<p class=\"card-text\">"
            + `<button onclick=\"removaQuestao(${q.Ordem})\" type=\"button\" class=\"btn btn-danger text-white\">`
            + "<i class=\"bi bi-trash\"></i>"
            + "</button>"
            + `<button onclick=\"visualizeQuestao(${q.Ordem})\" type=\"button\" class=\"btn btn-outline-primary float-right\">`
            + "<i class=\"bi bi-eye-fill\"></i>"
            + "</button>"
            + "</p>"
            + "</div>"
            + "</div>"
            + "</div>"
            + "</div>";

        divQuestoes.appendChild(divCard);
    });
}

function removaQuestao(ordemQuestao) {
    let questoesAdicionadas = JSON.parse(questoesInput.value || '[]');
    questoesAdicionadas = questoesAdicionadas.filter(q => q.Ordem != ordemQuestao);

    for (let i = 0; i < questoesAdicionadas.length; i++) {
        let questao = questoesAdicionadas[i];
        questao.Ordem = i + 1;
    }

    questoesInput.value = JSON.stringify(questoesAdicionadas);
    monteCardsQuestoes();
}

function visualizeQuestao(ordemQuestao) {
    let questoesAdicionadas = JSON.parse(questoesInput.value || '[]');
    let questaoProcurada = questoesAdicionadas.find(q => q.Ordem == ordemQuestao);

    inputPergunta.value = questaoProcurada.Pergunta;
    questaoTipoResposta.value = questaoProcurada.TipoResposta;
    inputOpcaoRespostaTexto1.value = questaoProcurada.OpcoesResposta?.find(op => op.Ordem == 1).Texto || '';
    inputOpcaoRespostaTexto2.value = questaoProcurada.OpcoesResposta?.find(op => op.Ordem == 2).Texto || '';
    questaoTipoResposta.dispatchEvent(new Event('change'));
}

function limpeCamposInputQuestao() {
    inputPergunta.value = '';
    questaoTipoResposta.value = 1;
    inputOpcaoRespostaTexto1.value = '';
    inputOpcaoRespostaTexto2.value = '';
}
