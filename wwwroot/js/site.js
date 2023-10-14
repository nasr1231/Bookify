// Messages
function ShowSuccessMessage(message = 'Updated Successfully!') {
    Swal.fire({
        position: 'center-center',
        icon: 'success',
        title: 'Success',
        text: message, // Use the parameter message here
        showConfirmButton: false,
        timer: 2500
    });
}

function ShowErrorMessage(message = 'Something went wrong!') {
    Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: message, // Use the parameter message here
        customClass: {
            confirmButton: "btn btn-primary"
        }
    });
}

function OnModalSuccess(item) {
    ShowSuccessMessage();
    $('#model-window').modal('hide');
    $('tbody').append(item);
    KTMenu.init();
    KTMenu.initHandlers();
}

// Bootstrap Modal
$(document).ready(function () {

    var message = $('#Message').text();
    if (message !== '') {
        ShowSuccessMessage(message);
    }

    $('.js-save-btn').on('click', function () {
        //var btn = $(this);
        ShowSuccessMessage();
    });

    $('.js-render-modal').on('click', function () {
        var btn = $(this);
        var ShowModel = $('#model-window');
        ShowModel.find('#modalLabel').text(btn.data('title'));

        $.get({
            url: btn.data('url'),
            success: function (form) {
                ShowModel.find('.modal-body').html(form);
                $.validator.unobtrusive.parse(ShowModel);
            },
            error: function () {
                ShowErrorMessage();
            }
        });

        $('#model-window').modal('show');
    });
});
