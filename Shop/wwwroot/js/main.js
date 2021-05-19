
    addtocart();
addtowishlist();
    var origin = window.location.origin;
    var home = window.location.origin + "/";
    var product = window.location.origin + "/Product";
    var Wishlist = window.location.origin + "/wishlist";
    var Cart = window.location.origin + "/cart/cart";
    // add cart,WishList html
function addtocart() {

        $.ajax({
            type: 'GET',
            url: `${origin}/Cart/GetCart`,
            success: function (result) {
                var row = ``;

                if (result.cartItemVms != null && result.cartItemVms != '') {
                    var cart = result.cartItemVms;

                } else {
                    $("#cartBody").html('Giỏ hàng trống');
                    $(".total").find(".total-cart").html('');
                    //$(".cart-content").html('Giỏ hàng trống');
                    $(".cart-count").html(`${0}`);
                    return;
                }
                console.log(cart);
                var count = 0;
                var totalPriceSales = function () {
                    let totalPriceSale = 0;
                    for (var i = 0; i < cart.length; i++) {

                        if (cart[i].productVm.sale != "") {
                            totalPriceSale += parseFloat(cart[i].productVm.sale * cart[i].quantity);
                        } else {
                            totalPriceSale += parseFloat(cart[i].productVm.price * cart[i].quantity);
                        }
                    }

                    return totalPriceSale;
                };

                var totalPrices = function () {
                    let totalPrice = 0;

                    for (var i = 0; i < cart.length; i++) {
                        totalPrice += parseFloat(cart[i].productVm.price * cart[i].quantity);

                    }
                    numeral(totalPrice).format('0,000')
                    return totalPrice;
                };
                var Total = 0;
                if (totalPriceSales() > 0) {
                    Total = "$" + numeral(totalPrices()).format('0,000') + "<small>₫</small>" + " - " + "<del style='color: #888'>" + numeral(totalPriceSales()).format('0,000') + " <small>₫</small>" + "</del>"
                } else {
                    Total = "$" + numeral(totalPrices()).format('0,000') + "<small>₫</small>"
                }
                $(".total").find(".total-cart").html(Total);
                for (var i = 0; i < cart.length; i++) {

                    row += `<li class="item-cart-1" data-id=${cart[i].id}>
        <div class="img-left">
            <a href="/chi-tiet/${cart[i].productVm.slug}">
                <img src="/user-content/${cart[i].productVm.thumbnailImageUrl}" style="width: 100%">
            </a>
        </div>
        <div class="meta-right">
            <a href="javascript:void(0)" class="remove-item">
                <i class="fa fa-times" aria-hidden="true"></i>
            </a>
            <a href="/chi-tiet/${cart[i].productVm.slug}" class="title cart-title">${cart[i].productVm.title}</a>
            <p>`
                    for (var j in cart[i].values) {
                        var item2 = cart[i].values[j];
                        if (item2.optionName == "Size") {
                            row += `<p class="cart-size">kích thước : <span>${item2.optionValues}</span></p>`
                        } else {
                            row += `<p class="cart-color">màu sắc : <span>${item2.optionValues}</span></p>`
                        }

                    }
                    if (cart[i].productVm.sale != "" && cart[i].productVm.sale != null) {
                        row += `<span class="price">${numeral(cart[i].productVm.sale).format('0,000')} <small>₫</small></span>
                    - <span class="sale">${numeral(cart[i].productVm.price).format('0,000')} <small>₫</small></span>`
                    } else {
                        row += `<span class="price">${numeral(cart[i].productVm.price).format('0,000')} <small>₫</small></span>`
                    }
                    row += `</p><div>
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
    function addtowishlist() {
        $.ajax({
            type: 'GET',
            url: `${origin}/Cart/GetWishList`,
            success: function (result) {
                var count = 0;
                if (result != null && result != '') {
                    count = result.length;

                } else {
                    $(".wishlist-count").html(`${0}`);
                    //$("#wishlist-icon .fa-heart").css("color","");
                    return;
                }
                $(".wishlist-count").html(`${count}`);
                $("#wishlist-icon .fa-heart").css("color", "#080000 !important");
            }
        });
    };



    // Remove Cart
    $(document).on('click', '.remove-item', function (e) {
        var parent = $(this).parents('.item-cart-1');
        var CartId = parseInt(parent.attr('data-id'));
    $.ajax({
        type: 'POST',
        url: `${origin}/Cart/RemoveCart`,
        data: {
        CartId: CartId,
        },
            success: function (result) {
        addtocart();
        }
     })
    });

    // Quantity Product
    $(document).on('change', '.count', function () {
        var parent = $(this).parents('.item-cart-1');
        var CartId = parseInt(parent.attr('data-id'));
        var prev = parseInt(parent.find('input[name=qty]').data('val'));
        var value = parseInt($(this).val());

    if (value >= 1 && value < 11) {
        $.ajax({
            type: 'POST',
            url: `${origin}/Cart/UpdateCart`,
            data: {
                CartId: CartId,
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
        var CartId = parseInt(parent.attr('data-id'));
        var value = parseInt(parent.find('.count').val());
        value++;
    if (value > 1 && value <= 10) {

        $.ajax({
            type: 'POST',
            url: `${origin}/Cart/UpdateCart`,
            data: {
                CartId: CartId,
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
            var CartId = parseInt(parent.attr('data-id'));
            var value = parseInt(parent.find('.count').val());
        if (value == 1) {
        $.ajax({
            type: 'POST',
            url: `${origin}/RemoveCart/Cart`,
            data: {
                CartId: CartId,
            },
            success: function (result) {
                addtocart();
            }
        });
        } else {
        value--;
        $.ajax({
        type: 'POST',
            url: `${origin}/Cart/UpdateCart`,
            data: {
        CartId: CartId,
                quantity: value,
            },
            success: function (result) {
        addtocart();
            }
        });
    }
    });

    //fadeIn fadeOut Cart
    $(".cart-togget").on("click", function (e) {
        $(".cart-content").toggleClass("fadein");
    });
    $(".close-cart i").on("click", function () {
        $(".cart-content").removeClass("fadein");
    });




    //Remove WishList
    $(document).on('click', '.remove-wishlish', function (e) {
    var parent = $(this).parents('.product-grid6');
    var id = parseInt(parent.attr('data-id'));
    $.ajax({
        type: 'POST',
        url: `${origin}/Cart/RemoveWishList`,
    data: {
        productid: id,
    },
        success: function (result) {
        addtowishlist();
            window.location.href = `${origin}/wishlist`;
    }
    })
});
    $(document).on('click', '.js-show-modal', function (e) {
        var parent = $(this).parents('.product-grid6');
        var id = parseInt(parent.attr('data-id'));
        $.ajax({
        type: 'POST',
            url: `${origin}/Cart/RecentlyViewed`,
            data: {
        productid: id,
            },
            success: function (result) {
        console.log("click vao");
            }
        });
    });



      //add to wishList
    $('.js-add-wishlist').on('click',function(e){
        e.preventDefault();
    var productid = parseInt($(this).data('id'));//ID sản phẩm

    var status = $(this).data('status');
    if (status) {
        $(this).addClass('active_item');
        $(this).data('status', false);
        $(this).attr('data-tip', 'REMOVE WISHLIST');
        animation(this);
        $.ajax({
        type: 'POST',
            url: `${origin}/Cart/AddtoWishList`,
            data: {
        productid: productid,
            },
            success: function (result) {
        addtowishlist();
            }
        });
    } else {
        $(this).removeClass('active_item');
        $(this).data('status', true);
        $(this).attr('data-tip', 'ADD TO WISHLIST');
          $.ajax({
        type: 'POST',
              url: `${origin}/Cart/RemoveWishList`,
            data: {
        productid: productid,
            },
            success: function (result) {
        addtowishlist();
            }
        });

        }
});

    function animation(e) {
             var animation;
             if ($(e).hasClass("js-add-wishlist")) {
        animation = $('#wishlist-icon');
             } else {
        animation = $('#cart');
             }

        var imgtodrag = $(e).parents('.product-grid6').find("img").eq(0);
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
        var Listvalue = [];
        $(".togget-add-cart").on('click', function (e) {
        e.preventDefault();
            var productid = parseInt($(this).data('id')); //ID sản phẩm
            var values = JSON.stringify(Listvalue);
                    if (window.location.href == "https://localhost:5001/cart/cart") {
    } else {
        animation(this);
                    }
                    $(this).addClass('active_item');
                    $.ajax({
        type: 'POST',
                        url: `${origin}/Cart/AddToCart`,
                        data: {
        productid: productid,
                            values: values
                        },
                        success: function (result) {
        addtocart();
                            if (window.location.href == `${origin}/cart/cart`) {
                                window.location.href = `${origin}/cart/cart`;
                                alert("thêm thành công");
                            }
                        }
                    });
        });
       //add to cart
    $('.js-add-cart').on('click', function (e) {
        e.preventDefault();
        var parent = $(this).parents((".product-grid6"));
        var contentoption = parent.find(".add-option-value").text().trim();
        if (contentoption == "") {
            return;
        }
        var Size = $("input[type='radio']:checked").val();
        if (Size == undefined) {
        alert("vui lòng chọn kích thước");
            return;
        }
        var productid = parseInt($(this).data('id')); //ID sản phẩm
        parent.find(".product-add-option").removeClass("active-option");
        if (window.location.href == `${origin}/cart/cart`) {
    } else {
        animation(this);
            }
        $(this).addClass('active_item');
        $(this).data('status', false);
        $(this).attr('data-tip', 'Remove Cart');
        $.ajax({
        type: 'POST',
            url: `${origin}/Cart/AddToCart`,
            data: {
        productid: productid,
                Size: Size,
            },
            success: function (result) {
        addtocart();
                if (window.location.href == `${origin}/cart/cart`) {
                    window.location.href = `${origin}/cart/cart`;
                    alert("thêm thành công");
                }
            }
        });

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

        /*==================================================================[ Show modal1 ]*/
        $('.js-show-modal1').on('click', function (e) {
        e.preventDefault();
            $('.js-modal1').addClass('show-modal1');
        });
        /*==================================================================[ Hidden modal1 ]*/
        $('.js-hide-modal1').on('click', function () {
        $('.js-modal1').removeClass('show-modal1');
        });

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
        $('input[name="query"]').on('keypress', function (e) {
            if (e.which == 13) {
                var value = $(this).val();
                if (value == "") {
        e.preventDefault();
                }
            }
        });
        $(".trans-04").on("click", function (e) {
            var value = $('input[name="query"]').val();
            if (value == "") {
        e.preventDefault();
            }
        });

        function loadevent(result) {
            var ListChecked = [];
            var optionvalue = result.productOptionVm;
            for (var i in optionvalue) {
                var item = optionvalue[i];
                var input = $(`input[name='inlineRadioOptions-${item.id}']`);

                input.each(function (i,index) {
                    if (i == 0) {
                        var id = parseInt($(this).next().attr("data-id"));
                        var value = $(this).val();
                        $(`.form-check-label[data-id=${id}]`).removeClass('optionactive')
                        $(this).prop('checked', $(input).is(':checked')).next().addClass("optionactive");
                        $(this).parents(".option").find('span').text(value);
                        let item = {
        optionId: id, optionName : null, optionValues: value };
                        ListChecked.push(item);
                    }
                })
            }
            for (let i in optionvalue) {
                var item = optionvalue[i];
                $(`input[name='inlineRadioOptions-${item.id}']`).change(function () {
                    var checked = $(this).val();

                    var id = parseInt($(this).next().attr("data-id"));
                    let item = {optionId: id, optionName: null, optionValues: checked };
                    var yet = ListChecked.findIndex(x => x.optionId == id);
                    if (yet != -1) {
        ListChecked.splice(yet, 1);
                    }
                    if ($(this).is(':checked')) {
        ListChecked.push(item);
                    }
                    console.log(ListChecked);
                });
                $(`input[name="inlineRadioOptions-${item.id}"]`).on('click', function () {
                    var id = parseInt($(this).next().attr("data-id"));
                    var value = $(this).val();
                    $(`.form-check-label[data-id=${id}]`).removeClass('optionactive')
                    $(this).prop('checked', $(this).is(':checked')).next().addClass("optionactive");
                    $(this).parents(".option").find('span').text(value);

                });


            }
              $(document).on('click', '.modal-add-cart', function (e) {
                  var parent = $(this).parents('.product-details-item');
                  var productid = parseInt(parent.attr('data-id'));
                  var quantity = parseInt(parent.find(".num-product").val());
                  var sortArr = ListChecked.sort(function (a, b) {
                      return a.optionId - b.optionId;
                  });
                  var values = JSON.stringify(sortArr)
                    $.ajax({
                         type: 'POST',
                         url: `${origin}/Cart/AddToCart`,
                         data: {
                         productid: productid,
                         quantity: quantity,
                         values: values

                        },
                        success: function (result) {
                            window.location.href = `${origin}/cart/cart`;
                        }
                    })
            });
        }


        function LoadModal(result) {
            var html = "";
            var item = result;
            console.log(item);
                html += `<div class="col-md-12">
        <div class="col-md-6 single-left gallery-lb">`
            for (var i = 0; i < item.product.images.length; i++){
                    if (i == 0)
                        {
                html += ` <div class="mySlides" style="display: block;">
                                <div class="numbertext">${i + 1} / ${item.product.images.length}  </div>
                                <a class="zoom " href="/user-content/${item.product.images[i].media.fileName}" tabindex="0">
                                    <i class="fal fa-expand"></i>
                                </a>
                                <img src="/user-content/${item.product.images[i].media.fileName}" style="width:100%">
                            </div>`
            }
                        else
                        {
                html += ` <div class="mySlides" style="display: none;">
                                <div class="numbertext">${i + 1} / ${item.product.images.length}  </div>
                                <a class="zoom " href="/user-content/${item.product.images[i].media.fileName}" tabindex="0">
                                    <i class="fal fa-expand"></i>
                                </a>
                                <img src="/user-content/${item.product.images[i].media.fileName}" style="width:100%">
                                </div>`
            }
                    }
                        html +=  `<button class="img-prev" onclick="plusSlides(-1,this)"><i class="fal fa-angle-left" aria-hidden="true"></i></button>
            <button class="img-next" onclick="plusSlides(1,this)"><i class="fal fa-angle-right" aria-hidden="true"></i></button>

            <div class="image-slide">`
            for (var i = 0; i < item.product.images.length; i++)
                    {
                    html += `<div class="column">
                            <img class="demo cursor" src="/user-content/${item.product.images[i].media.fileName}" style="width:100%" onclick="currentSlide(${i + 1},this)" alt="${item.product.title}">
                        </div>`
                }
               html+= ` </div>
        </div><!--Hình ảnh-->
                    <div class="col-md-6 single-right pd-xs-0 product-details">
            <div class=" product-details-item" data-id="${item.product.id}">
                <h1 class="single-title">${item.product.title}</h1>
                <p >`
                if (item.product.sale > 0)
                {
                        html += `<span class="price">${numeral(item.product.sale).format('0,000,00')}<small>₫</small> - </span>
                                <del class="sale">${numeral(item.product.price).format('0,000,00')} <small>₫</small></del> `
                    }
                        else
                        {
                        html += ` <span>
                                ${item.product.price.GetValueOrDefault(0).ToString("#,#.##")}<small>₫</small>
                            </span>`
                    }
                     html += ` </p>`
                    if (item.product.rating.length > 0)
                            {
                    html += `<div class="product-rating">
                                    <span class="rating star-rating">`
                             for (var i = 0; i < item.product.rating.length;i++) {
                    html += `${item.product.rating[i].scores}`
                }

                                 html +=  `</span>

            <a href="#reviews" class="review-link" rel="nofollow">(<span class="count">${item.product.rating.length}</span> nhận xét)</a>
        </div>
        <div itemprop="description" class="short-description">
            <p>${item.product.description} </p>
        </div>`
                            }

                 for (var i in item.productOptionVm) {
                            var item2 = item.productOptionVm[i];

                                if (item2.name == "Size")
                                {
            html += `<div class="option">
                                        <div>
                                            Kích thước:
                                            <span></span>
                                        </div>
                                        <ul>`
                                    for (var i in item2.values) {
                                        var item3 = item2.values[i]
                                            html += ` <li>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" name="inlineRadioOptions-${item2.id}" id="inlineRadio-${item3.key}" value="${item3.key}">
                    <label class="form-check-label" for="inlineRadio-${item3.key}" data-id="${item2.id}">${item3.display}</label>
                                                    </div>
                                                </li>`
                                            }

                                    html += ` </ul>
    </div>`
                                }
                                else
                                {

        html += ` <div class="option">
                                        <div>
                                            Màu Sắc:
                                            <span></span>
                                        </div>
                                        <ul>`
                                    for (var i in item2.values) {

                                                        var item3 = item2.values[i]
                                                    html += ` <li>
        <div class="form-check form-check-inline">
            <input class="form-check-input" type="radio" name="inlineRadioOptions-${item2.id}" id="inlineRadio-${item3.key}" value="${item3.key}">
                <label class="form-check-label" for="inlineRadio-${item3.key}" style="background-color:#${item3.display};border-radius: 50%;" data-id="${item2.id}"></label>
                                                    </div>
                                                     </li>`
                                                }
                                    html +=`</ul></div>`
                                }
                 }
                     // so luong
                   html +=`<div class="quantity" >
                            <div class="wrap-num-product">
                                <div class="btn-num-product-down">
                                    <i class="fal fa-minus" aria-hidden="true"></i>
                                </div>
                                <input class="txt-center num-product" type="number" name="num-product" value="1" data-val="1">

                                <div class="btn-num-product-up">
                                    <i class="fal fa-plus" aria-hidden="true"></i>
                                </div>
                            </div>
                        </div> <div class="clearfix"></div>`
                            //update giỏ hàng
            html +=` <div class="modal-add-cart" data-id="${item.product.id}">
    Thêm vào giỏ hàng
        <span> <i class="fal fa-shopping-basket" aria-hidden="true"></i></span>
                            </div>
                            <div class="clearfix"></div>
                            <div class="add-wishlist" data-status="true">
                                <a href="javascript:void(0)"> <span><i class="fal fa-heart" aria-hidden="true"></i></span>Thêm danh sách yêu thích</a>
                            </div>


                            <div class="product-meta">`
    if (item.product.brand != null)
                            {
                                html +=` <span class="product_brand">
                                        Thương hiệu:
                                        <a href="#">${item.product.brand.name}</a>
                                    </span>`
                            }
                                html +=` <span class="product_sku">
            Xuất xứ:
                                            <a href="#">N/A</a>
                                        </span>`
                            if (item.product.category != null)
                            {
                                html +=`<span class="product_category">
                                    Danh mục:
                                    <a href="#">${item.product.category.title}</a>
                                </span>`
                            }

                            html +=`    <span class="product_share">
            Chia sẻ:
                                <a href="#" class="share-facebook" style="color: #025aa5 !important"><i class="fa fa-facebook-official" aria-hidden="true"></i></a>
                            </span>
                        </div>
                        <div class="clearfix"></div>
                    </div >
                        </div >
                    </div >
                    </div >`;
            $("#modal-item").html(html);
            console.log(html);
            console.log($("#modal-item"));
            Rating();
            quantity();
            WishList();
            if (window.location.href == (origin + "/wishlist")) {
                noneWishList()
            }
            
        }
      $(document).on('click', '.js-show-modal', function (e) {
          var productId = $(this).data("id");
            $.ajax({
                type: 'POST',
                url: `${origin}/Product/Detail`,
                data: {
                    Id: productId,
                },
                success: function (result) {
                    LoadModal(result);
                    loadevent(result);
                }
            })
      });
    
        function WishList() {
            $(document).off('click', '.add-wishlist').on('click', '.add-wishlist', function (event) {
        event.preventDefault();
        let parent = $(this).parents('.product-details');
        let productid = parseInt(parent.find('.product-details-item').attr('data-id'));
              var status = $(this).data("status");
      
        if (status == true) {
            $(this).find("i").css('font-weight', '900');
            $(this).data('status', false);
            $(this).attr('data-tip', 'Remove TO WISHLIST');
 
            if (window.location.href == home || window.location.href == Wishlist || window.location.href == Cart || window.location.href == product) {
                console.log("vao day nay");
            } else {
                animation2(this);
            }
              
           
            $.ajax({
                type: 'POST',
                url: `${origin}/Cart/AddtoWishList`,
                data: {
                    productid: productid,
                },
                success: function (result) {
                    addtowishlist();
                }
            });
        } else {
            $(this).find("i").css('font-weight','300');
            $(this).data('status', true);
        $(this).attr('data-tip', 'ADD TO WISHLIST');
          $.ajax({
            type: 'POST',
              url: `${origin}/Cart/RemoveWishList`,
            data: {
                productid: productid,
            },
            success: function (result) {
                addtowishlist();
            }
        });
        }

        });
}
WishList();
    function noneWishList() {
        $(".add-wishlist").css("display", "none");
    }
