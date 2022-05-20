
$(function () {
    var origin = window.location.origin;
    var pathname = window.location.pathname;
    var local = origin + pathname;

    $("input[name='category']").on('change', function () {
        getProduct();
    });
    $(document).on('click', 'input[name="sort"]', function () {
        $('input[name="sort"]').not(this).prop('checked', false);
        getProduct();
    });
    $("#themes").on('change', function () {
        getProduct();
    });
    $("input[name='brand']").on('change', function () {
        getProduct();
    });
    $(".cloud-price button").off("click").on("click", function () {
        getProduct();
    })
    function getProduct() {
        var url_string = window.location.href;
        var url = new URL(url_string);

        var search = $("input[name='query']").val();
        var brand = $(".cloud-item[name='brand']:checked").map(function () {
            return this.value;
        }).get().join('--');
        var category = $(".cloud-item[name='category']:checked").map(function () {
            return this.value;
        }).get().join('--');
        var sort = $(".cloud-item[name='sort']:checked").val();
        var pageSize = $("#themes option:selected").val();
    
        var page = url.searchParams.get("page");
        var priceStart = $(".cloud-price input[name='pricestart']").val();
        var priceEnd = $(".cloud-price input[name='priceend']").val();
        var pageSize = $("#themes option:selected").val();
        var query = "";
        if (priceStart != "" && priceEnd != "") {
            query += (query.length == 0 ? "?" : "&") + "minPrice=" + priceStart + "&maxPrice=" + priceEnd;
        }
        if (category != "") {
            query += (query.length == 0 ? "?" : "&") + "category=" + category;
        }
        if (search != null && search != "") {

            query += (query.length == 0 ? "?" : "&") + "query=" + search;
        }
        if (brand != "") {
            query += (query.length == 0 ? "?" : "&") + "brand=" + brand;
        }
        if (sort != "" && sort != -1 && sort != undefined) {
            query += (query.length == 0 ? "?" : "&") + "sort=" + sort;
        }
        if (pageSize != null && pageSize != -1) {
            query += (query.length == 0 ? "?" : "&") + "pageSize=" + pageSize;
        }
        if (page != null && page != -1) {
            query += (query.length == 0 ? "?" : "&") + "page=" + page;
        }
        window.location = origin + pathname + query;
    }
    function init() {
        var url_string = window.location.href;
        var url = new URL(url_string);
        var search = url.searchParams.get("query");
        var category = url.searchParams.get("category");
        var sort = url.searchParams.get("sort");
        var brand = url.searchParams.get("brand");
        var priceStart = url.searchParams.get("minPrice");
        var priceEnd = url.searchParams.get("maxPrice");
        var pageSize = url.searchParams.get("pageSize");
        if (priceStart != null && priceEnd != null) {
            $("input[name='pricestart']").val(priceStart);
            $("input[name='priceend']").val(priceEnd);
            let tagitem = `<li class="tag-item"><span>${priceStart}đ - ${priceEnd}đ</span><i class="fas fa-times"></i></li>`
            $(".tags ul").append(tagitem);
        }
        if (sort != undefined) {
            var options = $("input[name='sort']");
            $.each(options, function (i, item) {
                let value = $(item).val();
                if (value == sort) {
                    $(item).prop("checked", true);
                    let checked = `<div class="cloud-item-checked"><span>✓</span></div>`
                    $(item).parents(".cloud-items").append(checked);
                    $(item).parent().css("border", "1px solid rgb(20, 53, 195)");
                }
            });

        }
        if (pageSize != undefined) {
            var options = $("#themes option");
            $.each(options, function (i, option) {
                let value = $(option).val();
                if (value == pageSize) {
                    $(option).prop("selected", true);
                }
            })

        }
        if (category != undefined && category != null) {
            var categories = $("input[name='category']");

            var Categoryarr = category.split('--');
            $.each(categories, function (i, item) {
                let value = $(item).val();
                $.each(Categoryarr, function (i, item2) {
                    if (value == item2) {
                        $(item).prop('checked', true);
                        let name = $(item).prev().text();
                        let checked = `<div class="cloud-item-checked"><span>✓</span></div>`
                        let tagitem = `<li class="tag-item"><span>${name}</span><i class="fas fa-times"></i></li>`
                        $(item).parents(".cloud-items").append(checked);
                        $(".tags ul").append(tagitem);
                        $(item).parent().css("border", "1px solid rgb(20, 53, 195)");
                    }
                });
            })
        }
        if (brand != undefined && brand != null) {
            var brands = $("input[name='brand']");
            var brandarr = brand.split('--');
            $.each(brands, function (i, item) {
                let value = $(item).val();
                $.each(brandarr, function (i, item2) {
                    if (value == item2) {
                        $(item).prop('checked', true);
                        let name = $(item).prev().text();
                        let checked = `<div class="cloud-item-checked"><span>✓</span></div>`
                        let tagitem = `<li class="tag-item"><span>${name}</span><i class="fas fa-times"></i></li>`
                        $(item).parents(".cloud-items").append(checked);
                        $(".tags ul").append(tagitem);
                        $(item).parent().css("border", "1px solid rgb(20, 53, 195)");
                    }
                });
            })
        }
        var cloudTag = $(".tags ul li").length;
        if (cloudTag != 0) {
            var query;
            if (search != undefined) {
                query = local + `?query=${search}`;
            } else {
                query = local;
            }
            var cloudClear = `<div class="cloud-clear">
                        <a target="_self" href="${query}">
                            <span>Xóa bộ lọc</span>
                        </a>
                    </div>`
            $(".tags").parent().append(cloudClear);
        }
        $("input[name='query']").val(search);
    }
    init();

    $(".tag-item i").off('click').on('click', function () {
        let item = $(this).prev().text().trim();
        let checkeds = $('.cloud-item:checkbox:checked');
        $.each(checkeds, function (i, item2) {
            let value2 = $(item2).prev().text().trim();
            if (item == value2) {
                $(item2).prop('checked', false);
                getProduct();
            }
        });
        if (item.includes('đ')) {
            $("input[name='pricestart']").val(null);
            $("input[name='priceend']").val(null);
            getProduct();
        }
    });
    //$(".tag-item i").off('click').on('click', function () {
    //    $("input[name='pricestart']").val(null);
    //    $("input[name='priceend']").val(null);
    //    getProduct();
    //});

    var tagsDrop = function tagsDrop() {
        $(".tags-drop").on("click", function () {
            var tagsList = $(this).prev(".tags-list");
            tagsList.toggleClass("h-auto");
            $(this).find("span").toggleClass("rotate-180");
        });
    }

    $(".tags-items").each(function (index, item) {
        var tagsList = $(item).find(".tags-list");
        var items = tagsList.find("li");
        var itemsWidth = 0;
        items.each(function (index2, item2) {
            itemsWidth += $(item2).width()
        })
        var tagsListWidth = tagsList.width();

        if (tagsListWidth < itemsWidth) {
            var html = `<div class="tags-drop">
                            <span>
                                <i class="fas fa-chevron-down"></i>
                            </span>
                          </div>`
            $(item).append(html);
            tagsDrop();
        }
    })
    $(window).resize(function () {
        $(".tags-items").each(function (index, item) {

            var tagsList = $(item).find(".tags-list");
            var items = tagsList.find("li");
            var itemsWidth = 0;
            items.each(function (index2, item2) {
                itemsWidth += $(item2).width()
            })
            var tagsListWidth = tagsList.width();
            if (tagsListWidth < itemsWidth) {
                if ($(item).find(".tags-drop").length == 0) {
                    var html = `<div class="tags-drop">
                                <span>
                                    <i class="fas fa-chevron-down"></i>
                                </span>
                              </div>`
                    $(item).append(html);
                    tagsDrop();
                }
            } else {
                if ($(item).find(".tags-drop").length > 0) {
                    $(item).find(".tags-drop").remove();
                }
            }

        });
    });

});