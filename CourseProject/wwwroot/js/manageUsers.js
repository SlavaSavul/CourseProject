

    function Locking() {
        SendRequest("/Manage/LockUser");
    }
    function Unlocking() {
        SendRequest("/Manage/UnLockUser");
    }
    function Delete() {
        SendRequest("/Manage/DeleteUser");
    }

    function SendRequest(url) {
        $.ajax({
            type: 'POST',
            url: url,
            data: { arr: getCheckedCheckBoxes() },
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

