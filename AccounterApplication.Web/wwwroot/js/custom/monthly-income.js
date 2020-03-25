$(document).ready(function ($) {
    $(".cell-clickable-edit").dblclick(function () {
        var id = $(this).data("id");
        var url = $(this).data("url") + "?id=" + id;
        $.ajax({
            type: "GET",
            url: url,
            dataType: 'html',
            success: function (result) {
                window.location = url;
            }
        });
    });
});

$(document).ready(function ($) {
    $(".btn-delete").click(function () {
        var id = $(this).data("id");
        alert("Delete " + id);
    });
});
