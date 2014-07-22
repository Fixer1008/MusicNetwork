
function LoadViewModel(isAuth, identityName, urls) {
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
        this.UserName = ko.observable(data.loginUserName);
        this.Pass = ko.observable(data.loginUserPassword);
    }

    function Track(data, userName) {
        this.SongId = ko.observable(data.aid);
        this.SongName = ko.observable(data.title);
        this.Artist = ko.observable(data.artist);
        this.Duration = ko.observable(format(data.duration));
        this.Url = ko.observable(data.url);
        this.UserName = ko.observable(userName);
        this.IsEnable = ko.observable(true);
    }

    function NetworkViewModel() {
        var self = this;

        function successLogin(data) {
            if (data == "True") {
                self.loginUser().isVisibleName(true);
                self.loginUser().profileName(self.loginUser().loginUserName());
                self.loginUser().isLoginError(false);
            } else {
                self.loginUser().isVisibleName(false);
                self.loginUser().isLoginError(true);
            }
        }

        self.loginUser = ko.validatedObservable({
                loginUserName: ko.observable().extend({
                    required: true,
                    minLength: 4,
                    maxLength: 20
                }),
                loginUserPassword: ko.observable().extend({
                    required: true,
                    minLength: 8,
                    maxLength: 20
                }),
                isVisibleName: ko.observable(isAuthFlag),
                isLoginError: ko.observable(false),
                profileName: ko.observable(name),
                avatar: ko.observable(),

                Login : function () {
                    var user = new User(self.loginUser());

                    $.ajax({
                        url: urls[0],
                        type: 'POST',
                        data: ko.toJSON(user),
                        contentType: "application/json",
                        success: function (data) {
                            successLogin(data);
                        }
                     });
                },

                 Logoff : function () {
                    $.ajax({
                        url: urls[1],
                        type: 'GET',
                        success: function () {
                            self.loginUser().isVisibleName(false);
                            self.loginUser().profileName('');
                            self.loginUser().loginUserName('');
                            self.loginUser().loginUserPassword('');
                            self.registerUser().registerUserName('');
                            self.registerUser().registerUserPassword('');
                            self.registerUser().registerUserEmail('');
                            $(location).attr('href', '#!/deep_navigation/start');
                        },
                    });
                 },

                 GetProfileInfo: function () {
                     self.loginUser().avatar('Image/GetImageByUsername/?name=' + self.loginUser().profileName());
                     $.ajax({
                         url: '../api/User/?name='+self.loginUser().profileName(),
                         type: 'GET',
                         success: function (data) {
                             self.registerUser().registerUserName(data.UserName);
                             self.registerUser().registerUserEmail(data.Email);
                             $(location).attr('href', '#!/deep_navigation/profile');                   
                         },
                         error: function(data) {
                             alert('Error!');
                         }
                     });
                 },

                 EditProfile: function () {
                     var editProfile = new newUser(self.registerUser());
                     $.ajax({
                         url: '../api/User/',
                         type: 'PUT',
                         data: ko.toJSON(editProfile),
                         contentType: "application/json",
                         success: function (data) {
                             alert('Profile was successfully changed!');
                         },
                         error: function (data) {
                             alert('Error!');
                         }
                     });
                 },

                 //ChangeAva: function () {
                 //    var postedImage = $('#newAvaFile').val();
                 //    $.ajax({
                 //        url: '/Image/Index',
                 //        type: 'POST',
                 //        data: postedImage,
                 //        enctype: 'multipart/form-data',
                 //        success: function (data) {
                 //            alert('Success!');
                 //        },
                 //        error: function (data) {
                 //            alert('Error!');
                 //        }
                 //    });
                 //}
        });

        function newUser(data) {
            this.UserName = ko.observable(data.registerUserName);
            this.Email = ko.observable(data.registerUserEmail);
            this.Password = ko.observable(data.registerUserPassword);
        }

        self.registerUser = ko.validatedObservable({
            registerUserName: ko.observable().extend({
                required: true,
                minLength: 4,
                maxLength: 20
            }),
            registerUserEmail: ko.observable().extend({
                required: true,
                email: true,
                minLength: 4,
                maxLength: 30
            }),
            registerUserPassword: ko.observable().extend({
                required: true,
                minLength: 8,
                maxLength: 20
            }),

            Register: function () {
                var user = new newUser(self.registerUser());
                $.ajax({
                    url: urls[2],
                    type: 'POST',
                    data: ko.toJSON(user),
                    contentType: "application/json",
                    success: function (data) {
                        if (data == "True") {
                            self.loginUser().isVisibleName(true);
                            self.loginUser().profileName(self.registerUser().registerUserName());
                        } else {
                            self.loginUser().isVisibleName(false);
                        }
                    }
             });
            }
         });

        self.tracks = ko.observableArray([]);

        self.AddTrack = function (track) {
            track.IsEnable(false);
            var jsonTrack = ko.toJSON(track);
            $.ajax({
                url: '../api/song',
                type: 'POST',
                data: jsonTrack,
                contentType: "application/json",
                success: function () {
                }
            });
        }

        self.myTracks = ko.observableArray([]);

        self.MyTracks = function() {
            $.ajax({
                url: '../api/song/?name=' + self.loginUser().profileName(),
                type: 'GET',
                success: function (data) {
                    for (var i = 0; i < data.length; i++) {
                        var formatDuration = format(data[i].Duration);
                        data[i].Duration = formatDuration;
                    }
                    self.myTracks(data);
                    $(location).attr('href', '#!/deep_navigation/userTracks');
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