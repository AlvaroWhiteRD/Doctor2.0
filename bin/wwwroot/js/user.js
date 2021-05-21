
//prevenimos el comportamiento por defecto del formulario
function stopDefAction(evt) {
    evt.preventDefault();
}

let typeUserSelecte = "";
//funcion que busca el tipo de usuario que sera creado.
function GetUserType() {

    typeUserSelecte = document.getElementById("userType").value;
    typeUserSelecteUpdate = document.getElementById("_userType").value;
    
    switch (typeUserSelecte) {

        case "1":
            getSecretary();
            break;
        case "2":
            getDoctor();
            break;
       
    }

}

//funcion que trae las secretarias al select option
function getSecretary() {

    fetch('Secretary/Secretary', {
        method: 'GET',
    }).then(response => response.json())
        .then(data => {
            userTypeSelect.innerHTML = `
                        <div class="input-group-prepend">
                            <button id="buttonError" class="btn btn-outline-success" type="button">Seleccione Secretaria</button>
                        </div>
                        <select class="custom-select" id="userSelect" onchange="UserTypeSelect()">
                         
                        <option selected value="0">Seleccione Aqui...</option>
                           ${data.map((item, i) =>`
                            <option value="${item["idSecretary"]}">${item.name}&nbsp &nbsp${item.lastName}</option>
                          
                            `.trim()).join('')
            }
                        </select>
        `;
        })

}
//funcion que trae los doctores al select option
function getDoctor() {

    fetch('Doctor/Doctor', {
        method: 'GET',
    }).then(response => response.json())
        .then(data => {
            console.log(data)
            userTypeSelect.innerHTML = `
                        <div class="input-group-prepend">
                            <button id="buttonError" class="btn btn-outline-success" type="button">Seleccione El Doctor</button>
                        </div>
                        <select class="custom-select" id="userSelect" onchange="UserTypeSelect()">
                         
                        <option value="0" selected>Seleccione Aqui...</option>
                           ${data.map((item, i) => `
                            <option value="${item["idDoctor"]}">${item.name}&nbsp &nbsp${item.lastName}</option>
                          
                            `.trim()).join('')
                }
                        </select>
        `;
        })

}


let userSelect = "";
function UserTypeSelect() {

    userSelect = document.getElementById("userSelect").value;

}


function UserDataSend() {


    let formData = new FormData();


    formData.append("typeUserSelecte", typeUserSelecte);
    formData.append("userSelect", userSelect);
    formData.append("userName", document.getElementsByName('_userName')[0].value);
    formData.append("password", document.getElementsByName('_password')[0].value);

    fetch('UserManager/Create', {
        method: 'POST',
        body: formData
    })
        //si se actualiza el registro, se envia la alert de confirmacion.
        //la alerta se desaparecera automaticamente lusgo de 9000 ms.
        .then(response => {
            //cerramos el modal
            $("#UserModal").modal("toggle");
            document.getElementById('messageSuccess').innerHTML = '<div class="alert alert-success" role="alert">Registro Insertado Correctamente.</div >';

            reloadWindows();

        })
        .catch(error => console.error('Error:', error))
}

function DeleteUserModal(id) {
    document.getElementById('idHiddenDelete').value = id;
}
//funcion que envia el id para eliminar el registro
function DeleteUser() {
    let formData = new FormData();
    idDelete = document.getElementById('idHiddenDelete').value;

    formData.append("id", idDelete);

    fetch('UserManager/Delete', {
        method: 'DELETE',
        body: formData
    })
        //si le elimina el registro, se envia la alert de confirmacion.
        //la alerta se desaparecera automaticamente lusgo de 9000 ms.
        .then(response => {
            document.getElementById('messageSuccess').innerHTML = '<div class="alert alert-success" role="alert">Registro Eliminado Correctamente.</div >';
            //cerramos el modal luego de eliminar el registro
            $("#deleteModal").modal("toggle");

            reloadWindows()

        })
        .catch(error => console.error('Error:', error))
}

let idDeleteUpdate = "";
//metodo que mueve la data al modal para que luedo se pueda editar
function EditUserModal(Name, LastName, UserName, Rol, Id) {
   
    //asociamos los textbox con sus respetivos valores provenientes de la base de datos.
    document.getElementsByName('userName')[0].value = UserName;
    document.getElementsByName('name')[0].value = Name;
    document.getElementsByName('lastName')[0].value = LastName;
    document.getElementById('userType')[0].value = Rol;
    idDeleteUpdate = document.getElementsByName('idHidden')[0].value = Id;
}

function SendDataUpdate() {
    let formData = new FormData();
    
    formData.append("id", idDeleteUpdate);
    //formData.append("typeUserSelecte", document.getElementById('userType')[0].value);
    formData.append("userName", document.getElementsByName('userName')[0].value);
    formData.append("typeUserSelecte", typeUserSelecteUpdate);

    fetch('UserManager/Update', {
        method: 'POST',
        body: formData
    })
        //si se actualiza el registro, se envia la alert de confirmacion.
        //la alerta se desaparecera automaticamente lusgo de 9000 ms.
        .then(response => {

            document.getElementById('messageSuccess').innerHTML = '<div class="alert alert-success" role="alert">Registro Actualizado Correctamente.</div >';

            //cerramos el modal
            $("#updateModal").modal("toggle");

            reloadWindows();

        })
        .catch(error => console.error('Error:', error))
}

//funcion que recargara la pagina luego de realizar crud
function reloadWindows() {

    setTimeout(function () {
        //cierra la alerta de bootstrap
        $(".alert").alert('close');
        location.reload();
    }, 2000);
}

function ValidateForm() {
    //typeUserSelecteUpdate;
    let userName = document.getElementsByName("_userName")[0].value;
    let password = document.getElementsByName("_password")[0].value;
    let passwordRepeat = document.getElementsByName("passwordRepeat")[0].value;
    

    //validamos que este seleccionado el tipo de usuario
    if (typeUserSelecte != 0) {
        document.getElementById("errorTypeUser").innerHTML = "";
        document.getElementById("userType").className = "custom-select sucess-input";
        document.getElementById("buttonErro").className = "btn btn-outline-success sucess-input";

    } else {
        document.getElementById("errorTypeUser").innerHTML = "* Selecciona el tipo de usuario";
        document.getElementById("userType").className = "custom-select erro-input";
        document.getElementById("buttonErro").className = "btn btn-danger erro-input";
        return
    }

    let userSelect = document.getElementById("userSelect").value;

    if (userSelect != 0) {
        document.getElementById("errorUserSelect").innerHTML = "";
        document.getElementById("userSelect").className = "custom-select sucess-input";
        document.getElementById("buttonError").className = "btn btn-outline-success sucess-input";
    } else {
        document.getElementById("errorUserSelect").innerHTML = "* Selecciona el usuario";
        document.getElementById("userSelect").className = "custom-select erro-input";
        document.getElementById("buttonError").className = "btn btn-danger erro-input";
        return
    }
    //expresion regular valida email
    RegExpEmail = `^[^@]+@[^@]+\.[a-zA-Z]{2,}$`;
    let regexEmail = new RegExp( RegExpEmail );

    if (userName == "" || userName == null || userName.trim() == "") {
        document.getElementById("errorUserName").innerHTML = "* Correo electronico es requerido";
        document.getElementsByName("_userName")[0].className = "form-control erro-input";
        document.getElementsByName("_userName")[0].focus();
        return;
    }
    document.getElementsByName("_userName")[0].className = "form-control sucess-input";
    document.getElementById("errorUserName").innerHTML = "";

    if (!regexEmail.test(userName)) {
        document.getElementsByName("_userName")[0].className = "form-control erro-input";
        document.getElementById("errorUserName").innerHTML = "* no es un correo valido";
        document.getElementsByName("_userName")[0].focus();
        return;
    }
    document.getElementsByName("_userName")[0].className = "form-control sucess-input";
    document.getElementById("errorUserName").innerHTML = "";

    if (password == "" || password == null || password.trim() == "") {
        document.getElementById("errorPassword").innerHTML = "* contrasena es requerida";
        document.getElementsByName("_password")[0].className = "form-control erro-input";
        document.getElementsByName("_password")[0].focus();
        return;
    }
    document.getElementsByName("_password")[0].className = "form-control sucess-input";
    document.getElementById("errorPassword").innerHTML = "";

    if (passwordRepeat == "" || passwordRepeat == null || passwordRepeat.trim() == "") {
        document.getElementById("errorPasswordRepeat").innerHTML = "* repita la contrasena";
        document.getElementsByName("passwordRepeat")[0].className = "form-control erro-input";
        document.getElementsByName("passwordRepeat")[0].focus();
        return;
    }
    document.getElementsByName("_password")[0].className = "form-control sucess-input";
    document.getElementById("errorPasswordRepeat").innerHTML = "";

    if (password != passwordRepeat) {
        document.getElementById("errorPasswordNoMatch").innerHTML = "* contrasenas no son iguales";
        document.getElementsByName("passwordRepeat")[0].value = "";
        document.getElementsByName("_password")[0].value = "";
        document.getElementsByName("_password")[0].focus();
        document.getElementsByName("_password")[0].className = "form-control erro-input";
        document.getElementsByName("passwordRepeat")[0].className = "form-control erro-input";
        return;  
    } 
    document.getElementById("errorPasswordNoMatch").innerHTML = "";
    document.getElementsByName("_password")[0].className = "form-control sucess-input";
    document.getElementsByName("passwordRepeat")[0].className = "form-control sucess-input";
  
    UserDataSend();
}



