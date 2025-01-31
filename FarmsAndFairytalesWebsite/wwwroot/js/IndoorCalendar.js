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

        //Prevents selcting before 10:00 AM and after 6:00 PM
        slotMinTime: '10:00:00',
        slotMaxTime: '18:00:00',

        //Defines business hours are from 10:00 AM to 6:00 PM
        businessHours: {
            daysOfWeek: [1, 2, 3, 4, 5, 6], // Monday to Saturday
            startTime: '10:00', // 10 AM
            endTime: '18:00' // 6 PM
        },

        // Restrict selection to within 48 hours and 2 months from today
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

        // Handle time slot selection
        select: function (info) {
            const duration = (info.end - info.start) / (1000 * 60);
            const sameDay = info.start.getDate() === info.end.getDate();

            // Enforce minimum booking duration of 30 minutes
            if (duration <= 30) {
                showModal('notificationModal', 'Please select more than 30 minutes');
                indoorCalendar.unselect();
                return;
            }

            // Ensure selection stays within the same day
            if (!sameDay) {
                showModal('notificationModal', 'Please select time slots on the same day');
                indoorCalendar.unselect();
                return;
            }

            const start = info.start.toISOString();
            const end = info.end.toISOString();

            // Check if the selected time slot is already booked
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
                        showModal('notificationModal', 'Time slot is already booked');
                    } else {
                        document.getElementById("milestoneChecbox").checked = false;
                        showModal('bookingModal', `You have selected a booking from <strong>${info.start.toLocaleTimeString()}</strong> to <strong>${info.end.toLocaleTimeString()}</strong>.`);

                        // Confirm booking event
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
                                closeModal('bookingModal');
                                indoorCalendar.refetchEvents();
                            })
                            .catch(error => console.error("Error booking time slot:", error));
                        };

                        document.getElementById("cancelBooking").onclick = function () {
                            closeModal('bookingModal');
                        };
                    }
                    indoorCalendar.unselect();
                })
                .catch(error => console.error("Error checking time slot:", error));
        }
    });

    indoorCalendar.render();

    // Handle modal clicks (close on outside click)
    window.onclick = function (event) {
        if (event.target.classList.contains("modal")) {
            event.target.style.display = "none";
        }
    };

    // Close modal buttons
    document.querySelectorAll(".modal .close").forEach(button => {
        button.onclick = function () {
            button.closest(".modal").style.display = "none";
        };
    });

    // Utility function to show a modal
    function showModal(modalId, message) {
        document.getElementById(modalId + 'Text').innerHTML = message;
        document.getElementById(modalId).style.display = 'block';
    }

    // Utility function to close a modal
    function closeModal(modalId) {
        document.getElementById(modalId).style.display = 'none';
    }
});
