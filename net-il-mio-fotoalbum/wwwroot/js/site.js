// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const initialize = filter => getFotos(filter).then(fotos => renderCards(fotos));

const getFotos = tiolo => axios.get('/Api/Foto', titolo ? { params: { titolo } } : {}).then(res => res.data).catch(error => error);

const renderCards = fotos => {
    const cards = document.getElementById('cards');
    cards.innerHTML = fotos.map(fotoComponent).join('');
}

const fotoComponent = foto => `
    <div class="col-lg-4 mb-3 d-flex align-items-stretch">
        <div class="card my-3" >
            @*style="width: 20rem"*@
            <img src=${foto.Url} class="card-img-top " alt="foto">
            <div class="card-body d-flex flex-column text-center">
                <h5 class="card-title ">${foto.Titolo}</h5>
                <p class="card-text">${foto.Description}</p>
            </div>
        </div>
    </div>

`;
const loadCategories = () => getCategories();

const getCategories = () => axios
    .get("/Api/Category")
    .then(res => res.data);


