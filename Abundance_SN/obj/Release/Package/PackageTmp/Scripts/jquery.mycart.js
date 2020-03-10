/*
 * jQuery myCart - v1.7 - 2018-03-07
 * http://asraf-uddin-ahmed.github.io/
 * Copyright (c) 2017 Asraf Uddin Ahmed; Licensed None
 */

(function ($) {

  "use strict";

  var OptionManager = (function () {
    var objToReturn = {};

    var _options = null;
    var DEFAULT_OPTIONS = {
      currencySymbol: '$',
      classCartIcon: 'my-cart-icon',
      classCartBadge: 'my-cart-badge',
      classProductQuantity: 'my-product-quantity',
      classProductRemove: 'my-product-remove',
      classProductRemoveReview:'my-product-remove2',
      classCheckoutCart: 'my-cart-checkout',
      affixCartIcon: true,
      showCheckoutModal: true,
      numberOfDecimals: 2,
      cartItems: null,
      clickOnAddToCart: function ($addTocart) {},
      afterAddOnCart: function (products, totalPrice, totalQuantity) {},
      //clickOnCartIcon: function ($cartIcon, products, totalPrice, totalQuantity) {},
      checkoutCart: function (products, totalPrice, totalQuantity) {
        return false;
      },
      getDiscountPrice: function (products, totalPrice, totalQuantity) {
        return null;
      }
    };


    var loadOptions = function (customOptions) {
      _options = $.extend({}, DEFAULT_OPTIONS);
      if (typeof customOptions === 'object') {
        $.extend(_options, customOptions);
      }
    };
    var getOptions = function () {
      return _options;
    };

    objToReturn.loadOptions = loadOptions;
    objToReturn.getOptions = getOptions;
    return objToReturn;
  }());

  var MathHelper = (function () {
    var objToReturn = {};
    var getRoundedNumber = function (number) {
      if (isNaN(number)) {
        throw new Error('Parameter is not a Number');
      }
      number = number * 1;
      var options = OptionManager.getOptions();
      return number.toFixed(options.numberOfDecimals);
    };
    objToReturn.getRoundedNumber = getRoundedNumber;
    return objToReturn;
  }());

  var ProductManager = (function () {
    var objToReturn = {};

    /*
    PRIVATE
    */
    localStorage.products = localStorage.products ? localStorage.products : "";
    var getIndexOfProduct = function (id,Size) {
      var productIndex = -1;
      var products = getAllProducts();
      $.each(products, function (index, value) {
          if (value.id == id && value.productOptionId == Size) {
          productIndex = index;
          return;
        }
      });
      return productIndex;
    };
    var setAllProducts = function (products) {
      localStorage.products = JSON.stringify(products);
    };
    var addProduct = function (id, name, summary, price, quantity, image, Size, key1, key2, key3, value1, value2, value3) {
      var products = getAllProducts();
      products.push({
        id: id,
        name: name,
        summary: summary,
        price: price,
        quantity: quantity,
        image: image,
        productOptionId: Size,
          key1:key1,
          key2: key2, 
          key3:key3,
          value1:value1,
          value2:value2, 
          value3: value3
      });
      setAllProducts(products);
    };

    /*
    PUBLIC
    */
    var getAllProducts = function () {
      try {
        var products = JSON.parse(localStorage.products);
        return products;
      } catch (e) {
        return [];
      }
    };
    var updatePoduct = function (id, quantity,Size) {
      var productIndex = getIndexOfProduct(id,Size);
      if (productIndex < 0) {
        return false;
      }
      var products = getAllProducts();
      products[productIndex].quantity = quantity === "undefined" ? products[productIndex].quantity * 1 + 1 : quantity;
      setAllProducts(products);
      return true;
    };
    var setProduct = function (id, name, summary, price, quantity, image, Size, key1, key2, key3, value1, value2, value3) {

        if (document.getElementById('btnkey1')) {


            if (Size != "" && Size != undefined && Size != "0") {

                if (typeof id === "undefined") {
                    console.error("id required");
                    return false;
                }
                if (typeof name === "undefined") {
                    console.error("name required");
                    return false;
                }
                if (typeof image === "undefined") {
                    console.error("image required");
                    return false;
                }
                if (!$.isNumeric(price)) {
                    console.error("price is not a number");
                    return false;
                }
                if (!$.isNumeric(quantity)) {
                    console.error("quantity is not a number");
                    return false;
                }
                if (Size === "") {
                    console.error("size is not defined is not seleced for the particular product");

                    return false;
                }
                summary = typeof summary === "undefined" ? "" : summary;


                if (!updatePoduct(id, quantity,Size)) {
                    addProduct(id, name, summary, price, quantity, image, Size, key1, key2, key3, value1, value2, value3);
                }
            } 
        }
        else if (document.getElementById('productButton')) {
            if (Size != "" && Size != undefined && Size != "0") {
                if (typeof id === "undefined") {
                    console.error("id required");
                    return false;
                }
                if (typeof name === "undefined") {
                    console.error("name required");
                    return false;
                }
                if (typeof image === "undefined") {
                    console.error("image required");
                    return false;
                }
                if (!$.isNumeric(price)) {
                    console.error("price is not a number");
                    return false;
                }
                if (!$.isNumeric(quantity)) {
                    console.error("quantity is not a number");
                    return false;
                }
                summary = typeof summary === "undefined" ? "" : summary;
                if (Size === "" && Size != "0") {
                    console.error("product Options is not seleced for the particular product");
                    return false;
                }
                if (!updatePoduct(id, "undefined", Size)) {
                    addProduct(id, name, summary, price, quantity, image, Size, key1, key2, key3, value1, value2, value3);
                }
            }
        }
        else if (document.getElementById('add-to-cart-Id')) {
            if (Size != "" && Size != undefined && Size != "0") {
                if (typeof id === "undefined") {
                    console.error("id required");
                    return false;
                }
                if (typeof name === "undefined") {
                    console.error("name required");
                    return false;
                }
                if (typeof image === "undefined") {
                    console.error("image required");
                    return false;
                }
                if (!$.isNumeric(price)) {
                    console.error("price is not a number");
                    return false;
                }
                if (!$.isNumeric(quantity)) {
                    console.error("quantity is not a number");
                    return false;
                }
                summary = typeof summary === "undefined" ? "" : summary;
                if (Size === "" && Size != "0") {
                    console.error("product Options is not seleced for the particular product");
                    return false;
                }
                if (!updatePoduct(id, "undefined", Size)) {
                    addProduct(id, name, summary, price, quantity, image, Size, key1, key2, key3, value1, value2, value3);
                }
            }
        }

    };
    var clearProduct = function () {
      setAllProducts([]);
    };
    var removeProduct = function (id, optionId) {
      var products = getAllProducts();
      products = $.grep(products, function (value, index) {
          return value.id != id || value.productOptionId != optionId;
      });
      setAllProducts(products);
    };
    var getTotalQuantity = function () {
      var total = 0;
      var products = getAllProducts();
      $.each(products, function (index, value) {
        total += value.quantity * 1;
      });
      return total;
    };
    var getTotalPrice = function () {
      var products = getAllProducts();
      var total = 0;
      $.each(products, function (index, value) {
        total += value.quantity * value.price;
        total = MathHelper.getRoundedNumber(total) * 1;
      });
      return total;
    };

    objToReturn.getAllProducts = getAllProducts;
    objToReturn.updatePoduct = updatePoduct;
    objToReturn.setProduct = setProduct;
    objToReturn.clearProduct = clearProduct;
    objToReturn.removeProduct = removeProduct;
    objToReturn.getTotalQuantity = getTotalQuantity;
    objToReturn.getTotalPrice = getTotalPrice;
    return objToReturn;
  }());


  var loadMyCartEvent = function (targetSelector) {

    var options = OptionManager.getOptions();
    var $cartIcon = $("." + options.classCartIcon);
    var $cartBadge = $("." + options.classCartBadge);
    var classProductQuantity = options.classProductQuantity;
    var classProductRemove = options.classProductRemove;
    var classCheckoutCart = options.classCheckoutCart;
    var classProductRemoveReview = options.classProductRemoveReview;

    var idCartModal = 'my-cart-modal';
    var idCartTable = 'my-cart-table';
    var idGrandTotal = 'my-cart-grand-total';
    var idEmptyCartMessage = 'my-cart-empty-message';
    var idDiscountPrice = 'my-cart-discount-price';
    var classProductTotal = 'my-product-total';
    var classAffixMyCartIcon = 'my-cart-icon-affix';
    var classSubTotal = 'sumTotal';
      var classSubTotal2 = 'sumTotal2';

    if (options.cartItems && options.cartItems.constructor === Array) {
      ProductManager.clearProduct();
      $.each(options.cartItems, function () {
          ProductManager.setProduct(this.id, this.name, this.summary, this.price, this.quantity, this.image, this.Size, this.key1,this.key2,this.key3, this.value1, this.value2,this.value3);
      });
    }

    $cartBadge.text(ProductManager.getTotalQuantity());

    if (!$("#" + idCartModal).length) {
      $('body').append(
        '<div class="modal fade" id="' + idCartModal + '" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">' +
        '<div class="modal-dialog" role="document">' +
        '<div class="modal-content">' +
        '<div class="modal-header">' +
        '<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>' +
        '<h4 class="modal-title" id="myModalLabel"><span class="glyphicon glyphicon-shopping-cart">' +
        '</div>' +
        '<div class="modal-body">' +
        '<table class="table table-hover table-responsive" id="' + idCartTable + '"></table>' +
        '</div>' +
        '<div class="modal-footer">' +
        '<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>' +
        '<a type="button" class="btn btn-primary" href="/Customer/Payment/Cart">Checkout</a>' +
        '</div>' +
        '</div>' +
        '</div>' +
        '</div>'
      );
    }

    var drawTable = function () {
      var $cartTable = $("#" + idCartTable);
      $cartTable.empty();

      var products = ProductManager.getAllProducts();
      $.each(products, function () {
        var total = this.quantity * this.price;
        $cartTable.append(


            '<ol class="minicart-items">' +
                '<li class="product-inner">' +
                    '<div class="product-thumb style1">'+
                        '<div class="thumb-inner">'+
                            '<a href="#">'+
                                '<img id="cartProductImage" src="images/home1/c1.jpg" alt="c1">'+
                                        '</a>'+
                                    '</div>'+
                        '</div>'+
                        '<div class="product-innfo">'+
                            '<div class="product-name">'+
                                '<a href="#">Xbox One S Halo Collection Bund</a>'+
                            '</div>'+
                            '<a href="#" class="remove">'+
                                '<i class="fa fa-times" aria-hidden="true"></i>'+
                            '</a>'+
                            '<span class="price">'+
                                '<ins id="discountedAmount"></ins>'+
                                '<del id="realPrice"></del>'+
                            '</span>'+
                        '</div>'+
                            '</li>'+
                            '</ol>'


          //'<tr title="' + this.summary + '" data-id="' + this.id + '" data-price="' + this.price + '">' +
          //'<td class="text-center" style="width: 30px;"><img width="30px" height="30px" src="/EasyShop' + this.image.trim() + '"/></td>' +
          //'<td>' + this.name + '</td>' +
          //'<td title="Unit Price" class="text-right">' + options.currencySymbol + MathHelper.getRoundedNumber(this.price) + '</td>' +
          //'<td title="Quantity"><input type="number" min="1" style="width: 70px;" class="' + classProductQuantity + '" value="' + this.quantity + '"/></td>' +
          //'<td title="Total" class="text-right ' + classProductTotal + '">' + options.currencySymbol + MathHelper.getRoundedNumber(total) + '</td>' +
          //'<td title="Remove from Cart" class="text-center" style="width: 30px;"><a href="javascript:void(0);" class="btn btn-xs btn-danger removeModalProduct">X</a></td>' +
          //'</tr>'
        );
      });

      $cartTable.append(products.length ?
        '<tr>' +
        '<td></td>' +
        '<td><strong>Total</strong></td>' +
        '<td></td>' +
        '<td></td>' +
        '<td class="text-right"><strong id="' + idGrandTotal + '"></strong></td>' +
        '<td></td>' +
        '</tr>' :
        '<div class="alert alert-danger" role="alert" id="' + idEmptyCartMessage + '">Your cart is empty</div>'
      );

      var discountPrice = options.getDiscountPrice(products, ProductManager.getTotalPrice(), ProductManager.getTotalQuantity());
      //if (products.length && discountPrice !== null) {
      //  $cartTable.append(
      //    '<tr style="color: red">' +
      //    '<td></td>' +
      //    '<td><strong>Total (including discount)</strong></td>' +
      //    '<td></td>' +
      //    '<td></td>' +
      //    '<td class="text-right"><strong id="' + idDiscountPrice + '"></strong></td>' +
      //    '<td></td>' +
      //    '</tr>'
      //  );
      //}

      showGrandTotal();
      showDiscountPrice();
    };
    var showModal = function () {
      drawTable();
      $("#" + idCartModal).modal('show');
    };
    var updateCart = function () {
      $.each($("." + classProductQuantity), function () {
        var id = $(this).closest("tr").data("id");
        ProductManager.updatePoduct(id, $(this).val());
      });
    };
    var showGrandTotal = function () {
      $("#" + idGrandTotal).text(options.currencySymbol + MathHelper.getRoundedNumber(ProductManager.getTotalPrice()));
    };
    var showDiscountPrice = function () {
      $("#" + idDiscountPrice).text(options.currencySymbol + MathHelper.getRoundedNumber(options.getDiscountPrice(ProductManager.getAllProducts(), ProductManager.getTotalPrice(), ProductManager.getTotalQuantity())));
    };

    /*
    EVENT
    */
    if (options.affixCartIcon) {
      var cartIconBottom = $cartIcon.offset().top * 1 + $cartIcon.css("height").match(/\d+/) * 1;
      var cartIconPosition = $cartIcon.css('position');
      $(window).scroll(function () {
        $(window).scrollTop() >= cartIconBottom ? $cartIcon.addClass(classAffixMyCartIcon) : $cartIcon.removeClass(classAffixMyCartIcon);
      });
    }

    $cartIcon.click(function () {
      options.showCheckoutModal ? showModal() : options.clickOnCartIcon($cartIcon, ProductManager.getAllProducts(), ProductManager.getTotalPrice(), ProductManager.getTotalQuantity());
    });

    $(document).on("input", "." + classProductQuantity, function () {
      var price = $(this).closest("tr").data("price");
      var id = $(this).closest("tr").data("id");
      var quantity = $(this).val();

      $(this).parent("td").next("." + classProductTotal).text(options.currencySymbol + MathHelper.getRoundedNumber(price * quantity));
      ProductManager.updatePoduct(id, quantity);

      $cartBadge.text(ProductManager.getTotalQuantity());
      showGrandTotal();
      showDiscountPrice();
    });

    $(document).on('keypress', "." + classProductQuantity, function (evt) {
      if (evt.keyCode == 38 || evt.keyCode == 40) {
        return;
      }
      evt.preventDefault();
    });

    $(document).on('click', "." + classProductRemoveReview, function () {
        $("#" + classSubTotal).text('');
        $("#" + classSubTotal2).text('');
        var cartVal = $(".totalCartAmountId").text('');
        var $tr = $(this).closest("tr");
        var id = $tr.attr("id");
        var optionId = $tr.attr("size");
        var $li = $(this).closest("tr");
        $li.hide(500, function () {
            ProductManager.removeProduct(id, optionId);
            //drawTable();
            var totalPrice = '₦' + ProductManager.getTotalPrice();
            $cartBadge.text(ProductManager.getTotalQuantity());
            $("#" + classSubTotal).text(totalPrice);
            $("#" + classSubTotal2).text(totalPrice);
            $(".totalCartAmountId").append(totalPrice);
            //$('#tableId').append(totalPrice);
            if (ProductManager.getTotalQuantity() == "0" || ProductManager.getTotalQuantity() == "") {
                $('#cartBody').html('');
                $('#cartBody').append('<div class="alert alert-danger" role="alert">Your cart is empty</div>');
            }

        });
    });

    $(document).on('click', "." + classProductRemove, function () {
        $("#" + classSubTotal).text('');
        $("#" + classSubTotal2).text('');
        var cartVal = $(".totalCartAmountId").text('');
        var $tr = $(this).closest("div");
        var id = $tr.attr("id");
        var optionId = $tr.attr("size");
        var $li = $(this).closest("li");
        $li.hide(500, function () {
              ProductManager.removeProduct(id, optionId);
              //drawTable();
              var totalPrice = '₦' + ProductManager.getTotalPrice();
              $cartBadge.text(ProductManager.getTotalQuantity());
              $("#" + classSubTotal).text(totalPrice);
              $("#" + classSubTotal2).text(totalPrice);
              $(".totalCartAmountId").append(totalPrice);
              if (ProductManager.getTotalQuantity() == "0" || ProductManager.getTotalQuantity() == "") {
                  $('#cartBody').html('');
                   $('#cartBody').append('<div class="alert alert-danger" role="alert">Your cart is empty</div>');
              }
              
          });
      });
    $(document).on('click', ".removeModalProduct", function () {
        $("#" + classSubTotal).text('');
        $("#" + classSubTotal2).text('');
        var $tr = $(this).closest("div");
        var optionId = $tr.attr("size");
        var id = $tr.attr("id");
        var $li = $(this).closest("li");
        $li.hide(500, function () {
            ProductManager.removeProduct(id, optionId);
            //drawTable();
            var totalPrice = '₦' + ProductManager.getTotalPrice();
            $cartBadge.text(ProductManager.getTotalQuantity());
            $("." + classSubTotal).text(totalPrice);
            $("#" + classSubTotal2).text(totalPrice);
            
            if (ProductManager.getTotalQuantity() == "0" || ProductManager.getTotalQuantity() == "") {
                $('#cartBody').html('');
                $('#cartBody').append('<div class="alert alert-danger" role="alert"> Your cart is empty</div>');
            }

        });
    });
    $(document).on('click', "." + classCheckoutCart, function () {
      var products = ProductManager.getAllProducts();
      if (!products.length) {
        $("#" + idEmptyCartMessage).fadeTo('fast', 0.5).fadeTo('fast', 1.0);
        return;
      }
      updateCart();
      var isCheckedOut = options.checkoutCart(ProductManager.getAllProducts(), ProductManager.getTotalPrice(), ProductManager.getTotalQuantity());
      if (isCheckedOut !== false) {
        ProductManager.clearProduct();
        $cartBadge.text(ProductManager.getTotalQuantity());
        $("#" + idCartModal).modal("hide");
      }
    });

    $(document).on('click', '#add-to-cart-Id', function (e) {
      var $target = $('#' + this.id);
      options.clickOnAddToCart($target);
      var id = $target.attr('data-id');

        if (id != undefined) {

            var name = $target.attr('data-name');
            var summary = $target.attr('data-summary');

            summary = escape(summary);
            var price = $target.attr('data-price');
            var quantity = $target.attr('data-quantity');
            var image = $target.attr('data-image');
            var Size = $target.attr('data-size');
            var key1 = $target.attr('data-valuekey1');
            var key2 = $target.attr('data-valuekey2');
            var key3 = $target.attr('data-valuekey3');
            var value1 = $target.attr('data-Value1');
            var value2 = $target.attr('data-Value2');
            var value3 = $target.attr('data-Value3');
            ProductManager.setProduct(id, name, summary, price, quantity, image, Size, key1, key2, key3, value1, value2, value3);
            $cartBadge.text(ProductManager.getTotalQuantity());
            options.afterAddOnCart(ProductManager.getAllProducts(), ProductManager.getTotalPrice(), ProductManager.getTotalQuantity());
        } else {
            e.stopPropagation();
        }
     
    });

  };


  $.fn.myCart = function (userOptions) {
    OptionManager.loadOptions(userOptions);
    loadMyCartEvent(this.selector);
    return this;
  };


})(jQuery);