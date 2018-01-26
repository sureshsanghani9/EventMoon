var countDownDate, defaultOpts, $pagination;

$(document).ready(function () {

    if ($(document).find("title").text() == "Home")
    {
        OpenMobileAppDownloadPopup();
        getUserGeoLocation();
    }

    if ($(document).find("title").text() == "Home" || $(document).find("title").text() == "Details")
    {
        // Set the date we're counting down to
        countDownDate = new Date($("#Startdate").val()).getTime();
        StartTimer(countDownDate);
    }

    if ($(document).find("title").text() == "Home" || $(document).find("title").text() == "Events") {
        
    }

    $("#btnFindEvent").on("click", function () {
        var MainCategoryId = $("#MainCatID").val();
        var Keyword = $("#keyword").val();
        var startdate = $("#starttime").val();
        $('.pageLoader').addClass("active");
        $.ajax({
            url: '/Home/LatestEventList',
            type: 'POST',
            cache: false,
            data: { MainCategoryId: MainCategoryId, Keyword: Keyword, startdate: startdate }
        }).done(function (result) {
            $('#divLatestEventsList').html(result);
            $('.pageLoader').removeClass("active");
        });
    });

    $("#btnSearchEvent").on("click", function () {
        $("#hdnCurrentPage").val("1");
        RefreshEventPage();
    });

    $(".sort li").on("click", function () {
        if (!$(this).hasClass("active")) {
            var activeLi = $(".sort li.active");
            var inActiveLi = $(".sort li").not(".active");

            activeLi.removeClass("active");
            inActiveLi.addClass("active");
            $("#hdnCurrentPage").val("1");
            RefreshEventPage();
        }

    });

    if ($(document).find("title").text() == "Events") {
        defaultOpts = {
            initiateStartPageClick: false,
            totalPages: 1,
            visiblePages: 5,
            onPageClick: function (event, page) {
                $("#hdnCurrentPage").val(page);
                RefreshEventPage();
            }
        };
        $pagination = $('#EventsPagination');
        $pagination.twbsPagination(defaultOpts);
        RefreshEventPage();
    }


});

function StartTimer(countDownDate) {
    // Update the count down every 1 second
    var x = setInterval(function () {

        // Get todays date and time
        var now = new Date().getTime();

        // Find the distance between now an the count down date
        var distance = countDownDate - now;
        //debugger;
        // Time calculations for days, hours, minutes and seconds
        var days = Math.floor(distance / (1000 * 60 * 60 * 24));
        var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((distance % (1000 * 60)) / 1000);


        // If the count down is finished, write some text 
        if (distance < 0) {
            clearInterval(x);
            //document.getElementById("demo").innerHTML = "EXPIRED";
            $("#startdays").html("00");
            $("#starthours").html("00");
            $("#startminutes").html("00");
            $("#startseconds").html("00");
        }
        else {
            $("#startdays").html(days);
            $("#starthours").html(hours);
            $("#startminutes").html(minutes);
            $("#startseconds").html(seconds);
        }
    }, 1000);
}

function RefreshEventPage() {
    var MainCategoryId = $("#MainCatID").val();
    var Keyword = $("#keyword").val();
    var PageNumber = $("#hdnCurrentPage").val();
    var PageSize = $("#hdnPageSize").val();
    var Sort = $(".sort li.active").attr("sort");

    $("#lblSearchFor").html($("#MainCatID option:selected").text());
    $('.pageLoader').addClass("active");
    $.ajax({
        url: '/Event/EventList',
        type: 'POST',
        cache: false,
        data: { MainCategoryId: MainCategoryId, Keyword: Keyword, PageNumber: PageNumber, PageSize: PageSize, Sort: Sort }
    }).done(function (result) {
        $('#divEventPage').html(result);
        var totalRecords = parseInt($("#hdnTotalRecords").val());
        var pageSize = parseInt($("#hdnPageSize").val());
        var totalPages = Math.ceil(totalRecords / pageSize);
        var currentPage = $pagination.twbsPagination('getCurrentPage');
        if ($("#hdnCurrentPage").val() == "1") {
            currentPage = 1;
        }
        $pagination.twbsPagination('destroy');
        $pagination.twbsPagination($.extend({}, defaultOpts, {
            initiateStartPageClick: false,
            startPage: currentPage,
            totalPages: totalPages,
            visiblePages: 5,
            onPageClick: function (event, page) {
                $("#hdnCurrentPage").val(page);
                RefreshEventPage();
            }
        }));
        getUserGeoLocation();
        $('.pageLoader').removeClass("active");
    });
}

function OpenMobileAppDownloadPopup()
{
    var uagent = navigator.userAgent.toLowerCase();
    if (uagent.search("iphone") > -1)
    {
        $("#appDownloadIOS2").modal('show');
    }
    else if (uagent.search("android") > -1)
    {
        $("#appDownloadAndriod2").modal('show');
    }
}

function getUserGeoLocation() {
    // Try HTML5 geolocation.
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            var pos = {
                lat: position.coords.latitude,
                lng: position.coords.longitude
            };

            $("#hdnUserLatitude").val(pos.lat);
            $("#hdnUserLongitude").val(pos.lng);

            resolveDistanceForEvent();
            
        }, function () {
            //error
        });
    }
}

function resolveDistanceForEvent() {
    var ulat = $("#hdnUserLatitude").val();
    var ulong = $("#hdnUserLongitude").val();
    $.each($(".evtBlock"), function (i, val) {
        var evt = $(this);
        var elat = $(evt).find("#hdnLatitude").val();
        var elong = $(evt).find("#hdnLongitude").val();
        $(evt).find("#spanDistance").html(parseFloat(calcCrow(ulat, ulong, elat, elong)).toFixed("0.00"));
    });
}

//This function takes in latitude and longitude of two location and returns the distance between them as the crow flies (in km)
function calcCrow(lat1, lon1, lat2, lon2) {
    var R = 6371; // km
    var dLat = toRad(lat2 - lat1);
    var dLon = toRad(lon2 - lon1);
    var lat1 = toRad(lat1);
    var lat2 = toRad(lat2);

    var a = Math.sin(dLat / 2) * Math.sin(dLat / 2) +
        Math.sin(dLon / 2) * Math.sin(dLon / 2) * Math.cos(lat1) * Math.cos(lat2);
    var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    var d = R * c;
    return d;
}

// Converts numeric degrees to radians
function toRad(Value) {
    return Value * Math.PI / 180;
}