const container = document.querySelector('.container');
const seats = document.querySelectorAll('.row .seat:not(.sold)');
populateUI();
function populateUI() {
    const selectedSeats = JSON.parse(localStorage.getItem('selectedSeats'));
    if (selectedSeats !== null && selectedSeats.lenght > -1) {
        seats.forEach((seats, index) => {
            if (selectedSeats.indexOf(index) > -1) {
                seats.classList.add('selected'); } });}}
function updateSelectedCount() {
    const selectedSeats = document.querySelectorAll('.seat');
    let selected = [];
    selectedSeats.forEach(function (element, index) {        
        if (element.classList.contains("selected")) selected.push(index);})
    localStorage.setItem('selectedSeats', JSON.stringify(selected));}
container.addEventListener('click', (e) => {
    if (e.target.classList.contains('seat') && !e.target.classList.contains('sold')) {
        e.target.classList.toggle('selected');
        updateSelectedCount(); }});