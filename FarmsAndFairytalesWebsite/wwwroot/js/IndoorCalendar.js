document.addEventListener('DOMContentLoaded', function () {
    var indoorCalendarEl = document.getElementById('calendar-indoor');
    var indoorCalendar = new FullCalendar.Calendar(indoorCalendarEl, {
        initialView: 'timeGridWeek',
        slotDuration: '00:30:00',
        slotLabelInterval: '00:30:00',
        slotLabelFormat: {
            hour: 'numeric',
            minute: '2-digit'
        },
        minTime: '10:00:00',
        maxTime: '18:00:00',
        slotMinTime: '10:00:00',
        slotMaxTime: '18:00:00',
        contentHeight: 'auto',
        validRange: {
            start: new Date(Date.now() + 48 * 60 * 60 * 1000), // 48 hours out
            end: new Date(Date.now() + 60 * 24 * 60 * 60 * 1000), // 2 months out
        },
        businessHours: {
            daysOfWeek: [1, 2, 3, 4, 5, 6], // Monday-Saturday
            startTime: '10:00', // 10 AM
            endTime: '18:00' // 6 PM
        },
        hiddenDays: [0], // Hide Sunday
        selectable: true,
        allDaySlot: false,
        events: function (fetchInfo, successCallback, failureCallback) {
            fetch('http://localhost:3000/api/getBookedSlots')
                .then(response => response.json())
                .then(data => {
                    var events = data.map(slot => ({
                        title: slot.title,
                        start: slot.start,
                        end: slot.end,
                        backgroundColor: '#FF0000',
                        borderColor: '#FF0000'
                    }));
                    successCallback(events);
                })
                .catch(error => {
                    console.error('Error fetching events:', error);
                    failureCallback(error);
                })
        },
        select: function (info) {
            if (info.start.getDate() !== info.end.getDate()) {
                alert('You can only select times within the same day.');
                indoorCalendar.unselect();
            } else {
                const duration = (info.end - info.start) / (1000 * 60);
                if (duration <= 30) {
                    alert('Please select a time slot longer than 30 minutes.')
                    indoorCalendar.unselect();
                    return;
                }

                const isConfirmed = confirm(`Are you sure you want to book this time slot?\nDate: ${info.start.toLocaleDateString()}\nTime: ${info.start.toLocaleTimeString()} - ${info.end.toLocaleTimeString()}`);
                if (isConfirmed) {
                    const event = {
                        title: 'Booked',
                        start: info.start.toISOString(),
                        end: info.end.toISOString(),
                        backgroundColor: '#FF0000',
                        borderColor: '#FF0000',
                    };

                    fetch('http://localhost:3000/api/bookSlot', { // Changed from https to http
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        body: JSON.stringify({ title: 'Booked', start: info.start.toISOString(), end: info.end.toISOString() })
                    }).then(response => {
                        if (response.ok) {
                            indoorCalendar.addEvent(event);
                        } else {
                            return response.json().then(errorData => {
                                alert('Failed to save booking. Error: ' + (errorData.error || 'Unknown error'));
                            });
                        }
                    }).catch(error => {
                        alert('Network error: ' + error.message);
                    });

                    indoorCalendar.unselect();
                } else {
                    indoorCalendar.unselect();
                }
            }
        },
    });

    indoorCalendar.render();
});
