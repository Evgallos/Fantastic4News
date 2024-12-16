// Function for like articles

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