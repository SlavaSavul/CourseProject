var hubUrl = '/chat';
const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(hubUrl)
    .build();

hubConnection.on('SendComment', function (data) {
    let HTMLstring =
        `<div><div class="media-block" >
            <div class="media-body">
                <div class="mar-btm">
                    <span class=" text-semibold media-heading box-inline comment-Name">  ${data.name}</span>
                    <span class="text-muted text-sm"><i class="fa  fa-lg"></i> - ${data.date}</span>
                </div>
                <br />
                <p name="comment">  </p>
                <div class="pad-ver">
                    <span class=" tag-sm"><i class="glyphicon glyphicon-heart " style="color:#860a0a;" ></i> <span id="likes_${data.id}">${data.likes}</span> </span>
                    <div class="btn-group" style="float:right;display:block;">
                        <button class="btn btn-sm btn-default btn-hover-success active" id="like" onclick="ProcessLike('${data.id}')">
                            <i class="glyphicon glyphicon-thumbs-up" ></i>
                        </button>
                    </div>
                </div>
                <hr>
            </div>
       </div> </div>`;
    $("#Comments_" + data.articleId).prepend(HTMLstring);
    document.getElementsByName('comment')[0].innerText = data.comment;
});

hubConnection.start();

$('#commentFiled').keyup(function () {
    if ($('#commentFiled').val() != null && $('#commentFiled').val() != "") {
        $('#submitComment').removeAttr('disabled');
    }
    else {
        $('#submitComment').attr('disabled', 'disabled');
    }
});

function sendRequest(url, data, successCallback, errorCallback) {
    $.ajax({
        type: 'POST',
        url: url,
        data: data,
        success: function (data) {
            if (successCallback != null) {
                successCallback(data);
            }
        },
        error: function (data) {
            if (errorCallback != null) {
                errorCallback(data);
            }
        }
    });
}

function ProcessRate(id, rate) {
    sendRequest("/Home/SetRate", { articleId: id, rate: rate }, function () {
        location.reload();
    });
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

$("#fieldSearch").keyup(function (e) {
    var value = $("#fieldSearch").val();
    sendRequest("/Home/AutocompleteSearch", { term: value }, function (data) {
        $("#fieldSearch").autocomplete({
            source: data
        });
    });
});


var table = $('.table').DataTable({
    "paging": true,
    "ordering": true,
});


function articleDelete(articleId,userId) {
    sendRequest("/Home/DeleteArticle", { articleId: articleId, userId: userId}, function (href) {
        window.location.href = href;
    });
}

function setLanguage() {
    let data = {
        culture: $('#languageForm').val()
    };
    sendRequest("/Home/SetLanguage", data, function (href) {
        window.location.reload();
    });
}





