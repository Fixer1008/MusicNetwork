function ValidateUser() {
    $('#loginForm').validate({
        rules: {
          login: {
              required: true,
              minlength: 6,
              maxlength: 15,
          },

          pass: {
              required: true,
              minlength: 6,
              maxlength: 25,
          }
        },

        messages: {
            login: {
                required: "This field is required!",
                minlength: "The min length login field is 6 symbols!",
                maxlength: "The max length login field is 6 symbols!",
            },

            pass: {
                required: "This field is required!",
                minlength: "The min length password field is 6 symbols!",
                maxlength: "The max length password field is 6 symbols!",
            }
        }
    });
}