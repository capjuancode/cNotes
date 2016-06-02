// form post serializer
$.fn.serializeObject = function() {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function() {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

// ==============================================================

$("Form").submit(function(event) {

    if ($('form').validate() == true) {

        var postData = JSON.stringify($('form').serializeObject());
        var formURL = $(this).attr("action");

        $.ajax({
            type: "post",
            url: formURL,
            data: postData,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(msg) {
                var SessionRecords = JSON.parse(msg.d);
            },
            error: function() {
                alert("An error has occurred during processing your request.");
            }
        });

    }
    event.preventDefault()

});

// ============================================================================

Table = $('#Example').DataTable({
    dom: 'Brltip',
    buttons: [
        'copy', 'csv', 'excel', 'pdf', 'print'
    ],
    "aaData": SessionRecords,
    "order": [
        [0, "desc"]
    ],
    "aoColumns": [{
            "mDataProp": "AssetItemID",
            "visible": false,
            "searchable": false
        }, {
            "mDataProp": "SerialNumber"
        }, {
            "mDataProp": "PrimaryLocation"
        }, {
            "mDataProp": "SecondaryLocation"
        }, {
            "mDataProp": "LocationMoreDetail"
        }, {
            "mDataProp": "AssetTypeName"
        }, {
            "mDataProp": "AssetMakeName"
        }, {
            "mDataProp": "AssetModel"
        }, {
            "mDataProp": "ReplacementValue"
        }, {
            "mDataProp": "DateCreated"
        }, {
            "mDataProp": "UserName"
        }

    ]

});




// use search box for filter
$('#txtSerialNumber').keyup(function() {
    activeSessionTable.search($(this).val()).draw();
});

// Event: On Click - to change from add to input
$('#Example').on('click', 'tbody tr', function() {

    var activeTableRow = Table.row(this).data();

    $('Form').attr('action', "purchase_order_entry .aspx/editPO"); //changes the action on the form
    $("#ddl option:contains(" + activeTableRow.SerialNumber + ")").attr('selected', 'selected'); //selects dropdown base on text not value
    $('#txt').val(activeTableRow.Notes); // add text to textfield
    $("#datepicker").datepicker('update', activeTableRow.DateRequested); //change date on datepicker

});

//===========================================================================================
//change date on datepiker
