﻿// Write your JavaScript code.


function ProcessLike(id) {
    SendRequest("/Home/SetLikeToComment", { articleId: id });
    $('#like').attr("disabled", false);
    //cheach current state - if user has set like, button inactiveS
}


function ProcessComment(id) {
    var text = $('#commentFiled').val();
    SendRequest("/Home/SetComment", { articleId: id, text: text });
}


function SendRequest(url, data) {
    $.ajax({
        type: 'POST',
        url: url,
        data: data,
        success: function (data) {
            if (data == true) {
                location.reload();
            }
            else {
            }
        }
    });
}