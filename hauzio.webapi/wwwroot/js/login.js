
(() => {
    var loginController = {
        token: $('#token').val(),
        loadData: () => {
            $("#btnLogin").on('click', async (e) => {
                e.preventDefault();
                var _formData = {
                    userName: '',
                    password: ''
                };
                _formData.userName = $('#username').val();
                _formData.password = $('#password').val();
                var data = await axios.post('/login', _formData);
                $('#token').val(data.data.token);
                window.location.href = '/Locations';
            });
        },
    }
    jQuery(function ($) {
        loginController.loadData();
    });
})();