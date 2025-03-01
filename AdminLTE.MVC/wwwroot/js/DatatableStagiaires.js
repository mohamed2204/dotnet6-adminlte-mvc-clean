﻿var Popup, dataTable;
$(document).ready(function () {
    //alert('(document).ready');
    var stageId = $('#stage').val();
    $('#stage').on('change', function () {
        console.log(`Stage selector changed to ${this.value}.`);
        stageId = $('#stage').val();
        dataTable.ajax.reload();
    });

    dataTable = $("#customerDatatable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/Stagiaires/GetList",
            "type": "POST",
            "datatype": "json",
            "data": function (d) {
                d.stage = { "stageId": stageId };
                // d.custom = $('#myInput').val();
                // etc
            }
        },
        "columnDefs": [{
            "targets": [0],
            "visible": false,
            "searchable": false
        }],
        "columns": [
            { "data": "id", "name": "Id", "autoWidth": true },
            { "data": "grade", "name": "Grade", "autoWidth": true },
            { "data": "prenom", "name": "Prénom", "autoWidth": true },
            { "data": "nom", "name": "Nom", "autoWidth": true },
            { "data": "specialite", "name": "Spécialite", "autoWidth": true },
            { "data": "dateDebut", "name": "Début", "autoWidth": true },
            { "data": "dateFin", "name": "Fin", "autoWidth": true },
            {
                "data": "id", "render": function (data) {
                    //url = "/Stagiaires/AddOrEdit/" + data;
                    //links = "<a class='btn btn-default btn-sm' onclick=PopupForm('" + url + "')><i class='fa fa-pencil'></i> Edit</a>";
                    //links += "<a class='btn btn-danger btn-sm' style='margin-left:5px' onclick=Delete(" + data + ") > <i class='fa fa-trash'></i> Delete</a > ";

                    //var url = "@Url.Action("Edit","Stagiaires")/' + data,

                    links = "<a href='/Stagiaires/Edit/" + data + "' class='btn btn-primary btn-sm rounded-0' style='margin:0 5px 0 0'><i class='fa fa-eye'></i></a>";
                    links += "<a href='/Stagiaires/Details/" + data + "' class='btn btn-success btn-sm rounded-0' style='margin:0 5px 0 0'><i class='fa fa-eye' aria-hidden='true'></i></a>";
                    links += "<a href='/Stagiaires/Delete/" + data + "' class='btn btn-danger btn-sm rounded-0' style='margin:0 5px 0 0'><i class='fa fa-trash' aria-hidden='true'></i></a>";

                    return links;
                },
                "orderable": false,
                "searchable": false,
                "width": "150px"
            }
            //{
            // < a onclick = "location.href='@Url.Action("Update", "Book", new { id = @Model.Id })'"
            //class= "btn btn-primary" > Edit</a >
            //    "render": function (data, row) {
            //        var links = "<a href='#' class='btn btn-danger' onclick=DeleteCustomer('" + data + "'); >View</a> | ";
            //        links += "<a href='#' class='btn btn-danger' onclick=DeleteCustomer('" + data + "'); >Edit</a> | ";
            //        links += "<a href='#' class='btn btn-danger' onclick=DeleteCustomer('" + row + "'); >Delete</a>";
            //        return links;

            //    }
            //},
        ],
        "language": {

            "emptyTable": "No data found, Please click on <b>Add New</b> Button"
        }
    });
});

function PopupForm(url) {
    var formDiv = $('#exampleModalCenter');
    //alert(url);
    //return;
    $.get(url)
        .done(function (response) {
            //console.log(response);
            //return;
            formDiv.html(response);

            //formDiv.modal()
            $('#exampleModalCenter').modal()


            //Popup = formDiv.dialog({
            //    autoOpen: true,
            //    resizable: false,
            //    title: 'Fill Employee Details',
            //    height: 500,
            //    width: 700,
            //    close: function () {
            //        Popup.dialog('destroy').remove();
            //    }

            //});
        });
}

function SubmitForm(form) {
    $.validator.unobtrusive.parse(form);
    if ($(form).valid()) {
        //alert($(form).serialize());

        //toastr.options = {
        //    "closeButton": true,
        //    "debug": false,
        //    "newestOnTop": false,
        //    "progressBar": false,
        //    "positionClass": "toast-top-right",
        //    "preventDuplicates": true,
        //    "onclick": null,
        //    "showDuration": "300",
        //    "hideDuration": "1000",
        //    "timeOut": "5000",
        //    "extendedTimeOut": "1000",
        //    "showEasing": "swing",
        //    "hideEasing": "linear",
        //    "showMethod": "fadeIn",
        //    "hideMethod": "fadeOut"
        //}
        //toastr["success"]($(form).serialize())
        //return false;
        $.ajax({
            type: "POST",
            url: form.action,
            data: $(form).serialize(),
            success: function (data) {
                if (data.success) {
                    //Popup.dialog('close');
                    $('#exampleModalCenter').modal('hide');
                    dataTable.ajax.reload();
                    toastr["success"](data.message)
                    //$.notify(data.message, {
                    //    globalPosition: "top center",
                    //    className: "success"
                    //})

                } else {
                    toastr["error"](data.message)
                }
            },
            error: function (data) {
                alert(data);
                if (data.success) {
                    ////Popup.dialog('close');
                    //$('#exampleModalCenter').modal('hide');
                    //dataTable.ajax.reload();
                    //toastr["success"](data.message)
                    //$.notify(data.message, {
                    //    globalPosition: "top center",
                    //    className: "success"
                    //})

                } else {
                    toastr["error"](data.message)
                }
            }
        });
    }
    return false;
}

function Delete(id) {
    if (confirm('Are You Sure to Delete this Employee Record ?')) {
        $.ajax({
            type: "POST",
            url: '@Url.Action("Delete","Employee")/' + id,
            success: function (data) {
                if (data.success) {
                    dataTable.ajax.reload();

                    $.notify(data.message, {
                        globalPosition: "top center",
                        className: "success"
                    })

                }
            }

        });
    }
}

//function to write actual data of a table row
function DeleteCustomer(e) {
    console.log(e)
}