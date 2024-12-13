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