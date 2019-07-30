

$(".changeAccommodationType").click(function () {
    var accommodationTypeId = $(this).attr("data-id");

    $(".accommodationTypeRow").hide();
    $("div.accommodationTypeRow[data-id=" + accommodationTypeId + "]").show();
});