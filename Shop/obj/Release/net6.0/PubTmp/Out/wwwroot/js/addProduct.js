$(document).ready(function () {
    addtocart();
    addtowishlist();
    function addtocart() {
        $.ajax({
            type: 'GET',
            url: '@Url.ActionLink("GetListProduct", "Cart")',
            success: function (result) {
                var row = ``;
                if (result != null && result != '') {
                    var cart = result;

                } else {
                    $("#cartBody").html('Giỏ hàng trống');
                    $(".total").find(".total-cart").html('');
                    //$(".cart-content").html('Giỏ hàng trống');
                    $(".cart-count").html(`${0}`);
                    return;

                }
                var count = 0;

                var totalPriceSales = function () {

                    let totalPriceSale = 0;
                    for (var i = 0; i < cart.length; i++) {

                        if (cart[i].Sale != "") {
                            totalPriceSale += parseFloat(cart[i].product.sale * cart[i].quantity);
                        } else {
                            totalPriceSale += parseFloat(cart[i].product.price * cart[i].quantity);
                        }
                    }
                    return totalPriceSale;
                };

                var totalPrices = function () {
                    let totalPrice = 0;

                    for (var i = 0; i < cart.length; i++) {
                        totalPrice += parseFloat(cart[i].product.price * cart[i].quantity);

                    }
                    return totalPrice;
                };
                var Total = 0;
                if (totalPriceSales() > 0) {
                    Total = "$" + totalPrices() + "₫" + "-" + "<del style='color: #888'>" + totalPriceSales() + " ₫" + "</del>"
                } else {
                    Total = "$" + totalPrices() + "₫"
                }
                $(".total").find(".total-cart").html(Total);
                for (var i = 0; i < cart.length; i++) {
                    row += `<li class="item-cart-1" data-id=${cart[i].product.id}>
                        <div class="img-left">
                            <a href="/chi-tiet/${cart[i].product.slug}">
                                <img src="https://localhost:5002/user-content/${cart[i].product.thumbnail.fileName}" style="width: 100%">
                            </a>
                        </div>
                        <div class="meta-right">
                            <a href="javascript:void(0)" class="remove-item">
                                <i class="fa fa-times" aria-hidden="true"></i>
                            </a>
                                 <a href="/chi-tiet/${cart[i].product.slug}" class="title cart-title">${cart[i].product.title}</a>
                            <p>
                            <span class="price">$${cart[i].product.price} ₫</span>`
                    if (cart[i].product.sale != "" && cart[i].product.sale != null) {
                        row += `- <span class="sale">${cart[i].product.sale} ₫</span> </p> `
                    }
                    row += `<div>
                             <p class="quantity">
                                <span class="name">Số lượng: </span>
                                <span>
                                    <div class="qty mt-1">
                                        <span class="minus bg-dark">-</span>
                                        <input type="number" class="count" name="qty" value="${cart[i].quantity}" min="1" max="10" data-val="${cart[i].quantity}">
                                        <span class="plus bg-dark">+</span>
                                     </div>
                                </span>
                            </p>
                            </div>

                        </div>
                            </li>`;
                    count += cart[i].quantity
                }
                $("#cartBody").html(row);
                $(".cart-count").html(`${count}`);
            }

        })
    };
    $(document).on('click', '.js-add-cart', function (event) {
        event.preventDefault();
        let parent = $(this).parents('.product-item');
        let productid = parseInt(parent.find('.product-grid6').attr('data-id'));

        $.ajax({
            type: 'POST',
            url: '@Url.ActionLink("AddToCart","Cart")',
            data: {
                productid: productid,

            },
            success: function (result) {
                addtocart();
                //window.location.href = '@Url.ActionLink("cart","cart")';
            }
        })

    });
    $(document).on('click', '.add-cart', function (event) {
        event.preventDefault();
        let parent = $(this).parents('.product-details');
        let productid = parseInt(parent.find('.product-details-item').attr('data-id'));
        let quantity = parseInt(parent.find('.num-product').val());
        $.ajax({
            type: 'POST',
            url: '@Url.ActionLink("AddToCart","Cart")',
            data: {
                productid: productid,
                quantity: quantity
            },
            success: function (result) {
                addtocart();
                //window.location.href = '@Url.ActionLink("cart","cart")';
            }
        })

    });
    $(document).on('click', '.checkout-cart', function (event) {
        event.preventDefault();
        let parent = $(this).parents('.product-details');
        let productid = parseInt(parent.find('.product-details-item').attr('data-id'));
        let quantity = parseInt(parent.find('.num-product').val());
        $.ajax({
            type: 'POST',
            url: '@Url.ActionLink("AddToCart","Cart")',
            data: {
                productid: productid,
                quantity: quantity
            },
            success: function (result) {
                addtocart();
                window.location.href = '@Url.ActionLink("checkout","cart")';
            }
        })

    });
    $(document).on('click', '.remove-item', function (e) {
        var parent = $(this).parents('.item-cart-1');
        var id = parseInt(parent.attr('data-id'));
        $.ajax({
            type: 'POST',
            url: '@Url.ActionLink("RemoveCart","Cart")',
            data: {
                productid: id,
            },
            success: function (result) {
                addtocart();
            }
        })
    });
    $(document).on('change', '.count', function () {
        var parent = $(this).parents('.item-cart-1');
        var id = parseInt(parent.attr('data-id'));
        var prev = parseInt(parent.find('input[name=qty]').data('val'));
        var value = parseInt($(this).val());

        if (value >= 1 && value < 11) {
            $.ajax({
                type: 'POST',
                url: '@Url.ActionLink("UpdateCart", "Cart")',
                data: {
                    productid: id,
                    quantity: value,
                },
                success: function (result) {
                    addtocart();
                }
            });
        } else {
            alert("Số lượng tối thiểu là 1 và tối đa là 10")
            parent.find('input[name=qty]').val(prev);
        }
    })
    $(document).on('click', '.plus', function () {

        var parent = $(this).parents('.item-cart-1');
        var id = parseInt(parent.attr('data-id'));
        var value = parseInt(parent.find('.count').val());
        value++;
        if (value > 1 && value <= 10) {

            $.ajax({
                type: 'POST',
                url: '@Url.ActionLink("UpdateCart", "Cart")',
                data: {
                    productid: id,
                    quantity: value,
                },
                success: function (result) {
                    addtocart();
                }
            });
        } else {
            alert("Không Quá 10 Sản Phẩm");
        }


    });
    $(document).on('click', '.minus', function () {
        var parent = $(this).parents('.item-cart-1');
        var id = parseInt(parent.attr('data-id'));
        var value = parseInt(parent.find('.count').val());
        if (value == 1) {
            $.ajax({
                type: 'POST',
                url: '@Url.ActionLink("RemoveCart", "Cart")',
                data: {
                    productid: id,
                },
                success: function (result) {
                    addtocart();
                }
            });
        } else {
            value--;
            $.ajax({
                type: 'POST',
                url: '@Url.ActionLink("UpdateCart", "Cart")',
                data: {
                    productid: id,
                    quantity: value,
                },
                success: function (result) {
                    addtocart();
                }
            });
        }


    });
    $(".cart-togget").on("click", function (e) {

        $(".cart-content").toggleClass("fadein");

    });
    $(".close-cart i").on("click", function () {
        $(".cart-content").removeClass("fadein");
    });
    $('.js-add-cart').on('click', function () {
        var cart = $('#cart');

        var imgtodrag = $(this).parents('.product-grid6').find("img").eq(0);
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
                    'z-index': '100'
                })
                .appendTo($('body'))
                .animate({
                    'top': cart.offset().top + 10,
                    'left': cart.offset().left + 10,
                    'width': 50,
                    'height': 50
                }, 1000, 'easeInOutExpo');

            setTimeout(function () {
                cart.effect("shake", {
                    times: 2
                }, 200);
            }, 1500);

            imgclone.animate({
                'width': 0,
                'height': 0
            }, function () {
                $(this).detach()
            });
            setTimeout(function () { $('.cart-content').addClass('fadein') }, 2000);
        }

    });
    $('.add-cart').on('click', function () {
        var cart = $('#cart');
        var imgtodrag = $(this).parents('.product-details').prev(".gallery-lb").find("img").eq(0);
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
                    'z-index': '100'
                })
                .appendTo($('body'))
                .animate({
                    'top': cart.offset().top + 10,
                    'left': cart.offset().left + 10,
                    'width': 50,
                    'height': 50
                }, 1000, 'easeInOutExpo');

            setTimeout(function () {
                cart.effect("shake", {
                    times: 2
                }, 200);
            }, 1500);

            imgclone.animate({
                'width': 0,
                'height': 0
            }, function () {
                $(this).detach()
            });
            setTimeout(function () { $('.cart-content').addClass('fadein') }, 2000);
        }

    });
    $('.js-add-wishlist').on('click', function () {
        var cart = $('#wishlist-icon');
        var imgtodrag = $(this).parents('.product-grid6').find("img").eq(0);
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
                    'z-index': '100'
                })
                .appendTo($('body'))
                .animate({
                    'top': cart.offset().top + 10,
                    'left': cart.offset().left + 10,
                    'width': 50,
                    'height': 50
                }, 1000, 'easeInOutExpo');

            setTimeout(function () {
                cart.effect("shake", {
                    times: 2
                }, 200);
            }, 1500);

            imgclone.animate({
                'width': 0,
                'height': 0
            }, function () {
                $(this).detach()
            });
        }

    });
    $(document).on('click', '.js-add-wishlist', function (event) {
        event.preventDefault();
        let parent = $(this).parents('.product-item');
        let productid = parseInt(parent.find('.product-grid6').attr('data-id'));

        $.ajax({
            type: 'POST',
            url: '@Url.ActionLink("AddtoWishList","Cart")',
            data: {
                productid: productid,
            },
            success: function (result) {
                addtocart();
                //window.location.href = '@Url.ActionLink("cart","cart")';
            }
        })

    });
    function addtowishlist() {
        $.ajax({
            type: 'GET',
            url: '@Url.ActionLink("GetWishList", "Cart")',
            success: function (result) {
                var count = 0;
                if (result != null && result != '') {
                    count = result.length;
                } else {
                    $(".wishlist-count").html(`${0}`);
                    return;
                }
                $(".wishlist-count").html(`${count}`);
            }
        })
    };
});
