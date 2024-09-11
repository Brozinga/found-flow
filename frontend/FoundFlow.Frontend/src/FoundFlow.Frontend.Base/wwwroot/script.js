
window.showToasts = (element) => {
    let toast = new bootstrap.Toast(element);
    toast.show();
};

const notification = new Notyf({
    position: {
        x: 'right',
        y: 'top',
    }
});

window.alertError = (html, duration, dismissible) => {
    notification.error({
        message: html,
        duration: duration,
        dismissible: dismissible
    });
}

window.alertSuccess = (html, duration, dismissible) => {
    notification.success({
        message: html,
        duration: duration,
        dismissible: dismissible
    });
}

window.alertBasic = (html, duration, dismissible) => {
    notification.error({
        message: html,
        duration: duration,
        dismissible: dismissible
    });
}