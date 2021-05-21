let monthNames = ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octobre', 'Noviembre', 'Diciembre'];

let currentDate = new Date();
let currentDay = currentDate.getDate();
let monthNumber = currentDate.getMonth();
let currentYear = currentDate.getFullYear();

let dates = document.getElementById('dates');
let month = document.getElementById('month');
let year = document.getElementById('year');

//botones de siguiente y atras
let prevMonthDOM = document.getElementById('prev-month');
let nextMonthDOM = document.getElementById('next-month');
//pintamos en pantalla el mes y el año
month.textContent = monthNames[monthNumber];
year.textContent = currentYear.toString();
//cambiar fechas luego de dar click a los botones netx y prev
prevMonthDOM.addEventListener('click', () => lastMonth());
nextMonthDOM.addEventListener('click', () => nextMonth());


//saber el mes en el que estamos
const writeMonth = (month) => {
    //GetDataToFillCalendar()
    for (let i = startDay(); i > 0; i--) {
        dates.innerHTML += ` <div class="calendar__date calendar__item calendar__last-days">
                                ${getTotalDays(monthNumber - 1) - (i - 1)}
                            </div>`;
    }

    for (let i = 1; i <= getTotalDays(month); i++) {
        //sombreamos el dia de hoy
        if (i === currentDay) {
            dates.innerHTML += ` <div class="calendar__date calendar__item calendar__today 
                                    calendar_click_item" data-toggle="modal" data-target="#selectDateModalCenter">
                                    ${i}
                                      <!-- <spam id="appointmentDays"></spam> -->
                                 </div>`;
        } else {
            dates.innerHTML += ` <div class="calendar__date calendar__item calendar_click_item" data-toggle="modal" 
                                        data-target="#selectDateModalCenter">
                                    ${i}
                                        <spam id="appointmentCurrenDays"></spam>
                                 </div>`;
        }

    }
   

}
//total de dia del mes
const getTotalDays = month => {
    if (month === -1) month = 11;

    if (month == 0 || month == 2 || month == 4 || month == 6 || month == 7 || month == 9 || month == 11) {
        return 31;

    } else if (month == 3 || month == 5 || month == 8 || month == 10) {
        return 30;

    } else {

        return isLeap() ? 29 : 28;
    }
}
//saber año visieto
const isLeap = () => {
    return ((currentYear % 100 !== 0) && (currentYear % 4 === 0) || (currentYear % 400 === 0));
}
//dia en el que empieza la semana
const startDay = () => {
    let start = new Date(currentYear, monthNumber, 1);
    return ((start.getDay() - 1) === -1) ? 6 : start.getDay() - 1;
}
//dibujar el mes anterio
const lastMonth = () => {
    if (monthNumber !== 0) {
        monthNumber--;
    } else {
        monthNumber = 11;
        currentYear--;
    }

    setNewDate();
}
//dubujar el proximo mes
const nextMonth = () => {
    if (monthNumber !== 11) {
        monthNumber++;
    } else {
        monthNumber = 0;
        currentYear++;
    }

    setNewDate();
}
//establecer nueva fecha en el calendario
const setNewDate = () => {
    currentDate.setFullYear(currentYear, monthNumber, currentDay);
    month.textContent = monthNames[monthNumber];
    year.textContent = currentYear.toString();
    dates.textContent = '';
    writeMonth(monthNumber);

    GetDataToFillCalendar();
}

writeMonth(monthNumber); //daySelecte


//metodo que se ejecuta cuando se le da click a un numero del calendario
document.getElementById('dates').addEventListener('click', function (e) {
    //encontrar el numero del calendario al cual se le dio click
    let self = e.target
    // month.textContent = monthNames[monthNumber];
    // year.textContent = currentYear.toString();

    let selectedDays = `${year.textContent}/${monthNumber + 1}/${self.innerText}`;

    document.getElementById('selectedDay').value = selectedDays;
})


//funcion que recargara la pagina luego de realizar crud
function reloadWindows() {

    setTimeout(function () {
        //cierra la alerta de bootstrap
        $(".alert").alert('close');
        location.reload();
    }, 2000);
}
function stopDefAction(evt) {
    evt.preventDefault();
}


//funcion que envia la nueva cita
function InsertAppointmentNew() {

    let formData = new FormData();

    // let hola = document.getElementsByName('_doctor')[0].value;timeAppointment

    formData.append("idDoctor", document.getElementsByName('_doctor')[0].value);
    formData.append("idCustomer", document.getElementsByName('_customer')[0].value);
    formData.append("description", document.getElementsByName('_description')[0].value);
    formData.append("dateAppointment", document.getElementsByName('_dateAppointment')[0].value);
    formData.append("timeAppointment", document.getElementsByName('_timeAppointment')[0].value);

    fetch('Appointment/Create', {
        method: 'POST',
        body: formData
    }).then(response => response.json())
        .then(data => {

            if (data == 1) {
                document.getElementById('messageSuccess').innerHTML = '<div class="alert alert-success" role="alert">Registro Insertado Correctamente.</div >';
                //cerramos el modal
                $("#exampleModal").modal("toggle");
            } else {
                document.getElementById('messageSuccess').innerHTML = '<div class="alert alert-danger" role="alert">Algo anda mal ${data}</div >';
            }



            reloadWindows();

        })
        .catch(error => console.error('Error:', error))
}
//funcion que trae la data con fetch de la base de datos
//para rellenar el calendario con la misma 
function GetDataToFillCalendar() {
   

    /*TODO:
      //esta funciona se trabajara luego
     */
    fetch('Appointment/GetDataCalendar', {
        method: 'GET'
    }).then(response => response.json())
        .then(data => {

            console.log(data)

            var fechauno = new Date();
            //fechauno.toDateString();

            for (let i = 0; i < data.length; i++) {

                let fechados = new Date(`${data[i]["dateAppointment"]}`);
               // fechados.toDateString()

                //alert(fechados + "  // " + fechauno);
                if (fechauno.toDateString() == fechados.toDateString()) {
                    appointmentDays.innerHTML = `
                        ${data[i]["timeAppointment"]}
                    `;
                } else {
                    //alert("no, no lo son /" + data[i]["dateAppointment"] + "//" + currentDate);
                    appointmentCurrenDays.innerHTML = `
                        ${data[i].timeAppointment}
                    `;
                    //alert("no son iguales //" + fechauno.toDateString() + " /" + fechados.toDateString());
                }/*
                appointmentDays.innerHTML = `
                        ${data["timeAppointment"]}
                    `;*/
             
            }

        })

}

window.onload = function () {
    GetDataToFillCalendar();
};