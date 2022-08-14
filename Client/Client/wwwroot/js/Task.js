$(document).ready(() => {
    let table = $('#tableTask').DataTable({
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
            "url": "https://localhost:44335/Task/GetJSON",
            "dataType": "json",
        },
        "columns": [{
                "data": "TaskId",
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            {
                "data": "RoleUserTaskId"
            },
            {
                "data": "project.Name"
            },
            {
                "data": "category.Name"
            },
            {
                "data": "Name"
            },
            {
                "data": "Description"
            },
            {
                "data": "DueDate"
            },
            {
                "data": "IsCompleted"
            },
            {
                // data: null,
                render: function (data, type, row) {

                    return `    
                                <button type="button" onclick="detailTask(${row['TaskId']})" data-toggle="modal" data-target="#detailTask" class="btn btn-success">
                                    Details
                                </button>
                                <button type="button" onclick="editTask(${row['TaskId']})" data-toggle="modal" data-target="#editTask" class="btn btn-warning">
                                    Edit
                                </button>
                                <button type="button" onclick="deleteTask(${row['TaskId']})" class="btn btn-danger">
                                    Delete
                                </button>`
                }
            }
        ],
    })
})

function addTask() {
    let createModalBody =
        `
        <div class="form" id="form-post">
            <div class="row">
            <div class="col-md-12">
                <label for="roleUser">Role User</label>
                <input asp-for="RoleUserTaskId" name="roleUser" type="text" class="form-control form-control-alternative"
                    id="roleUser" required>
                <div class="valid-feedback">
                    Looks good!
                </div>
                <div class="invalid-feedback">
                    Please Input Valid Role User!
                </div>
            </div>
            <div class="col-md-12">
                <label for="project">Project</label>
                <input asp-for="ProjectId" name="projectId" type="text" class="form-control form-control-alternative"
                    id="projectId" required>
                <div class="valid-feedback">
                    Looks good!
                </div>
                <div class="invalid-feedback">
                    Please Input Valid Project!
                </div>
            </div>
            <div class="col-md-12">
                <label for="category">Category</label>
                <input asp-for="categoryId" name="category" type="text" class="form-control form-control-alternative"
                    id="category" required>
                <div class="valid-feedback">
                    Looks good!
                </div>
                <div class="invalid-feedback">
                    Please Input Valid Category!
                </div>
            </div>
            <div class="col-md-12">
                <label for="taskName">Task Name</label>
                <input asp-for="Name" name="taskName" type="text" class="form-control form-control-alternative"
                    id="taskName" required>
                <div class="valid-feedback">
                    Looks good!
                </div>
                <div class="invalid-feedback">
                    Please Input Valid Task Name!
                </div>
            </div>
            <div class="col-md-12">
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
            <div class="col-md-12">
                <label for="DueDate">Due Date</label>
                <input asp-for="DueDate" name="dueDate" type="date" class="form-control form-control-alternative"
                    id="dueDate" required>
                <div class="valid-feedback">
                    Looks good!
                </div>
                <div class="invalid-feedback">
                    Please Input Valid Date!
                </div>
            </div>
            <div class="col-md-12">
                <input asp-for="IsCompleted" name="isComplete" type="hidden" class="form-control form-control-alternative"
                    id="isComplete" value="False" required>
                <div class="valid-feedback">
                    Looks good!
                </div>
                <div class="invalid-feedback">
                    Please Input Valid!
                </div>
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
                obj.roleUser = parseInt($("#roleUser").val());
                obj.project = parseInt($("#projectId").val());
                obj.category = parseInt($("#categoryId").val());
                obj.name = $("#taskName").val();
                obj.description = $("#description").val();
                obj.dueDate = parseDate($("#dueDate").val());
                obj.isCompleted = parseBoolean($("#isCompletes").val());
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
                            url: "https://localhost:44335/task/postjson",
                            type: "post",
                            dataType: "json",
                            data: obj,
                            beforeSend: data => {
                                data.setRequestHeader("RequestVerificationToken", $("[name='__RequestVerificationToken']").val());
                            },
                            success: () => {
                                $("#tableTask").DataTable().ajax.reload();
                                $("#addTask").modal('hide'),
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

function editTask(id) {
    $.ajax({
        url: `https://localhost:44335/task/getjsonbyid/${id}`,
        type: 'get'
    }).done((result) => {
        let editModalBody =
            `
        <div class="form-row">
            <div class="col-md-12">
                    <label for="TaskId">Task Id</label>
                    <input name="taskId" type="number" class="form-control form-control-alternative"
                        id="taskId" value=${result.data.TaskId} readonly required>
                    <div class="valid-feedback">
                        Looks good!
                    </div>
                </div>
            <div class="col-md-12">
                    <label for="RoleUserTaskId">Role User</label>
                    <input name="roleUserTaskId" type="number" class="form-control form-control-alternative"
                        id="RoleUserTaskId" value=${result.data.RoleUserTaskId} required>
                    <div class="valid-feedback">
                        Looks good!
                    </div>
                </div>
            <div class="col-md-12">
                <label for="project">Project</label>
                <input id="ProjectId" type="text" class="form-control form-control-alternative"
                         value="${result.data.ProjectId}" required>
                <div class="valid-feedback">
                    Looks good!
                </div>
            </div>
            <div class="col-md-12">
                <label for="category">Category</label>
                <input id="CategoryId" type="text" class="form-control form-control-alternative"
                         value="${result.data.CategoryId}" required>
                <div class="valid-feedback">
                    Looks good!
                </div>
            </div>
            <div class="col-md-12">
                <label for="name">Task Name</label>
                <input id="name" type="text" class="form-control form-control-alternative"
                         value="${result.data['Name']}" required>
                <div class="valid-feedback">
                    Looks good!
                </div>
            </div>
            <div class="col-md-12">
                <label for="description">Description</label>
                <input id="description" type="text" class="form-control form-control-alternative"
                         value="${result.data['Description']}" required>
                <div class="valid-feedback">
                    Looks good!
                </div>
            </div>
            <div class="col-md-12">
                <label for="dueDate">Due Date</label>
                <input id="DueDate" type="date" class="form-control form-control-alternative"
                         value="${result.data.DueDate}" required >
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
                <i class='ni ni-bag-17'></i>
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
                <i class='ni ni-align-left-2'></i>
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
            let projectId = [];
            let hashTaskProject = {};
            let hashTaskDoneProject = {};
            for (let i = 0; i < result.data.length; i++) {
                if (projectId.includes(result.data[i].ProjectId)) {
                    continue;
                } else {
                    projectId.push(result.data[i].ProjectId);
                }
            }
            for (let i = 0; i < projectId.length; i++) {
                hashTaskProject[projectId[i]] = 0;
                hashTaskDoneProject[projectId[i]] = 0;
            }
            for (let i = 0; i < result.data.length; i++) {
                if (result.data[i].IsCompleted == true) {
                    for (let y = 0; y < projectId.length; y++) {
                        if (Object.keys(hashTaskDoneProject)[y] == result.data[i].ProjectId) {
                            hashTaskDoneProject[Object.keys(hashTaskDoneProject)[y]] += 1;
                        }
                    }
                }
                for (let y = 0; y < projectId.length; y++) {
                    if (Object.keys(hashTaskProject)[y] == result.data[i].ProjectId) {
                        hashTaskProject[Object.keys(hashTaskProject)[y]] += 1;
                    }
                }
            }
            let task =
                `
                <i class='ni ni-check-bold'></i>
                    <label for="Task">Checklist Task</label>
                    <div class="progress-wrapper">
                        <div class="progress-info">
                            <div class="progress-label">
                            <span>Task completed</span>
                            </div>
                            <div class="progress-percentage" id="progressPercent">
                            
                            </div>
                        </div>
                        <div class="progress" id="progressBar">
                            
                        </div>
                        </div>
                        <ul class="list-group">
                            <div class="row">
                                <div class="col" id="listGroup">
            `;
            $("#taskList").html(task);
            let progressPercent = "";
            let progressBar = "";
            for (let i = 0; i < result.data.length; i++) {
                
                if (projectId[i] == id) {
                    progressPercent += 
                    `
                    <span>${Math.floor((Object.values(hashTaskDoneProject)[i] / Object.values(hashTaskProject)[i])*100)}%</span>
                    `
                    progressBar +=
                    `
                    <div class="progress-bar bg-success" role="progressbar" aria-valuenow="${Math.floor((Object.values(hashTaskDoneProject)[i] / Object.values(hashTaskProject)[i])*100)}" aria-valuemin="0" aria-valuemax="100" style="width: ${Math.floor((Object.values(hashTaskDoneProject)[i] / Object.values(hashTaskProject)[i])*100)}%;"></div>
                    `
                }
            }
            $("#progressPercent").html(progressPercent);
            $("#progressBar").html(progressBar);
            let taskList = "";
            for (let i = 0; i < result.data.length; i++) {
                if (result.data[i].ProjectId == id) {
                    if (result.data[i].IsCompleted == true) {
                        taskList +=
                            `
                        <li class="list-group-item">
                            <input type="checkbox" aria-label="Checkbox for following text input" id="checkbox${i}" checked>
                            <s>${result.data[i].Name}</s>
                            <span class="badge badge-pill badge-success">${result.data[i].DueDate}</span>
                        </li>
                        `
                    }
                    if (result.data[i].IsCompleted == false) {
                        taskList +=
                            `
                        <li class="list-group-item">
                            <input type="checkbox" aria-label="Checkbox for following text input" id="checkbox${i}">
                            ${result.data[i].Name}
                            <span class="badge badge-pill badge-danger">${result.data[i].DueDate}</span>
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
                                    $("#detailProject").modal('hide');
                                    swal({
                                        title: "Success!",
                                        text: `${obj.Name} is not done yet !`,
                                        timer: 1000
                                    })
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