var origin = window.location.origin;
var url = window.location + "";
var path = url.replace(window.location.protocol + "//" + window.location.host + "/", "")
var link = path.split("/");
link[0] = link[0].includes("Widget") ? "Widget" : link[0]; // check Widget 

$('#btn-submit').on('click', function (e) {
    e.preventDefault();
    var $form = $(this).closest("form");
    var enctype = $($form).attr("enctype");
    var data;
    if (enctype == "multipart/form-data") {
        var form = $($form)[0];
        var formData = new FormData(form);
        $.ajaxSetup({
            processData: false,
            contentType: false,
        });
        data = formData;
    } else {
        data = $form.serializeArray();
    }
    if (!$form.valid || $form.valid()) {
        $.post($form.attr('action'), data)
            .then(function (result) {
                window.location = `${origin}/${link[0]}/Index`;
            }).catch(function (error) {
                loadError(error.responseJSON)
            });
    } else {
        if ($form.find('.validation-summary-errors').length) {
            var html = $('.validation-summary-errors').html();
            html = `<div class="alert alert-danger alert-dismissible" role="alert">${html}<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>`
            $('#loaderror').html(html);
        }
    }
  
});
$(document).ready(function () {
    var form2 = $('#btn-submit').closest("form");
    if ($(form2).data('validator')) {
        $(form2).data('validator').settings.ignore = ".ck-editor *";
    }
});