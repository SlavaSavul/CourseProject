var hubUrl = '/chat';
const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(hubUrl)
    .configureLogging(signalR.LogLevel.Information)
    .build();

hubConnection.on('SendComment', function (data) {
    let HTMLstring = `<div>${data.date} -  ${data.name}</div>\
    ${data.comment}
    <input type="button" id="like" onclick="ProcessLike('${data.Id}')" value="like!" />
    <div>${data.likes}</div>`
    $("#" + data.articleId).prepend("<li> " + HTMLstring + " </li>");
});

hubConnection.start();

function getDataFromForm() {
    let data = {
        data: simplemde.value(),
        id: $('input[name=Id]').val(),
        description: $('input[name=Description]').val(),
        speciality: $('input[name=Speciality]').val(),
        name: $('input[name=Name]').val(),
        tags: $('input[name=Tags]').tagsinput('items')
    };
    return data;
}

$("#buttonSubmitCreate").click(function () {
    let data = getDataFromForm();
    sendRequest("/Home/CreateArticle", data, function (href) {
        window.location.href = href;
    });
});

$("#buttonSubmitSaveUpdated").click(function () {
    let data = getDataFromForm();
    sendRequest("/Home/SaveUpdatedArticle", data, function (href) {
        window.location.href = href;
    });
});

function ProcessLike(id) {
    sendRequest("/Home/SetLikeToComment", { id: id });
    $('#like').attr("disabled", false);
}

function ProcessComment(id) {
    var text = $('#commentFiled').val();
    sendRequest("/Home/SetComment", { articleId: id, text: text });
}

function sendRequest(url, data,callback) {
    $.ajax({
        type: 'POST',
        url: url,
        data: data,
        success: function (data) {
            if (callback != null) {
                callback(data);
            }
        }
    });
}

function ProcessRate(id) {
    var rate = $('#rateComboBox').val();
    sendRequest("/Home/SetRate", { articleId: id, rate: rate });
}


function Locking() {
    sendRequest("/Manage/LockUser", { arr: getCheckedCheckBoxes() }, function () {
        location.reload();
 });
}

function Unlocking() {
    sendRequest("/Manage/UnLockUser", { arr: getCheckedCheckBoxes() },function () {
        location.reload();
 });
}

function Delete() {
    sendRequest("/Manage/DeleteUser", { arr: getCheckedCheckBoxes() },function () {
        location.reload();
 });
}

function AddAdminRole(data) {
    sendRequest("/Roles/AssignRole", { id: data.id, role: data.role },function () {
        location.reload();
 });
}

function DeleteAdminRole(data) {
    sendRequest("/Roles/DeleteRole", { id: data.id, role: data.role },function () {
        location.reload();
 });
}

function getCheckedCheckBoxes() {
    var checkboxes = document.getElementsByClassName('checkbox');
    var checkboxesChecked = [];
    for (var index = 0; index < checkboxes.length; index++) {
        if (checkboxes[index].checked) {
            checkboxesChecked.push(checkboxes[index].value);
        }
    }
    return checkboxesChecked;
}

$('#select_all').click(function () {
    var c = this.checked;
    $(':checkbox').prop('checked', c);
});

function Search() {
    var keyword = $('#search').val();
    var l = $(this).attr("href");
    window.location.href = '/Home/SearchByKeyword?keyword=' + keyword;
}

$("#search").keyup(function (e) {
    var value = $("#search").val();
    if (e.keyCode == 13)
        Search();
   /* sendRequest("/Home/AutocompleteSearch", { term: value }, function (data) {
        $("#search").autocomplete({
            source: data
        });
    });*/
});