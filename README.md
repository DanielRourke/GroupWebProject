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
			- [ ] My Details (/Customers/MyDetails)
			- [ ] Book a Room
			- [ ] Search Rooms
			- [ ] My Bookings
			- [x] Logout (default)
		- [ ] admin links
			- [x] Home
			- [ ] Manage Bookings
			- [ ] Statistics
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
	- [ ] Authorise pages
	- [ ] 6.1 My Details (Customers)
		- [ ] Edit Surname, Given Name, Postcode (new view model)
		- [ ] Redirect from register to this page
		- [ ] Create or Update table
		- [ ] Success notification
		- [ ] Check redirection works from oother pages
  	- [ ] 6.2 Search Rooms (Rooms)
		- [ ] Build form - Number of Beds (select tag helper) , Check in Date, check out date, submit (tag helpers) 
		- [ ] booking View Model
		- [ ] Raw SQL
		- [ ] Show Table
		- [ ] NOT REQUIRED (add booking link)
	- [ ] 6.3 Book a Room (Rooms)
		- [ ]  create booking form - Room id, check in, check out, submit
		- [ ] view model for this
		- [ ] Raw SQL
		- [ ] notification
	- [ ] 6.4 My Bookings
		- [ ] Table SurName, given name, roomID, Checkin date, checkout date, Cost (this customer only)
		- [ ] Toggle order Checkin and Cost

- [ ] 7 Admin Pages 
	- [ ] Authorise pages
	- [ ] 7.1 Mangage Bookings
		- [ ] create linnk
		- [ ] Table - RoomID, Customer Surname, Customer Given Name, Checking in Date, Checking out Date, and Cost
		- [ ] Links (delete detils)
		- [ ] create page 
			- [ ] room id
			- [ ] Customer drop down with Full name
			- [ ] check in
			- [ ] check out
			- [ ] cost (manual)
			- [ ] submit
			- [ ] CHeck if aviable Raw SQL
			- [ ] succes notification
			- [ ] redirect (return page maybe ?)
		- [ ] delete page
			- [ ] details
			- [ ] Delete confirmation
	- [ ] 7.2 Statistics
		- [ ] Table How many customers are located in each postcode. 'Postcode' and 'Number of Customers
		- [ ] Table How many bookings have been made for each room. 'Room ID' and 'Number of Bookings'

