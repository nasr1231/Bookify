// Messages
function ShowSuccessMessage(message = 'Saved Successfully!'){
    Swal.fire({
        position: 'center-center',
        icon: 'success',
        title: 'Success',
        text: 'Updated Successfully!',
        showConfirmButton: false,
        timer: 2500
    });

}
function ShowErrorMessage(message = 'Saved Successfully!') {
    Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Something went wrong!',
        customClass: {
            confirmButton: "btn btn-primary"
        }
    })
}

function OnModalSuccess(item, message) {
    ShowSuccessMessage(message);
    $('#modal').modal('hide');
    $('tbody').append(item);
}

//Bootstrap Modal
$(document).ready(function () {

    var message = $('#Message').text();
    if (message !== '') {
        ShowSuccessMessage(message);
    }

    $('.js-save-btn').on('click', function () {
        var btn = $(this);
        success:
        ShowSuccessMessage();
    });

    $('.js-render-modal').on('click', function () {
        var btn = $(this);
        var ShowModel = $('#modal');
        ShowModel.find('#modalLabel').text(btn.data('title'));

        $.get({ 
            url: btn.data('url'),
            success: function (form) {
                ShowModel.find('.modal-body').html(form);
                
            },
            error: function () {
                ShowErrorMessage();
            }

        });

        $('#modal').modal('show');
    });
});
