window.global = {
    openModal: function (name) {
        name = '#' + name;
        $(name).modal('show')
    },
    closeModal: function (name) {
        name = '#' + name;
        $(name).modal('hide')
    }
}

function confirmDelete(uniqueId, isTrue) {
    var deleteSpan = 'deleteSpan_' + uniqueId;
    var confirmDeleteSpan = 'confirmDeleteSpan_' + uniqueId;
    if (isTrue) {
        $('#' + deleteSpan).hide();
        $('#' + confirmDeleteSpan).show();
    } else {
        $('#' + deleteSpan).show();
        $('#' + confirmDeleteSpan).hide();
    }
}