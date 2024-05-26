import loginController from './login.mjs';

const ubicacionesController = (async () => {
    var ubicaciones = {
        ubicacionId: localStorage.getItem('locationId'),
        loadData: () => {
            $("#btnLocation").show();
            $("#btnDeleteLocation").css('visibility', 'hidden');
            $("#btnUnsubcribe").css('visibility', 'hidden');
            $("#btnLocation").on('click', async (e) => {
                e.preventDefault();
                var _formData = {
                    negocio: '',
                    descripcion: '',
                    latitud: 0,
                    longitud: 0
                };
                _formData.negocio = $('#negocio').val();
                _formData.descripcion = $('#descripcion').val();
                _formData.latitud = parseFloat($('#latitud').val());
                _formData.longitud = parseFloat($('#longitud').val());
                var _userId = loginController.userToken;
                var _headers = { headers: { "Authorization": `Bearer ${_userId}` } }
                var data = await axios.post('/api/Insert', _formData, _headers);
                setTimeout(() => { document.location.href = "/Locations" }, 500);
            });
        },
        loadDataById: (_id) => {
            localStorage.setItem("locationId", _id);
            setTimeout(async () => {
                var _ubicacionId = localStorage.getItem('locationId');
                var _userId = loginController.userToken;
                var _headers = { headers: { "Authorization": `Bearer ${_userId}` } }
                var data = await axios.get('/api/FindById?id=' + _ubicacionId, _headers);
                $('#negocio').val(data.data.negocio);
                $('#descripcion').val(data.data.descripcion);
                $('#latitud').val(data.data.latitud);
                $('#longitud').val(data.data.longitud);
            }, 500);
            $("#btnLocation").show();
            $("#btnDeleteLocation").show();
            $("#btnLocation").on('click', async (e) => {
                e.preventDefault();
                var _formData = {
                    negocio: '',
                    descripcion: '',
                    latitud: 0,
                    longitud: 0,
                    status: true
                };
                _formData.negocio = $('#negocio').val();
                _formData.descripcion = $('#descripcion').val();
                _formData.latitud = parseFloat($('#latitud').val());
                _formData.longitud = parseFloat($('#longitud').val());
                var _ubicacionId = localStorage.getItem('locationId');
                var _userId = loginController.userToken;
                var _headers = { headers: { "Authorization": `Bearer ${_userId}` } }
                var data = await axios.post('/api/FullActions?id=' + _ubicacionId, _formData, _headers);
                setTimeout(() => { document.location.href = "/Locations" }, 500);
            });
            $("#btnUnsubcribe").on('click', async (e) => {
                e.preventDefault();
                var _formData = {
                    negocio: '',
                    descripcion: '',
                    latitud: 0,
                    longitud: 0,
                    status: false
                };
                _formData.negocio = $('#negocio').val();
                _formData.descripcion = $('#descripcion').val();
                _formData.latitud = parseFloat($('#latitud').val());
                _formData.longitud = parseFloat($('#longitud').val());
                var _ubicacionId = localStorage.getItem('locationId');
                var _userId = loginController.userToken;
                var _headers = { headers: { "Authorization": `Bearer ${_userId}` } }
                var data = await axios.post('/api/FullActions?id=' + _ubicacionId, _formData, _headers);
                setTimeout(() => { document.location.href = "/Locations" }, 500);
            });
            $("#btnDeleteLocation").on('click', async (e) => {
                e.preventDefault();
                var _formData = {
                    negocio: undefined,
                    descripcion: undefined,
                    latitud: 0,
                    longitud: 0
                };
                var _ubicacionId = localStorage.getItem('locationId');
                var _userId = loginController.userToken;
                var _headers = { headers: { "Authorization": `Bearer ${_userId}` } }
                var data = await axios.post('/api/FullActions?id=' + _ubicacionId, _formData, _headers);
                setTimeout(() => { document.location.href = "/Locations" }, 500);
            });
        }
    }
    jQuery(($) => {
        const urlParams = new URLSearchParams(window.location.search);
        if (urlParams.has('id')) {
            ubicaciones.loadDataById(urlParams.get('id'));
        } else {
            ubicaciones.loadData();
        }
    });
})();

export default ubicacionesController;