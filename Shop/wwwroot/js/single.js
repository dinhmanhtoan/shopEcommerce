var ratings = $(".rating");
if (ratings !== undefined && ratings.length > 0) {
    $.each(ratings, function (i, rating) {
        let scores = $(rating).text();
        if (scores !== undefined) {
            if (scores > 4.5) {
                html = '<i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-star" aria-hidden="true"></i>';

            } else if (scores > 4) {
                html = '<i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-star-half" aria-hidden="true"></i>';
            } else if (scores > 3.5) {
                html = '<i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-star" aria-hidden="true"></i>';
            } else if (scores > 3) {
                html = '<i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-star-half" aria-hidden="true"></i>';
            } else if (scores > 2.5) {
                html = '<i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-star" aria-hidden="true"></i>';
            } else if (scores > 2) {
                html = '<i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-star-half" aria-hidden="true"></i>';
            } else if (scores > 1.5) {
                html = '<i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-star" aria-hidden="true"></i>';
            } else if (scores > 1) {
                html = '<i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-half" aria-hidden="true"></i>';
            } else if (scores > 0.5 && scores > 0) {
                html = '<i class="fa fa-star" aria-hidden="true"></i>';
            } else {
                html = '<i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-star" aria-hidden="true"></i><i class="fa fa-star" aria-hidden="true"></i>';
            }
        }
        $(rating).html(html);
    })
}

var html = '';


/*=============================Slide Ảnh===================================*/
var slideIndex = 1;
showSlides(slideIndex);

function plusSlides(n) {
    showSlides(slideIndex += n);
}

function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    var i;
    var slides = document.getElementsByClassName("mySlides");
    var dots = document.getElementsByClassName("demo");

    if (n > slides.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slides.length }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";

}
/*==================================================================
[ +/- num product ]*/
$('.btn-num-product-down').on('click', function () {
    var numProduct = Number($(this).next().val());
    if (numProduct > 0) $(this).next().val(numProduct - 1);
});

$('.btn-num-product-up').on('click', function () {
    var numProduct = Number($(this).prev().val());
    $(this).prev().val(numProduct + 1);
});

/*================================================================*/


/*================================================================*/

/*============================Rating Riews====================================*/

$('.wrap-rating').each(function () {
    var item = $(this).find('.fa-star');
    var rated = -1;
    var input = $(this).find('input');
    $(input).val(0);

    $(item).on('mouseenter', function () {
        var index = item.index(this);
        var i = 0;
        for (i = 0; i <= index; i++) {
            $(item[i]).removeClass('zmdi-star-outline');
            $(item[i]).addClass('zmdi-star');
        }

        for (var j = i; j < item.length; j++) {
            $(item[j]).addClass('zmdi-star-outline');
            $(item[j]).removeClass('zmdi-star');
        }
    });

    $(item).on('click', function () {
        var index = item.index(this);
        rated = index;
        $(input).val(index + 1);
    });

    $(this).on('mouseleave', function () {
        var i = 0;
        for (i = 0; i <= rated; i++) {
            $(item[i]).removeClass('zmdi-star-outline');
            $(item[i]).addClass('zmdi-star');
        }

        for (var j = i; j < item.length; j++) {
            $(item[j]).addClass('zmdi-star-outline');
            $(item[j]).removeClass('zmdi-star');
        }
    });
});