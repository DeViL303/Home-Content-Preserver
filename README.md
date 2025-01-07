# Home-Content-Preserver v1.00

![image](https://github.com/user-attachments/assets/2be39726-979e-4d4b-b980-4b14026ce7a5)


## A simple app created to download files from Playstation Home servers. This application simply spoofs the useragent to "PSHome PS3Application libhttp/4.9.0-000 (CellOS)". 

## Usage
1. Paste your original Playstation Home URLs into the top section. eg. guessed links to potential sdats, or links copied directly out of screenlink xmls. 
2. Choose your download location by entering a path into the download box or use the default C:\downloads\ (Note these settings are not remebered between sessions)
3. Enter the domain or IP for the server you want to download files from (in format shown in the example) 
4. Select automatic link propagation options: (Note these all relate specifically to sdats/odc/sdcs/thumbnails, Just use "As is" for any other file types)
   - As is: Just replaces the domain and downloads the specific urls you entered. Use this setting when you know exactly what url you want to check.
   - Txxx to Txxx: Automatically Checks for all versions from first value to last value- T001 to T100 is the recommended setting in most cases (where the available or wanted version is not known).
   - 2 second delay between downloads: by default it will download files as fast as possible - Use this setting to limit it to one download every 2 seconds max.
   - For sdat urls check for odc/sdc/png: By default it will only check for versions chosen. This setting will auto check for each sdats secondary files, if its an object it checks for odc xml, if its a scene it checks for sdc xml. 
   - Note this feature is generallyreliable, but rarely it might not always get the associated files, sometimes sony didnt use the expcected naming system for thumbnails for example.
5. Next Click "Build List", it will generate links for all items based on the entered urls + chosen options in the lower box. At this stage you can confirm if its generated links as expected and decide to proceed or not. 
6. If the generated URLs are as expected then next Click Download - it will download all files sequentually one at a time and show a success or fail message in the UI.
7. Note as a bonus feature. if you dont want any URL manipulation or extra versions adding you can just enter URLs into the BOTTOM textbox and skip the buiold list step, just paste URLs and click Download. Then it will act just like a normal http downloader with simple useragent spoof.

   
## IMPORTANT:
- This application can NOT bypass any whitelist. You must get whitelisted to play BEFORE using this application.
- Then it allows you to download the sdat files without needing to use the Home client and then pull the sdats from cache.
- If enabled it can auto check for associated ODC/SDC/PNGs to match the downloaded sdats.

This application was created becuase I could not find another download manager that ticks all the boxes:
- Free (and No ads or restrictions)
- Can bulk download reliably (and no limit on file count or GBs)
- Simple to use - In other words has a UI rather than some script reading links from a file.   
- Keeps exact folder structure and naming of original files by grabbing it from URL.
- Most download managers will download all files into one folder or are missing one of these
- Can do all the other things AND also spoof useragent

Also it can get pretty tedious doing manual find and replace on Playstation Home's 100+ different domains used by Sony and various 3rd party developers. 

As a bonus, this is open source. :)
