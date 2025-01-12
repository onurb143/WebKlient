// Funktion til at sende login-anmodning og modtage JWT-token
async function loginUser() {
    const email = document.getElementById("email").value;  // Hent email fra input-felt
    const password = document.getElementById("password").value;  // Hent password fra input-felt

    const loginData = {
        email: email,
        password: password
    };

    try {
        // Send POST anmodning til /api/auth/login for at logge brugeren ind
        const response = await fetch('/api/auth/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(loginData)
        });

        if (!response.ok) {
            throw new Error('Login failed');
        }

        // Modtag JWT-token som svar
        const data = await response.json();
        const token = data.token;  // JWT-token er sendt i svaret

        // Gem tokenet i localStorage for fremtidig brug
        localStorage.setItem('jwtToken', token);

        // Informér brugeren om succesfuldt login og tokenet
        console.log('Login successful, token: ', token);
        alert('Login successful!');

        // Eventuelt, send brugeren videre til en anden side eller opdater UI med brugerdata
    } catch (error) {
        console.error('Error logging in:', error);
        alert('Login failed. Please try again.');
    }
}

// Event listener for login-formen
document.getElementById("loginForm").addEventListener("submit", function (event) {
    event.preventDefault();  // Forhindrer standard form submission
    loginUser();  // Kald loginUser funktionen
});
