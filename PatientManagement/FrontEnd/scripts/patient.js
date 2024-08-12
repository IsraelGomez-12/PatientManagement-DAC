document.addEventListener('DOMContentLoaded', () => {
    loadPatients();

    document.getElementById('addPatientForm').addEventListener('submit', async function(event) {
        event.preventDefault();
        const token = localStorage.getItem('token');

        const newPatient = {
            firstName: document.getElementById('firstName').value,
            lastName: document.getElementById('lastName').value,
            dateOfBirth: document.getElementById('dateOfBirth').value,
            email: document.getElementById('email').value,
            phone: document.getElementById('phone').value,
            address: document.getElementById('address').value,
            socialSecurityNumber: document.getElementById('ssn').value,
            idNumber: document.getElementById('idNumber').value,
            comments: document.getElementById('comments').value
        };

        try {
            const response = await fetch('http://localhost:5246/patient', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                },
                body: JSON.stringify(newPatient)
            });

            if (response.ok) {
                $('#addPatientModal').modal('hide');
                loadPatients();
            } else {
                console.error('Error adding patient');
            }
        } catch (error) {
            console.error('Error:', error);
        }
    });
});

document.getElementById('editPatientForm').addEventListener('submit', async function(event) {
    event.preventDefault();
    const token = localStorage.getItem('token');
    const patientId = document.getElementById('editPatientId').value;

    const updatedPatient = {
        patientId : document.getElementById('editPatientId').value,
        firstName: document.getElementById('editFirstName').value,
        lastName: document.getElementById('editLastName').value,
        dateOfBirth: document.getElementById('editDateOfBirth').value,
        email: document.getElementById('editEmail').value,
        phone: document.getElementById('editPhone').value,
        address: document.getElementById('editAddress').value,
        socialSecurityNumber: document.getElementById('editSSN').value,
        idNumber: document.getElementById('editIDNumber').value,
        comments: document.getElementById('editComments').value
    };

    try {
        const response = await fetch(`http://localhost:5246/patient/${patientId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(updatedPatient)
        });

        if (response.ok) {
            console.log('response OK')
            $('#editPatientModal').modal('hide');
            loadPatients();
        } else {
            console.error('Error updating patient');
        }
    } catch (error) {
        console.error('Error:', error);
    }
});


async function loadPatients() {
    const token = localStorage.getItem('token');

    try {
        const response = await fetch('http://localhost:5246/patient', {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });

        if (response.ok) {
            const patients = await response.json();
            const tbody = document.getElementById('patientsTableBody');
            tbody.innerHTML = '';

            patients.forEach(patient => {
                const row = `<tr>
                    <td>${patient.patientId}</td>
                    <td>${patient.firstName}</td>
                    <td>${patient.lastName}</td>
                    <td>${new Date(patient.dateOfBirth).toLocaleDateString()}</td>
                    <td>${patient.email}</td>
                    <td>
                      <button class="btn btn-warning btn-sm" onclick="editPatient(${patient.patientId})">Edit</button>
                      <button class="btn btn-danger btn-sm"  onclick="deletePatient(${patient.patientId})">Delete</button>
                    </td>
                </tr>`;
                tbody.innerHTML += row;
            });
        } else {
            console.error('Error loading patients');
        }
    } catch (error) {
        console.error('Error:', error);
    }
}

async function deletePatient(patientId) {
    const token = localStorage.getItem('token');

    try {
        const response = await fetch(`http://localhost:5246/patient/${patientId}`, {
            method: 'DELETE',
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });

        if (response.ok) {
            loadPatients();
        } else {
            console.error('Error deleting patient');
        }
    } catch (error) {
        console.error('Error:', error);
    }
}

async function editPatient(patientId) {
    const token = localStorage.getItem('token');

    try {
        const response = await fetch(`http://localhost:5246/patient/${patientId}`, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });

        if (response.ok) {
            const patient = await response.json();

            // Fill in the form with patient data
            document.getElementById('editPatientId').value = patient.patientId;
            document.getElementById('editFirstName').value = patient.firstName;
            document.getElementById('editLastName').value = patient.lastName;
            document.getElementById('editDateOfBirth').value = new Date(patient.dateOfBirth).toISOString().split('T')[0];
            document.getElementById('editEmail').value = patient.email;
            document.getElementById('editPhone').value = patient.phone;
            document.getElementById('editAddress').value = patient.address;
            document.getElementById('editSSN').value = patient.socialSecurityNumber;
            document.getElementById('editIDNumber').value = patient.idNumber;
            document.getElementById('editComments').value = patient.comments;

            // Show the edit modal
            new bootstrap.Modal(document.getElementById('editPatientModal')).show();
        } else {
            console.error('Error loading patient data');
        }
    } catch (error) {
        console.error('Error:', error);
    }
}

