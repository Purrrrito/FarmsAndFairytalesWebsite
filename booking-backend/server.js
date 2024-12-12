const express = require('express');
const app = express();
const bodyParser = require('body-parser');
const cors = require('cors');

// Middleware to parse JSON requests
app.use(bodyParser.json());

// Enable CORS for all routes
app.use(cors());

// In-memory storage for booked slots
let bookedSlots = [];

// API to book a time slot
app.post('/api/bookSlot', (req, res) => {
    const { title, start, end } = req.body;

    if (!title || !start || !end) {
        return res.status(400).json({ error: 'Invalid booking data' });
    }

    // Check for overlapping bookings
    const hasOverlap = bookedSlots.some(slot =>
        (new Date(slot.start) < new Date(end)) && (new Date(start) < new Date(slot.end))
    );

    if (hasOverlap) {
        return res.status(400).json({ error: 'Time slot already booked' });
    }

    // Save the booking
    bookedSlots.push({ title, start, end });
    console.log('Booking successful:', { title, start, end });
    res.status(200).json({ message: 'Booking successful' });
});

// API to get all booked slots
app.get('/api/getBookedSlots', (req, res) => {
    res.json(bookedSlots);
});

// Start the server
const PORT = 3000;
app.listen(PORT, () => {
    console.log(`Server running at http://localhost:${PORT}`);
});
