function LoadPagination(url, page) {
    var stringsearch = $("#object_Search").val();
    var sizeperpage = $("#sizeperpageselect").val();
    $('#paginatedsection').empty();
    $.ajax({
        url: "/" + url + "?page=" + page + "&&pagesize=" + sizeperpage + "&&stringsearch=" + stringsearch + "",
        type: 'GET',
        success: function (result) {
            $('#paginatedsection').html(result);
            $('#sizeperpageselect').val(sizeperpage);
        }
    });
}