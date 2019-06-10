angular.module('DirectoryApp', [])
    .controller('personController', function ($scope, $http) {
        $scope.isVisible = false;
        $scope.searchFail = false;
        $scope.addSuccess = false;
        $scope.sortField = 'LastName';

        $scope.addPerson = function () {
            var Person = {
                FirstName: $scope.add.firstName,
                LastName: $scope.add.lastName,
                Address: $scope.add.address,
                City: $scope.add.city,
                State: $scope.add.state,
                Zip: $scope.add.zip
            };

            if (Person.FirstName && Person.LastName && Person.Address)
            {
                $http.post("/api/person", Person).then(function () {
                    $scope.add.firstName = "";
                    $scope.add.lastName = "";
                    $scope.add.address = "";
                    $scope.add.city = "";
                    $scope.add.state = "";
                    $scope.add.zip = "";
                    $scope.addSuccess = true;
                });
            } 
        }

        $scope.addChange = function () {
            $scope.addSuccess = false;
        }

        $scope.searchPerson = function () {
            var Person = {
                FirstName: $scope.person.firstName,
                LastName: $scope.person.lastName,
                Address: $scope.person.address,
            };

            var sUrl = "/api/person/search?";
            var sQuery = "";
            if (Person.FirstName)
            {
                sQuery = "firstName=" + Person.FirstName;
            }   
            if (Person.LastName)
            {
                if (sQuery)
                    sQuery += "&";
                sQuery += "lastName=" + Person.LastName;
            }
            if (Person.Address)
            {
                if (sQuery)
                    sQuery += "&";
                sQuery += "address=" + Person.Address;
            }

            if (!Person.FirstName && !Person.LastName && !Person.Address)
            {
                $scope.isVisible = false;
            }   
            else
            {
                $http.get(sUrl + sQuery).then(function (res) {
                    $scope.people = res.data;
                    if ($scope.people.length > 0)
                        $scope.isVisible = true;
                    else
                    {
                        $scope.isVisible = false;
                        $scope.person.firstName = "";
                        $scope.person.lastName = "";
                        $scope.person.address = "";
                        $scope.searchFail = true;
                    }    
                })
            }
        }

        $scope.searchChange = function () {
            $scope.searchFail = false;
        }
    })