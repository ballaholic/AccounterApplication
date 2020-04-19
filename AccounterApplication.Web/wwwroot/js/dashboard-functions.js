$(function () {
    $(document).on("click", ".dropdown-item-toggle-expense-paging", (e) => triggerExpensePaging(e));

    function triggerExpensePaging(e) {

        let target = $(e.target);
        let url = target.data("url")
        let selectedCount = target.data("count");
        let data = { countOfExpenses: selectedCount }

        $.get(url, data, "html")
            .done((result) => {
                $(".card-body-last-expenses").html(result);
            });
    };
});