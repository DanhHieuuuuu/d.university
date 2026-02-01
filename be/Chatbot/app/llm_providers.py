import os
from dotenv import load_dotenv
from dataclasses import dataclass
from typing import Dict, Any
from app.services.aes_decryption import decrypt_api_key

load_dotenv()


@dataclass
class LLMConfig:
    name: str
    base_url: str
    api_key: str
    model: str
    headers: Dict[str, str]


# Cau hinh Groq
GROQ = LLMConfig(
    name="groq",
    base_url="https://api.groq.com/openai/v1/chat/completions",
    api_key=os.getenv("GROQ_API_KEY", ""),
    model=os.getenv("GROQ_MODEL", "llama-3.3-70b-versatile"),
    headers={
        "Authorization": f"Bearer {os.getenv('GROQ_API_KEY', '')}",
        "Content-Type": "application/json"
    }
)

# Cau hinh vLLM Local
VLLM = LLMConfig(
    name="vllm",
    base_url=os.getenv("VLLM_BASE_URL", "http://localhost:3403") + "/v1/chat/completions",
    api_key="not-needed",  # vLLM local khong can API key
    model=os.getenv("VLLM_MODEL", "default"),
    headers={
        "Content-Type": "application/json"
    }
)


_PROVIDERS = {
    "groq": GROQ,
    "vllm": VLLM,
}


def get_provider(name: str = None) -> LLMConfig:
    if name is None:
        name = os.getenv("LLM_PROVIDER", "groq")
    
    name = name.lower()
    if name not in _PROVIDERS:
        raise ValueError(f"Provider '{name}' khong ton tai. Chon: {list(_PROVIDERS.keys())}")
    
    return _PROVIDERS[name]


def create_config_from_request(
    name: str,
    base_url: str,
    model: str,
    api_key: str = None
) -> LLMConfig:
    """Tao LLMConfig tu cac tham so request API.
    
    Args:
        name: Ten provider ('groq' hoac 'vllm')
        base_url: URL cua LLM API (tuong duong VLLM_BASE_URL)
        model: Ten model (vd: 'llama-3.3-70b-versatile' hoac 'Qwen/Qwen2.5-7B-Instruct')
        api_key: API key da duoc ma hoa (se duoc giai ma truoc khi su dung)
    
    Returns:
        LLMConfig: Cau hinh LLM da tao
    """
    name = name.lower()
    
    # Giai ma API key neu co
    decrypted_api_key = None
    if api_key:
        try:
            decrypted_api_key = decrypt_api_key(api_key)
        except Exception:
            # Neu giai ma that bai, su dung nguyen ban (co the la key chua ma hoa)
            decrypted_api_key = api_key
    
    if name == "groq":
        # Groq can API key va endpoint co dinh
        if not decrypted_api_key:
            raise ValueError("Groq yeu cau api_key")
        
        # Dam bao base_url co endpoint chat completions
        if not base_url.endswith("/v1/chat/completions"):
            if base_url.endswith("/"):
                base_url = base_url.rstrip("/")
            base_url = base_url + "/v1/chat/completions"
        
        return LLMConfig(
            name="groq",
            base_url=base_url,
            api_key=decrypted_api_key,
            model=model,
            headers={
                "Authorization": f"Bearer {decrypted_api_key}",
                "Content-Type": "application/json"
            }
        )
    
    elif name == "vllm":
        # vLLM local khong can API key
        if not base_url.endswith("/v1/chat/completions"):
            if base_url.endswith("/"):
                base_url = base_url.rstrip("/")
            base_url = base_url + "/v1/chat/completions"
        
        headers = {"Content-Type": "application/json"}
        if decrypted_api_key:
            headers["Authorization"] = f"Bearer {decrypted_api_key}"
        
        return LLMConfig(
            name="vllm",
            base_url=base_url,
            api_key=decrypted_api_key or "not-needed",
            model=model,
            headers=headers
        )
    
    else:
        raise ValueError(f"Provider '{name}' khong duoc ho tro. Chon: ['groq', 'vllm']")


DEFAULT = get_provider()