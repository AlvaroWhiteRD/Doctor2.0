let monthNames = ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octobre', 'Noviembre', 'Diciembre'];

let currentDate = new Date();
let currentDay = currentDate.getDate();
let monthNumber = currentDate.getMonth();
let currentYear = currentDate.getFullYear();

let dates = document.getElementById('dates');
let month = document.getElementById('month');
let year = document.getElementById('year');


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

    for (let i = startDay(); i > 0; i--) {
        dates.innerHTML += ` <div class="calendar__date calendar__item calendar__last-days">
            ${getTotalDays(monthNumber - 1) - (i - 1)}
        </div>`;
    }

    for (let i = 1; i <= getTotalDays(month); i++) {

        if (i === currentDay) {
            dates.innerHTML += ` <div class="calendar__date calendar__item calendar__today calendar_click_item" data-toggle="modal" data-target="#selectDateModalCenter">${i}</div>
`;
        } else {
            dates.innerHTML += ` <div class="calendar__date calendar__item calendar_click_item" data-toggle="modal" data-target="#selectDateModalCenter">${i}</div>
          `;
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
}

writeMonth(monthNumber); //daySelecte


document.getElementById('dates').addEventListener('click', function (e) {
    //encontrar el numero del calendario al cual se le dio click
    let self = e.target
    // month.textContent = monthNames[monthNumber];
    // year.textContent = currentYear.toString();
    console.log(`se le dio click a ${self.innerText} ${monthNumber + 1} y el año es ${year.textContent}`);
    //console.log("Click on day: " + getTotalDays(month) + " (" + e.target + " events)");
})