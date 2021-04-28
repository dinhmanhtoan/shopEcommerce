   
            //(function ($) {
            //    $.fn.inputFilter = function (inputFilter) {
            //        return this.on("input keydown keyup mousedown mouseup select contextmenu drop", function () {
            //            debugger;
            //            if (inputFilter(this.value)) {
            //                this.oldValue = this.value;
            //                this.oldSelectionStart = this.selectionStart;
            //                this.oldSelectionEnd = this.selectionEnd;
            //            } else if (this.hasOwnProperty("oldValue")) {
            //                this.value = this.oldValue;
            //                this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
            //            } else {
            //                this.value = "";
            //            }
            //        });
            //    };
            //}(jQuery));
            //$(document).ready(function () {
            //    $(".count").inputFilter(function (value) {
            //        return "^[0-9]*$".test(value);    // Allow digits only, using a RegExp
            //    });
            //});
            //function Updatetotal(totalPrices = 0, totalPriceSales = 0) {
            //    $(".total").find(".total-cart").html("$" + totalPrices + "-" + totalPriceSales);
            //}
            //$(document).ready(function () {
            //    $('.count').prop('disabled', true);
            //    $(document).on('click', '.plus', function () {
            //        var parent = $(this).parents('.item-cart-1');
            //        var id = parseInt(parent.attr('data-id'));
            //        //var totalPriceSale = getTotalSaleCost();
            //        //var totalPrice = getTotalCost();

            //        for (var i in cart) {
            //            var item = cart[i];
            //            if (item.Id == id) {
            //                item.Qty = item.Qty + 1;
            //                Updatetotal(totalPrices(), totalPriceSales());
            //            }
            //        }
            //        saveCart();
            //        Count();
            //        showCart();
            //    });
            //    $(document).on('change', '.count', function ()
            //    {
            //        var parent = $(this).parents('.item-cart-1');
            //        var id = parseInt(parent.attr('data-id'));
            //        var value = parseInt($(this).val());

            //        if (value == null || value == '') {
            //            (this).val()
            //        } else {
            //            for (var i in cart) {
            //                var item = cart[i];
            //                if (item.Id == id) {
            //                    item.Qty = value;
            //                    Updatetotal(totalPrices(), totalPriceSales());
            //                }
            //            }
            //            saveCart();
            //            Count();
            //            showCart();
            //        }

            //    })
            //    $(document).on('click', '.minus', function () {

            //        var parent = $(this).parents('.item-cart-1');
            //        var id = parseInt(parent.attr('data-id'));

            //        if ($('.count').val() <= 1) {
            //            remove(id);
            //            parent.fadeOut().empty();
            //            //var totalPriceSale = getTotalSaleCost();
            //            //var totalPrice = getTotalCost();
            //            /*  Updatetotal(totalPrices(), totalPriceSales());*/
            //            var dataList = $(".product-grid6").map(function () {
            //                return $(this).data("id");
            //            })
            //            for (var i = 0; i < dataList.length; i++) {
            //                if (dataList[i] == id) {
            //                    $(`.product-grid6[data-id=${dataList[i]}]`).find('.js-add-cart').removeClass('active-card');
            //                }

            //            }
            //            saveCart();
            //            Count();
            //            showCart();
            //        } else {


            //            for (var i in cart) {
            //                var item = cart[i];
            //                if (item.Id == id) {
            //                    item.Qty = item.Qty - 1;
            //                }
            //            }
            //            //var totalPriceSale = getTotalSaleCost();
            //            //var totalPrice = getTotalCost();
            //            /*     Updatetotal(totalPrices(), totalPriceSales());*/
            //            saveCart();
            //            Count();
            //            showCart();
            //        }
            //    });
            //});
            //var cart = [];



            //$(function () {
            //    if (localStorage.cart) {
            //        // load cart data from local storage
            //        cart = JSON.parse(localStorage.cart);

            //        Count();
            //        showCart();  // display cart that is loaded into cart array
            //    }
            //});

            //var addcart = function () {
            //    $(this).toggleClass('active-card');
            //    var parent = $(this).parents('.product-item');
            //    var id = parseInt(parent.find('.product-grid6').attr('data-id'));
            //    var img = parent.find('img').attr('src');
            //    var title = parent.find('.title').text();
            //    var price = parent.find('.price').text().slice(1);
            //    var sale = parent.find('.sale').text().slice(1);
            //    var item = { Id: id, Title: title, Img: img, Price: price, Sale: sale, Qty: 1 };
            //    var i = cart.findIndex(cart => cart.Id === id);

            //    if (i === -1) {
            //        cart.push(item);
            //    } else {
            //        remove(id);
            //    }

            //    saveCart();
            //    showCart();
            //    Count();
            //};
            //$(document).off('click', '.js-add-cart').on('click', '.js-add-cart', addcart);


            //// getTotalSaleCost
            //var totalPriceSales = function () {
            //    let totalPriceSale = 0;
            //    for (var i = 0; i < cart.length; i++) {
            //        if (cart[i].Sale != "") {
            //            totalPriceSale += parseFloat(cart[i].Sale * cart[i].Qty);
            //        } else {
            //            totalPriceSale += parseFloat(cart[i].Price * cart[i].Qty);
            //        }
            //    }
            //    return totalPriceSale;
            //}
            ////getTotalCost
            //var totalPrices = function () {
            //    let totalPrice = 0;
            //    for (var i = 0; i < cart.length; i++) {
            //        totalPrice += parseFloat(cart[i].Price * cart[i].Qty);
            //    }

            //    return totalPrice;
            //};
            //$(document).on('click', '.remove-item', function (e) {
            //    var parent = $(this).parents('.item-cart-1');
            //    var id = parseInt(parent.attr('data-id'));

            //    var dataList = $(".product-grid6").map(function () {
            //        return $(this).data("id");
            //    })
            //    for (var i = 0; i < dataList.length; i++) {
            //        if (dataList[i] == id) {
            //            $(`.product-grid6[data-id=${dataList[i]}]`).find('.js-add-cart').removeClass('active-card');
            //        }

            //    }
            //    remove(id);
            //    parent.empty();
            //    Count();
            //});
            //function remove(id) {
            //    var i = cart.findIndex(cart => cart.Id === id);

            //    if (i !== -1) {
            //        cart.splice(i, 1);
            //        localStorage.setItem('cart', JSON.stringify(cart));
            //        showCart();
            //    }

            //}
            //function Count() {
            //    var count = 0;
            //    for (var i = 0; i < cart.length; i++) {
            //        count += cart[i].Qty
            //    }
            //    $('.cart-count').html(count);
            //}
            //function saveCart() {
            //    if (window.localStorage) {

            //        localStorage.cart = JSON.stringify(cart);
            //    }
            //}
            //function showCart() {
            //    //if (cart.length == 0) {
            //    //    $("#cart-").css("visibility", "hidden"); // hide table that shows cart
            //    //    return;
            //    //}
            //    $("#cart").css("visibility", "visible");
            //    $("#cartBody").empty();  // empty tbody of table
            //    for (var i in cart) {
            //        var item = cart[i];

            //        var row = `<li class="item-cart-1" data-id=${item.Id}>
            //                    <div class="img-left">
            //                        <a href="../html/single.html">
            //                            <img src="${item.Img}" style="width: 100%">
            //                        </a>
            //                    </div>
            //                    <div class="meta-right">
            //                        <a href="javascript:void(0)" class="remove-item">
            //                            <i class="fa fa-times" aria-hidden="true"></i>
            //                        </a>
            //                             <a href="../html/single.html" class="title cart-title">${item.Title}</a>
            //                        <p>
            //                        <span class="price">${item.Price}</span>`
            //        if (item.Sale != "") {
            //            row += `-<span class="sale">$${item.Sale}</span> </p> `
            //        } else {
            //            row += `<span class="sale">${item.Sale}</span> </p>`
            //        }
            //        row += `        <div>
            //                         <p class="quantity">
            //                            <span class="name">Số lượng: </span>
            //                            <span>
            //                                <div class="qty mt-1">
            //                                    <span class="minus bg-dark">-</span>
            //                                    <input type="number" class="count" name="qty" value="${item.Qty}" min="1" max="10">
            //                                    <span class="plus bg-dark">+</span>
            //                                 </div>
            //                            </span>
            //                        </p>
            //                        </div>

            //                    </div>
            //                        </li>`;
            //        $("#cartBody").append(row);
            //    }
            //    Updatetotal(totalPrices(), totalPriceSales());
            //}
