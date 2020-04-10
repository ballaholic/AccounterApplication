$(function () {
    let url;
    let redirectUrl;
    let target;

    //Append Delete modal
    $("body").append(`
                    <div class="modal fade" id="deleteModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                    <div class="modal-dialog modal-dialog-centered" role="document">
                        <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title text-gray-800">

                            </h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        </div>
                        <div class="modal-body delete-modal-body text-gray-800">
                            
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary text-gray-200" id="confirm-delete">
                            </button>
                            <button type="button" class="btn btn-secondary text-gray-200" data-dismiss="modal" id="cancel-delete">
                            </button>
                        </div>
                        </div>
                    </div>
                    </div>`);

    //Delete Action
    // The click() binding is called a "direct" binding which will only attach the handler to elements that already exist. 
    // It won't get bound to elements created in the future.To do that, we have to create a "delegated" binding by using on(). 
    $(document).on("click", ".trigger-delete", (e) => triggerDelete(e));

    // Confirm Delelte Action
    // The click() binding is called a "direct" binding which will only attach the handler to elements that already exist. 
    // It won't get bound to elements created in the future.To do that, we have to create a "delegated" binding by using on(). 
    $(document).on("click", "#confirm-delete", () => triggerConfirmDelete());

    function triggerDelete(e) {
        e.preventDefault();
        target = $(e.target);

        // If Child element is clicked
        if (target[0].nodeName.toLowerCase() === "i") {
            target = target.parent();
        }

        let id = target.data("id");
        let controller = target.data("controller");
        let action = target.data("action");
        let title = target.data("modal-title");
        let bodyMessage = target.data("body-message");
        let buttonDelete = target.data("button-delete");
        let buttonCancel = target.data("button-cancel");
        redirectUrl = target.data("redirect-url");

        url = "/" + controller + "/" + action + "?id=" + id;

        $(".modal-title").text(title);
        $(".delete-modal-body").text(bodyMessage);
        $("#confirm-delete").text(buttonDelete);
        $("#cancel-delete").text(buttonCancel);

        $("#deleteModal").modal('show');
    }

    function triggerConfirmDelete() {
        $.get(url)
            .done(() => {
                if (!redirectUrl) {
                    return $(target).parent().parent().hide("slow");
                }
                window.location.href = redirectUrl;
            })
            .fail(() => {
                if (redirectUrl)
                    window.location.href = redirectUrl;
            }).always(() => {
                $("#deleteModal").modal('hide');
            });
    }
}());

$(function () {

    // Edit item
    // The click() binding is called a "direct" binding which will only attach the handler to elements that already exist. 
    // It won't get bound to elements created in the future.To do that, we have to create a "delegated" binding by using on(). 
    $(document).on("dblclick", ".cell-clickable-edit", (e) => triggerEdit(e));

    // Sorting
    $(".sort-date-expenses").click((e) => triggerSortExpenses(e));

    $(".sort-amount-expenses").click((e) => triggerSortExpenses(e));

    $(".sort-description-expenses").click((e) => triggerSortExpenses(e));

    $(".sort-date-incomes").click((e) => triggerSortIncomes(e));

    $(".sort-amount-incomes").click((e) => triggerSortIncomes(e));

    // Grouping
    $(".dropdown-item-toggle-group-expenses").click((e) => triggerGrouping(e));

    function triggerEdit(e) {

        let target = $(e.target);
        let id = target.data("id");
        let url = target.data("url") + "?id=" + id;

        $.ajax({
            type: "GET",
            url: url,
            dataType: "html",
            success: function (result) {
                window.location = url;
            }
        });
    }

    function triggerSortExpenses(e) {

        let target = $(e.target);

        // If Child element is clicked
        if (target[0].nodeName.toLowerCase() === "i") {
            target = target.parent();
        }

        let sortType = target.data("sort-type");
        let url = target.data("url");

        let groupId = $('input[name = "expenseGroupSelectedValue"]').val();
        let data = {
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

        let target = $(e.target);

        // If Child element is clicked
        if (target[0].nodeName.toLowerCase() === "i") {
            target = target.parent();
        }

        let sortType = target.data("sort-type");
        let url = target.data("url");

        let data = {
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

    function triggerGrouping(e) {

        let target = $(e.target);
        let groupId = target.data("id");
        let url = target.parent().data("url");
        let data = { groupId: groupId };

        $.get(url, data)
            .done((result) => {
                $('input[name = "expenseGroupSelectedValue"]').val(groupId);
                $(".expenses-table-body").html(result);
            });
    }
});