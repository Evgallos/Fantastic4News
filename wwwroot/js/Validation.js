$(document).ready(function () {
    $('#startDate').on('change', function () {
        var selectedDate = $(this).val();
        console.log("date " + selectedDate);
        checkDateInDatabase(selectedDate);
    });

    //validation fro login email or username

    $('#emailOrUsrname').on('change', function () {
        var emailOrusrname = $(this).val();
        console.log(emailOrusrname);
        $.ajax({
            type: 'POST',
            url: '/Customer/ValidateLoginEmailOrUsrName',
            dataType: 'json',
            data: { emailOrusrname: emailOrusrname },
            success: function (response) {
                if (!response.success) {                   

                    $('#emailOrUsrname').tooltip('dispose')
                        .attr('title', response.message.replace(/\n/g, ' <br /> '))
                        .tooltip({
                            html: true,
                            content: function ()//The content option ensures the tooltip correctly interprets HTML.
                            {
                                return $(this).prop('title');
                            }

                        })
                        .tooltip('show');
                    $('#login-submit').prop('disabled', true);

                } else {
                    $('#emailOrUsrname').tooltip('dispose');
                    $('#login-submit').prop('disabled', false);
                }
            },
            error: function (xhr, status, error) {
                console.error('An error occurred: ' + error);
            }
        });
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
                    $('#registerSubmit').prop('disabled',true)

                } else {
                    $('#regEmail').tooltip('dispose');
                    $('#registerSubmit').prop('disabled', false)
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
                    $('#registerSubmit').prop('disabled', true);

                } else {
                    $('#usrName').tooltip('dispose');
                    $('#registerSubmit').prop('disabled', true);
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
