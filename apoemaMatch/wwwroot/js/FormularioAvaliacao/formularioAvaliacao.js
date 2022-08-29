﻿
function AddInput() {
    var inputList = document.getElementById("inputList");
    var inputCount = inputList.getElementsByTagName('input').length;
    var newInput = `<div class="input-group">
        <input asp-for="Titulo" class="form-control" id="EventMembers_${inputCount}" name="EventMembers[${inputCount}]" placeholder="Insira a questão" />
        <div class="dropdown demolist">
            <button class="btn btn-outline-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" onclick="AddElement()">
            </button>
            <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                <a class="dropdown-item" href="#">Textual</a>
                <a class="dropdown-item" href="#">Opções</a>
                <a class="dropdown-item" href="#">Seleção única</a>
            </div>
        </div>
        <a class="btn btn-danger text-white rmv_btn" onclick="RemoveInput()"><i class="bi bi-trash"></i></a>
    </div>
    <div class="input-element"></div>`;
    var element = document.createElement("div");
    element.classList.add('input-group');
    element.classList.add('teste-class');
    element.innerHTML = newInput;
    inputList.appendChild(element);
}

function RemoveInput() {
    $(document).on('click', '.rmv_btn', function () {
        // "this" is current button event occurs on
        $(this).closest('.teste-class').remove()
    })
}

function AddElement() {
    $(document).ready(function () {
        $('.demolist a').one('click', function () {
            var txt = ($(this).text());
            if (txt == "Textual") {
                $(this).closest('.teste-class').find('.input-element').replaceWith("<div class='input-element  mx-auto'><input type='text' class='form-control' id='lname' name='lname' placeholder='Entrada de Texto'></div>");
            } else if (txt == "Opções") {
                $(this).closest('.teste-class').find('.input-element').replaceWith("<div class='input-element mx-auto'> \
                                                                                           <div class='form-check'> \
                                                                                               <input class='form-check-input' type='checkbox' value='' name='flexCheckDefault' id='flexCheckDefault1'> \
                                                                                               <input type='' class='form-control form-control-sm' value=''> \
                                                                                           </div> \
                                                                                           <div class='form-check'> \
                                                                                              <input class='form-check-input' type='checkbox' value='' name='flexCheckDefault' id='flexCheckDefault2' checked> \
                                                                                              <input type='' class='form-control form-control-sm' value=''> \
                                                                                           </div> \
                                                                                           <a class='btn'  onclick=''><i class='bi bi-plus' style='font-size: 2rem; color: #0099ff;'></i></a> \
                                                                                        </div>");
            } else if (txt == "Seleção única") {
                $(this).closest('.teste-class').find('.input-element').replaceWith("<div class='input-element  mx-auto'> \
                                                                                            <div class='form-check'> \
                                                                                                <input class='form-check-input' type='radio' value='' name='flexRadioDefault' id='flexRadioDefault1'> \
                                                                                                <input type='' class='form-control form-control-sm' value=''> \
                                                                                            </div> \
                                                                                            <div class='form-check'> \
                                                                                                <input class='form-check-input' type='radio' value='' name='flexRadioDefault' id='flexRadioDefault1' checked> \
                                                                                                <input type='' class='form-control form-control-sm' value=''> \
                                                                                            </div> \
                                                                                            <a class='btn'><i class='bi bi-plus' style='font-size: 2rem; color: #0099ff;'></i></a> \
                                                                                       </div>");
            }
        });
    });
}
