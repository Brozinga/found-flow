
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

window.DataTableCreate = (table, additionalOptions) => {
    $(document).ready(function () {
        $(table).DataTable({
            ...additionalOptions,
            "language": {
                "url": "./datatables@2.1.6/pt-BR.json"
            },
            "responsive": true
        });
    });
}

window.DataTableRemove = (table) => {
    $(document).ready(function () {
        $(table).DataTable().destroy();
    });
}

window.Show = (element) => {
    $(element).show();
}

window.Hide = (element) => {
    $(element).hide();
}

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

$(document).ready(() => {
})