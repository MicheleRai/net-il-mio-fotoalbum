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
            </div>
        </div>
    </div>

`;

//const visible = document.querySelector('.vis');
//if (foto.visibile) {
//    visible.classList.add("d-none");
//} else {
//    visible.classList.remove("d-none");
//}

//Foto

//Messaggio

const postMessaggio = messaggio => axios
    .post("/Home/ApiIndex", messaggio)
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


