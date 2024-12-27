# Home-Content-Preserver v1.00

![image](https://github.com/user-attachments/assets/2be39726-979e-4d4b-b980-4b14026ce7a5)


An app created to download files from Playstation Home servers. This application simply spoofs the useragent to "PSHome PS3Application libhttp/4.9.0-000 (CellOS)". 

This application can be used to download from any server, Just change the "Replace Domain" box to suit your chosen server. 

## Notes:
- This application can NOT bypass any whitelist. You must get whitelisted to play BEFORE using this application.
- Then it allows you to download the sdat files without needing to use the Home client and then pull the sdats from cache.
- If enabled it can auto check for associated ODC/SDC/PNGs to match the downloaded sdats.

## Usage
1. Paste your original URLs into the top section.
2. Choose your download location by entering a path into the download box or use the default C:\downloads\
3. Choose the domain for the server you want to download from.
4. Select options:
   - As is: Just replaces the domain and downloads the specific files you chose
   - Txxx to Txxx: Checks the all version from first value to last value- T031 to T080 recommended in most cases.
   - 2 second delay between downloads: by default it will download as fast as possible - use this setting to limit it to one download every 2 seconds.
   - For sdat urls chec for odc/sdc/png: This setting will auto check for each sdats file, if its an object it checks for odc, if its a scene it checks for sdc
   - Note this feature might not always get the associated files, sometimes sony didnt use the same expcected naming system for thumbnails.
5. Click build list, it will generate links for all items based on the entered urls + chosen options.
6. Note if you dont want any URL manipulation or extra version adding you can just enter URLs into the bottom textbox and click download. Then it will act just like a normal http downloader. 

     
