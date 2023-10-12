# TollFee
Gothenburg City collects the Congestion tax charged during fixed hours for vehicles driving into and out of Gothenburg.

This is an API that calculates the toll fee for a number of vehicle passes during certain dates.
- Request parameters: dates when the vehicle passed the tolling station.
- Result: Object with the Congestion tax amount

## Gothenburg congestion tax toll rules
- https://www.transportstyrelsen.se/en/road/road-tolls/Congestion-taxes-in-Stockholm-and-Goteborg/congestion-tax-in-gothenburg/hours-and-amounts-in-gothenburg/
  - Fees differ depending on the time of the day and some other factors.
  - There are Free Dates (e.g., during public holidays) when the fee is not collected. They can differ depending on the year.

# Todo
- Make the Dates configurable runtime and stored persistently instead of being hardcoded so that we can change the calculation depending on the year. Return relevant error message if the request to "CalculateFee" endpoint contains a year not yet configured.
- Make sure business critical functionality is covered by unit tests.
- Refactor/comment as you see fit as if it would be real business critical code, as long as the existing endpoint's functionality doesn't change.
