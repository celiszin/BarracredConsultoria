// Usa o objeto CONFIG_DATA que foi injetado pelo C# no HTML
let currentDate = new Date(CONFIG_DATA.ano, CONFIG_DATA.mes, 1);
let selectedDate = new Date(CONFIG_DATA.ano, CONFIG_DATA.mes, CONFIG_DATA.dia);

const calendarDaysEl = document.getElementById('calendarDays');
const monthYearDisplay = document.getElementById('monthYearDisplay');
const prevMonthBtn = document.getElementById('prevMonth');
const nextMonthBtn = document.getElementById('nextMonth');
const menuButton = document.getElementById('menuButton');
const requestSheetBtn = document.getElementById('requestSheetBtn');
const formAgendamento = document.getElementById('formAgendamento');

function showToast(message, isError = false) {
    const toast = document.getElementById('toastMessage');
    toast.textContent = message;
    toast.style.backgroundColor = isError ? '#c62828' : '#1a5c38';
    toast.classList.add('show');
    setTimeout(() => { toast.classList.remove('show'); }, 3000);
}

function renderCalendar() {
    const year = currentDate.getFullYear();
    const month = currentDate.getMonth();
    const monthNames = ['JANEIRO', 'FEVEREIRO', 'MARÇO', 'ABRIL', 'MAIO', 'JUNHO', 'JULHO', 'AGOSTO', 'SETEMBRO', 'OUTUBRO', 'NOVEMBRO', 'DEZEMBRO'];
    monthYearDisplay.textContent = `${monthNames[month]} ${year}`;
    
    const firstDayOfMonth = new Date(year, month, 1);
    const lastDayOfMonth = new Date(year, month + 1, 0);
    const daysInMonth = lastDayOfMonth.getDate();
    const startingDayOfWeek = firstDayOfMonth.getDay(); 
    const prevMonthLastDay = new Date(year, month, 0).getDate();
    
    calendarDaysEl.innerHTML = '';
    
    // Dias do mês anterior
    for (let i = 0; i < startingDayOfWeek; i++) {
        const prevMonthDay = prevMonthLastDay - startingDayOfWeek + i + 1;
        const dayEl = document.createElement('div');
        dayEl.className = 'calendar-day other-month';
        dayEl.textContent = prevMonthDay;
        calendarDaysEl.appendChild(dayEl);
    }
    
    // Dias do mês atual
    for (let day = 1; day <= daysInMonth; day++) {
        const dayEl = document.createElement('div');
        dayEl.className = 'calendar-day';
        dayEl.textContent = day;
        
        const isSelected = (selectedDate.getDate() === day && selectedDate.getMonth() === month && selectedDate.getFullYear() === year);
        if (isSelected) { dayEl.classList.add('selected'); }
        
        dayEl.addEventListener('click', (function(d) {
            return function() { selectDate(d, month, year); };
        })(day));
        
        calendarDaysEl.appendChild(dayEl);
    }
    
    // Preenchimento dos dias seguintes
    const totalCells = 42; 
    const currentCells = startingDayOfWeek + daysInMonth;
    const remainingCells = totalCells - currentCells;
    
    for (let i = 1; i <= remainingCells; i++) {
        const dayEl = document.createElement('div');
        dayEl.className = 'calendar-day other-month';
        dayEl.textContent = i;
        calendarDaysEl.appendChild(dayEl);
    }
}

function selectDate(day, month, year) {
    selectedDate = new Date(year, month, day);
    renderCalendar();
    const monthNames = ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'];
    showToast(`📅 Data selecionada: ${day} de ${monthNames[month]}`);
}

function prevMonth() { currentDate = new Date(currentDate.getFullYear(), currentDate.getMonth() - 1, 1); renderCalendar(); }
function nextMonth() { currentDate = new Date(currentDate.getFullYear(), currentDate.getMonth() + 1, 1); renderCalendar(); }

prevMonthBtn.addEventListener('click', prevMonth);
nextMonthBtn.addEventListener('click', nextMonth);
menuButton.addEventListener('click', () => { showToast('📋 Menu - Opções em breve'); });
requestSheetBtn.addEventListener('click', () => { showToast('📊 Solicitação de planilha enviada ao e-mail.'); });

// INTERCEPTAR O ENVIO DO FORMULÁRIO PARA O BACKEND (C#)
formAgendamento.addEventListener('submit', function(e) {
    e.preventDefault(); // Impede o envio imediato
    
    const timeInput = document.getElementById('horaEscolhida').value;
    if (!timeInput) {
        showToast('⚠️ Selecione um horário válido.', true);
        return;
    }
    
    // Monta a string ISO (YYYY-MM-DDTHH:mm) que o C# consegue converter em DateTime
    const year = selectedDate.getFullYear();
    const month = String(selectedDate.getMonth() + 1).padStart(2, '0');
    const day = String(selectedDate.getDate()).padStart(2, '0');
    
    document.getElementById('dataEscolhidaHidden').value = `${year}-${month}-${day}T${timeInput}`;
    
    // Envia para o servidor
    this.submit();
});

renderCalendar();