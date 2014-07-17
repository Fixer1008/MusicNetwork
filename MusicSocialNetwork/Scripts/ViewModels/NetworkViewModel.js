
function LoadViewModel(isAuth) {
    var isAuthFlag;

    if (isAuth == "True") {
        isAuthFlag = true;
    } else {
        isAuthFlag = false;
    }

    function User(data) {
        this.UserName = ko.observable(data.newUserName);
        this.Pass = ko.observable(data.newUserPassword);
    }

    function NetworkViewModel() {
        var self = this;

        self.newUser = {
            newUserName: ko.observable(),
            newUserPassword: ko.observable(),
            isVisibleForm: ko.observable(!isAuthFlag),
            isVisibleName: ko.observable(isAuthFlag),
            Login: function () {
                var user = new User(self.newUser);
                $.ajax({
                    url: '../Account/Login',
                    type: 'POST',
                    data: ko.toJSON(user),
                    contentType: "application/json",
                    success: function (data) {
                        if (data == "True") {
                            self.newUser.isVisibleForm(false);
                            self.newUser.isVisibleName(true);
                        } else {
                            self.newUser.isVisibleForm(true);
                            self.newUser.isVisibleName(false);
                            self.newUser.newUserName('');
                            self.newUser.newUserPassword('');
                        }
                    },
                });
            },
            Logoff: function () {
                $.ajax({
                    url: '../Account/LogOff',
                    type: 'GET',
                    success: function () {
                            self.newUser.isVisibleForm(true);
                            self.newUser.isVisibleName(false);
                            self.newUser.newUserName('');
                            self.newUser.newUserPassword('');
                    },
                });
            }
        }
    }

    var newtworkViewModel = new NetworkViewModel();
    pager.Href.hash = '#!/';
    pager.extendWithPage(newtworkViewModel);
    ko.applyBindings(newtworkViewModel);
    pager.start();
}