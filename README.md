# EHealth-Calendar
__________________

A basic calender which you can switch between month and day view.
Add/change/delete tasks, connection to database on mobile memory with SQLlite.



MainActivity + Main.axml
________________________

Contains tabhost with reference to two activities, Month- and DayActivity.


MonthActivity + Month.axml
__________________________

Month representation with it's own view based on CalenderView android widget. 
-> Button (T) to switch the selector to today's date
-> Button (+) to add tasks (taakLayout.axml)
-> listView on Click to show all the information about the panned   task and to change it if necessary. (taakLayout.axml)
-> listView on LongClick provides option to delete item from the listview (TaskMenu.axml)


DayActivity + Day.axml
______________________

Listview of the date with a textview for the selected day
Still under construction


SQLliteConn
___________

SQLlite connection to local database (on device, not network). 
Synchronous connection: coded + tested and working
Asynchronous connection: coded.


CalenderClass.cs
________________

Currently not in use, might be removed soon.
