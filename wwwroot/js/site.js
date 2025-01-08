// Function to show create form when selected category

//document.getElementById('drpCategory').addEventListener('change', function () {
//    document.getElementById('createArticleForm').style.display = 'block';
//});


function ChooseSubs(MonthNum) {
    console.log(MonthNum);
    var monthNum = MonthNum;
    var startDate = new Date($('#startDate').val());    // Parse the start date
    var expiresDate = new Date();

    console.log("monthnum " + monthNum);
    console.log("startdate " + startDate);

    var expiresDate = new Date(startDate);
    expiresDate.setMonth(startDate.getMonth() + monthNum);
    console.log(expiresDate);

    var formattedExpiresDate = expiresDate.toISOString().split('T')[0];
    console.log("formattedExpiresDate: ", formattedExpiresDate);
    $('#ExpiresDate').val(formattedExpiresDate);
    $('#errmsg').text("");

    console.log("this is exp date " + $('#ExpiresDate').val());

}


//show hide the choose subscription form
function chooseDateTimepartial(subtpId, typName) {
    console.log("its here" + subtpId);
    console.log("tyname" + typName);
    $('#subsTp').text(typName);
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

