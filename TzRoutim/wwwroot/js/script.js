const url = 'https://localhost:7116/api/find';
const urlPostDelete = 'https://localhost:7116/api/find/';

const search = document.querySelector('#search')
const button = document.querySelector('#button')
const buttonDelete = document.querySelector('#delete')
const buttonHide = document.querySelector('#hide')

const row = document.querySelector('#row')

// Удаление записи
if (buttonDelete) {
    buttonDelete.addEventListener('click', () => {
        let id;

        fetch(url)
            .then(res => {
                return res.json()
            })
            .then((data) => {
                data.map((d) => {
                    if (d.stringSearch == search.value) {
                        id = d.id
                    }
                })

                fetch(urlPostDelete + id, {
                    method: 'DELETE',
                    headers: {
                        id: id
                    }
                })
            })
        location.reload()
    })
}

// Получение записей по значению
if (button) {
    button.addEventListener('click', () => {
        fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(search.value)
        }).then(res => {
            return res.json()
        })
            .then((data) => {

                if (data.stringSearch != search.value) {

                    data.map((search) => {
                        search.gitEntity.map((git) => {
                            createCard(git)
                        })
                    })
                }
                else {
                    data.gitEntity.map((git) => {
                        createCard(git)
                    })

                }
            })
    })
}
// Показ всех записей
if (buttonHide) {
    buttonHide.addEventListener('click', () => {
        fetch(url)
            .then(res => {
                return res.json()
            })
            .then((data) => {
                data.map((s) => {
                    s.gitEntity.map((git) => {
                        createCard(git)
                    })
                })
            })
    })
}

// Функция для создания карточек 
function createCard(data) {
    let card = document.createElement('div')
    let link = document.createElement('a')
    let projectName = document.createElement('h5')
    let authorName = document.createElement('h6')
    let star = document.createElement('p')
    let sub = document.createElement('p')

    card.classList.add('card', 'mb-3', 'mx-1')
    card.style.width = '32%'

    link.classList.add('card-body')
    link.classList.add('text-decoration-none')
    link.href = data.htmlUrl;

    projectName.textContent = data.name
    projectName.classList.add('card-title', 'text-dark')

    authorName.classList.add('card-subtitle', 'mb-3', 'text-muted')
    authorName.textContent = data.login

    star.classList.add('card-text', 'text-warning', 'my-1')
    star.textContent = 'Количество звезд ' + data.stargazersCount

    sub.classList.add('card-text', 'my-1', 'text-info')
    sub.textContent = 'Количество просмотров ' + data.subscribersCount

    link.append(projectName, authorName, star, sub)
    card.append(link)
    row.append(card)
}



