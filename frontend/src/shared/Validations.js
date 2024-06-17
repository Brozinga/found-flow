export function validateName(name) {
    if (name.length < 3) {
        return "O nome deve ter pelo menos 3 caracteres.;";
    }
    return ""; // Retorna uma string vazia se não houver erro
}

// Função para validar o email
export function validateEmail(email) {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email)) {
        return "Por favor, insira um email válido.;";
    }
    return ""; // Retorna uma string vazia se não houver erro
}

// Função para validar a senha
export function validatePassword(password) {
    const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$/;
    if (password.length < 6) {
        return "A senha deve ter pelo menos 6 caracteres.;";
    }
    if (!passwordRegex.test(password)) {
        return "A senha deve conter pelo menos uma letra minúscula, uma letra maiúscula e um dígito.;";
    }
    return ""; // Retorna uma string vazia se não houver erro
}

// Função para validar a confirmação da senha
export function validatePasswordConfirm(password, passwordConfirm) {
    if (password !== passwordConfirm) {
        return "As senhas não coincidem.;";
    }
    return ""; // Retorna uma string vazia se não houver erro
}