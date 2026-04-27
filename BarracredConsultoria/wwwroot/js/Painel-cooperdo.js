document.addEventListener("DOMContentLoaded", function() {
    // 1. Lógica do BarraCred para Formulários de Satisfação
    if (localStorage.getItem("formSatisfacaoRespondido") === "true") {
        exibirFormularioFinal();
    }

    // Exportar a função para o escopo global para que o onclick="marcarComoRespondido()" no HTML funcione
    window.marcarComoRespondido = function() { 
        localStorage.setItem("formSatisfacaoRespondido", "true"); 
        exibirFormularioFinal(); 
        
        const formTitle = document.getElementById('form-title');
        if(formTitle) {
            formTitle.scrollIntoView({ behavior: 'smooth' });
        }
    };

    function exibirFormularioFinal() {
        const title = document.getElementById('form-title');
        const subtitle = document.getElementById('form-subtitle');
        const icon = document.getElementById('form-icon');
        const btnConcluir = document.getElementById('btn-concluir');
        const iframeSatisfacao = document.getElementById('iframe-satisfacao');
        const iframeFinal = document.getElementById('iframe-final');

        if(title) title.innerText = "Formulário de Consultoria";
        if(subtitle) subtitle.innerText = "Continue preenchendo as informações abaixo.";
        if(icon) icon.innerText = "description";
        if(btnConcluir) btnConcluir.style.display = "none";
        if(iframeSatisfacao) iframeSatisfacao.style.display = "none";
        if(iframeFinal) iframeFinal.style.display = "block";
    }

    // 2. Lógica da Busca de Consultores na Sidebar
    const searchInput = document.getElementById('searchInput');
    const consultantItems = document.querySelectorAll('.consultant-item');
    
    if (searchInput) {
        searchInput.addEventListener('input', (e) => {
            const searchTerm = e.target.value.toLowerCase();
            consultantItems.forEach(item => {
                const nameElement = item.querySelector('.consultant-name');
                const name = nameElement ? nameElement.innerText.toLowerCase() : '';
                item.style.display = name.includes(searchTerm) ? 'flex' : 'none';
            });
        });
    }

    // 3. Selecionar consultor na barra lateral (Feedback visual)
    consultantItems.forEach(item => {
        item.addEventListener('click', () => {
            consultantItems.forEach(i => i.classList.remove('active'));
            item.classList.add('active');
        });
    });
});