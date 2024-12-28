# Home-Content-Preserver v1.00

![image](https://github.com/user-attachments/assets/2be39726-979e-4d4b-b980-4b14026ce7a5)


## A simple app created to download files from Playstation Home servers. This application simply spoofs the useragent to "PSHome PS3Application libhttp/4.9.0-000 (CellOS)". 

## Usage
1. Paste your original Home URLs into the top section.
2. Choose your download location by entering a path into the download box or use the default C:\downloads\
3. Enter the domain or IP for the server you want to download files from.
4. Select options:
   - As is: Just replaces the domain and downloads the specific urls you entered.
   - Txxx to Txxx: Checks for all versions from first value to last value- T001 to T100 is the recommended setting in most cases (where the available version is not known).
   - 2 second delay between downloads: by default it will download files as fast as possible - Use this setting to limit it to one download every 2 seconds max.
   - For sdat urls check for odc/sdc/png: This setting will auto check for each sdats secondary files, if its an object it checks for odc xml, if its a scene it checks for sdc xml
   - Note this feature is relaible, but rarely it might not always get the associated files, sometimes sony didnt use the expcected naming system for thumbnails for example.
5. Bext Click "Build List", it will generate links for all items based on the entered urls + chosen options in the lower box.
6. If the generated URLs are as expected then next Click Download - it will download all files sequentually one at a time and show a success or fail message in the UI.
7. Note if you dont want any URL manipulation or extra versions adding you can just enter URLs into the bottom textbox and click Download. Then it will act just like a normal http downloader with useragent spoof.

   
## Notes:
- This application can NOT bypass any whitelist. You must get whitelisted to play BEFORE using this application.
- Then it allows you to download the sdat files without needing to use the Home client and then pull the sdats from cache.
- If enabled it can auto check for associated ODC/SDC/PNGs to match the downloaded sdats.
