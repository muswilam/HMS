
$(document).ready(function () {
    $(".action-btn").click(function () {
        $.ajax({
            url: $(this).attr("data-href"),
        })
        .done(function (result) {
            $("#actionModal .modal-dialog").html(result);
        });
    });
});