

function calculateExpiresDate() {

    var monthNum = parseInt($('#monthnum').val(), 10);  // Parse the month number as an integer ,10 tells that it is decimal num containin 2 nums only
    var startDate = new Date($('#startDate').val());    // Parse the start date
    if (monthNum == 1) {
        $('#errmsg').text("Month value cannot be 0");
    }
    else if (monthNum >= 12) {
        $('#errmsg').text("max value is a year");

    }
    else {

        console.log("monthnum " + monthnum);
        console.log("startdate " + startDate);

        var expiresDate = new Date(startDate);
        expiresDate.setMonth(startDate.getMonth() + monthNum);
        $('#ExpiresDate').val(expiresDate);
    }

}



//plus and minus month


function minusval() {
    var monthNum = $('#monthnum').val();
    var startdate = $('#startDate').val();
    if (monthNum == 1) {
        $('#errmsg').text("Month value cannot be 0");
    }
    else {
        monthNum--;
        $('#monthnum').val(monthNum);
    }
}

function plusval() {
    var monthNum = $('#monthnum').val();
    if (monthNum >= 12) {
        $('#errmsg').text("max value is a year");

    }
    else {
        monthNum++;
        $('#monthnum').val(monthNum);
    }
}



//show hide the choose subscription form
function chooseDateTimepartial(subtpId) {
    console.log("its here" + subtpId);
    $('#forsubtyid').val(subtpId);

    $('#chooseDate').show();
}
function cancelSubscription() {
    $('#chooseDate').hide();
}











//for free subscription
function chooseFreeSubscription(subsId) {
    console.log(subsId);
    $.ajax({
        url: '/Customer/ChooseFreeSubscription',
        data: { id: subsId },
        dataType: 'json',

        success: function (data) {
            if (data.success) {
                window.location.href = data.redirectToUrl;
            }
        },

        error: function (err) {
            console.log(err);
        }


    });
}

// Function for like articles

function likeArticle(id) {

    $.ajax({
        type: 'post',
        url: '/Article/LikeArticle',
        dataType: 'json',
        data: { id: id },

        success: function (data) {
            const el = document.getElementById('likes')
            if (el) {
                el.textContent = data;
            }

        },

        error: function (err) {
            console.log('Error: ' + err);
        }
    });
}