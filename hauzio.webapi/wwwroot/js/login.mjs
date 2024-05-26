
const loginController = (() => {
    var login = {
        loadData: () => {
            $("#btnLogin").on('click', async (e) => {
                e.preventDefault();
                var _formData = {
                    userName: '',
                    password: ''
                };
                _formData.userName = $('#username').val();
                _formData.password = $('#password').val();
                var data = await axios.post('/api/login', _formData);
                localStorage.setItem("token", data.data.token);
                setTimeout(() => { document.location.href = "/Locations" }, 500);
            });
        },
    }
    jQuery(function ($) {
        login.loadData();
    });
    return {
        userToken: localStorage.getItem('token'),
    };
})();

export default loginController;