var hubUrl = '/chat';
const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(hubUrl)
    .configureLogging(signalR.LogLevel.Information)
    .build();

hubConnection.on('SendComment', function (data) {
    let HTMLstring = 
        ` <div><div class="media-block" >
            <div class="media-body">
                <div class="mar-btm">
                    <span class=" text-semibold media-heading box-inline comment-Name">  ${data.name}</span>
                    <span class="text-muted text-sm"><i class="fa  fa-lg"></i> - ${data.date}</span>
                </div>
                <br />
                <p>  ${data.comment}</p>
                <div class="pad-ver">
                    <span class=" tag-sm"><i class="fa fa-heart text-danger" ></i> ${data.likes} </span>
                    <div class="btn-group">
                        <button class="btn btn-sm btn-default btn-hover-success active" id="like" onclick="ProcessLike('${data.Id}')">
                            <i class="fa fa-thumbs-up" ></i>
                        </button>
                    </div>
                </div>
                <hr>
            </div>
       </div> </div>`;
    $("#Comments_" + data.articleId).prepend(HTMLstring);
});

hubConnection.start();

function getFormData() {
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
    let data = getFormData();
    if (data != null) {
        sendRequest("/Home/CreateArticle", data, function (href) {
            window.location.href = href;
        });
    }
});

$("#buttonSubmitSaveUpdated").click(function () {
    let data = getFormData();
    sendRequest("/Home/SaveUpdatedArticle", data, function (href) {
        window.location.href = href;
    });
});

function ProcessLike(id) {
    sendRequest("/Home/SetLikeToComment", { id: id });
}

function ProcessComment(id) {
    var text = $('#commentFiled').val();
    if (text != null && text != "") {
        $('#commentFiled').val("");
        sendRequest("/Home/SetComment", { articleId: id, text: text });
    }
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




$(document).ready(function () {
    $('.table tfoot th').each(function () {
        var title = $(this).text();
        $(this).html('<input type="text" placeholder="Search ' + title + '" />');
    });

    var table = $('.table').DataTable({
        "paging": true,
        "ordering": true,

    });

    table.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();
            }
        });
    });
   
});


$(document).ready(function () {
    $('#dataTables_filter').hide();
});
