
let tipoCriterio = document.getElementById("CriterioBase_TipoCriterio");
let inputDescricaoCriterio = document.getElementById("CriterioBase_Descricao");
let spanDescricaoCriterio = document.getElementById("spanDescricaoCriterio");
let divOpcoesCriterio = document.getElementById("div_opcoes_criterio");
let inputCriterios = document.getElementById("InputCriterios");
let divCriterios = document.getElementById("div_criterios");
let tabelaOpcoesCorpo = document.querySelector("#tabela_opcoes_criterio tbody");

window.addEventListener('load', () => {
    monteCardsCriterios();

    document.getElementById("botaoAdicionarCriterio").addEventListener('click', () => {
        valideInput(descricaoCriterioEstaValida(), spanDescricaoCriterio, "Critério é obrigatório");
        if (ehTipoCriterioComOpcoes()) {
            valideOpcoesCriterios();
        }

        if (criterioEstaValido())
            adicioneCriterio();
    });

    tipoCriterio.addEventListener('change', () => {
        if (ehTipoCriterioComOpcoes()) {
            divOpcoesCriterio.classList.remove("d-none");
        }
        else {
            valideOpcoesCriterios(true);
            divOpcoesCriterio.classList.add("d-none");
        }
    });

    document.getElementById("botaoAdicionarOpcaoCriterio").addEventListener('click', () => {
        adicioneOpcao();
    });
})

function removaOpcao(botao) {
    if (tabelaOpcoesCorpo.rows.length == 2) return; // Não pode ter menos de 2 opcoes
    let indexRow = botao.parentElement.parentElement.rowIndex - 1;

    tabelaOpcoesCorpo.deleteRow(indexRow);
    reordeneTabela();
}

function adicioneOpcao() {
    if (tabelaOpcoesCorpo.rows.length == 10) return; // Não pode ter mais de 10 opcoes
    let indexNovo = tabelaOpcoesCorpo.rows.length;
    let linha = crieLinha(indexNovo);

    tabelaOpcoesCorpo.appendChild(linha);
}

function crieLinha(index) {
    let linha = document.createElement('tr');

    let colunaTexto = document.createElement('td');
    let inputTexto = document.createElement('input');
    inputTexto.setAttribute('id', `CriterioBase_OpcoesCriterioBase_${index}_`);
    inputTexto.setAttribute('name', `CriterioBase.OpcoesCriterioBase[${index}]`);
    inputTexto.classList.add('form-control');
    let spanTexto = document.createElement('span');
    spanTexto.setAttribute('id', `spanOpcaoCriterioTexto${index}`);
    spanTexto.classList.add('text-danger');
    colunaTexto.appendChild(inputTexto);
    colunaTexto.appendChild(spanTexto);

    let colunaAcaoRemover = document.createElement('td');
    let botaoRemover = document.createElement('button');
    botaoRemover.setAttribute('onclick', 'removaOpcao(this)');
    botaoRemover.setAttribute('type', 'button');
    botaoRemover.classList.add("btn", "btn-danger", "text-white");
    let iconeRemover = document.createElement('i');
    iconeRemover.classList.add("bi", "bi-trash");
    botaoRemover.appendChild(iconeRemover);
    colunaAcaoRemover.appendChild(botaoRemover);

    linha.appendChild(colunaTexto);
    linha.appendChild(colunaAcaoRemover);

    return linha;
}

function reordeneTabela() {
    Array.from(tabelaOpcoesCorpo.rows).forEach(r => {
        let input = r.querySelector('input');
        let span = r.querySelector('span');
        let idOpcao = r.rowIndex - 1;

        input.setAttribute('id', `CriterioBase_OpcoesCriterioBase_${idOpcao}`);
        input.setAttribute('name', `CriterioBase.OpcoesCriterioBase[${idOpcao}]`);
        span.setAttribute('id', `spanOpcaoCriterioTexto${idOpcao}`);
    })
}

function valideInput(estaValido, span, textoValidacao) {
    if (!estaValido) {
        span.innerText = textoValidacao;
    }
    else {
        span.innerText = "";
    }
}

function valideOpcoesCriterios(marcarComoValidos) {
    Array.from(tabelaOpcoesCorpo.rows).forEach(r => {
        let input = r.querySelector('input');
        let span = r.querySelector('span');
        let indexOpcao = r.rowIndex;
        let textoValidacao = `Texto opção ${indexOpcao} é obrigatório`;

        valideInput(marcarComoValidos || inputEstaValido(input), span, textoValidacao);
    });
}

function inputEstaValido(input) {
    return !!input.value;
}

function ehTipoCriterioComOpcoes() {
    return tipoCriterio.value == 2 || tipoCriterio.value == 3;
}

function descricaoCriterioEstaValida() {
    return inputEstaValido(inputDescricaoCriterio);
}

function opcoesCriteriosEstaoValidos(input) {
    if (ehTipoCriterioComOpcoes()) {
        for (let i = 0; i < tabelaOpcoesCorpo.rows.length; i++) {
            let r = tabelaOpcoesCorpo.rows[i];
            let input = r.querySelector('input');

            if (!inputEstaValido(input)) return false;
        }

        return true;
    } else {
        return true;
    }
}

function criterioEstaValido() {
    return descricaoCriterioEstaValida() && opcoesCriteriosEstaoValidos();
}

function adicioneCriterio() {
    let opcoes = [];

    Array.from(tabelaOpcoesCorpo.rows).forEach(r => {
        let input = r.querySelector('input');
        opcoes.push(input.value);
    });

    let criteriosAdicionados = JSON.parse(inputCriterios.value || '[]');
    let novoCriterio = {
        Ordem: criteriosAdicionados.length + 1,
        Descricao: inputDescricaoCriterio.value,
        TipoCriterio: tipoCriterio.value,
        TextoTipoCriterio: tipoCriterio.options[tipoCriterio.selectedIndex].text,
        OpcoesCriterios: ehTipoCriterioComOpcoes() ?
            opcoes
            : null
    }

    criteriosAdicionados.push(novoCriterio);
    inputCriterios.value = JSON.stringify(criteriosAdicionados);
    limpeCamposCadastroCriterio();
    monteCardsCriterios();
}

function monteCardsCriterios() {
    divCriterios.innerHTML = "";
    let criteriosAtuais = JSON.parse(inputCriterios.value || '[]');

    if (criteriosAtuais.length) divCriterios.classList.remove("d-none");
    else {
        divCriterios.classList.add("d-none");
        return;
    }

    criteriosAtuais.forEach(c => {
        let divCard = document.createElement('div');
        divCard.classList.add("col-md-4", "col-xs-6", "border-primary", "mb-3");

        divCard.innerHTML =
            "<div class=\"card mb-3\">"
            + "<div class=\"row g-0\">"
            + "<div class=\"col-md-12\">"
            + "<div class=\"card-header cor-apoema\" style=\"text-align: center;\">"
            + "<p class=\"card-text\">"
            + "<h5 class=\"card-title\">"
            + `Critério ${c.Ordem}`
            + "</h5>"
            + "</p>"
            + "</div>"
            + "</div>"
            + "<div class=\"col-md-12\">"
            + "<div class=\"card-body\">"
            + `<p class=\"card-text\"><b>Critério: </b>${c.Descricao}</p>`
            + `<p class=\"card-text\"><b>Tipo de critério: </b>${c.TextoTipoCriterio}</p>`
            + "</div>"
            + "</div>"
            + "<div class=\"col-md-12\">"
            + "<div class=\"card-footer\">"
            + "<p class=\"card-text\">"
            + `<button onclick=\"removaCriterio(${c.Ordem})\" type=\"button\" class=\"btn btn-danger text-white\">`
            + "<i class=\"bi bi-trash\"></i>"
            + "</button>"
            + `<button onclick=\"visualizeCriterio(${c.Ordem})\" type=\"button\" class=\"btn btn-outline-primary float-right\">`
            + "<i class=\"bi bi-eye-fill\"></i>"
            + "</button>"
            + "</p>"
            + "</div>"
            + "</div>"
            + "</div>"
            + "</div>";

        divCriterios.appendChild(divCard);
    });
}

function removaCriterio(ordem) {
    let criteriosAdicionados = JSON.parse(inputCriterios.value || '[]');
    criteriosAdicionados = criteriosAdicionados.filter(q => q.Ordem != ordem);

    for (let i = 0; i < criteriosAdicionados.length; i++) {
        let questao = criteriosAdicionados[i];
        questao.Ordem = i + 1;
    }

    inputCriterios.value = JSON.stringify(criteriosAdicionados);
    monteCardsCriterios();
}

function visualizeCriterio(ordem) {
    reseteCorpoTabela();
    let criteriosAdicionados = JSON.parse(inputCriterios.value || '[]');
    let criterioBuscado = criteriosAdicionados.find(q => q.Ordem == ordem);

    inputDescricaoCriterio.value = criterioBuscado.Descricao;
    tipoCriterio.value = criterioBuscado.TipoCriterio;

    let i = 0;
    criterioBuscado.OpcoesCriterios?.forEach(op => {
        if (i <= 1) {
            let linha = tabelaOpcoesCorpo.rows[i];
            let input = linha.querySelector('input');
            input.value = op;
        }
        else {
            let novaLinha = crieLinha(i);
            let input = novaLinha.querySelector('input');
            input.value = op;
            tabelaOpcoesCorpo.appendChild(novaLinha);
        }
        i++;
    })

    tipoCriterio.dispatchEvent(new Event('change'));
}

function limpeCamposCadastroCriterio() {
    inputDescricaoCriterio.value = '';
    tipoCriterio.value = 1;
    reseteCorpoTabela();
    tipoCriterio.dispatchEvent(new Event('change'));
}

function reseteCorpoTabela() {
    let i = 0;
    Array.from(tabelaOpcoesCorpo.rows).forEach(r => {
        let input = r.querySelector('input');
        if (i <= 1) input.value = '';
        else r.remove();
        i++
    });
}
