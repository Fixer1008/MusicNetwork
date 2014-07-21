
function LoadViewModel(isAuth, identityName) {
    var isAuthFlag;
    var name;
    if (isAuth == "True") {
        isAuthFlag = true;
        name = identityName;
    } else {
        isAuthFlag = false;
        name = '';
    }

    function format(duration) {
        var minutes = (duration / 60).toFixed(0);
        var seconds = duration % 60;
        var secondsString;

        if (seconds.toString().length == 1) {
            secondsString = '0' + seconds.toString();
        } else {
            secondsString = seconds.toString();
        }

        var resultString = minutes.toString() + ':' + secondsString;
        return resultString;
    }

    function User(data) {
        this.UserName = ko.observable(data.newUserName);
        this.Pass = ko.observable(data.newUserPassword);
    }

    function Track(data, userName) {
        this.SongId = ko.observable(data.aid);
        this.SongName = ko.observable(data.title);
        this.Artist = ko.observable(data.artist);
        this.Duration = ko.observable(format(data.duration));
        this.Url = ko.observable(data.url);
        this.UserName = ko.observable(userName);
    }

    function NetworkViewModel() {
        var self = this;

        self.loginUser = ko.validatedObservable({
                newUserName: ko.observable().extend({
                    required: true,
                    minLength: 4,
                    maxLength: 20
                }),
                newUserPassword: ko.observable().extend({
                    required: true,
                    minLength: 8,
                    maxLength: 20
                }),
                isVisibleName: ko.observable(isAuthFlag),
                isLoginError: ko.observable(false),
                profileName: ko.observable(name),

            Login : function () {
                var user = new User(self.loginUser());

                $.ajax({
                    url: '../Account/Login',
                    type: 'POST',
                    data: ko.toJSON(user),
                    contentType: "application/json",
                    success: function (data) {
                        if (data == "True") {
                            self.loginUser().isVisibleName(true);
                            self.loginUser().profileName(self.loginUser().newUserName());
                            self.loginUser().isLoginError(false);
                        } else {
                            self.loginUser().isVisibleName(false);
                            self.loginUser().isLoginError(true);
                        }
                    },
                });
            },

             Logoff : function () {
                $.ajax({
                    url: '../Account/LogOff',
                    type: 'GET',
                    success: function () {
                        self.loginUser().isVisibleName(false);
                        self.loginUser().profileName('');
                        self.loginUser().newUserName('');
                        self.loginUser().newUserPassword('');
                        $(location).attr('href', '#!/deep_navigation/start');
                    },
                });
            }
        });


        self.tracks = ko.observableArray([]);

        self.AddTrack = function (track) {
            var jsonTrack = ko.toJSON(track);
            $.ajax({
                url: 'api/song',
                type: 'POST',
                data: jsonTrack,
                contentType: "application/json",
                success: function () {
                    alert('success!');
                }
            });
        }

        $.getJSON("https://api.vk.com/method/audio.get?owner_id=-12382840&album_id=40235721" +
            "&access_token=3b4b6f177a010da28cf8d8a03dfb0156e01dbd332182d7441392dbfb5f244c87a018dcf929a798ac63a25&callback=?",
            function (allData) {
                for (var j = 1; j < allData.response[0]; j++) {
                    self.tracks.push(new Track(allData.response[j], self.loginUser().profileName));
                }
            });
    }

    var newtworkViewModel = new NetworkViewModel();
    pager.Href.hash = '#!/';
    pager.extendWithPage(newtworkViewModel);
    ko.applyBindings(newtworkViewModel);
    pager.start();
}