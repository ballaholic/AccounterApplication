$(function () {
    var url;
    var redirectUrl;
    var target;

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
    $(".trigger-delete").click(function (e) {
        e.preventDefault();
        target = $(this);

        var id = $(this).data("id");
        var controller = $(this).data("controller");
        var action = $(this).data("action");
        var title = $(this).data("modal-title");
        var bodyMessage = $(this).data("body-message");
        var buttonDelete = $(this).data("button-delete");
        var buttonCancel = $(this).data("button-cancel");
        redirectUrl = $(this).data("redirect-url");

        url = "/" + controller + "/" + action + "?id=" + id;

        $(".modal-title").text(title);
        $(".delete-modal-body").text(bodyMessage);
        $("#confirm-delete").text(buttonDelete);
        $("#cancel-delete").text(buttonCancel);

        $("#deleteModal").modal('show');
    });

    $("#confirm-delete").click(function (result) {
        $.get(url)
            .done((result) => {
                if (!redirectUrl) {
                    return $(target).parent().parent().hide("slow");
                }
                window.location.href = redirectUrl;
            })
            .fail((error) => {
                if (redirectUrl)
                    window.location.href = redirectUrl;
            }).always(() => {
                $("#deleteModal").modal('hide');
            });
    });

}());

