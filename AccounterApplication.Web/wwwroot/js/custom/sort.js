$(document).ready(function ($) {
    $(".sort-date-expenses").click((e) => triggerSortExpenses(e));

    $(".sort-amount-expenses").click((e) => triggerSortExpenses(e));

    $(".sort-description-expenses").click((e) => triggerSortExpenses(e));

    $(".sort-date-incomes").click((e) => triggerSortIncomes(e));

    $(".sort-amount-incomes").click((e) => triggerSortIncomes(e));
});

function triggerSortExpenses(e) {
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

function triggerSortIncomes(e) {
    var target = $(e.target);
    var sortType = target.data("sort-type");
    var url = target.data("url");
    var data = {
        sortType: sortType
    }

    $.get(url, data, "html")
        .done((result) => {
            $(".incomes-table-body").html(result);
        })
        .always(() => {
            if (sortType === "ascending") {
                target.data("sort-type", "descending");
            } else if (sortType === "descending") {
                target.data("sort-type", "ascending");
            }
        });
}