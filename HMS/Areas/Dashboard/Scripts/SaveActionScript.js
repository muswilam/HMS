
$("#saveActionBtn").click(function () {
    $.ajax({
        url: $(this).attr("data-href"),
        type: 'Post',
        data: $("#actionForm").serialize()
    })
    .done(function (result) {
        if (result.success) {
            location.reload();
        } else {
            $(".errorDiv").html(result.message);
        }
    });
});
