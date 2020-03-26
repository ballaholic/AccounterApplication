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

$('#deleteIncomeModal').on('show.bs.modal', function (event) {

    // Button that triggered the modal
    var button = $(event.relatedTarget);

    // Extract info from data-* attributes
    var incomeId = button.data('incomeid');

    // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
    // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
    var modal = $(this);
    modal.find('#incomeId-hidden').val(incomeId);
});

$(document).ready(function ($) {
    $(".btn-delete").click(function () {
        var id = $('#incomeId-hidden').val();

        alert("Delete " + id);
    });
});
