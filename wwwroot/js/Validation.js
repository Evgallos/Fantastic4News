$(document).ready(function () {
    $('#startDate').on('change', function () {
        var selectedDate = $(this).val();
        console.log("date " + selectedDate);
        checkDateInDatabase(selectedDate);
    });


    //validation for register Email
    $('#regEmail').on('change', function () {
        var email = $(this).val();
        console.log(email);
        $.ajax({
            type: 'POST',
            url: '/Customer/ValidateRegisterEmail',
            dataType: 'json',
            data: { email: email },
            success: function (response) {
                if (!response.success) {
                    $('#regEmail').tooltip('dispose')
                        .attr('title', response.message)
                        .tooltip('show');

                } else {
                    $('#regEmail').tooltip('dispose')
                }
            },
            error: function (xhr, status, error) {
                console.error('An error occurred: ' + error);
            }
        });
    });


    $('#usrName').on('change', function () {
        var userName = $(this).val();
        console.log(userName);
        $.ajax({
            type: 'POST',
            url: '/Customer/ValidateRegisterUsername',
            dataType: 'json',
            data: { userName: userName },
            success: function (response) {
                if (!response.success) {
                    $('#usrName').tooltip('dispose')
                        .attr('title', response.message)
                        .tooltip('show');

                } else {
                    $('#usrName').tooltip('dispose')
                }
            },
            error: function (xhr, status, error) {
                console.error('An error occurred: ' + error);
            }
        });
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
