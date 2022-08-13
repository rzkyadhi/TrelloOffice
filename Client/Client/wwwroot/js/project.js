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
            "url": "https://localhost:44335/Project/GetJSON",
            "dataType": "json",
        },
        "columns": [{
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
                                <button type="button" onclick="detailProject(${row['ProjectId']})" data-toggle="modal" data-target="#detailProject" class="btn btn-success">
                                    Details
                                </button>
                                <button type="button" onclick="editProject(${row['ProjectId']})" data-toggle="modal" data-target="#editProject" class="btn btn-warning">
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

function editProject(id) {
    $.ajax({
        url: `https://localhost:44335/project/getjsonbyid/${id}`,
        type: 'get'
    }).done((result) => {
        let editModalBody =
            `
        <div class="form-row">
            <div class="col-md-4 mb-3">
                    <label for="projectId">Project Id</label>
                    <input name="projectId" type="number" class="form-control form-control-alternative"
                        id="projectId" value=${result.data.ProjectId} readonly required>
                    <div class="valid-feedback">
                        Looks good!
                    </div>
                </div>
            <div class="col-md-4 mb-3">
                <label for="name">Project Name</label>
                <input id="name" type="text" class="form-control form-control-alternative"
                         value="${result.data['Name']}" required>
                <div class="valid-feedback">
                    Looks good!
                </div>
            </div>
            <div class="col-md-4 mb-3">
                <label for="description">Description</label>
                <input id="description" type="text" class="form-control form-control-alternative"
                         value="${result.data['Description']}" required>
                <div class="valid-feedback">
                    Looks good!
                </div>
            </div>
        </div>
        `;
        $("#modalEdit").html(editModalBody);
        var formsEdit = document.getElementsByClassName('edit-validation');
        // Loop over them and prevent submission
        var validationEdit = Array.prototype.filter.call(formsEdit, function (form) {
            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                } else {
                    event.preventDefault();
                    let obj = {};
                    obj.ProjectId = $("#projectId").val();
                    obj.Name = $("#name").val();
                    obj.Description = $("#description").val();
                    swal({
                        title: "Are you sure?",
                        text: `You will edit ${obj.Name}`,
                        buttons: {
                            cancel: true,
                            confirm: true,
                            closeModal: false
                        },
                    }).then(function (isConfirm) {
                        if (isConfirm === true) {
                            $.ajax({
                                url: "https://localhost:44335/project/editjson",
                                type: "put",
                                dataType: "json",
                                data: obj,
                                beforeSend: data => {
                                    data.setRequestHeader("RequestVerificationToken", $("[name='__RequestVerificationToken']").val());
                                },
                                success: function (data) {
                                    $("#tableProject").DataTable().ajax.reload();
                                    $("#editProject").modal('hide'),
                                        swal(
                                            "Success!",
                                            `${obj.Name} has been edited`,
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
                }
                form.classList.add('was-validated');
            }, false);
        });
    })
}

function deleteProject(id) {
    $.ajax({
        url: `https://localhost:44335/project/getjsonbyid/${id}`,
        type: 'get'
    }).done((result) => {
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

function detailProject(id) {
    $.ajax({
        url: `https://localhost:44335/project/getjsonbyid/${id}`,
        type: 'get'
    }).done((result) => {
        let detailModalBody =
            `
        <div class="form" id="form-post">
            <div class="col mb-3">
                <label for="projectName">Project Name</label>
                <input asp-for="Name" name="projectName" type="text" class="form-control form-control-alternative"
                    id="projectName" value="${result.data.Name}" readonly required>
                <div class="valid-feedback">
                    Looks good!
                </div>
                <div class="invalid-feedback">
                    Please Input Valid Project Name!
                </div>
            </div>
            <div class="col mb-3">
                <label for="Description">Description</label>
                <input asp-for="Description" name="description" type="text" class="form-control form-control-alternative"
                    id="description" value="${result.data.Description}" required>
                <div class="valid-feedback">
                    Looks good!
                </div>
                <div class="invalid-feedback">
                    Please Input Valid Description!
                </div>
            </div>
            <div class="col mb-3" id="taskList">
            </div>
        </div>               
        `;
        $("#modalDetail").html(detailModalBody);
        $.ajax({
            url: `https://localhost:44335/task/getjson`,
            type: 'get'
        }).done((result) => {
            let task =
                `
                    <label for="Task">Task</label>
                        <ul class="list-group">
                            <div class="row">
                                <div class="col" id="listGroup">
            `;
            $("#taskList").html(task);
            let taskList = "";
            for (let i = 0; i < result.data.length; i++) {
                if (result.data[i].ProjectId == id) {
                    if (result.data[i].IsCompleted == true) {
                        taskList +=
                            `
                        <li class="list-group-item">
                            <input type="checkbox" aria-label="Checkbox for following text input" id="checkbox${i}" checked>
                            <s>${result.data[i].Name}</s>
                        </li>
                        `
                    }
                    if (result.data[i].IsCompleted == false) {
                        taskList +=
                            `
                        <li class="list-group-item">
                            <input type="checkbox" aria-label="Checkbox for following text input" id="checkbox${i}">
                            ${result.data[i].Name}
                        </li>
                        `
                    }
                }


            }
            taskList +=
                `
                    </div>
                </div>
            </ul>   
            `
            $("#listGroup").html(taskList);

            for (let i = 0; i < result.data.length; i++) {

                $(function () {
                    $(`#checkbox${i}`).on('change', function (e) {

                        if ($(this).is(':checked')) {
                            let check = true;
                            let obj = {};
                            obj.TaskId = result.data[i].TaskId;
                            obj.RoleUserTaskId = result.data[i].RoleUserTaskId;
                            obj.ProjectId = result.data[i].ProjectId;
                            obj.CategoryId = result.data[i].CategoryId;
                            obj.Name = result.data[i].Name;
                            obj.Description = result.data[i].Description;
                            obj.DueDate = result.data[i].DueDate;
                            obj.IsCompleted = check;
                            console.log(obj);
                            console.log(obj.DueDate);
                            $.ajax({
                                url: "https://localhost:44335/task/editjson",
                                type: "put",
                                dataType: "json",
                                data: obj,
                                beforeSend: data => {
                                    data.setRequestHeader("RequestVerificationToken", $("[name='__RequestVerificationToken']").val());
                                },
                                success: function (data) {
                                    $("#tableProject").DataTable().ajax.reload();
                                    $("#detailProject").modal('hide'),
                                        swal(
                                            "Success!",
                                            `${obj.Name} is done !`,
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
                        } else {
                            let check = false;
                            let obj = {};
                            obj.TaskId = result.data[i].TaskId;
                            obj.RoleUserTaskId = result.data[i].RoleUserTaskId;
                            obj.ProjectId = result.data[i].ProjectId;
                            obj.CategoryId = result.data[i].CategoryId;
                            obj.Name = result.data[i].Name;
                            obj.Description = result.data[i].Description;
                            obj.DueDate = result.data[i].DueDate;
                            obj.IsCompleted = check;
                            $.ajax({
                                url: "https://localhost:44335/task/editjson",
                                type: "put",
                                dataType: "json",
                                data: obj,
                                beforeSend: data => {
                                    data.setRequestHeader("RequestVerificationToken", $("[name='__RequestVerificationToken']").val());
                                },
                                success: function (data) {
                                    $("#tableProject").DataTable().ajax.reload();
                                    $("#detailProject").modal('hide'),
                                        swal(
                                            "Success!",
                                            `${obj.Name} is not done yet !`,
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
                    });
                });

            }
        })
    });

}