$.ajax({
    url: "https://localhost:44335/project/GetJSON"
}).done((result) => {
    let totalProject =
        `
    <h5 class="card-title text-uppercase text-muted mb-0">Total Project</h5>
    <span class="h2 font-weight-bold mb-0">${result.data.length}</span>
    `;
    $("#totalProject").html(totalProject);
})