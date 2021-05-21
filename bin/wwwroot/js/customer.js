
/**
* ejecutar antes de la carga del dom
window.onload = function() {
    // code to execute here
}
*/
/*window.addEventListener("load", function () {
    GetCustomer();
});*/


function stopDefAction(evt) {
    evt.preventDefault();
}

let idDeleteUpdate;
let getArray = [];
function IdSaves(id) {
    idDeleteUpdate = id;

    // var fileField = document.querySelector("input[type='file']");

}

//funcion que envia el id para eliminar el registro
function SendId() {
    let formData = new FormData();

    formData.append("id", idDeleteUpdate);

    fetch('Customer/Delete', {
        method: 'DELETE',
        body: formData
    })
        //.then(response => response.json())
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

//metodo que mueve la data al modal para que luedo se pueda editar
function EditCustomerModal(Name, LastName, MotherLastName, Gender, CivilStatus, Dni, Phone, Address, Ars) {

    //IdCustomer.value = idDeleteUpdate;
    //asociamos los textbox con sus respetivos valores provenientes de la base de datos.
    document.getElementById('name').value = Name;
    lastName.value = LastName;
    motherLastname.value = MotherLastName;
    gender.value = Gender;
    civilStatus.value = CivilStatus;
    dni.value = Dni;
    phone.value = Phone;
    address.value = Address;
    if (Ars == "1") {
        document.getElementById("ars").checked = true;
    } else {
        document.getElementById("ars").checked = false;
    }
    //ars.value = Ars;


    //let editForm = true;



}
function SendDateUpdate() {
    let formData = new FormData();

    formData.append("id", idDeleteUpdate);
    formData.append("Name", document.getElementById('name').value);
    formData.append("LastName", lastName.value);
    formData.append("MotherLastName", motherLastname.value);
    formData.append("Gender", gender.value);
    formData.append("CivilStatus", civilStatus.value);
    formData.append("Dni", dni.value);
    formData.append("Phone", phone.value);
    formData.append("Ars", ars.value);
    formData.append("Address", address.value);
    //document.getElementById('lbResultado').innerHTML = resultado;

    fetch('Customer/Update', {
        method: 'POST',
        body: formData
    })
        //si se actualiza el registro, se envia la alert de confirmacion.
        //la alerta se desaparecera automaticamente lusgo de 9000 ms.
        .then(response => {

            document.getElementById('messageSuccess').innerHTML = '<div class="alert alert-success" role="alert">Registro Actualizado Correctamente.</div >';
            //cerramos el modal luego de eliminar el registro
            //GetCustomer();
            //cerramos el modal
            $("#updateModal").modal("toggle");

            reloadWindows();

        })
        .catch(error => console.error('Error:', error))
}

function SendDataInsert() {


    let formData = new FormData();


    formData.append("Name", document.getElementsByName('_name')[0].value);
    formData.append("LastName", document.getElementsByName('_lastName')[0].value);
    formData.append("MotherLastName", document.getElementsByName('_motherLastname')[0].value);
    formData.append("Gender", document.getElementsByName('_gender')[0].value);
    formData.append("CivilStatus", document.getElementsByName('_civilStatus')[0].value);
    formData.append("Dni", document.getElementsByName('_dni')[0].value);
    formData.append("Phone", document.getElementsByName('_phone')[0].value);
    formData.append("Ars", document.getElementsByName('_ars')[0].value);
    formData.append("Address", document.getElementsByName('_address')[0].value);

    fetch('Customer/Create', {
        method: 'POST',
        body: formData
    })
        //si se actualiza el registro, se envia la alert de confirmacion.
        //la alerta se desaparecera automaticamente lusgo de 9000 ms.
        .then(response => {

            document.getElementById('messageSuccess').innerHTML = '<div class="alert alert-success" role="alert">Registro Insertado Correctamente.</div >';
            //cerramos el modal luego de eliminar el registro
            //GetCustomer();

            //GetAllCustomer();
            //cerramos el modal
            $("#exampleModal").modal("toggle");

            reloadWindows();

        })
        .catch(error => console.error('Error:', error))
}
//.then(data => _displayItems(data))
//.then(() => getItems())

//funcion que recargara la pagina luego de realizar crud
function reloadWindows() {

    setTimeout(function () {
        //cierra la alerta de bootstrap
        $(".alert").alert('close');
        location.reload();
    }, 2000);
}


function ValidateForm() {
    let name = document.getElementsByName("_name")[0].value;
    let lastName = document.getElementsByName("_lastName")[0].value;
    let motherLastname = document.getElementsByName("_motherLastname")[0].value;
    let dni = document.getElementsByName("_dni")[0].value;
    let address = document.getElementsByName("_address")[0].value;
    let phone = document.getElementsByName("_phone")[0].value;
    let civilStatus = document.getElementsByName("_civilStatus")[0].value;
    let gender = document.getElementsByName("_gender")[0].value;
  
    if (name == "" || name == null || name.trim() == "") {
        document.getElementById("errorName").innerHTML = "* nombre es requerido";
        document.getElementsByName("_name")[0].className = "form-control erro-input";
        document.getElementsByName("_name")[0].focus();
        return;
    }
    document.getElementsByName("_name")[0].className = "form-control sucess-input";
    document.getElementById("errorName").innerHTML = "";

    if (lastName == "" || lastName == null || lastName.trim() == "") {
        document.getElementById("errorLastName").innerHTML = "* apellido es requerido";
        document.getElementsByName("_lastName")[0].className = "form-control erro-input";
        document.getElementsByName("_lastName")[0].focus();
        return;
    }
    document.getElementsByName("_lastName")[0].className = "form-control sucess-input";
    document.getElementById("errorLastName").innerHTML = "";

    if (motherLastname == "" || motherLastname == null || motherLastname.trim() == "") {
        document.getElementById("errorMotherLastName").innerHTML = "* apellido materno es requerido";
        document.getElementsByName("_motherLastname")[0].className = "form-control erro-input";
        document.getElementsByName("_motherLastname")[0].focus();
        return;
    }
    document.getElementsByName("_motherLastname")[0].className = "form-control sucess-input";
    document.getElementById("errorMotherLastName").innerHTML = "";

    if (dni == "" || dni == null || dni.trim() == "") {
        document.getElementById("errorDni").innerHTML = "* cedula es requerido";
        document.getElementsByName("_dni")[0].className = "form-control erro-input";
        document.getElementsByName("_dni")[0].focus();
        return;
    }
    document.getElementsByName("_dni")[0].className = "form-control sucess-input";
    document.getElementById("errorDni").innerHTML = "";
    document.getElementById("errorOnlyNumber").innerHTML = "";
    document.getElementById("errorOnlyNumberPhone").innerHTML = "";

    if (address == "" || address == null || address.trim() == "") {
        document.getElementById("errorAddress").innerHTML = "* direccion es requerida";
        document.getElementsByName("_address")[0].className = "form-control erro-input";
        document.getElementsByName("_address")[0].focus();
        return;
    }
    document.getElementsByName("_address")[0].className = "form-control sucess-input";
    document.getElementById("errorAddress").innerHTML = "";

    if (phone == "" || phone == null || phone.trim() == "") {
        document.getElementById("errorPhone").innerHTML = "* telefono es requerido";
        document.getElementsByName("_phone")[0].className = "form-control erro-input";
        document.getElementsByName("_phone")[0].focus();
        return;
    }
    document.getElementsByName("_phone")[0].className = "form-control sucess-input";
    document.getElementById("errorPhone").innerHTML = "";
    document.getElementById("errorOnlyNumber").innerHTML = "";
    document.getElementById("errorOnlyNumberPhone").innerHTML = "";

    if (civilStatus == "" || civilStatus == null || civilStatus == 0 || civilStatus == '0') {
        document.getElementById("errorCivilStatus").innerHTML = "* estado civil es requerido";
        document.getElementsByName("_civilStatus")[0].className = "custom-select erro-input";
        document.getElementsByName("_civilStatus")[0].focus();
        return;
    }
    document.getElementsByName("_civilStatus")[0].className = "form-control sucess-input";
    document.getElementById("errorCivilStatus").innerHTML = "";

    if (gender == "" || gender == null || gender == 0 || gender == '0') {
        document.getElementById("errorGender").innerHTML = "* genero es requerido";
        document.getElementsByName("_gender")[0].className = "custom-select erro-input";
        document.getElementsByName("_gender")[0].focus();
        return;
    }
    document.getElementsByName("_gender")[0].className = "form-control sucess-input";
    document.getElementById("errorGender").innerHTML = "";

    SendDataInsert();
}

function OnlyNumber(event) {
    if (event.key != (key >= 48 && key <= 57) || event.key != (key == 8)) {
        document.getElementById("errorOnlyNumber").innerHTML = "* campo de solo numeros";
        document.getElementById("errorOnlyNumberPhone").innerHTML = "* campo de solo numeros";
    }
    var key = window.Event ? event.which : event.keyCode
    return ((key >= 48 && key <= 57) || (key == 8))
}
function OnlyLetters(event) {
    //alert("event " + event.keyCode);
    letter = (document.all) ? event.keyCode : event.which;
    if (letter == 8) return true;//espacio
    pattern = /[A-Za-z\s]/;
    keyPressed = String.fromCharCode(letter);

    return pattern.test(keyPressed);
}