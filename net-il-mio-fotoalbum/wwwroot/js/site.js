// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Foto
const popola = filter => getFotos(filter).then(renderCards);

//const getFotos = titolo => axios.get('/Api/HomeApi', titolo ? { params: { titolo } } : {}).then(res => res.data).catch(error => error);
const getFotos = titolo => axios.get('/Api/HomeApi', titolo ? { params: { titolo } } : {}).then(res => ({ fotos: res.data, visibile: true }));

const renderCards = ({ fotos }) => {
    const cards = document.getElementById('cards');
    cards.innerHTML = fotos.map(fotoComponent).join('');
}

const fotoComponent = foto => `
    <div class="col-lg-4 mb-3 d-flex vis align-items-stretch">
        <div class="card my-3" >
            <img src=${foto.url} class="card-img-top " alt="foto">
            <div class="card-body d-flex flex-column text-center">
                <h5 class="card-title ">${foto.titolo}</h5>
                <p class="card-text">${foto.description}</p>
                 <a class="btn btn-primary " href="/Home/ApiDettagli/${foto.id}">Dettagli</a>
            </div>
        </div>
    </div>

`;

//dettagli

function initDettagli() {
    var id = Number(location.pathname.split("/")[3])
    getFoto(id).then(foto => renderFoto(foto));
}

const getFoto = id => axios
    .get(`/Api/HomeApi/${ id }`)
    .then(res => res.data);

const renderFoto = foto => {
    const pagina = document.getElementById("dett");
    pagina.innerHTML = fotoDettagli(foto);
}
const fotoDettagli = foto => `
    <h2>${foto.titolo}</h2>

    <div class="container">
	    <div class="image">
		    <img class="img-fluid" src="${foto.url}">
	    </div>
	    <div>
		    <p>
			    ${foto.description}
		    </p>
	    </div>
    </div>
`
//dettagli

//Foto

//Messaggio

const postMessaggio = messaggio => axios
    .post("/Api/HomeApi", messaggio)
    .then(() => location.href = "/Home/ApiIndex")
    .catch(err => renderErrors(err.response.data.errors));

const initCreateForm = () => {
    const form = document.querySelector("#messaggio-create-form");
    const email = document.querySelector('#email');
    const testo = document.querySelector('#testo');

    form.addEventListener("submit", e => {
        e.preventDefault();

        const messaggio = getMessaggioFromForm(form);
        postMessaggio(messaggio);
        email.value = '';
        testo.value = '';
    });
};

const getMessaggioFromForm = form => {
    const email = form.querySelector("#email").value;
    const testo = form.querySelector("#testo").value;
    

    return {
        email,
        testo,
    };
};

//Messaggio


