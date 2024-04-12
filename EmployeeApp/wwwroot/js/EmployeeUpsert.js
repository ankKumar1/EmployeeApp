$(document).ready(function () {
    $('#employee-form').submit(function (event) {
        event.preventDefault();
        var employee = {
            Id: $('#employee-id').val() === "" ? 0 : $('#employee-id').val(),
            Name: $('#employee-name').val(),
            Email: $('#employee-email').val(),
            Phone: $('#employee-phone').val(),
            Salary: $('#employee-salary').val(),
            Address: $('#employee-address').val(),
            DepartmentId: $('#employee-departmentId').val()
        };

        console.log(employee);

        var url = employee.Id == 0 ? '/employee/create' : '/employee/update/' + employee.Id;
        var method = employee.Id == 0 ? 'POST' : 'PUT';

        $.ajax({
            url: url,
            type: method,
            contentType: 'application/json',
            data: JSON.stringify(employee),
            success: function (response) {
                window.location.href = '/employee/index';
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error('Request failed: ' + textStatus, errorThrown);
            }
        });
    });
});