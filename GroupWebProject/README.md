# GroupWebProject
Web Systems Development Group Project

Task Make a mock website for a Hotel

TODO

- [ ] 2 Customer Layout file custom links

- [ ] 3 Nav and Layout LINKS
		- [x] not logged in links 
			- [x] Home
			- [x] Register
			- [x] Login
		- [ ] customers links
			- [x] Home (/Index)
			- [X] My Details (/Customers/MyDetails)
			- [x] Book a Room
			- [X] Search Rooms
			- [X] My Bookings
			- [x] Logout (default)
		- [ ] admin links
			- [x] Home
			- [X] Manage Bookings
			- [x] Statistics
			- [x] Logout (default)

- [ ] 4 Home Page
	- [ ] Images
	- [ ] Carosel Tags
	- [x] Columns/Links

- [ ] 5 Models
	- [x] Models
	- [ ] Double check models
	- [ ] add data to Database

- [ ] 6  Customer Pages 
	- [X] Authorise pages
	- [ ] 6.1 My Details (Customers)
		- [x] Edit Surname, Given Name, Postcode (new view model)
		- [X] Redirect from register to this page
		- [x] Create or Update table
		- [x] Success notification
		- [ ] Check redirection works from oother pages
  	- [ ] 6.2 Search Rooms (Rooms)
		- [x] Build form - Number of Beds (select tag helper) , Check in Date, check out date, submit (tag helpers) 
		- [x] booking View Model
		- [x] Raw SQL
		- [x] Show Table
		- [ ] NOT REQUIRED (add booking link)
	- [ ] 6.3 Book a Room (Rooms)
		- [X]  create booking form - Room id, check in, check out, submit
		- [x] view model for this
		- [x] Raw SQL
		- [ ] notification
	- [ ] 6.4 My Bookings
		- [x] Table SurName, given name, roomID, Checkin date, checkout date, Cost (this customer only)
		- [x] Toggle order Checkin and Cost

- [ ] 7 Admin Pages 
	- [X] Authorise pages
	- [ ] 7.1 Mangage Bookings
		- [x] create linnk
		- [x] Table - RoomID, Customer Surname, Customer Given Name, Checking in Date, Checking out Date, and Cost
		- [x] Links (delete detils)
		- [x] create page 
			- [x] room id
			- [x] Customer drop down with Full name
			- [x] check in
			- [x] check out
			- [x] cost (manual)
			- [x] submit
			- [X] CHeck if aviable Raw SQL
			- [ ] succes notification
			- [ ] redirect (return page maybe ?)
		- [X] Edit page 
		- [ ] delete page
			- [x] details
			- [ ] Delete confirmation
	- [X] 7.2 Statistics
		- [X] Table How many customers are located in each postcode. 'Postcode' and 'Number of Customers
		- [X] Table How many bookings have been made for each room. 'Room ID' and 'Number of Bookings'

