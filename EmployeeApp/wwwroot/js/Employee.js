$(document).ready(function () {
    fetchEmployeeData();
});

function fetchEmployeeData() {
    $.ajax({
        url: '/employee/getall',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            var tbody = $('#employee-data');
            tbody.empty(); // Clear existing table data

            $.each(data, function (index, employee) {
                var row = '<tr>' +
                    '<td>' + employee.id + '</td>' +
                    '<td>' + employee.name + '</td>' +
                    '<td>' + employee.email + '</td>' +
                    '<td>' + employee.phone + '</td>' +
                    '<td>' + employee.salary + '</td>' +
                    '<td>' + employee.address + '</td>' +
                    '<td>' + employee.department.name + '</td>' +
                    '<td>' +
                    '<a href="/employee/upsert?id=' + employee.id + '" class="btn btn-primary mx-2" data-id="' + employee.id + '">Edit</a>' +
                    '<a onClick = Delete('+employee.id+') class="btn btn-danger">Delete</button>' +
                    '</td>' +
                    '</tr>';
                tbody.append(row);
            });
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.error('Request failed: ' + textStatus, errorThrown);
        }
    });
}


function Delete(id) {
    if (confirm("Are you sure you want to delete this employee?")) {
        $.ajax({
            url: '/employee/delete/'+id,
            type: 'DELETE',
            success: function () {
                fetchEmployeeData();
            }
        });
    }
}