$(function () {
    $("#txtCustomer").autocomplete({
        source: function (request, response) {

            $.ajax({
                url: '/MemberAccount/AutoCompleteAccount/',
                data: "{ 'term': '" + request.term + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data,
                        function(item) {
                            return item;
                        }));
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (e, i) {
            $("#MemberAccountId").val(i.item.val);
        },
        minLength: 3
    });
});


$(function () {
    $("#txtMember").autocomplete({
        source: function (request, response) {

            $.ajax({
                url: '/MemberAccount/AutoCompleteLinkMember/',
                data: "{ 'term': '" + request.term + "'}",
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    response($.map(data,
                        function(item) {
                            return item;
                        }));
                },
                error: function (response) {
                    alert(response.responseText);
                },
                failure: function (response) {
                    alert(response.responseText);
                }
            });
        },
        select: function (e, i) {
            $("#MemberId").val(i.item.val);
        },
        minLength: 3
    });
});

    //$(document).ready(function () {
    //    $("#Country").autocomplete({
    //        source: function(request,response) {
    //            $.ajax({
    //                url: "/Home/AutoCompleteCountry",
    //                type: "POST",
    //                dataType: "json",
    //                data: { term: request.term },
    //                success: function(data) {
    //                    response($.map(data,
    //                        function(item) {
    //                            return { label: item.FirstName, value: item.FirstName };
    //                        }));


    //                }
    //            });
    //        },
    //        messages: {
    //            noResults: "", results: ""
    //        }
    //    });
    //})
