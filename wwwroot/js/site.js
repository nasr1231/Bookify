//Variables Definition
var table;
var datatable;
var exported_Columns = [];

function OnModalBegin() {
    $('.body :submit').attr('disabled', 'disabled').attr('data-kt-indicator', 'on');
}

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

function OnModalComplete() {
    $('.body :submit').removeAttr('disabled').removeAttr('data-kt-indicator');
}

function OnModalSuccess(row) {
    ShowSuccessMessage();
    $('#model-window').modal('hide');
    $('tbody').append(row);

    var newRow = $(row);
    datatable.row.add(newRow).draw();

    KTMenu.init();
    KTMenu.initGlobalHandlers();
}

var headers = $('th');
$.each(headers, function (i) {
    if (!$(this).hasClass('js-no-export'))
        exported_Columns.push(i);
});

// Datatable Configuration
var KTDatatables = function () {
    // Private functions
    var initDatatable = function () {
        // Init datatable --- more info on datatables: https://datatables.net/manual/
        datatable = $(table).DataTable({
            "info": false,
            'pageLength': 10,
        });
    }

    // Hook export buttons
    var exportButtons = () => {
        const documentTitle = $('.js-datatables').data('doc-title');
        var buttons = new $.fn.dataTable.Buttons(table, {
            buttons: [
                {
                    extend: 'copyHtml5',
                    title: documentTitle,
                    exportOptions: {
                        columns: exported_Columns
                    }
                },
                {
                    extend: 'excelHtml5',
                    title: documentTitle,
                    exportOptions: {
                        columns: exported_Columns
                    }
                },
                {
                    extend: 'csvHtml5',
                    title: documentTitle,
                    exportOptions: {
                        columns: exported_Columns
                    }
                },
                {
                    extend: 'pdfHtml5',
                    title: documentTitle,
                    exportOptions: {
                        columns: exported_Columns
                    }
                }
            ]
        }).container().appendTo($('#kt_datatable_example_buttons'));

        // Hook dropdown menu click event to datatable export buttons
        const exportButtons = document.querySelectorAll('#kt_datatable_example_export_menu [data-kt-export]');
        exportButtons.forEach(exportButton => {
            exportButton.addEventListener('click', e => {
                e.preventDefault();

                // Get clicked export value
                const exportValue = e.target.getAttribute('data-kt-export');
                const target = document.querySelector('.dt-buttons .buttons-' + exportValue);

                // Trigger click event on hidden datatable export buttons
                target.click();
            });
        });
    }

    // Search Datatable --- official docs reference: https://datatables.net/reference/api/search()
    var handleSearchDatatable = () => {
        const filterSearch = document.querySelector('[data-kt-filter="search"]');
        filterSearch.addEventListener('keyup', function (e) {
            datatable.search(e.target.value).draw();
        });
    }

    // Public methods
    return {
        init: function () {
            table = document.querySelector('.js-datatables');

            if (!table) {
                return;
            }

            initDatatable();
            exportButtons();
            handleSearchDatatable();
        }
    };
}();


function OnModalToaster() {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": true,
        "progressBar": false,
        "positionClass": "toastr-bottom-right",
        "preventDuplicates": true,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "3000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    toastr.success("Updated Successfully!");
}

// Bootstrap Modal
$(document).ready(function () {
    // Sweet Alert
    var message = $('#Message').text();
    if (message !== '') {
        ShowSuccessMessage(message);
    }
    // Data Table Setting
    KTUtil.onDOMContentLoaded(function () {
        KTDatatables.init();
    });

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
