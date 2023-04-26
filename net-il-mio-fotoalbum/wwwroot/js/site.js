// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const popola = filter => getFotos(filter).then(renderCards);

const getFotos = titolo => axios.get('/Api/HomeApi', titolo ? { params: { titolo } } : {}).then(res => res.data).catch(error => error);

const renderCards = fotos => {
    const cards = document.getElementById('cards');
    cards.innerHTML = fotos.map(fotoComponent).join('');
}

const fotoComponent = foto => `
    <div class="col-lg-4 mb-3 d-flex align-items-stretch">
        <div class="card my-3" >
            <img src=${foto.url} class="card-img-top " alt="foto">
            <div class="card-body d-flex flex-column text-center">
                <h5 class="card-title ">${foto.titolo}</h5>
                <p class="card-text">${foto.description}</p>
            </div>
        </div>
    </div>

`;



