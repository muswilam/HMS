var pictureIds = [];

//iterate over pics and pushing them in array for serializing and saving
$("#saveActionBtn").click(function () {

    $("#picsArea img").each(function () {
        var picId = $(this).attr("data-id");
        pictureIds.push(picId);
    });

    $("#pictureIds").val(pictureIds.join());

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

//select pics then sending them by ajax for uploading in file and db ,then backing these pics to listing them 
$("#selectPictures").change(function () {
    var pictures = this.files;

    $("#deleteMsg").show();

    var formData = new FormData();
    for (var i = 0; i < pictures.length; i++) {
        formData.append("Picture", pictures[i]);
    }
    $.ajax({
        url: $(this).attr("pics-href"),
        type: 'Post',
        data: formData,
        processData: false,
        contentType: false
    })
    .done(function (result) {

        for (var i = 0; i < result.length; i++) {
            var picture = result[i];

            var imgHTML = $("#picTemplate").clone();
            imgHTML.find("img").attr("src", "/images/site/" + picture.URL);
            imgHTML.find("img").attr("data-id", picture.Id);

            $("#picsArea").append(imgHTML.html());
        }
    });
});

//remove pic by pressing on it
function removeMe(element) {
    pictureIds.pop(element.dataset.id);
    element.remove();
    
    // hide label message for deleting when no pics in the div
    if (!$.trim($('#picsArea').html()).length) {
        $("#deleteMsg").hide();
    }
}

