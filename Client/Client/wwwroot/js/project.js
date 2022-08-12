$(document).ready(() => {
    let table = $('#tableProject').DataTable({
        "language": {
            "paginate": {
                "previous": "<i class='ni ni-bold-left'></i>",
                "next": "<i class='ni ni-bold-right'></i>"
            }
        },
        columnDefs: [{
                orderable: false,
                targets: -1
            },
            {
                className: 'text-center',
                targets: [0, 1, 2, 3]
            }
        ],
        "ajax": {
            "url": "https://localhost:44335/Project/getjson",
            "dataType": "json",
        },
        "columns": [
            {
                "data": "ProjectId",
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            {
                "data": "Name"
            },
            {
                "data": "Description"
            },
            {
                // data: null,
                render: function (data, type, row) {

                    return `
                                <button type="button" onclick="editProduct(${row['id']})" data-toggle="modal" data-target="#editProduct" class="btn btn-success">
                                    Details
                                </button>
                                <button type="button" onclick="editProduct(${row['id']})" data-toggle="modal" data-target="#editProduct" class="btn btn-warning">
                                    Edit
                                </button>
                                <button type="button" onclick="deleteProject(${row['ProjectId']})" class="btn btn-danger">
                                    Delete
                                </button>`
                }
            }
        ],
    })
})

function addProject() {
    let createModalBody =
        `
        <div class="form-row" id="form-post">
            <div class="col-md-6 mb-3">
                <label for="projectName">Project Name</label>
                <input asp-for="Name" name="projectName" type="text" class="form-control form-control-alternative"
                    id="projectName" required>
                <div class="valid-feedback">
                    Looks good!
                </div>
                <div class="invalid-feedback">
                    Please Input Valid Project Name!
                </div>
            </div>
            <div class="col-md-6 mb-3">
                <label for="Description">Description</label>
                <input asp-for="Description" name="description" type="text" class="form-control form-control-alternative"
                    id="description" required>
                <div class="valid-feedback">
                    Looks good!
                </div>
                <div class="invalid-feedback">
                    Please Input Valid Description!
                </div>
            </div>
        </div>               
        `;
    $("#modalCreate").html(createModalBody);
    let forms = document.getElementsByClassName("needs-validation");

    let validation = Array.prototype.filter.call(forms, (form) => {
        form.addEventListener('submit', (event) => {
            if (form.checkValidity() === false) {
                event.preventDefault();
                event.stopPropagation();
            } else {
                event.preventDefault();
                let obj = {};
                obj.name = $("#projectName").val();
                obj.description = $("#description").val();
                console.log(obj);
                swal({
                    title: "Are you sure?",
                    text: `You will add Project : ${obj.name}`,
                    buttons: {
                        cancel: true,
                        confirm: true,
                        closeModal: false
                    },
                }).then((isConfirm) => {
                    if (isConfirm === true) {
                        $.ajax({
                            url: "https://localhost:44335/project/postjson",
                            type: "post",
                            dataType: "json",
                            data: obj,
                            beforeSend: data => {
                                data.setRequestHeader("RequestVerificationToken", $("[name='__RequestVerificationToken']").val());
                            },
                            success: () => {
                                $("#tableProject").DataTable().ajax.reload();
                                $("#addProject").modal('hide'),
                                    swal(
                                        "Success",
                                        `${obj.name} has been saved`,
                                        "success"
                                    )
                            },
                            failure: () => {
                                swal(
                                    "Internal Server Error",
                                    `Oops, ${obj.name} was not saved`,
                                    "error"
                                )
                            }
                        })
                    }
                })
            }
            form.classList.add('was-validated');
        }, false);
    })
}

function deleteProject(id) {
    $.ajax({
        url: `https://localhost:44335/project/getjsonbyid/${id}`,
        type: 'get'
    }).done((result) => {
        console.log(result);
        let obj = {};
        obj.ProjectId = result.data.ProjectId;
        obj.Name = result.data.Name;
        obj.Description = result.data.Description;
        swal({
            title: "Are you sure?",
            text: `You will delete ${obj.Name}`,
            buttons: {
                cancel: true,
                confirm: true,
                closeModal: false
            },
        }).then(function (isConfirm) {
            if (isConfirm === true) {
                $.ajax({
                    url: "https://localhost:44335/project/deletejson",
                    type: "delete",
                    dataType: "json",
                    data: obj,
                    beforeSend: data => {
                        data.setRequestHeader("RequestVerificationToken", $("[name='__RequestVerificationToken']").val());
                    },
                    success: function (data) {
                        $("#tableProject").DataTable().ajax.reload();
                        swal(
                            "Success!",
                            `${obj.Name} has been deleted !`,
                            "success"
                        )
                    },
                    failure: function (data) {
                        swal(
                            "Internal Error",
                            "Oops, Product was not saved.",
                            "error"
                        )
                    }
                });
            }
        })
    })
}