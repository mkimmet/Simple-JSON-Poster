# Simple-JSON-Poster
This is a simple windows command line tool that posts JSON to a URL.  It is very basic and only allows you to have 2 json elements.

It uses the CommandLine nuget package to manage the arguments.

Example Usage
=============
For example if you want to post into Slack:
JSONPoster.exe -u "https://hooks.slack.com/services/ASDFASDF/ASDFASDF/ASDFASDF" -n "username" -c "The Robot" -a "text" -b "I am a robot, here's a link to google <http://google.com>."

Known Issues / Bugs
=====================
- I think my escaping of single quotes, double quotes, slashes, and just general escaping is messed up.
- You can only have 2 elements in your JSON.
