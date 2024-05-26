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
                var _userId = localStorage.getItem('token');
                var _headers = { headers: { "Authorization": `Bearer ${_userId}` } }
                var data = await axios.post('/api/Insert', _formData, _headers);
                setTimeout(() => { window.location.reload(); }, 500);
            });
        },
        loadDataById: (_id) => {
            localStorage.setItem("locationId", _id);
            setTimeout(async () => {
                var _ubicacionId = localStorage.getItem('locationId');
                var _userId = localStorage.getItem('token');
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
                var _userId = localStorage.getItem('token');
                var _headers = { headers: { "Authorization": `Bearer ${_userId}` } }
                var data = await axios.post('/api/FullActions?id=' + _ubicacionId, _formData, _headers);
                setTimeout(() => { window.location.reload(); }, 500);
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
                var _userId = localStorage.getItem('token');
                var _headers = { headers: { "Authorization": `Bearer ${_userId}` } }
                var data = await axios.post('/api/FullActions?id=' + _ubicacionId, _formData, _headers);
                setTimeout(() => { window.location.reload(); }, 500);
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
                var _userId = localStorage.getItem('token');
                var _headers = { headers: { "Authorization": `Bearer ${_userId}` } }
                var data = await axios.post('/api/FullActions?id=' + _ubicacionId, _formData, _headers);
                setTimeout(() => { document.location.href = "/admin" }, 500);
            });
        },
        loadLocations: () => {
            setTimeout(async () => {
                var _userId = localStorage.getItem('token');
                var _headers = { headers: { "Authorization": `Bearer ${_userId}` } }
                var data = await axios.get('/api/GetAll', _headers);
                var map = L.map('map')
                    .setView([19.2087846, -96.2297964], 15);
                L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
                    maxZoom: 19,
                    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
                }).addTo(map);
                L.control.scale().addTo(map);
                data.data.forEach((item) => {
                    L.marker([item.latitud, item.longitud], { draggable: true }).addTo(map).bindPopup(`
                    <h4> Negocio: ${item.negocio} </h4>
                    <h5> Descripcion: ${item.descripcion}</h5>
                    <a class="btn btn-outline-warning" href='${'/admin?id=' + item.id}'>Ver</a>
                `);
                });
            }, 100);
        }
    }
    jQuery(($) => {

        setTimeout(async () => {
            var _userId = localStorage.getItem('token');
            if (_userId === null || _userId === undefined) {
                document.location.href = "/login";
            } else {
                var _headers = { headers: { "Authorization": `Bearer ${_userId}` } };
                var data = await axios.get('/api/HasSession', _headers);
                if (data.data === false) {
                    document.location.href = "/login";
                } else {
                    const urlParams = new URLSearchParams(window.location.search);
                    if (window.location.pathname.includes("locations")) {
                        ubicaciones.loadLocations();
                    } else if (urlParams.has('id')) {
                        ubicaciones.loadDataById(urlParams.get('id'));
                    } else {
                        ubicaciones.loadData();
                    }
                }
            }
        }, 100);
    });
})();

export default ubicacionesController;