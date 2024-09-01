export function alertUser() {
    alert('The button was selected!');
}

export function addHandlers() {
    const btn = document.getElementById("notification-trigger");
    btn.addEventListener("click", alertUser);
}