# Simple-JSON-Poster
This is a simple windows command line tool that posts JSON to a URL.  It is very basic and allows you to post an array of elements, or 2 elements individually (see example usage below).  The code might be a little ugly, but does the job.

It uses the CommandLineParser nuget package to manage the arguments.

Example Usage
=============
For example if you want to post into Slack:

JSONPoster.exe -u "https://hooks.slack.com/services/ASDFASDF/ASDFASDF/ASDFASDF" -n "username" -c "The Robot" -a "text" -b "I am a robot, here's a link to google <http://google.com>."

Example of Posting with an ElementArray (where x is the name array and y is the value array, making sure both are equal size and correctly ordered:

JSONPoster.exe -u "https://mysiteexample100.com/myAPI" -t ElementArray -x "FirstName|LastName|Age" -y "Henry|Jones|60"

Known Issues / Bugs
=====================
- The escaping of single quotes, double quotes, slashes, and just general escaping might not be working.
- Code is a little ugly
