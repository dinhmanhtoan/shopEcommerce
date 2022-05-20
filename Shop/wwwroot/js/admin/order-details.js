var Status;
function listStatus() {
    var url = `${origin}/Order/GetOrderStatus/order-status`
    $.ajax({
        type: "GET",
        url: url,
        success: function (data) {
            var orderStatus = $(".Orderstatus");
            orderStatus.empty();
            if (data.length > 0) {
                Status = data;
                $.each(data, function (index, option) {
                    orderStatus.append($("<option></option>").attr("value", option.id).text(option.name));
                });
            }
        }
    });
}
function OrderHistory() {
    var form = $("form[name='orderStatusForm']");
    var OrderId = parseInt($(form).find("input[name='OrderId']").val());
    var url = `${origin}/api/orders/${OrderId}/history`;
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json",
        success: function (data) {
            var orderStatus = $("#Orderstatus");
            if (data.length > 0) {
                orderStatus.empty();
                $.each(data, function (index, value) {
                    orderStatus.append($("<tr></tr>").append(
                        `<td>${moment(value.createdOn).format("MM-DD-YYYY")}</td>
                        <td>${value.oldStatus} -> ${value.newStatus}</td>
                        <td>${value.createdByFullName}</td>
                        <td>${value.note ? value.note : ""}</td>`));
                })
            }
        }
    });
}
function changeOrderStatus() {
    var form = $("form[name='orderStatusForm']");
    var OrderId = parseInt($(form).find("input[name='OrderId']").val());
    var StatusId = parseInt($(form).find("select[name='StatusId']").find(":selected").val());
    var Note = $(form).find("textarea[name='Note']").val();
    var OrderStatusForm = { StatusId: StatusId, Note: Note };
    var url = `${origin}/Order/ChangeStatus/change-order-status/${OrderId}`;
    $.ajax({
        type: "POST",
        url: url,
        data: JSON.stringify(OrderStatusForm),
        contentType: "application/json",
        success: function (data) {
            var StatusString = Status.find(function (item) { return item.id === data; }).name;
            toastr.success('The order now is ' + StatusString);
            OrderHistory();
        },
        error: function (response) {
            toastr.error('The order now is ' + response.statusText);
        }
    });
};
function shippingMethod() {
    var orderId = parseInt($("#OrderId").text().trim());
    var url = `${origin}/api/shipments/${orderId}/shipments`;
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json",
        success: function (data) {
            var shippingMethod = $("#shippingMethod");
            if (data.length > 0) {
                shippingMethod.empty();
                $.each(data, function (index, value) {
                    shippingMethod.append($("<tr></tr>").append(
                        `<td>${value.id}</td>
                            <td>${value.trackingNumber ? value.trackingNumber : ""}</td>
                            <td>${value.warehouseName}</td>
                            <td>${moment(value.createdOn).format("MM-DD-YYYY")}</td>
                            <td><a href="/shipment/get/${value.id}">
                                    <i class="fas fa-eye"></i>
                               </a>
                            </td>`));
                })
            }
        }
    });
}
listStatus();
OrderHistory();
shippingMethod();
function orderDownload() {
    $(".btn-dowload").on("click", function () {
        toastr.error("chức năng này chưa có");
    })
}
orderDownload();