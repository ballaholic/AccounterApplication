$(document).ready(function ($) {
    $(".sort-date").click((e) => triggerSort(e));

    $(".sort-amount").click((e) => triggerSort(e));

    $(".sort-description").click((e) => triggerSort(e));
});

function triggerSort(e) {
    var target = $(e.target);
    var sortType = target.data("sort-type");
    var url = target.data("url");
    var groupId = $('input[name = "expenseGroupSelectedValue"]').val();
    var data = {
        sortType: sortType,
        groupId: groupId
    }

    $.get(url, data, "html")
        .done((result) => {
            $(".expenses-table-body").html(result);
        })
        .always(() => {
            if (sortType === "ascending") {
                target.data("sort-type", "descending");
            } else if (sortType === "descending") {
                target.data("sort-type", "ascending");
            }
        });
}