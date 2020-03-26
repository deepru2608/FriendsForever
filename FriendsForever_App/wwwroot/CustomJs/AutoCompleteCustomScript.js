$(document).ready(function () {
    var availableTags = [
        "Male",
        "Female",
        "Transgender"
    ];

    $('#exampleGender').autocomplete({
        source: availableTags
    });

    $('#exampleCourtry').autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Account/BindCountry",
                type: "POST",
                dataType: "json",
                data: { Prefix: request.term },
                success: function (data) {
                    response($.map(data, function (item) {
                        return { value: item.countryList };
                    }))
                }
            })
        },
        messages: {
            noResults: "", results: ""
        }
    });
});