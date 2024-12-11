document.addEventListener('DOMContentLoaded', function () {
    var indoorCalendarEl = document.getElementById('calendar-indoor');
    var indoorCalendar = new FullCalendar.Calendar(indoorCalendarEl, {
        initialView: 'timeGridWeek',
        slotDuration: '00:30:00',
        slotLabelInterval: '00:30:00',
        slotLabelFormat: { // Formats the labels to the left of the slots
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
        select: function (info) {
            if (info.start.getDate() !== info.end.getDate()) {
                alert('You can only select times within the same day.');
                indoorCalendar.unselect();
            }
        }
    });

    indoorCalendar.render();
});
