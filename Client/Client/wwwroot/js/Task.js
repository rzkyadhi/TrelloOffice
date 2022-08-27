// $(document).ready(() => {
//     let table = $('#tableTask').DataTable({
//         "language": {
//             "paginate": {
//                 "previous": "<i class='ni ni-bold-left'></i>",
//                 "next": "<i class='ni ni-bold-right'></i>"
//             }
//         },
//         columnDefs: [{
//                 orderable: false,
//                 targets: -1
//             },
//             {
//                 className: 'text-center',
//                 targets: [0, 1, 2, 3, 4, 5]
//             },
//         ],
//         "ajax": {
//             "url": "https://localhost:44335/taskuser/GetJSON",
//             "dataSrc": function (json) {
//                 let jsonFiltered = [];
//                 for (let i = 0; i < json.data.length; i++) {
//                     if (json.data[i].UserId == $("#sessionUserId").val()) {
//                         jsonFiltered.push(json.data[i]);
//                     }
//                 }
//                 return jsonFiltered;
//             },
//         },
//         "columns": [{
//                 "data": "UserId",
//                 render: function (data, type, row, meta) {
//                     return meta.row + 1;
//                 }
//             },
//             {
//                 "data": "task.category.Name",
//                 render: function (data, type, row) {
//                     return row.task.category['Name'] == "Priority" ?
//                         `<span class='badge-lg badge-pill badge-danger'>${row.task.category['Name']}</span>` :
//                         `<span class='badge-lg badge-pill badge-info'>${row.task.category['Name']}</span>`;

//                 },
//             },
//             {
//                 "data": "task.Name"
//             },
//             {
//                 "data": "task.Description"
//             },
//             {
//                 "data": "task.project.Name"
//             },
//             {
//                 "data": "task.DueDate",
//                 render: function (data, type, row) {
//                     return row.task['IsCompleted'] == true ?
//                         `<span class='badge-lg badge-pill badge-success'>${row.task['DueDate']}</span>` :
//                         `<span class='badge-lg badge-pill badge-warning'>${row.task['DueDate']}</span>`

//                 },
//             },
//             {
//                 /*"data": "task.IsCompleted",*/
//                 render: function (data, type, row) {
//                     return row.task['IsCompleted'] == true ?
//                         `<button type="button" onclick="detailTask(${row['TaskId']})" class="btn btn-success">
//                     Finished Task
//                 </button>` :
//                         `<button type="button" onclick="detailTask(${row['TaskId']})" class="btn btn-warning">
//                     Unfinished Task
//                 </button>`
//                 }
//             }
//         ]
//     });
// })

$.ajax({
    url: 'https://localhost:44335/taskuser/GetJSON',
    type: 'get'
}).done((result) => {
    console.log(result.data);
    // Hashmap for total Task per Project, Keys is ProjectId and Value is Total Task!
    const hashProjectTask = {};
    let hashTask = {};
    for (let i = 0; i < result.data.length; i++) {
        if (result.data[i].UserId == $("#sessionUserId").val()) {
            if (Object.keys(hashProjectTask).length == 0) {
                hashProjectTask[result.data[i].task.project.ProjectId] = [];
                hashTask["TaskId"] = result.data[i].TaskId;
                hashTask["Name"] = result.data[i].task.Name;
                hashTask["Description"] = result.data[i].task.Description;
                hashTask["DueDate"] = result.data[i].task.DueDate;
                hashTask["CategoryName"] = result.data[i].task.category.Name;
                hashTask["IsCompleted"] = result.data[i].task.IsCompleted;
                hashTask["ProjectName"] = result.data[i].task.project.Name;
                hashTask["ProjectDescription"] = result.data[i].task.project.Description;
                hashProjectTask[result.data[i].task.project.ProjectId]
                .push(hashTask);
                hashTask = {};
                i++;
                if (i == result.data.length) break;
            }

            if (result.data[i].task.project.ProjectId in hashProjectTask) {
                hashTask["TaskId"] = result.data[i].TaskId;
                hashTask["Name"] = result.data[i].task.Name;
                hashTask["Description"] = result.data[i].task.Description;
                hashTask["DueDate"] = result.data[i].task.DueDate;
                hashTask["CategoryName"] = result.data[i].task.category.Name;
                hashTask["IsCompleted"] = result.data[i].task.IsCompleted;
                hashTask["ProjectName"] = result.data[i].task.project.Name;
                hashTask["ProjectDescription"] = result.data[i].task.project.Description;
                hashProjectTask[result.data[i].task.project.ProjectId]
                .push(hashTask);
                hashTask = {};
            }

            if (result.data[i].task.project.ProjectId in hashProjectTask == false) {
                hashProjectTask[result.data[i].task.project.ProjectId] = [];
                hashTask["TaskId"] = result.data[i].TaskId;
                hashTask["Name"] = result.data[i].task.Name;
                hashTask["Description"] = result.data[i].task.Description;
                hashTask["DueDate"] = result.data[i].task.DueDate;
                hashTask["CategoryName"] = result.data[i].task.category.Name;
                hashTask["IsCompleted"] = result.data[i].task.IsCompleted;
                hashTask["ProjectName"] = result.data[i].task.project.Name;
                hashTask["ProjectDescription"] = result.data[i].task.project.Description;
                hashProjectTask[result.data[i].task.project.ProjectId]
                .push(hashTask);
                hashTask = {};
            }
        }
    }

    // Card DOM Manipulation
    let containerHtml = "";
    let totalProject = Object.keys(hashProjectTask).length;
    for (let i = 0; i < Object.keys(hashProjectTask).length; i++) {
        if (Object.values(hashProjectTask)[i].length > 1) {
            containerHtml += 
            `
            <div class="col-lg-4">
                <div class="card">
                    <div class="card-body">
			            <h5 class="card-title">${Object.values(hashProjectTask)[i][0].ProjectName}</h5>
			            <p class="card-text">${Object.values(hashProjectTask)[i][0].Description}</p>
			            <a href="" class="btn btn-primary" data-toggle="modal" data-target="#detailTask">Details</a>
		            </div>
		        </div>
            </div>
            
            `
        }
        if (Object.values(hashProjectTask)[i].length == 1) {
            containerHtml += 
            `
            <div class="col-lg-4">
                <div class="card">
                    <div class="card-body">
			            <h5 class="card-title">${Object.values(hashProjectTask)[i][0].ProjectName}</h5>
			            <p class="card-text">${Object.values(hashProjectTask)[i][0].Description}</p>
			            <a href="" class="btn btn-primary" data-toggle="modal" data-target="#detailTask">Details</a>
		            </div>
		        </div>
            </div>
            `
        }
    }
    $('#containerDOM').html(containerHtml);
    console.log(hashProjectTask);
})
// function detailTask(id) {
//     $.ajax({
//         url: `https://localhost:44335/task/getjsonbyid/${id}`,
//         type: 'get'
//     }).done((result) => {
//         let check = "";
//         let text = "";
//         let obj = {};
//         obj.TaskId = id;
//         obj.ProjectId = result.data.ProjectId;
//         obj.CategoryId = result.data.CategoryId;
//         obj.Name = result.data.Name;
//         obj.Description = result.data.Description;
//         obj.DueDate = result.data.DueDate;
//         if (result.data.IsCompleted == true) {
//             check = false;
//             text = `${obj.Name} will marked as not done`
//         } 
//         if (result.data.IsCompleted == false) {
//             check = true;
//             text = `${obj.Name} will marked as done`
//         }
//         obj.IsCompleted = check;
//         swal({
//             title: "Are you sure?",
//             text: text,
//             buttons: {
//                 cancel: true,
//                 confirm: true,
//                 closeModal: false
//             },
//         }).then((isConfirm) => {
//             if (isConfirm === true) {
//                 $.ajax({
//                     url: "https://localhost:44335/task/editjson",
//                     type: "put",
//                     dataType: "json",
//                     data: obj,
//                     beforeSend: data => {
//                         data.setRequestHeader("RequestVerificationToken", $("[name='__RequestVerificationToken']").val());
//                     },
//                     success: () => {
//                         $("#tableTask").DataTable().ajax.reload();
//                         swal(
//                             "Success",
//                             `${obj.Name} has been changed`,
//                             "success"
//                         )
//                     },
//                     failure: () => {
//                         swal(
//                             "Internal Server Error",
//                             `Oops, ${obj.Name} was not saved`,
//                             "error"
//                         )
//                     }
//                 })
//             }
//         })
//     });

// }