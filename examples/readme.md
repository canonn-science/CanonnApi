# Canonn API Examples

Below you will find information regarding the examples for pulling data from the Canonn API.
If you would like to suggest or add anything please submit a pull request.

Thanks,
Canonn R&D Team

## Table of Contents

- Google Docs
  - Google Sheets - getruins.js
  - getus.js (Coming Soon)
  

### Google Docs

Below you will find scripts for use with Google Sheets and other Google Docs related tools.

To use these scripts you will need to add them to each document using the Script Editor, which can be found under Tools > Script Editor.

When adding the script it is recommended you rename it to the script name but using .gs instead of .js

For more information on the Google Script Editor, please see this Google KB Article: https://developers.google.com/apps-script/overview

#### Google Sheets - getruins.js

Please see the getruins.js file for the code, simply copy and paste it into the google script editor, you can make the following changes based on how you would like the data formatted:

- urlSystemRuinList
  - Change this URL to match the API URL, you can use the CanonnAPI Beta if you wish but note that the data may not be up to date with the live data.
    - https://api.canonn.technology:8001
- ruinCoords
  - If you wish to change the formatting of the Coords 
    - Simply change the `(' // ')` to whatever you wish such as `(' , ')`
  
It should also be noted you can setup a trigger in the script editor to automatically pull new information based on a time, we currently do not implement any rate limits.
However please try and keep it to every 30 minutes or more to keep the load down and allow easy and fair access for everyone.
For more information on script triggers, please see the following Google KB article: https://developers.google.com/apps-script/guides/triggers/

To call the function in a google spreadsheet, start at the topmost left cell and enter: `=getRuins()`

The data should auto populate the cells required.

If you require any assitance with this please speak with the Canonn R&D team on the Canonn Discord.