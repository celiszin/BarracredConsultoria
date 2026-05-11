// wwwroot/js/site-barracred.js

// Função global para mostrar a notificação (Toast)
function showMessage(message) {
    const toast = document.getElementById('toastMsg');
    if (toast) {
        toast.textContent = message;
        toast.classList.add('show');
        
        // Esconde após 4 segundos
        setTimeout(() => { 
            toast.classList.remove('show'); 
        }, 4000);
    }
}

// Rolagem suave para links da Landing Page
document.addEventListener("DOMContentLoaded", function() {
    document.querySelectorAll('a[href^="#"]').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            const targetId = this.getAttribute('href');
            if(targetId !== '#') {
                const target = document.querySelector(targetId);
                if (target) {
                    e.preventDefault();
                    target.scrollIntoView({ behavior: 'smooth' });
                }
            }
        });
    });
});