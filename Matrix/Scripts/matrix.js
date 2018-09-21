$(document).ready(function () {
    $('#btn_export').click(function () {
        $.post('/Home/ExportData', { flattened: window.matrix.flattened.join(','), size: window.matrix.size });
    });

    $(document).on('input[type=file]').change(function () {
        if ($('input[type=file]').val() == '') {
            $('#submitFile').attr('disabled', true)
        }
        else {
            $('#submitFile').attr('disabled', false);
        }
    })
});