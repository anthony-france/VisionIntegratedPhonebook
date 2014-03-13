$(document).ready(function () {
    $("#directory").tablesorter();
    $("img.unveil").unveil();
    

    $("form#search input[type=text]").on('input', function (e) {

        if (this.value.length > 3) {
            $("div.directory").load(
                "/Search?q=" + encodeURIComponent(this.value),
                function () {
                    $("img.unveil").unveil();
                }
            );
        }
    });
});

