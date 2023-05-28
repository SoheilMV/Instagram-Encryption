# Instagram web encryption algorithm in PHP
First, open your browser and enter the following page
>https://www.instagram.com/data/shared_data/

Now search the text below
>"encryption":{

In the page source, a text similar to the following text is found
>"encryption":{"key_id":"157","public_key":"406e3f65b2c08b7d7b69a127d1db3bcef0a7153628c64fc5e8a3041382c8ae14","version":"10"}

Now, after finding the above text, modify the following address with the obtained specifications and enter it in the browser
>yourdomin.com/instagram.php?password=1234&key=406e3f65b2c08b7d7b69a127d1db3bcef0a7153628c64fc5e8a3041382c8ae14&id=157&version=10"