// Função para mostrar/esconder senha em qualquer campo
function togglePasswordVisibility(inputId, btnElement) {
    const passwordInput = document.getElementById(inputId);
    if (!passwordInput) return;

    const type = passwordInput.getAttribute('type') === 'password' ? 'text' : 'password';
    passwordInput.setAttribute('type', type);
    
    const svgPath = btnElement.querySelector('svg');
    if (svgPath) {
        if (type === 'text') {
            svgPath.innerHTML = '<path stroke-linecap="round" stroke-linejoin="round" d="M3.98 8.223A10.477 10.477 0 001.934 12C3.226 16.338 7.244 19.5 12 19.5c.993 0 1.953-.138 2.863-.395M6.228 6.228A10.45 10.45 0 0112 4.5c4.756 0 8.773 3.162 10.065 7.498a10.523 10.523 0 01-4.293 5.774M6.228 6.228L3 3m3.228 3.228l3.65 3.65m7.894 7.894L21 21m-3.228-3.228l-3.65-3.65M16.5 12a4.5 4.5 0 01-4.5 4.5m0-9a4.5 4.5 0 00-4.5 4.5" />';
        } else {
            svgPath.innerHTML = '<path stroke-linecap="round" stroke-linejoin="round" d="M2.036 12.322a1.012 1.012 0 010-.639C3.423 7.51 7.36 4.5 12 4.5c4.638 0 8.573 3.007 9.963 7.178.07.207.07.431 0 .639C20.577 16.49 16.64 19.5 12 19.5c-4.638 0-8.573-3.007-9.963-7.178z" /><path stroke-linecap="round" stroke-linejoin="round" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z" />';
        }
    }
}

// Lógica de Lembrar-me
(function() {
    const emailInput = document.getElementById('Login_Email');
    const rememberMeCheckboxInput = document.getElementById('rememberMeCheckboxInput');
    const rememberCheckbox = document.getElementById('rememberCheckbox');
    const rememberWrapper = document.getElementById('rememberCheckboxWrapper');
    const loginForm = document.querySelector('#loginSection form');

    let isChecked = false;

    function updateCheckboxUI() {
        if (rememberCheckbox) {
            if (isChecked) {
                rememberCheckbox.classList.add('checked');
            } else {
                rememberCheckbox.classList.remove('checked');
            }
        }
    }

    if (rememberWrapper) {
        rememberWrapper.addEventListener('click', (e) => {
            isChecked = !isChecked;
            if (rememberMeCheckboxInput) rememberMeCheckboxInput.checked = isChecked;
            updateCheckboxUI();
        });
    }

    function loadRememberedUser() {
        const isRemembered = localStorage.getItem('rememberMe') === 'true';
        if (isRemembered) {
            isChecked = true;
            if (rememberMeCheckboxInput) rememberMeCheckboxInput.checked = true;
            if (emailInput && localStorage.getItem('savedUsername')) {
                emailInput.value = localStorage.getItem('savedUsername');
            }
        }
        updateCheckboxUI();
    }

    if (loginForm) {
        loginForm.addEventListener('submit', () => {
            if (rememberMeCheckboxInput && rememberMeCheckboxInput.checked && emailInput.value) {
                localStorage.setItem('rememberMe', 'true');
                localStorage.setItem('savedUsername', emailInput.value);
            } else {
                localStorage.removeItem('rememberMe');
                localStorage.removeItem('savedUsername');
            }
        });
    }

    loadRememberedUser();
})();