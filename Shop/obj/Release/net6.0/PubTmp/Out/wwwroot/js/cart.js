
var origin = window.location.origin;
function Isvalid(data) {
    $(".subtotal").text(data.subTotalString);
    $(".discount").text(data.discountString);
    $(".ordertotal").text(data.orderTotalString);

    if (!data.isValid) {
        $(".order-summary .btn").addClass("disabled");
    } else {
        $(".order-summary .btn").removeClass("disabled");
    }
    var error1 = $(".badge.badge-pill");
    $(error1).each(function (index, item) {
        $(item).remove();
    });
    $(data.items).each(function (index,item) {
        if (!item.isProductAvailabeToOrder) {
            var html = `<span class="badge badge-pill badge-danger">Not availabe any more</span>`;
            $(".cart tbody tr").eq(index).find(".hidden-md-up").append(html)
        }
        if (item.productStockTrackingIsEnabled && item.productStockQuantity < item.quantity) {
            var html = `<span class="badge badge-pill badge-danger">Not enough stock. Available: ${item.productStockQuantity}</span>`;
            $(".cart tbody tr .cart-item-image").eq(index).find(".hidden-md-up").append(html)
        }
    })
    $("#cart .badge").text(data.items.length)
    if (data.items.length == 0) {
        var html = `<td colspan="5" class="text-center">
                        There are no items in this cart. <a href="/" class="text-primary">Go to shopping</a>
                    </td>`
        $(".cart tbody").html(html);
        $(".order-summary").remove();
        $(".coupon-form").remove();
      
    }
}


// event
$(document).on('click', '.quantity-button', function () {

});

//ajax remove cartItem
$(document).on('click', '.remove-in-cart', function (e) {
    var parent = $(this).parents('.item-in-cart');
    var CartId = parseInt(parent.attr('data-id'));
    var url = `${origin}/Cart/Remove`
    $(".loading").css("display", "block");
    $.ajax({
        type: 'POST',
        url: url,
        data: {
            CartId: CartId
        },
        success: function (data) {
            if (data.error) {
                $(".loading").css("display", "none");
                toastr.error(data.message);
            } else {
                $(".loading").css("display", "none");
                Isvalid(data);
                $(parent).remove();
                toastr.success("thành công");
            }

        }
    });
});
// ajax plus,minus
$(document).on('click', '.quantity-button', function () {
    var parent = $(this).parents('.item-in-cart');
    var CartId = parseInt(parent.attr('data-id'));
    var quantityInput = parent.find('.quantity-field');
    var quantityButton = $(this).val();
    var url = `${origin}/Cart/UpdateQuantity`;

    if (quantityButton == '-' && quantityInput.val() == 1) {
        return;
    }
    if ($(this).val() === '+') {
        quantityInput.val(parseInt(quantityInput.val(), 10) + 1);
    }
    else if (quantityInput.val() > 1) {
        quantityInput.val(quantityInput.val() - 1);
    }
    var quantity = parseInt(quantityInput.val());
    $(".loading").css("display", "block");
    $.ajax({
        type: 'POST',
        url: url,
        data: {
            CartItemId: CartId,
            quantity: quantity
        }
    }).then(function (data) {
        if (data.error) {
            $(".loading").css("display", "none");
            toastr.error(data.message);
            if (quantityButton == '+') {
                quantityInput.val(quantityInput.val() - 1);
            } else {
                quantityInput.val(quantityInput.val() + 1);
            }
        } else {
            $(".loading").css("display", "none");
            Isvalid(data);
            toastr.success("thành công");
        }
    }).catch(function (error) {
        $(".loading").css("display", "none");
        toastr.error(error);
    });
});

function saveOrderNote() {
    var content = $("textarea[name='orderNote']").val();
    var url = `${origin}/Cart/save-ordernote`;
    $(".loading").css("display", "block");
    $.ajax({
        type: 'POST',
        url: url,
        data: {
            OrderNote: content,
        }
    }).then(function (data) {
        if (data.error) {
            $(".loading").css("display", "none");
            toastr.error(data.message);
        } else {
            $(".loading").css("display", "none");
            toastr.success("Thanh Cong");
        }

    }).catch(function (error) {
        $(".loading").css("display", "none");
        toastr.error(error);
    });
}
function unlock() {
    $(".loading").css("display", "block");
    var url = `${origin}/cart/unlock`;
    $.ajax({
        type: 'POST',
        url: url,
    }).then(function (data) {
        if (data.error) {
            $(".loading").css("display", "none");
            toastr.error(data.message);
        } else {
            $(".loading").css("display", "none");
            $("#unlock").css("display", "none");
            $("#unlock").prev().removeClass("disabled");
            toastr.success("Thanh Cong");
        }

    }).catch(function (error) {
        $(".loading").css("display", "none");
        toastr.error(error);
    });
}

function applyCoupon(event) {
    event.preventDefault();
    var url = `${origin}/cart/apply-coupon`;
    var form = $(event.target).closest("form");
    var CouponCode = $(form).find("input[name='couponCode']").val();
    $(".loading").css("display", "block");
    $.post(url, { CouponCode: CouponCode }).then(function (data) {
        if (data.errorMessage) {
            $(".loading").css("display", "none");
            $("#couponEroor").html(data.errorMessage);
        } else {
            $(".loading").css("display", "none");
            Isvalid(data);
            toastr.success("su dung thanh cong");
        }
    }).catch(function (error) {
        $(".loading").css("display", "none");
        toastr.error(error);
    });
}
