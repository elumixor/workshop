﻿var workshopIS = angular.module("workshopIS", ["ngRoute"]);

workshopIS.directive("tester",function () {
        return {
            require: "ngModel",
            link: function (scope, elm, attrs, ctrl) {
                ctrl.$validators.tester = function (modelValue) {
                    this.regexs = [
                        { reg: /^(([a-z -'`A-Z]*)|([а-я -'`А-Я]*)|([a-z -'`A-ZěščřžýáíéúůĚŠČŘŽÝÁÍÉÚŮ]*))$/, key: "name" },
                        { reg: /^((\+[0-9][0-9][0-9][ -]?)?[0-9][0-9][0-9][ -]?[0-9][0-9][0-9][ -]?[0-9][0-9][0-9])$/, key: "phone" },
                        { reg: /^([a-zA-Z0-9.])*@([a-zA-Z])*\.([a-zA-Z])+$/, key: "email" },
                        { reg: /^ *$/, key: "empty" },
                        { reg: /^.*$/, key: "any" }
                    ];

                    if (ctrl.$isEmpty(modelValue)) {
                        return true;
                    }

                    switch (attrs.tester) {
                        case "amount":
                            return (modelValue >= 20000 && modelValue <= 500000 );
                        case "duration":
                            return (modelValue >= 6 && modelValue <= 96);
                        case "interest":
                            return (modelValue >= 0 && modelValue < 1);
                        case "phone":
                            return this.regexs.find(r => r.key === "phone").reg.test(modelValue);
                        case "email":
                            return this.regexs.find(r => r.key === "email").reg.test(modelValue);
                        case "name":
                            return this.regexs.find(r => r.key === "name").reg.test(modelValue);
                        default:
                            return false;
                    }
                }
            }
        }
});

workshopIS.filter("state",
    function() {
        return function(input) {
            input = input || 0;
            var out;
            switch (parseInt(input)) {
                case 0:
                    out = "Not contacted yet";
                    break;
                case 1:
                    out = "Could not make contact";
                    break;
                case 2:
                    out = "Contacted, confirmed";
                    break;
                case 3:
                    out = "Contacted, accepted";
                    break;
                default:
                    out = "Invalid contact state";
                    break;
            }
            return out;
        };
    });

workshopIS.service("HttpService", HttpService);
workshopIS.service("DataService", DataService);

workshopIS.controller("MainController", MainController);
workshopIS.controller("LoanController", LoanController);
workshopIS.controller("PartnersController", PartnersController);
workshopIS.controller("ReportsController", ReportsController);
workshopIS.controller("CallCentreController", CallCentreController);



var configFunction = function ($routeProvider) {
    $routeProvider.when("/loan",
        {
            templateUrl: "Loan",
            controller: "LoanController"
        })
        .when("/partners",
        {
            templateUrl: "Partners",
            controller: "PartnersController"
        })
        .when("/reports",
            {
                templateUrl: "Reports",
                controller: "ReportsController"
        })
        .when("/callcentre",
            {
                templateUrl: "CallCentre",
                controller: "CallCentreController"
            })
        .when("/:any*",
        {
            templateUrl: "NotFound"
        });
}
configFunction.$inject = ["$routeProvider"];

workshopIS.config(configFunction);