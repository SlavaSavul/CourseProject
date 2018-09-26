// Write your JavaScript code.


function ProcessLike(id) {
    SendRequest("/Home/SetLikeToComment", { id: id });
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
            
        }
    });
}

function ProcessRate(id) {
    var rate = $('#rateComboBox').val();
    SendRequest("/Home/SetRate", { articleId: id, rate: rate });
}


