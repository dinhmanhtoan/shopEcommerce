    var origin = window.location.origin;

// Get to WishList And Change Count();
function GetWishList() {
    var url = `${origin}/wishlist/GetWishList`;
    $.get(url).then(function (result) {
        var count = 0;
        if (result != null && result != '') {
            count = result.items.length;
        } else {
            $(".wishlist-count").html(`${0}`);
            return;
        }
        $(".wishlist-count").html(`${count}`);
        $("#wishlist-icon .fa-heart").css("color", "#080000 !important");
    }).catch(function (error) {
        toastr.error(error);
    })
};

// Add to wishList
$('.js-add-wishlist').on('click', function (e) {
    e.preventDefault();
    var productid = parseInt($(this).data('id'));
    var quantity = 1;
    var status = $(this).data('status');
    if (status) {
        $(this).addClass('wishlist-active');
        $(this).data('status', false);
        $(this).attr('data-tip', 'REMOVE WISHLIST');
        animation(this);
        $.ajax({
            type: 'POST',
            url: `${origin}/wishlist/add-item`,
            contentType: "application/json",
            data: JSON.stringify({ 
                productId: productid,
                quantity: quantity,
            })
        }).then(function (result) {
            GetWishList();
        }).catch(function (error) {
            toastr.error(error)
        });
    } else {
        $(this).removeClass('wishlist-active');
        $(this).data('status', true);
        $(this).attr('data-tip', 'ADD TO WISHLIST');
        $.ajax({
            type: 'DELETE',
            url: `${origin}/wishlist/remove-item?Id=${productid}`,
            contentType: "application/json"
        }).then(function (result) {
            GetWishList();
        }).catch(function (error) {
            toastr.error(error)
        });

    }
});

/*==================================================================[ Animation Wishlist and Card]*/
    function animation(e) {
        var animation;
        if ($(e).hasClass("js-add-wishlist")) {
                animation = $('#header-mid .wishlist-icon');

            // check window.width() has navbar mobile
        } else {
                animation = $('#header-mid .cart-icon');
        }

        var imgtodrag = $(e).closest('.product__item').find("img").eq(0);
        if (imgtodrag) {
            var imgclone = imgtodrag.clone()
                .offset({
                    top: imgtodrag.offset().top,
                    left: imgtodrag.offset().left
                })
                .css({
                    'opacity': '0.5',
                    'position': 'absolute',
                    'height': '150px',
                    'width': '150px',
                    'z-index': '3000'
                })
                .appendTo($('body'))
                .animate({
                    'top': animation.offset().top + 10,
                    'left': animation.offset().left + 10,
                    'width': 50,
                    'height': 50
                }, 1000, 'easeInOutExpo');

            setTimeout(function () {
                animation.effect("shake", {
                    times: 2
                }, 200);
            }, 1500);
            imgclone.animate({
                'width': 0,
                'height': 0
            }, function () {
                $(this).detach()
            });
            if ($(e).hasClass("togget-add-cart")) {
                setTimeout(function () { $('.cart-content').addClass('fadein') }, 2000);
            }

        }
}

/*==================================================================[ Animation Wishlist and Card]*/


    /*==================================================================[ Show menu mobile ]*/
    $('.open-close').on('click', function (e) {
        e.preventDefault();
        $('#header-mid').addClass('show-menu');
        $('.overlay-modal2').addClass('js-show-modal2');
    });

    /*==================================================================[ Hidden menu mobile ]*/
    $('.close-open').on('click', function (e) {
        e.preventDefault();
        $('#header-mid').removeClass('show-menu');
        $('.overlay-modal2').removeClass('js-show-modal2');
    });
    /*==================================================================[ Hidden menu mobile ]*/
    $('.overlay-modal2').on('click', function () {
        $('#header-mid').removeClass('show-menu');
        $('.overlay-modal2').removeClass('js-show-modal2');

    });

/*==================================================================[ Show modal Search ]*/
    $('.js-show-modal-search').on('click', function (e) {
        e.preventDefault();
        $('.modal-search-header').addClass('show-modal-search');
        $(this).css('opacity', '0');
    });
    /*==================================================================[ Hidden modal Search ]*/
    $('.js-hide-modal-search').on('click', function () {
        $('.modal-search-header').removeClass('show-modal-search');
        $('.js-show-modal-search').css('opacity', '1');
    });

    $('.container-search-header').on('click', function (e) {
        e.stopPropagation();
    });
/*==================================================================[ check Empty Search  ]*/
    $('input[name="query"]').on('keypress', function (e) {
        if (e.which === 13) {
            var value = $(this).val();
            if (value === "") {
                e.preventDefault();
            }
        }
    });
    $(".trans-04").on("click", function (e) {
        var value = $('input[name="query"]').val();
        if (value === "") {
            e.preventDefault();
        }
    });
/*==================================================================[ check Empty Search  ]*/

(function ($) {
    "use strict";

    /*[ Back to top ]
    ===========================================================*/
    var windowH = $(window).height() / 2;

    $(window).on('scroll', function () {
        if ($(this).scrollTop() > windowH) {
            $("#myBtn").css('display', 'flex');
        } else {
            $("#myBtn").css('display', 'none');
        }
    });

    $('#myBtn').on("click", function () {
        $('html, body').animate({ scrollTop: 0 }, 300);
    });


    /*==================================================================
    [ Fixed Header ]*/
    var wrapMenu = $('#header-mid');
    var posWrapHeader;
    if ($('#header-top').length > 0) {
        posWrapHeader = $('#header-top').outerHeight();
    }
    else {
        posWrapHeader = 0;
    }


    if ($(window).scrollTop() > posWrapHeader) {
        $(wrapMenu).css('top', 0);
    }
    else {
        $(wrapMenu).css('top', posWrapHeader - $(this).scrollTop());
    }

    $(window).on('scroll', function () {
        if ($(this).scrollTop() > posWrapHeader) {
            $(wrapMenu).css('top', 0);
        }
        else {
            $(wrapMenu).css('top', posWrapHeader - $(this).scrollTop());
        }
        if ($(window).width() <= 768) {
            $(wrapMenu).css('top', 0);
        }
    });

    //$(wrapMenu).css('top', 0);
    //$(window).width() <= 768) {
    // }
    /*==================================================================
    [ Menu mobile ]*/
    //$('.btn-show-menu-mobile').on('click', function () {
    //    $(this).toggleClass('is-active');
    //    $('.menu-mobile').slideToggle();
    //});

    //var arrowMainMenu = $('.arrow-main-menu-m');

    //for (var i = 0; i < arrowMainMenu.length; i++) {
    //    $(arrowMainMenu[i]).on('click', function () {
    //        $(this).parent().find('.sub-menu-m').slideToggle();
    //        $(this).toggleClass('turn-arrow-main-menu-m');
    //    });
    //}

    $(window).resize(function () {
        if ($(window).width() >= 768) {
            var has_js_show_modal = $(".overlay-modal2").hasClass("js-show-modal2");
            if (has_js_show_modal) {
                $(".overlay-modal2").removeClass("js-show-modal2");

            };
            $(wrapMenu).css('top', posWrapHeader - $(this).scrollTop());
        } else {
            var has_show_menu = $('#header-mid').hasClass('show-menu');
            if (has_show_menu) {
                $(".overlay-modal2").addClass("js-show-modal2");
            };
            $(wrapMenu).css('top', 0);
        }
    });
    $('input.rating-loading').rating({
        size: 'xs',
        filledStar: '<i class="fal fa-star"></i>',
        emptyStar: '<i class="fal fa-star"></i>'
    });
})(jQuery);
