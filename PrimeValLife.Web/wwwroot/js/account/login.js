let email = document.getElementById('txtEmail')
let password = document.getElementById('txtPassword')
let btnSubmit = document.getElementById('btnSubmit')

btnSubmit.addEventListener('click', e => {
    e.preventDefault()
    fetch('/Account/SignIn', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            userName: email.value,
            password: password.value
        }),
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            if (data.success) {
                window.location.href = '/Home/Index';
            }
            else {
                ShowToaster(data.errors.description, 'fail')
            }
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
            // Handle errors, e.g., display an error message to the user
        });
});