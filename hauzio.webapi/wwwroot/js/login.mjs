﻿
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
                if (data.data.estatus) {
                    localStorage.setItem("token", data.data.data);
                    setTimeout(() => { window.location.href = "/admin"; }, 500);
                } else {
                    setTimeout(() => { Swal.fire(data.data.error); }, 500);
                }
            });
        },

    }
    jQuery(function ($) {
        setTimeout(async () => {
            var _userId = localStorage.getItem('token');
            if (_userId === null || _userId === undefined) {
                login.loadData();
            } else {
                var _headers = { headers: { "Authorization": `Bearer ${_userId}` } };
                var data = await axios.get('/api/HasSession', _headers);
                if (data.data === true) {
                    window.location.href = "/admin";
                } else {
                    login.loadData();
                }
            }
        }, 500);
    });
    return {
        userToken: localStorage.getItem('token'),
    };
})();

export default loginController;