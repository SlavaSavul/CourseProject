﻿

$("#buttonSubmit").click(function () {
    let data = {
        id: $('input[name=Id]').val(),
        data: $('input[name=Data]').val(),
        description: $('input[name=Description]').val(),
        specialty: $('input[name=Specialty]').val(),
        name: $('input[name=Name]').val(),
        tags: $('input[name=Tags]').tagsinput('items')
    };

    $.ajax({
        type: 'POST',
        url: '/Home/SaveUpdatedArticle',
        data: data,
        success: function (data) {

        }
    });
});
