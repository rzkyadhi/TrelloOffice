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
                targets: [0, 1, 2, 3, 4, 5]
            },
        ],
        "ajax": {
            "url": "https://localhost:44335/taskuser/GetJSON",
            "dataSrc": function (json) {
                let jsonFiltered = [];
                for (let i = 0; i < json.data.length; i++) {
                    if (json.data[i].UserId == $("#sessionUserId").val()) {
                        jsonFiltered.push(json.data[i]);
                    }
                }
                return jsonFiltered;
            },
        },
        "columns": [{
                "data": "UserId",
                render: function (data, type, row, meta) {
                    return meta.row + 1;
                }
            },
            {
                "data": "task.category.Name",
                render: function (data, type, row) {
                    return row.task.category['Name'] == "Priority" ?
                        `<span class='badge-lg badge-pill badge-danger'>${row.task.category['Name']}</span>` :
                        `<span class='badge-lg badge-pill badge-info'>${row.task.category['Name']}</span>`;

                },
            },
            {
                "data": "task.Name"
            },
            {
                "data": "task.Description"
            },
            {
                "data": "task.project.Name"
            },
            {
                "data": "task.DueDate",
                render: function (data, type, row) {
                    return row.task['IsCompleted'] == true ?
                        `<span class='badge-lg badge-pill badge-success'>${row.task['DueDate']}</span>` :
                        `<span class='badge-lg badge-pill badge-warning'>${row.task['DueDate']}</span>`

                },
            },
            {
                /*"data": "task.IsCompleted",*/
                render: function (data, type, row) {
                    return row.task['IsCompleted'] == true ?
                        `<button type="button" onclick="detailTask(${row['TaskId']})" class="btn btn-success">
                    Finished Task
                </button>` :
                        `<button type="button" onclick="detailTask(${row['TaskId']})" class="btn btn-warning">
                    Unfinished Task
                </button>`
                }
            }
        ]
    });
})

function detailTask(id) {
    $.ajax({
        url: `https://localhost:44335/task/getjsonbyid/${id}`,
        type: 'get'
    }).done((result) => {
        let check = "";
        let text = "";
        let obj = {};
        obj.TaskId = id;
        obj.ProjectId = result.data.ProjectId;
        obj.CategoryId = result.data.CategoryId;
        obj.Name = result.data.Name;
        obj.Description = result.data.Description;
        obj.DueDate = result.data.DueDate;
        if (result.data.IsCompleted == true) {
            check = false;
            text = `${obj.Name} will marked as not done`
        } 
        if (result.data.IsCompleted == false) {
            check = true;
            text = `${obj.Name} will marked as done`
        }
        obj.IsCompleted = check;
        swal({
            title: "Are you sure?",
            text: text,
            buttons: {
                cancel: true,
                confirm: true,
                closeModal: false
            },
        }).then((isConfirm) => {
            if (isConfirm === true) {
                $.ajax({
                    url: "https://localhost:44335/task/editjson",
                    type: "put",
                    dataType: "json",
                    data: obj,
                    beforeSend: data => {
                        data.setRequestHeader("RequestVerificationToken", $("[name='__RequestVerificationToken']").val());
                    },
                    success: () => {
                        $("#tableTask").DataTable().ajax.reload();
                        swal(
                            "Success",
                            `${obj.Name} has been changed`,
                            "success"
                        )
                    },
                    failure: () => {
                        swal(
                            "Internal Server Error",
                            `Oops, ${obj.Name} was not saved`,
                            "error"
                        )
                    }
                })
            }
        })
    });

}