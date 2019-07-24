
$("#deleteActionBtn").click(function () {
    var target = $(this).attr('href-target');
    $.ajax({
        url: $(this).attr("data-href"),
        type: 'Post',
        data: $("#actionForm").serialize()
    })
    .done(function (result) {
        if (result.success) {
            location.href = target;
        } else {
            $(".errorDiv").html(result.message);
        }
    });
});
