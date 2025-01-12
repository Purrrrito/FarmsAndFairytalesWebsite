document.addEventListener('DOMContentLoaded', function () {
    var indoorCalendarEl = document.getElementById('calendar-indoor');
    var indoorCalendar = new FullCalendar.Calendar(indoorCalendarEl, {
        timeZone: 'Pacific/ Honolulu',
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
        eventColor: '#FF0000',
        events: function (info, successCallback, failureCallback) {
            $.ajax({
                url: '/BookedTimeSlots/GetBookedTimeSlots',
                type: 'GET',
                dataType: 'json',
                success: function (data) {

                    const events = data.map(slot => ({
                        start: new Date(slot.start).toISOString(), // Parse to ensure consistency
                        end: new Date(slot.end).toISOString(),
                        backgroundColor: slot.color,
                        borderColor: slot.color
                    }));

                    // Pass the modified events to the successCallback
                    successCallback(events);
                },
                error: function (xhr, status, error) {
                    console.error("Error fetching events: ", error);
                    failureCallback(error);
                }
            });
        },
        select: function (info) {
            const duration = (new Date(info.end) - new Date(info.start)) / (1000 * 60);
            const sameDay = info.start.getDate() === info.end.getDate();

            if (duration <= 30) {
                alert('Please select at least 30 minutes');
                indoorCalendar.unselect();
                return;
            }
            if (!sameDay) {
                alert('Please select time slots in the same day');
                indoorCalendar.unselect();
                return;
            }

            const offset = -8 * 60; // Offset in minutes for UTC-10:00
            const start = new Date(info.start.getTime() + offset * 60000).toISOString();
            const end = new Date(info.end.getTime() + offset * 60000).toISOString();

            console.log("Java script: " , start, end);
            $.ajax({
                url: '/BookedTimeSlots/CheckAndBookSlot',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ start, end }),
                success: function (response) {
                    if (response.isBooked) {
                        alert('Time slot is already booked');
                        indoorCalendar.unselect();
                    } else {
                        alert('Time slot booked successfully');
                        indoorCalendar.refetchEvents();
                    }
                },
                error: function (xhr, status, error) {
                    console.error("Error booking time slot: ", error);
                }
            });
        }

    });

    indoorCalendar.render();
});
