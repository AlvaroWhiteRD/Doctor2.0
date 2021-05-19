
/**
* ejecutar antes de la carga del dom
window.onload = function() {
    // code to execute here
}
*/
/*window.addEventListener("load", function () {
    GetCustomer();
});*/

//const { RETURN } = require("../template/plugins/pdfmake/pdfmake");

//const { String } = require("../template/plugins/pdfmake/pdfmake");

//prevenimos el comportamiento por defecto del formulario
function stopDefAction(evt) {
    evt.preventDefault();
}

let idDeleteUpdate;
let getArray = [];
function IdSaves(id) {
    idDeleteUpdate = id;

    // var fileField = document.querySelector("input[type='file']");

}
/* function GetAllCustomer() {
     //funcion que trae todos los registros

     fetch('Customer/GetCustomer', {
         method: 'get'
     }).then(response => {
         location.reload();
     })
         //.then(response => response.json())
         .then(data => {
             //data.forEach(function (item, index) {
 
             // } )
             /* })
              .catch(error => document.getElementById('messageSuccess').innerHTML = `<div class="alert alert-danger" role="alert">Algo no anda bien. Intenta recargar el navegador ->${error} </div >`)
  
      }*/
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

function SendDateInsert() {


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
    let name = document.getElementsByName("_name").value;
    let lastName = document.getElementsByName("_lastName").value;
    let motherLastname = document.getElementsByName("_motherLastname").value;
    let dni = document.getElementsByName("_dni").value;
    let address = document.getElementsByName("_address").value;
    let phone = document.getElementsByName("_phone").value;
    let civilStatus = document.getElementsByName("_civilStatus").value;
    let gender = document.getElementsByName("_gender").value;

    /* let errorName = document.getElementById("errorName");
 
     if (name == "" || name == undefined || name == null ) {
         errorName = "El nombre es obligatorio.";
         //btnInsert.disabled = false
         return
     } else {
         btnInsert.disabled = true
 
     }*/


}


function ValidateName() {
    //document.getElementById("btnInsert").disabled = !document.getElementsByName("_name").value;
    let name = document.getElementsByName("_name");
    if (name == "" || name == null || name == undefined || name.length <= 3) {
        btnInsert.disabled = true
        errorName.innerHTML = "El nombre es obligatorio.";
        return false;
    } else {
        btnInsert.disabled = false
    }
}

