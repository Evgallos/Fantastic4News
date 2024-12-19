$(document).ready(function () {
    $('#startDate').on('change', function () {
        var selectedDate = $(this).val();
        console.log("date " + selectedDate);
        checkDateInDatabase(selectedDate);
    });
});

function checkDateInDatabase(date) {
    $.ajax({
        url: '/Customer/CheckDate', 
        type: 'GET',
        data: { date: date },
        success: function (msg) {
            if (msg != 'na') {
                console.log(msg);
                $('#errmsg').text(msg);
                $('#submitbtn').prop('disabled', true)

            }
            else {
                console.log("empty");
                $('#errmsg').text();
                $('#submitbtn').prop('disabled', false)

            }
        },
        error: function (xhr, status, error) {
            console.log('Error: ' + error);
        }
    });
}
