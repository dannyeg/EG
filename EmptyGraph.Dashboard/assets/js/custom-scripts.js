/*------------------------------------------------------
    Author : www.webthemez.com
    License: Commons Attribution 3.0
    http://creativecommons.org/licenses/by/3.0/
---------------------------------------------------------  */

(function ($) {
    "use strict";
    var mainApp = {

        initFunction: function () {
            /*MENU 
            ------------------------------------*/
            $('#main-menu').metisMenu();
			
            $(window).bind("load resize", function () {
                if ($(this).width() < 768) {
                    $('div.sidebar-collapse').addClass('collapse')
                } else {
                    $('div.sidebar-collapse').removeClass('collapse')
                }
            });

            /* Analytics Demographic(Age) Chart
			-----------------------------------------*/
            Morris.Bar({
                element: 'analytics-demoAge-chart',
                data: [{
                    y: '12-17',
                    a: 100,
                    b: 50,
                    c: 25
                }, {
                    y: '18-24',
                    a: 75,
                    b: 65,
                    c: 50
                }, {
                    y: '25-34',
                    a: 50,
                    b: 40,
                    c: 150
                }, {
                    y: '35-44',
                    a: 75,
                    b: 65,
                    c: 225
                }, {
                    y: '45-54',
                    a: 50,
                    b: 40,
                    c: 10
                }, {
                    y: '55-64',
                    a: 75,
                    b: 65,
                    c: 25
                }, {
                    y: '65+',
                    a: 10,
                    b: 20,
                    c: 30
                }],
                xkey: 'y',
                ykeys: ['a', 'b', 'c'],
                labels: ['Female', 'Male', 'Unknown'],
                hideHover: 'auto',
                resize: true
            });

            /* Analytics Demographic(Age) Chart
			----------------------------------------*/

            /* Analytics Timezone Chart
			----------------------------------------*/
            Morris.Donut({
                element: 'analytics-timezone-chart',
                data: [{
                    label: "Central Time (US & Canada)",
                    value: 785
                }, 
                {
                    label: "Mountain Time (US & Canada)",
                    value: 225
                },
                {
                    label: "Eastern Time (US & Canada)",
                    value: 447
                }, 
                {
                    label: "Pacific Time (US & Canada)",
                    value: 120
                }
                ],
                resize: true
            });

            /* Analytics Timezone Chart
			----------------------------------------*/


            /* Analytics Login Frequency
			----------------------------------------*/
            Morris.Line({
                element: 'analytics-loginFrequency-chart',
                data: [{
                    y: '2006',
                    a: 100,
                    b: 90
                }, {
                    y: '2007',
                    a: 75,
                    b: 65
                }, {
                    y: '2008',
                    a: 50,
                    b: 40
                }, {
                    y: '2009',
                    a: 75,
                    b: 65
                }, {
                    y: '2010',
                    a: 50,
                    b: 40
                }, {
                    y: '2011',
                    a: 75,
                    b: 65
                }, {
                    y: '2012',
                    a: 100,
                    b: 90
                }],
                xkey: 'y',
                ykeys: ['a', 'b'],
                labels: ['Female', 'Male'],
                hideHover: 'auto',
                resize: true
            });

			            /* Analytics Demographic(Age) Chart
			-----------------------------------------*/
            Morris.Bar({
                element: 'analytics-categoryComp-chart',
                data: [{
                    y: 'Nike',
                    a: 100,
                    b: 500,
                }, {
                    y: 'Puma',
                    a: 750,
                    b: 650,
                }, {
                    y: 'Adidas',
                    a: 250,
                    b: 400,
                }, {
                    y: 'sneakerhead.com',
                    a: 75,
                    b: 65,
                }, {
                    y: 'Zappos',
                    a: 100,
                    b: 200,
                }],
                xkey: 'y',
                ykeys: ['a', 'b'],
                labels: ['Female', 'Male'],
                hideHover: 'auto',
                resize: true
            });

            /* Analytics Demographic(Age) Chart
			----------------------------------------*/


            /* MORRIS BAR CHART
			-----------------------------------------*/
            Morris.Bar({
                element: 'morris-bar-chart',
                data: [{
                    y: '2006',
                    a: 100,
                    b: 100,
                    c: 90
                }, {
                    y: '2007',
                    a: 75,
                    b: 65
                }, {
                    y: '2008',
                    a: 50,
                    b: 40
                }, {
                    y: '2009',
                    a: 75,
                    b: 65
                }, {
                    y: '2010',
                    a: 50,
                    b: 40
                }, {
                    y: '2011',
                    a: 75,
                    b: 65
                }, {
                    y: '2012',
                    a: 100,
                    b: 90
                }],
                xkey: 'y',
                ykeys: ['a', 'b', 'c'],
                labels: ['Series A', 'Series B'],
                hideHover: 'auto',
                resize: true
            });

            /* MORRIS DONUT CHART
			----------------------------------------*/
            Morris.Donut({
                element: 'morris-donut-chart',
                data: [{
                    label: "Download Sales",
                    value: 12
                }, {
                    label: "In-Store Sales",
                    value: 30
                }, {
                    label: "Mail-Order Sales",
                    value: 20
                }],
                resize: true
            });

            /* MORRIS AREA CHART
			----------------------------------------*/

            Morris.Area({
                element: 'morris-area-chart',
                data: [{
                    period: '2010 Q1',
                    iphone: 2666,
                    ipad: null,
                    itouch: 2647
                }, {
                    period: '2010 Q2',
                    iphone: 2778,
                    ipad: 2294,
                    itouch: 2441
                }, {
                    period: '2010 Q3',
                    iphone: 4912,
                    ipad: 1969,
                    itouch: 2501
                }, {
                    period: '2010 Q4',
                    iphone: 3767,
                    ipad: 3597,
                    itouch: 5689
                }, {
                    period: '2011 Q1',
                    iphone: 6810,
                    ipad: 1914,
                    itouch: 2293
                }, {
                    period: '2011 Q2',
                    iphone: 5670,
                    ipad: 4293,
                    itouch: 1881
                }, {
                    period: '2011 Q3',
                    iphone: 4820,
                    ipad: 3795,
                    itouch: 1588
                }, {
                    period: '2011 Q4',
                    iphone: 15073,
                    ipad: 5967,
                    itouch: 5175
                }, {
                    period: '2012 Q1',
                    iphone: 10687,
                    ipad: 4460,
                    itouch: 2028
                }, {
                    period: '2012 Q2',
                    iphone: 8432,
                    ipad: 5713,
                    itouch: 1791
                }],
                xkey: 'period',
                ykeys: ['iphone', 'ipad', 'itouch'],
                labels: ['iPhone', 'iPad', 'iPod Touch'],
                pointSize: 2,
                hideHover: 'auto',
                resize: true
            });

            /* MORRIS LINE CHART
			----------------------------------------*/
            Morris.Line({
                element: 'morris-line-chart',
                data: [{
                    y: '2006',
                    a: 100,
                    b: 90
                }, {
                    y: '2007',
                    a: 75,
                    b: 65
                }, {
                    y: '2008',
                    a: 50,
                    b: 40
                }, {
                    y: '2009',
                    a: 75,
                    b: 65
                }, {
                    y: '2010',
                    a: 50,
                    b: 40
                }, {
                    y: '2011',
                    a: 75,
                    b: 65
                }, {
                    y: '2012',
                    a: 100,
                    b: 90
                }],
                xkey: 'y',
                ykeys: ['a', 'b'],
                labels: ['Series A', 'Series B'],
                hideHover: 'auto',
                resize: true
            });
           
     
        },

        initialization: function () {
            mainApp.initFunction();

        }

    }
    // Initializing ///

    $(document).ready(function () {
        mainApp.initFunction();
    });

}(jQuery));
