const container = document.querySelector('.container');
const seats = document.querySelectorAll('.row .seat:not(.sold)');

function updateSelectedCount() {
    const selectedSeats = document.querySelectorAll('.seat');
    let selected = [];
    selectedSeats.forEach(function (element, index) {
        if (element.classList.contains("selected")) selected.push(index);
    })
    localStorage.setItem('selectedSeats', JSON.stringify(selected));
}

container.addEventListener('click', (e) => {
    if (e.target.classList.contains('seat') && !e.target.classList.contains('sold')) {
        e.target.classList.toggle('selected');
        updateSelectedCount();
    }
});


