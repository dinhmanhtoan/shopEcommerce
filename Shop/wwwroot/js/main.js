
var data_product =[{'id':1,'name': 'Lightweight Jacket','img':'../img/2_1-7-555x739.jpg','price':'$620.000↵<span>$720.000</span>','brand':'Zara'}
,{'id':2,'name': 'Lightweight Jacket 02','img':'../img/4_1-1-555x739.jpg','price':'$620.000↵<span>$720.000</span>','brand':'Puma'}
,{'id':3,'name': 'Lightweight Jacket 03','img':'../img/3_1-9-555x739.jpg','price':'$620.000↵<span>$720.000</span>','brand':'Zara'}
,{'id':4,'name': 'Karpell','img':'../img/1.jpg','price':'$620.000↵<span>$720.000</span>','brand':'Zara'}
,{'id':5,'name': 'Bae Top','img':'../img/2_2-2-555x739.jpg','price':'$620.000↵<span>$720.000</span>','brand':'Adidas'}
,{'id':6,'name': 'Flounce','img':'../img/1_4-1-555x739.jpg','price':'$62	0.000↵<span>$720.000</span>','brand':'Zara'}
,{'id':7,'name': 'Originals California','img':'../img/3_1-9-555x739.jpg','price':'$620.000↵<span>$720.000</span>','brand':'Zara'}
,{'id':8,'name': 'New Sweatshirt','img':'../img/1_1-6-555x739.jpg','price':'$620.000↵<span>$720.000</span>','brand':'Zara'}
,{'id':9,'name': 'Levis Neck Ringer','img':'../img/2_2-4-555x739.jpg','price':'$620.000↵<span>$720.000</span>','brand':'Nike'}
,{'id':10,'name': 'Print shirt','img':'../img/3_4-5-555x739.jpg','price':'$620.000↵<span>$720.000</span>','brand':'Zara'}
,{'id':11,'name': 'The Trend Polo','img':'../img/2_4-2-555x739.jpg','price':'$620.000↵<span>$720.000</span>','brand':'Zara'}
,{'id':12,'name': 'American Vintage','img':'../img/1_4-4.jpg','price':'$620.000↵<span>$720.000</span>','brand':'Zara'}];

var data_wishlist =[{'id':1,'name': 'Lightweight Jacket','img':'../img/product-detail-01.jpg','price':'$620.000↵<span>$720.000</span>'}
,{'id':2,'name': 'Lightweight Jacket 02','img':'../img/product-detail-02.jpg','price':'$620.000↵<span>$720.000</span>'}
,{'id':3,'name': 'Lightweight Jacket 03','img':'http://bestjquery.com/tutorial/product-grid/demo10/images/img-1.jpg','price':'$620.000↵<span>$720.000</span>'}];

var data_cart =  [{'id':1,'name': 'Lightweight Jacket','img':'../img/product-detail-01.jpg','price':'$620.000↵<span>$720.000</span>','quantity':1}
,{'id':2,'name': 'Lightweight Jacket 02','img':'../img/product-detail-02.jpg','price':'$620.000↵<span>$720.000</span>','quantity':1}
,{'id':3,'name': 'Lightweight Jacket 03','img':'http://bestjquery.com/tutorial/product-grid/demo10/images/img-1.jpg','price':'$620.000↵<span>$720.000</span>','quantity':1}];
var data_product_by_brand = [];
$( document ).ready(function() {



	/*==================================================================[ Show modal Search ]*/
	$('.js-show-modal-search').on('click', function(e){
		e.preventDefault();
		$('.modal-search-header').addClass('show-modal-search');
		$(this).css('opacity','0');
	});
	/*==================================================================[ Hidden modal Search ]*/
	$('.js-hide-modal-search').on('click', function(){
		$('.modal-search-header').removeClass('show-modal-search');
		$('.js-show-modal-search').css('opacity','1');
	});

	$('.container-search-header').on('click', function(e){
		e.stopPropagation();
	});

	/*==================================================================[ Show modal1 ]*/
	$('.js-show-modal1').on('click',function(e){
		e.preventDefault();
		$('.js-modal1').addClass('show-modal1');
	});
	/*==================================================================[ Hidden modal1 ]*/
	$('.js-hide-modal1').on('click',function(){
		$('.js-modal1').removeClass('show-modal1');
	});
	
	/*==================================================================[ Show menu mobile ]*/
	$('.open-close').on('click',function(e){
		e.preventDefault();
		$('#header-mid').addClass('show-menu');
		$('.overlay-modal2').addClass('js-show-modal2');
	});

	/*==================================================================[ Hidden menu mobile ]*/
	$('.close-open').on('click',function(e){
		e.preventDefault();
		$('#header-mid').removeClass('show-menu');
		$('.overlay-modal2').removeClass('js-show-modal2');
		
	});
	/*==================================================================[ Hidden menu mobile ]*/
	$('.overlay-modal2').on('click',function(){
		$('#header-mid').removeClass('show-menu');
		$('.overlay-modal2').removeClass('js-show-modal2');
		
	});
	/*/////////////////////////////////////////////////////////*/

	/*fixed menu on scroll */
	
/*	window.onscroll = function() {fixed_header_pc()};
	
	var header = document.getElementById("header-mid");
	var sticky = header.offsetTop;
	var a = window.pageYOffset;
	function fixed_header_pc() {
		if (window.pageYOffset > sticky) {
			$(header).addClass("sticky");
			
		} else {
			$(header).removeClass("sticky");
		}
	}*/
	/*fixed menu mobile on scroll */
	$('.js-choose-brands').on('click',function(e){
		e.preventDefault();
		var status = $(this).data('status');
		var text = $(this).text();
		if(status == 0){
			// Chưa chọn
			
			$(this).data('status','1');
			$(this).attr('data-status','1');
			$(this).find('.brands_icon').html('<i class="fa fa-check-square-o" aria-hidden="true"></i>');
			var brands = $("#brands_input").val();

			var data = [];
			if(brands.length > 0)
			{
				//Chon lan dau
				console.log(text);
				console.log('lan sau');
				$("#brands_input").val(brands+ ","+text);
				data = data_product.filter(x=>x.brand == text.replace(' ',''));
				
				data_product_by_brand = data_product_by_brand.concat(data);
				console.log(data_product_by_brand);
				main.loaddata_product_all('#list-product-all',data_product_by_brand,36); // load data sản phẩm
			}else{
				console.log('lan dau');
				$("#brands_input").val(text);
				console.log(text);
				data = data_product.filter(x=>x.brand == text.replace(' ',''));
				data_product_by_brand = data;
				console.log(data_product_by_brand);
				main.loaddata_product_all('#list-product-all',data_product_by_brand,36); // load data sản phẩm
			}
		/*	brands = $("#brands_input").val();
			
			var arr = brands.split(',');
			$.each(arr, function (i, item) {
				if(item.length > 0)
				{
					console.log(item);
					var data1 = data_product.filter(x=>x.brand == ""+item+"")
					data.concat(data1);
				}

			})*/
			
		}else{
			
			// Đã chọn
			console.log('da chon');
			$(this).data('status','0');
			$(this).attr('data-status','0');
			$(this).find('.brands_icon').html('<i class="fa fa-square-o" aria-hidden="true"></i>');
			var brands = $("#brands_input").val();

			console.log(text);
			var data = data_product_by_brand.filter(x=>x.brand == text.replace(' ',''));
			$.each(data, function (i, item) {
				var k = data_product_by_brand.findIndex(x=>x.id == item.id);
				data_product_by_brand.splice(k,1);
			});
			console.log(data_product_by_brand);
			main.loaddata_product_all('#list-product-all',data_product_by_brand,36); // load data sản phẩm

			// if(brands.search(text) == 0)// chuỗi đứng vị trí đầu tiên
			// {
			// 	brands = brands.replace(text,'');
			// }else{
			// 	brands = brands.replace(','+text,'');
			// }
			// $("#brands_input").val(brands);
			// // var data = data_product.filter(x=>x.brand == brands_input)
			// var data = [];
			// var arr = brands.split(',');
			// $.each(arr, function (i, item) {
			// 	if(item.length > 0)
			// 	{
			// 		console.log(item);
			// 		var data1 = data_product.filter(x=>x.brand == item.substr(0,item.length - 1))
			// 		data.concat(data1);
			// 	}

			// })
			
		}


	});


	/*/////////////////////////////////////////////////////////*/
	/*Add wishlist*/
	$('.js-add-wishlist').on('click',function(e){
		e.preventDefault();

		var id = $(this).data('id');//id sản phẩm
		// lấy dữ liệu sản phẩm
		var product = main.getdata_product_wishlist(id);
		// lấy html sản phẩm yêu thích
		var html = main.html_addwishlist(product.id,product.img,product.name,product.price);
		// Thêm sản phẩm yêu thích vào danh sách yêu thích ở headder
		if(data_wishlist.find(x=>x.id== id) == undefined)
		{
			$('.wishlist-content').find('.scroll-js').append(html);	
		}else{
			main.remove_item_wishlist(id);
		}
		

		var status = $(this).data('status');
		
		if(status)
		{
			// Thêm vào danh sách yêu thích
			$(this).addClass('active_item');
			$(this).data('status',false);
			$(this).attr('data-tip','Remove Wishlist');
			main.number_wishlist('ADD');
		}else{
			// Xóa khỏi danh sách yêu thích
			$(this).removeClass('active_item');
			$(this).data('status',true);
			$(this).attr('data-tip','Add to Wishlist');
			main.number_wishlist('REMOVE');
			main.remove_item_wishlist(id);
		}
	});

	$('.js-add-cart').on('click',function(e){
		e.preventDefault();

		var id = $(this).data('id');//id sản phẩm
		// lấy dữ liệu sản phẩm
		var product = main.getdata_product_cart(id,1);
		// lấy html sản phẩm yêu thích
		var html = main.html_addcart(product.id,product.img,product.name,product.price,product.quantity);
		// Thêm sản phẩm yêu thích vào danh sách yêu thích ở headder
		if(data_cart.find(x=>x.id== id) == undefined)
		{
			$('.cart-content').find('.scroll-js').append(html);
		}else{
			main.remove_item_cart(id);
		}
		
		var status = $(this).data('status');
		if(status)
		{
			// Thêm vào danh sách yêu thích
			$(this).addClass('active_item');
			$(this).data('status',false);
			$(this).attr('data-tip','Remove Wishlist');
			main.number_cart('ADD');
		}else{
			// Xóa khỏi danh sách yêu thích
			$(this).removeClass('active_item');
			$(this).data('status',true);
			$(this).attr('data-tip','Add to Wishlist');
			main.number_cart('REMOVE');
		}
	});


});

var main = {
	init:function(){
		main.loaddata_wishlist();
		main.loaddata_cart();
		
	},
	getdata_product_wishlist:function(id){
		
		var img = $("#item_"+id+"").children('.product-image6').find('img').attr('src');
		var name = $("#item_"+id+"").find('.product-content').find('.title').find('a').text();
		var price = $("#item_"+id+"").find('.product-content').find('.price').html();
		var product = {'id': id,'name': name,'img':img,'price':price};
		if(data_wishlist.find(x=>x.id == id) != undefined)
		{
			var i =data_wishlist.findIndex(x=>x.id == id);
			data_wishlist[i] = product;
		}else{
			data_wishlist.push(product);
		}
		return product;
	},
	getdata_product_cart:function(id,quantity){
		
		var img = $("#item_"+id+"").children('.product-image6').find('img').attr('src');
		var name = $("#item_"+id+"").find('.product-content').find('.title').find('a').text();
		var price = $("#item_"+id+"").find('.product-content').find('.price').html();
		var product = {'id': id,'name': name,'img':img,'price':price};
		
		
		if(quantity == undefined)
		{
			quantity = 1;
		}

		var product = {'id': id,'name': name,'img':img,'price':price,'quantity':quantity};

		if(data_cart.find(x=>x.id == id) != undefined)
		{
			var i = data_cart.findIndex(x=>x.id == id);
			data_cart[i] = product;
		}else{
			data_cart.push(product);
		}

		return product;
	},
	number_wishlist:function(fn,num_all){
		var num = 0;	
		var check_mb = false;
		if($(window).width() > 766)
		{
			num = $("#wishlist-count-input").val();	
		}
		else{
			num = $("#wishlist-count-input-mb").val();	
			check_mb = true;
		}
		if(fn == 'ADD')
		{
			num  = parseInt(num) + 1;
		}else if(fn == 'REMOVE')
		{
			num  = parseInt(num) - 1;
		}else{
			num = num_all;
		}

		if(check_mb)
		{
			$(".wishlist-count-mb").html(num);
			$("#wishlist-count-input-mb").val(num);
		}else{
			$(".wishlist-count").html(num);
			$("#wishlist-count-input").val(num);
		}
	},
	html_addwishlist:function(id,img,name,price){
		var html = '';
		html = '<li id="item-wishlist-'+id+'"><div class="img-left"><a href="../html/single.html"><img src="'+img+'" style="width: 100%"></a></div>'+
		'<div class="meta-right">'+
		'<a href="" class="remove-item"><i class="fa fa-times" aria-hidden="true"></i></a>'+
		'<a href="../html/single.html">'+name+'</a><p class="price">'+price+'</p></div></li>';

		return html;
	},
	loaddata_wishlist:function(){
		var html = '';
		var data = data_wishlist;

		var number = 0;
		$.each(data, function (i, item) {
			html+=main.html_addwishlist(item.id, item.img,item.name ,item.price);
			number++;
		});
		main.number_wishlist('',number);
		$('.wishlist-content').find('.scroll-js').append(html);
	},
	remove_item_wishlist:function(id){
		var i = data_wishlist.findIndex(x=>x.id == id);
		data_wishlist.splice(i,1);
		$("#item-wislish-"+id+"").remove();
	},
	number_cart:function(fn,num_all){
		num = 0;	
		var check_mb = false;
		if($(window).width() > 766)
		{
			num = $("#wishlist-count-input").val();	
		}
		else{
			num = $("#wishlist-count-input-mb").val();	
			check_mb = true;
		}

		if(fn == 'ADD')
		{
			num  = parseInt(num) + 1;
		}else if(fn == 'REMOVE')
		{
			num  = parseInt(num) - 1;
		}else{
			num = num_all;
		}

		if(check_mb)
		{
			$(".cart-count").html(num);
			$("#cart-count-input-mb").val(num);
		}else{
			$(".cart-count").html(num);
			$("#cart-count-input").val(num);
		}
	},
	html_addcart:function(id,img,name,price,quantity){
		var html = '';
		html = '<li id="item-cart-'+id+'"><div class="img-left">'+
		'<a href="../html/single.html"><img src="'+img+'" style="width: 100%"></a></div>'+
		'<div class="meta-right">'+
		'<a href="" class="remove-item"><i class="fa fa-times" aria-hidden="true"></i></a>'+
		'<a href="../html/single.html">'+name+'</a>'+
		'<p class="price">'+price+'</p>'+
		'<p class="quantity"><span class="name">Số lượng: </span><span> '+quantity+'</span></p></div></li>';

		return html;
	},
	loaddata_cart:function(){
		var html = '';
		var data = data_cart;

		var number = 0;
		$.each(data, function (i, item) {
			html+=main.html_addcart(item.id, item.img,item.name ,item.price,item.quantity);
			number++;
		});
		main.number_cart('',number);
		$('.cart-content').find('.scroll-js').append(html);
	},
	remove_item_cart:function(id){
		var i = data_cart.findIndex(x=>x.id == id);
		data_cart.splice(i,1);
		$("#item-cart-"+id+"").remove();
	},
	loaddata_product:function(location,title,data,size){
		var number = 0;
		var html = '<div class="container"><div class="row">'+
		'<h2 class="title">'+title+'</h2>';
		
		
		$.each(data, function (i, item) {
			html+='<div class="col-md-3 col-6">'+
			'<div class="product-grid6" id="item_'+item.id+'">'+
			'<div class="product-image6">'+
			'<a href="#"><img class="pic-1" src="'+item.img+'"></a>'+
			'</div><div class="product-content">'+
			'<h3 class="title"><a href="#">'+item.name+'</a></h3>'+
			'<div class="price">'+item.price+'</div>'+
			'</div><ul class="social">'+
			'<li><a href="" class="js-show-modal1" data-tip="Quick View"><i class="fa fa-search"></i></a></li>'+
			'<li><a href="" class="js-add-wishlist"  data-status="true" data-id="'+item.id+'" data-tip="Add to Wishlist"><i class="fa fa-heart" aria-hidden="true"></i></a></li>'+
			'<li><a href="" class="js-add-cart" data-status="true" data-id="'+item.id+'" data-tip="Add to Cart"><i class="fa fa-shopping-basket" aria-hidden="true"></i></a></li>'+
			'</ul></div></div>'
			number++;
		});
		html +='</div></div>'
		$(location).html(html);
	},
	loaddata_product_all:function(location,data,size){
		var number = 1;
		var html = '';
		
		$.each(data, function (i, item) {
			
			html+='<div class="col-md-4 col-6">'+
			'<div class="product-grid6" id="item_'+item.id+'">'+
			'<div class="product-image6">'+
			'<a href="#"><img class="pic-1" src="'+item.img+'"></a>'+
			'</div><div class="product-content">'+
			'<h3 class="title"><a href="#">'+item.name+'</a></h3>'+
			'<div class="price">'+item.price+'</div>'+
			'</div><ul class="social">'+
			'<li><a href="" class="js-show-modal1" data-tip="Quick View"><i class="fa fa-search"></i></a></li>'+
			'<li><a href="" class="js-add-wishlist"  data-status="true" data-id="'+item.id+'" data-tip="Add to Wishlist"><i class="fa fa-heart" aria-hidden="true"></i></a></li>'+
			'<li><a href="" class="js-add-cart" data-status="true" data-id="'+item.id+'" data-tip="Add to Cart"><i class="fa fa-shopping-basket" aria-hidden="true"></i></a></li>'+
			'</ul></div></div>'
			if(number%3 == 0)
			{
				html+='<div class="clearfix"></div>';
			}
			number++;
		});
		
		$(location).html(html);
	},



}
main.init();


