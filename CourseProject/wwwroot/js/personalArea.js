//function changeSort() {
//    SendRequest("","");
//}



//function SendRequest(url, data) {
//    $.ajax({
//        type: 'POST',
//        url: url,
//        data: data,
//        success: function (data) {
//            fillTable(data);
//        }
//    });
//}

//function fillTable(data){
//    $("#articlesTable tbody tr").remove();
//    for (i = 0; i < data.length; i++) {
//        addRow(data[i]);
//    }
//}

//function addRow(data) {
//    $("#articlesTable > tbody").append(`
//     <tr > \
//     <td>${data.name}</td>\
//     <td>${data.description}</td> \
//     <td>${data.spetialty}</td>
//    </tr>`);

//}