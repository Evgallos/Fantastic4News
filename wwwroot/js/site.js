
// Coockie consent at _Layout.cshtml

var button = document.querySelector("#cookieConsent button[data-cookie-string]");

function SaveConsentCoockie() {
    document.cookie = button.dataset.cookieString;
    document.getElementById("cookieConsent").remove("show");
}

//to fetch Edit employee view component 

function displayEditVC(empId) {
    console.log("id " + empId);
    fetch(`/Admin/LoadEditComponent?empId=${empId}`)
        .then(response => response.text())
        .then(html => {
            const res = document.getElementById("resultdiv");
            res.style.display = 'block';
            res.innerHTML = html;
        });
}

//to fetch Register view component 

function displayRegisterVC() {
    console.log("test ");
    fetch(`/Admin/LoadRegisterComponent`)
        .then(response => response.text())
        .then(html => {
            const res = document.getElementById("resultdiv");
            res.style.display = 'block';
            res.innerHTML = html;
        });
}



// Function to show create form when selected category

//document.getElementById('drpCategory').addEventListener('change', function () {
//    document.getElementById('createArticleForm').style.display = 'block';
//});

function showArticleForm() {
      document.getElementById('createArticleForm').style.display = 'block';
}

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


//function for posting upload images
function uploadImg() {

    var input = document.getElementById("imageFile");
    var file = input.files[0];
    var formData = new FormData();
    formData.append("imageFile", file);

    $.ajax({
            type: 'post',
            url: '/Article/UploadImage',
            data: formData,
            processData: false, 
            contentType: false,
        // By setting processData and contentType to false,ensures that the file upload is handled
        // correctly by the browser and sent to the server in the appropriate format.
        success: function (response) {
            console.log("url" + response);
            console.log("Upload successful for file: " + file.name);
            uploadedurl = response; 
            document.getElementById("uploadedImg").value = uploadedurl;
            },
        error: function (xhr) {
            console.log("error " + xhr.statusText());
                console.log("Upload failed for file: " + file.name);
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

