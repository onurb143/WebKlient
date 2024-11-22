document.addEventListener("DOMContentLoaded", function () {
    // Hent data fra API
    fetch('/api/Disks') // Sørg for, at denne URL matcher din API's route
        .then(response => {
            if (!response.ok) {
                throw new Error('Netværksrespons ikke OK');
            }
            return response.json();
        })
        .then(data => {
            const list = document.getElementById("disk-list");
            if (list) {
                data.forEach(disk => {
                    const listItem = document.createElement("li");
                    listItem.textContent = `ID: ${disk.diskID}, Name: ${disk.name}`;
                    list.appendChild(listItem);
                });
            }
        })
        .catch(error => {
            console.error("Fejl under hentning af data:", error);
        });
});
