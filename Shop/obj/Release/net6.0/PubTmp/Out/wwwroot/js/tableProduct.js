var table = $("#example").DataTable({
    "language": {
        "processing": "loading data",
        "info": "bản ghi từ _START_ đến _END_ và có tổng _TOTAL_ bản ghi",
        "emptyTable": "Không tìm thấy bản ghi",
        "search": "Tìm kiếm",
        "lengthMenu": "Xem _MENU_ bản ghi"
    },
    "processing": true,
    "serverSide": true,
    "filter": true,
    "ajax": {
        "url": `/Product/query`,
        "type": "POST",
        "datatype": "json"
    },
    "columnDefs": [
        {
            "visible": false,
            "searchable": false,
            "targets": [0]
        }],
    "columns": [
        { "data": "id", "name": "id" },
        {
            "data": "id", "name": "id",
            "render": function (isbool, type, row) {
                return `<input type="checkbox" class="form-check-input" />`
            }
        },
        { "data": "name", "name": "Name" },
        {
            "data": "isVisibleIndividually", "name": "IsVisibleIndividually",
            "render": function (IsVisibleIndividually, type, full, meta) {
                if (IsVisibleIndividually) {
                    return '<i class="fas fa-circle"></i>';
                } else {
                    return '<i class="far fa-circle"></i>';
                }
            }
        },
        {
            "data": "isPublished", "name": "IsPublished",
            "render": function (IsPublished, type, full, meta) {
                if (IsPublished) {
                    return '<i class="fas fa-circle"></i>';
                } else {
                    return '<i class="far fa-circle"></i>';
                }
            }
        }]
});
table.on('draw', function () {
    var table = $("#example").DataTable()
    var selectedProductIds = product.map(function (item) { return item.id; });
    table.rows().every(function (rowIdx, tableLoop, rowLoop) {
        var data = this.data();
        var checked = selectedProductIds.indexOf(data.id);
        if (checked > -1) {
            $(this.node()).find("input").attr("checked", true);
        }
    });
});
var product = [];
$('#example').on('click', 'tbody td input', function (e) {
    var checked = $(this).is(":checked");
    var data = table.row(this.closest('tr')).data();
    var Index = product.findIndex(x => x.id == data.id)
    if (Index > -1) {
        product.splice(Index, 1);
    } else {
        product.push(data);

    }
    loaddata();
});