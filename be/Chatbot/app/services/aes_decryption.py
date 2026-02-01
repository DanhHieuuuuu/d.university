import base64
from Crypto.Cipher import AES
from Crypto.Util.Padding import unpad

DEFAULT_KEY = "D.University2026SecretKey1234567"  # 32 bytes for AES-256


def decrypt_api_key(cipher_text: str, key: str = None) -> str:
    """
    Decrypt an AES-256-CBC encrypted string that was encrypted by C#.
    """
    if not cipher_text:
        return cipher_text
    
    secret_key = key or DEFAULT_KEY
    secret_key = secret_key.ljust(32)[:32]
    
    full_cipher = base64.b64decode(cipher_text)
    
    iv = full_cipher[:16]
    cipher_bytes = full_cipher[16:]
    
    cipher = AES.new(secret_key.encode('utf-8'), AES.MODE_CBC, iv)
    decrypted_bytes = unpad(cipher.decrypt(cipher_bytes), AES.block_size)
    
    return decrypted_bytes.decode('utf-8')


def encrypt_api_key(plain_text: str, key: str = None) -> str:
    """
    Encrypt a string using AES-256-CBC (compatible with C# encryption).
    """
    from Crypto.Util.Padding import pad
    import os
    
    if not plain_text:
        return plain_text
    
    secret_key = key or DEFAULT_KEY
    secret_key = secret_key.ljust(32)[:32]
    
    iv = os.urandom(16)
    
    cipher = AES.new(secret_key.encode('utf-8'), AES.MODE_CBC, iv)
    padded_data = pad(plain_text.encode('utf-8'), AES.block_size)
    encrypted_bytes = cipher.encrypt(padded_data)
    
    result = iv + encrypted_bytes
    
    return base64.b64encode(result).decode('utf-8')
