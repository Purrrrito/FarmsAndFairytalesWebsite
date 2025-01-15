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
            $.ajax({
                url: '/BookedTimeSlots/GetBookedTimeSlots', //Uses the method from the controller
                type: 'GET',
                dataType: 'json',
                success: function (data) {
                    //Maps each slot to the correct event
                    successCallback(data.map(slot => ({ 
                        //Ensure start and end time consistency
                        start: slot.start, 
                        end: slot.end,
                    })));
                },
                error: function (xhr, status, error) {
                    console.error("Error fetching events: ", error);
                    failureCallback(error);
                }
            });
        },

        //When time slots are selected
        select: function (info) {
            const duration = (info.end - info.start) / (1000 * 60); //Calculate duration in minutes
            const sameDay = info.start.getDate() === info.end.getDate(); // heck if the selection is on the same day

            //Must select more than 30(two time slots) min on the same day    
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

            const start = new Date(info.start.getTime()).toISOString();
            const end = new Date(info.end.getTime()).toISOString();

            //Send the selected time slots to the controller to check and book
            $.ajax({
                url: '/BookedTimeSlots/CheckAndBookSlot', //Uses the method from the controller
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify({ start, end }), //Sends the start and end time 
                success: function (response) {
                    alert(response.isBooked ? 'Time slot is already booked' : 'Time slot booked successfully');
                    if (!response.isBooked) {
                        indoorCalendar.refetchEvents();
                    }
                    indoorCalendar.unselect();
                },
                error: function (xhr, status, error) {
                    console.error("Error booking time slot: ", error);
                }
            });
        }

    });

    // Render the calendar
    indoorCalendar.render();
});
