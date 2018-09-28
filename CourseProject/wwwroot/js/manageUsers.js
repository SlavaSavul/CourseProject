﻿

function Locking() {
    SendRequest("/Manage/LockUser", { arr: getCheckedCheckBoxes() });
}

function Unlocking() {
    SendRequest("/Manage/UnLockUser", { arr: getCheckedCheckBoxes() });
}

function Delete() {
    SendRequest("/Manage/DeleteUser", { arr: getCheckedCheckBoxes() });
}

function AddAdminRole(data) {
    SendRequest("/Roles/AssignRole", { id: data.id, role: data.role });
}

function DeleteAdminRole(data) {
    SendRequest("/Roles/DeleteRole", { id: data.id, role: data.role });
}

    function SendRequest(url,data) {
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

