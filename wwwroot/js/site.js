//function chooseDateTimepartial(subId) {
//    console.log(subId);
//    $("#chooseDate").show();
//}
function chooseDateTimepartial(subtpId) {
    console.log("its here" + subtpId);
    $('#forsubtyid').val(subtpId);

    $('#chooseDate').show();
}
function cancelSubscription() {
    $('#chooseDate').hide();
}

////for other Subscription



//function chooseOtherSubscription(subId) {
//    consoe.log(subId);
//    $.ajax({
//        url: '/Customer/ChooseOtherSubscription',
//        data: { id: subsId },
//        dataType: 'json',

//        success: function (data) {
//            if (data.success) {
//                window.location.href = data.redirectToUrl;
//            }
//        },

//        error: function (err) {
//            console.log(err);
//        }


//    });

//}




//for free subscription
function chooseFreeSubscription(subsId) {
    console.log(subsId);
    $.ajax({
        url: '/Customer/ChooseFreeSubscription',
        data: { id: subsId},
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