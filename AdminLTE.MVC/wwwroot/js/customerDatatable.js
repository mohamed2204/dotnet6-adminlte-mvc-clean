$(document).ready(function () {
    //alert('(document).ready');
    $("#customerDatatable").DataTable({
        "processing": true,
        "serverSide": true,
        "filter": true,
        "ajax": {
            "url": "/StagiaireStages/GetList2",
            "type": "POST",
            "datatype": "json"
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
                "render": function (data, row) { return "<a href='#' class='btn btn-danger' onclick=DeleteCustomer('" + row.id + "'); >Delete</a>"; }
            },
        ]
    });
});