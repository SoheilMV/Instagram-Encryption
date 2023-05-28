<?php

function encrypt($password, $publicKey, $keyId)
{
    $time = time();
    $key  = openssl_random_pseudo_bytes(32);
    $iv   = hex2bin('000000000000000000000000');

    $aesEncrypted = openssl_encrypt($password, 'aes-256-gcm', $key, OPENSSL_RAW_DATA, $iv, $tag, strval($time));
    $encryptedKey = sodium_crypto_box_seal($key, hex2bin($publicKey));

    return [
        'time'      => $time,
        'encrypted' => base64_encode("\x01" | pack('n', intval($keyId)) . pack('s', strlen($encryptedKey)) . $encryptedKey . $tag . $aesEncrypted)
    ];
}

function generateEncPassword($password, $publicKey, $keyId, $version)
{
    $result = encrypt($password, $publicKey, $keyId);
    return '#PWD_INSTAGRAM_BROWSER:' . $version . ':' . $result['time'] . ':' . $result['encrypted'];
}

if(isset($_GET['password']) && isset($_GET['key']) && isset($_GET['id']) && isset($_GET['version']))
{
    $password = $_GET['password']; //1234
    $publicKey = $_GET['key']; //'406e3f65b2c08b7d7b69a127d1db3bcef0a7153628c64fc5e8a3041382c8ae14';
    $keyId = $_GET['id']; //157;
    $version = $_GET['version']; //10;

    print_r(generateEncPassword($password, $publicKey, $keyId, $version));
}
else
    echo "Error<br>Example : instagram.php?password=1234&key=406e3f65b2c08b7d7b69a127d1db3bcef0a7153628c64fc5e8a3041382c8ae14&id=157&version=10";

?>