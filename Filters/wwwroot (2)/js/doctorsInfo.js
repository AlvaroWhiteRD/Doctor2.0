
//prevenimos el comportamiento por defecto del formulario
function stopDefAction(evt) {
    evt.preventDefault();
}

let idDeleteUpdate;
function IdSaves(id) {
    idDeleteUpdate = id;

}

//funcion que envia el id para eliminar el registro
function SendId() {
    let formData = new FormData();

    formData.append("id", idDeleteUpdate);

    fetch('Doctor/Delete', {
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

//metodo que mueve la data al modal para que luedo se pueda editar
function EditDoctorModal(Name, LastName, Description, IdAppointment) {

    //asociamos los textbox con sus respetivos valores provenientes de la base de datos.
    _name.value = Name;
    _lastName.value = LastName;
    _description.value = Description;
    _idAppointment.value = IdAppointment

}
function UpdatePatientInformation() {
    let formData = new FormData();

    formData.append("idAppointment", document.getElementById('_idAppointment').value);
    formData.append("medicalNotes", document.getElementById('_medicalNotes').value);
    formData.append("prescribedMedical", document.getElementById('_prescribedMedical').value);
    formData.append("appointmentProcess", document.getElementById('_appointmentProcess').value);
    formData.append("idDoctor", document.getElementById('_idDoctor').value);
  
    fetch('Doctors/Update', {
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

function SendDataInsert() {


    let formData = new FormData();

    formData.append("Name", document.getElementsByName('_name')[0].value);
    formData.append("LastName", document.getElementsByName('_lastName')[0].value);
    formData.append("MotherLastName", document.getElementsByName('_motherLastname')[0].value);
    formData.append("Gender", document.getElementsByName('_gender')[0].value);
    formData.append("CivilStatus", document.getElementsByName('_civilStatus')[0].value);
    formData.append("Dni", document.getElementsByName('_dni')[0].value);
    formData.append("Phone", document.getElementsByName('_phone')[0].value);
    formData.append("Title", document.getElementsByName('_title')[0].value);
    formData.append("Execuatur", document.getElementsByName('_execuatur')[0].value);
    formData.append("Specialty", document.getElementsByName('_specialty')[0].value);
    formData.append("Address", document.getElementsByName('_address')[0].value);

    fetch('Doctor/Create', {
        method: 'POST',
        body: formData
    })
        //si se actualiza el registro, se envia la alert de confirmacion.
        //la alerta se desaparecera automaticamente lusgo de 9000 ms.
        .then(response => {

            document.getElementById('messageSuccess').innerHTML = '<div class="alert alert-success" role="alert">Registro Insertado Correctamente.</div >';

            //cerramos el modal
            $("#exampleModal").modal("toggle");

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


