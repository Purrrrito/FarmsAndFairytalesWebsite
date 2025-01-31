document.addEventListener('DOMContentLoaded', function () {
    // Get the element for the indoor calendar
    var indoorCalendarEl = document.getElementById('calendar-indoor');

    // Initialize the FullCalendar object
    var indoorCalendar = new FullCalendar.Calendar(indoorCalendarEl, {
        timeZone: 'America/Los_Angeles',
        initialView: 'timeGridWeek',
        slotDuration: '00:30:00',
        slotLabelInterval: '00:30:00',
        slotLabelFormat: {
            hour: 'numeric',
            minute: '2-digit'
        },
        //Prevents selc ting before 10:00 AM and after 6:00 PM
        slotMinTime: '10:00:00',
        slotMaxTime: '18:00:00',
        //Defines business hours are from 10:00 AM to 6:00 PM
        businessHours: {
            daysOfWeek: [1, 2, 3, 4, 5, 6], // Monday to Saturday
            startTime: '10:00', // 10 AM
            endTime: '18:00' // 6 PM
        },
        //How close and far out you can select a time slot
        validRange: {
            start: new Date(Date.now() + 48 * 60 * 60 * 1000).toISOString(), // 48 hours out
            end: new Date(Date.now() + 60 * 24 * 60 * 60 * 1000).toISOString(), // 2 months out
        },
        hiddenDays: [0], // Hide Sunday
        selectable: true,
        allDaySlot: false,
        eventColor: '#FF0000',
        contentHeight: 'auto',
        //Fetches booked time slots from the database
        events: function (info, successCallback, failureCallback) {
            fetch('/BookedTimeSlots/GetBookedTimeSlots')
                .then(response => response.json())
                .then(data => successCallback(data.map(slot => ({ start: slot.start, end: slot.end }))))
                .catch(error => {
                    console.error("Error fetching events:", error);
                    failureCallback(error);
                });
        },

        select: function (info) {
            const duration = (info.end - info.start) / (1000 * 60);
            const sameDay = info.start.getDate() === info.end.getDate();

            if (duration <= 30) {
                document.getElementById('notificationModalText').innerHTML =
                    `Please select more than 30 minutes`;
                const modal = document.getElementById('notificationModal');
                modal.style.display = 'block';

                indoorCalendar.unselect();
                return;
            }
            if (!sameDay) {
                document.getElementById('notificationModalText').innerHTML =
                    `Please select time slots on the same day`;
                const modal = document.getElementById('notificationModal');
                modal.style.display = 'block';
                indoorCalendar.unselect();
                return;
            }

            const start = info.start.toISOString();
            const end = info.end.toISOString();

            fetch('/BookedTimeSlots/CheckSlot', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({
                    IndoorStart: start,
                    IndoorEnd: end,
                })
            })
                .then(response => response.json())
                .then(data => {
                    if (data.isBooked) {
                        document.getElementById('notificationModalText').innerHTML =
                            `Time slot is alerady booked`;
                        const modal = document.getElementById('notificationModal');
                        modal.style.display = 'block';

                    } else {
                        document.getElementById("milestoneChecbox").checked = false;

                        document.getElementById('bookingModalText').innerHTML =
                            `You have selected a booking from <strong>${info.start.toLocaleTimeString()}</strong> to <strong>${info.end.toLocaleTimeString()}</strong>.`;
                        const modal = document.getElementById('bookingModal');
                        modal.style.display = 'block';

                        document.getElementById("confirmBooking").onclick = function () {
                            const isMilestone = document.getElementById("milestoneChecbox").checked;

                            fetch('/BookedTimeSlots/BookSlot', {
                                method: 'POST',
                                headers: { 'Content-Type': 'application/json' },
                                body: JSON.stringify({
                                    IndoorStart: start,
                                    IndoorEnd: end,
                                    IndoorMilestoneCompleted: isMilestone
                                })
                            })
                            .then(() => {
                                modal.style.display = 'none';
                                indoorCalendar.refetchEvents();
                            })
                            .catch(error => console.error("Error booking time slot:", error));
                        };

                        document.getElementById("cancelBooking").onclick = function () {
                            modal.style.display = 'none';
                        };
                    }
                    indoorCalendar.unselect();
                })
                .catch(error => console.error("Error checking time slot:", error));
        }
    });

    indoorCalendar.render();

    window.onclick = function (event) {
        const bookingModal = document.getElementById("bookingModal");
        const notificationModal = document.getElementById("notificationModal");

        if (event.target === bookingModal) {
            bookingModal.style.display = "none";
        }

        if (event.target === notificationModal) {
            notificationModal.style.display = "none";
        }
    };

    document.querySelector("#bookingModal .close").onclick = function () {
        document.getElementById("bookingModal").style.display = "none";
    };

    document.querySelector("#notificationModal .close").onclick = function () {
        document.getElementById("notificationModal").style.display = "none";
    };
});
