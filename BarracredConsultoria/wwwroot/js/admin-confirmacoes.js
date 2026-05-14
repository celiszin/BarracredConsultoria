document.addEventListener('DOMContentLoaded', function () {
    const searchInput = document.getElementById('searchInput');
    const confirmModal = document.getElementById('confirmModal');

    // Função para abrir o modal com dados dinâmicos
    window.openModal = function (id, nome, acao) {
        const title = document.getElementById('modalTitle');
        const msg = document.getElementById('modalMessage');
        const icon = document.getElementById('modalIcon');
        const iconCircle = document.getElementById('modalIconCircle');
        const confirmBtn = document.getElementById('modalConfirmBtn');
        const form = document.getElementById('modalForm');
        const inputId = document.getElementById('modalInputId');

        inputId.value = id;
        confirmModal.style.display = 'flex';

        if (acao === 'aprovar') {
            title.innerText = "Aprovar Cadastro";
            msg.innerHTML = `Deseja confirmar a aprovação de <strong>${nome}</strong>?`;
            icon.className = "ti ti-user-check";
            iconCircle.style.backgroundColor = "#dcfce7";
            iconCircle.style.color = "#16a34a";
            confirmBtn.innerText = "Sim, Aprovar";
            confirmBtn.style.backgroundColor = "#1e5c3a";
            form.action = "/Admin/ConfirmarAprovacao"; // Altere para sua rota real
        } else {
            title.innerText = "Rejeitar Cadastro";
            msg.innerHTML = `Tem certeza que deseja recusar o cadastro de <strong>${nome}</strong>?`;
            icon.className = "ti ti-user-x";
            iconCircle.style.backgroundColor = "#fef2f2";
            iconCircle.style.color = "#ef4444";
            confirmBtn.innerText = "Sim, Rejeitar";
            confirmBtn.style.backgroundColor = "#ef4444";
            form.action = "/Admin/ConfirmarRejeicao"; // Altere para sua rota real
        }
    };

    window.closeModal = function () {
        confirmModal.style.display = 'none';
    };

    confirmModal.addEventListener('click', function (e) {
        if (e.target === confirmModal) closeModal();
    });

    if (searchInput) {
        searchInput.addEventListener('input', function () {
            const termo = this.value.toLowerCase();
            const cards = document.querySelectorAll('.card');

            cards.forEach(card => {
                const nome = card.getAttribute('data-name').toLowerCase();
                // Você pode adicionar CPF ou Email na busca também
                card.style.display = nome.includes(termo) ? 'block' : 'none';
            });
        });
    }
});