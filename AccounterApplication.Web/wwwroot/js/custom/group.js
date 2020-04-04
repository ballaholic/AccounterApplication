$(document).ready(function ($) {
    $(".dropdown-item-toggle-group-expenses").click(function (e) {
        var target = $(this);
        var groupId = target.data("id");
        var url = target.parent().data("url");
        var data = { groupId: groupId };

        $.get(url, data)
            .done((result) => {
                $('input[name = "expenseGroupSelectedValue"]').val(groupId);
                $(".expenses-table-body").html(result);
            });
    });
});