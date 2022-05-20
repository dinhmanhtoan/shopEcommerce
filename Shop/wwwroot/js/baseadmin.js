//altFormat: "DD-MM-YYYY",
//parseDate: (datestr, format) => {
//  return moment(datestr, format, true);
//},
//formatDate: (date, format, locale) => {
//  // locale can also be used
//  return moment(date).format(format);
//}

$('form').on('keyup keypress', function (e) {
    var keyCode = e.keyCode || e.which;
    if (keyCode === 13) {
        e.preventDefault();
        return false;
    }
});

$(".datepicker").flatpickr(
{
    wrap: true,
    enableTime: true,
    //altInput: true,
    dateFormat: "Y-m-d H:i",
    allowInput: true
});
// validate Number
function setInputFilter(textbox, inputFilter) {
    ["input", "keydown", "keyup", "mousedown", "mouseup", "select", "contextmenu", "drop"].forEach(function (event) {
        for (var i = 0; i < textbox.length; i++) {
            var self = textbox[i];
            self.addEventListener(event, function () {
                if (inputFilter(this.value)) {
                    this.oldValue = this.value;
                    this.oldSelectionStart = this.selectionStart;
                    this.oldSelectionEnd = this.selectionEnd;
                } else if (this.hasOwnProperty("oldValue")) {
                    this.value = this.oldValue;
                    this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
                } else {
                    this.value = "";
                }
            });
        }
    });
}
function setInputFilterLabel(textbox) {
    $(textbox).on("focusout input", function () {
        if (isNaN(parseFloat(this.value))) {
            var value = this.value;
        } else {
            var value = parseFloat(this.value).toFixed(2);
        }
        var labelValue = addCommas(value);
        $(this).next("label").text(labelValue);
    })
}
function numberValidate() {
    var input = document.getElementsByClassName("number-validate");
    setInputFilter(input, function (value) {
        return /^\d*\.?\d*$/.test(value);
    });
}
function numberValidateLabel() {
    var input = $(".number-validate");
    setInputFilterLabel(input);
}
numberValidateLabel()
function addCommas(nStr) {
    nStr += '';
    x = nStr.split('.');
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    x2 = x[1] == "00" || x[1] == undefined ? '' : '.' + x[1];
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}
function decimalValidateView() {
    $(".decimal-validate-view").each(function (index, item) {
        var value = parseFloat($(item).text());
        var valueFormart = addCommas(value);
        $(item).text(valueFormart);
    })
}
decimalValidateView()
numberValidate();

function loadError(arr) {
    var html = "";
    $.each(arr, function (index, item) {
        var item2 = item[0]
        html += `<li>${item2}</li>`;
    });
    alert22(html, "danger");
}

function alert22(message, type) {
    message = `<ul>${message}</ul>`
    var html = '<div class="alert alert-' + type + ' alert-dismissible" role="alert">' + message + '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button></div>'
    $("#loaderror").html(html);
}
function onModifyOption(callback) {
    bootbox.confirm(' Are you sure you want to do this?', function (result) {
        if (result) {
            setTimeout(function () {
                callback();
            }, 1);
        }
    });
}