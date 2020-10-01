$(document).ready(function () {
    $('[data-required]').each(function () {
        $(this).parent().find('.control-label').after().append('<span style="color:red;"> *</span>');
    });

    $("input").attr("autocomplete", "off");

    $(".alert").fadeTo(5000, 0);

});

$(document).on('focus', 'input,select,textarea', function () {
    $(this).removeClass("is-invalid");
    $(this).parent().find('.text-danger').hide();
});

function bsAlert(msg, status) {
    var type = "";
    if (status == 1) {
        type = "alert-success";
    } else if (status == -999) {
        type = "alert-warning";
    } else if (status == -1) {
        type = "alert-danger";
    }

    var al = '<div class="alert ' + type + '">' + msg + '</div>';

    $('#divAlert').append(al);
    $(".alert").fadeOut(5000, function () {
        $(this).remove();
    });
    setTimeout(function () {


    }, 5000);
}

function FormValidate(Elem) {

    if (Elem == undefined) {
        Elem = "form";
    }

    var _return = true;
    var oHTMLStr = "<ul>";
    //FOR REQUIRED FIELD
    $(Elem).find('[data-required]').each(function () {
        if ($(this).val() == null || $(this).val().trim().length == 0 || $(this).val().trim() == "") {
            $(this).addClass("is-invalid");
            if ($(this).attr("SelectionMode") == "Single")
                $(this).parent().parent().css("border", "1px solid #f75b36");
            if ($(this).hasClass("datepicker"))
                $(this).addClass("is-invalid");
            _return = false;
        }
    });

    //FOR VALIDATING MAX LENGTH OF FIELD
    $(Elem).find('[data-maxlength]').each(function () {
        //alert(parseInt($(this).val().length) + " | " + $(this).attr("data-maxlength"));
        var len = $(this).val().length;
        var maxlen = parseInt($(this).attr("data-maxlength"));
        if (len > maxlen) {
            $(this).parent().find('.text-danger').removeAttr('style');
            $(this).parent().find('.text-danger').text("Max " + maxlen + " characters allowed");
            $(this).addClass("is-invalid");

            _return = false;
        }
    });

    $(Elem).find('[data-numeric]').each(function () {
        //var regex = /[0-9 -()+]+$/;
        var regex = /^[0-9]+$/;
        var key = $(this).val();
        if (!regex.test(key) && key != '') {
            $(this).parent().find('.text-danger').removeAttr('style');
            $(this).parent().find('.text-danger').text("Only numbers are allowed");
            $(this).addClass("is-invalid");
            _return = false;
        }
    });

    $(Elem).find('[data-decimal]').each(function () {

        if ($(this).val().indexOf('.') != -1 && $(this).val().split('.')[1].length > 2) {
            $(this).parent().find('.text-danger').removeAttr('style');
            $(this).parent().find('.text-danger').text("Max 2 digits allowed after decimal");
            $(this).addClass("is-invalid");
            _return = false;
        }

        if (_return == true) {

            var regex = /^\d+(\.\d{1,2})?$/;
            var key = $(this).val();
            if (!regex.test(key) && key != '') {
                $(this).parent().find('.text-danger').removeAttr('style');
                var txt = $(this).parent().find('.text-danger').text();
                $(this).parent().find('.text-danger').text("Online numbers are allowed");
                $(this).addClass("is-invalid");
                _return = false;
            }
        }
    });
    return _return;
}

$(document).on('keypress', '[data-numeric]', function (e) {
    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
        return false;
    }
});

//FOR ONLY ACCEPTING DECIMAL NUMBERS IN FIELD
$(document).on('keypress', '[data-decimal]', function (e) {

    if (e.which != 8 && e.which != 0 && e.which != 46 && e.which != 45 && (e.which < 48 || e.which > 57)) {
        return false;

    }
    if (e.which == 46 && $(this).val().indexOf('.') != -1) {
        return false;
    }

});

$(document).on('keypress', '[data-maxlength]', function (e) {
    var len = $(this).val().length;
    var maxlen = parseInt($(this).attr("data-maxlength"));
    if (maxlen <= len) {
        if (!(e.which == 8 || e.which == 0)) {
            return false;
        }
    }
});