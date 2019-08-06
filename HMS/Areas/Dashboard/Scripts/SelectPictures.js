
$("#selectPictures").change(function () {
    var pictures = this.files;

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

        // put all pics ids in hidden input to serialize values to controller 
        var pictureIds = [];

        $("#picsArea img").each(function () {
            var picId = $(this).attr("data-id");
            pictureIds.push(picId);
        });

        $("#pictureIds").val(pictureIds.join());
    });
});

